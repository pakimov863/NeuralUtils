using System;
using System.Collections.Generic;
using System.Linq;
using Montemdraco.NeuralUtils.Library.Helpers;
using Montemdraco.NeuralUtils.Library.Interfaces.Functions;
using Montemdraco.NeuralUtils.Library.Interfaces.Net;
using Montemdraco.NeuralUtils.Library.Interfaces.Teachers;
using Montemdraco.NeuralUtils.Library.Model;
using Montemdraco.NeuralUtils.Library.Model.Teachers;
using Montemdraco.NeuralUtils.Library.Model.Teachers.BackPropagation;

namespace Montemdraco.NeuralUtils.Library.Services.Teachers
{
    /// <summary>
    /// Учитель для метода "Обратного распространения ошибки".
    /// </summary>
    public class BackPropagationTeacher : NeuralNetTeacherBase, IBackPropagationTeacher
    {
        /// <summary>
        /// Скорость обучения.
        /// </summary>
        private double _epsilon;

        /// <summary>
        /// Момент обучения.
        /// </summary>
        private double _alpha;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BackPropagationTeacher"/>.
        /// </summary>
        /// <param name="errorFunction">Используемая функция подсчета ошибки.</param>
        public BackPropagationTeacher(IErrorFunction errorFunction)
            : base(errorFunction)
        {
        }

        /// <inheritdoc />
        public void Initialize(double epsilonValue, double alphaValue)
        {
            _epsilon = epsilonValue;
            _alpha = alphaValue;
        }

        /// <inheritdoc />
        protected override void ProcessLesson(LessonData lessonData)
        {
            //// Первоначальный запуск.
            _neuralNet.SetInput(lessonData.ExpectedInput);
            _neuralNet.Run();

            //// Обучение.
            var synapseCorrectionTable = CalculateDeltas(lessonData.ExpectedOutput);
            ApplyWeightCorrection(synapseCorrectionTable);

            //// Повторный запуск.
            _neuralNet.SetInput(lessonData.ExpectedInput);
            _neuralNet.Run();

            var receivedOutput = _neuralNet.GetOutput();
            var expectedOutput = lessonData.ExpectedOutput;
            var error = CalculateError(expectedOutput, receivedOutput);
            lessonData.AddAccruedError(error);
        }

        /// <summary>
        /// Вычисляет измененные значения для весов синапсов.
        /// </summary>
        /// <param name="expectedContainer">Контейнер эталонных выходных данных.</param>
        /// <returns>Таблица с синапсами и их информацией о корректировке.</returns>
        private Dictionary<INeuralSynapse, SynapseCorrectionInfo> CalculateDeltas(NeuralOutputData expectedContainer)
        {
            var deltaTable = new Dictionary<INeuralNode, double>();
            var synapseTable = new Dictionary<INeuralSynapse, SynapseCorrectionInfo>();

            var nodes = _neuralNet.Nodes
                .Where(e => e.IsOutputNode)
                .ToList();

            while (true)
            {
                var prevNodes = new List<INeuralNode>();

                foreach (var node in nodes)
                {
                    prevNodes.AddRange(node.GetPreviousNodes());

                    if (node.IsOutputNode)
                    {
                        var expectedOutput = expectedContainer.OutputContainer[node.Name];
                        var deltaTemp1 = CalculateDeltaForOutput(node, expectedOutput);
                        if (!double.IsNormal(deltaTemp1))
                        {
                            throw new Exception("Incorrect value of Delta (output node).");
                        }

                        deltaTable.Add(node, deltaTemp1);
                        continue;
                    }

                    var nextNodeDeltas = node.GetNextNodes()
                        .Select(nextNode => deltaTable[nextNode])
                        .ToList();
                    var nextNodeWeights = node.GetNextLinks()
                        .Select(e => e.CurrentWeight)
                        .ToList();
                    var linksSum = nextNodeWeights
                        .Zip(nextNodeDeltas, (w, d) => w * d)
                        .Sum();
                    var deltaTemp2 = CalculateDeltaForOtherNode(node, linksSum);
                    deltaTable.Add(node, deltaTemp2);

                    foreach (var nextSynapse in node.GetNextLinks())
                    {
                        if (!synapseTable.ContainsKey(nextSynapse))
                        {
                            synapseTable.Add(nextSynapse, new SynapseCorrectionInfo());
                        }

                        var correctionInfo = synapseTable[nextSynapse];
                        correctionInfo.Gradient = nextSynapse.LeftNode.GetOutput() * deltaTable[nextSynapse.RightNode];
                        correctionInfo.DeltaWeight = _epsilon * correctionInfo.Gradient + _alpha * correctionInfo.DeltaWeight;
                        correctionInfo.NewLinkWeight = nextSynapse.CurrentWeight + correctionInfo.DeltaWeight;

                        if (!double.IsNormal(correctionInfo.Gradient)
                            || !double.IsNormal(correctionInfo.DeltaWeight)
                            || !double.IsNormal(correctionInfo.NewLinkWeight))
                        {
                            throw new Exception("Incorrect value of Gradient/DeltaWeight/NewWeight.");
                        }
                    }
                }

                nodes = prevNodes
                    .Distinct(new NeuralNodeEqualityComparer())
                    .ToList();

                if (!nodes.Any())
                {
                    break;
                }
            }

            return synapseTable;
        }

        /// <summary>
        /// Вычисляет значение дельты для выходного узла.
        /// </summary>
        /// <param name="node">Узел, для которого выполняется расчет.</param>
        /// <param name="expectedValue">Ожидаемое выходное значение узла.</param>
        /// <returns>Значение дельты.</returns>
        private double CalculateDeltaForOutput(INeuralNode node, double expectedValue)
        {
            var receivedValue = node.GetOutput();
            return (expectedValue - receivedValue) * node.ActivationFunction.CalculateDerivative(receivedValue);
        }

        /// <summary>
        /// Вычисляет значение дельты для других узлов.
        /// </summary>
        /// <param name="node">Узел, для которого выполняется расчет.</param>
        /// <param name="inputSum">Сумма входных значений узла.</param>
        /// <returns>Значение дельты.</returns>
        private double CalculateDeltaForOtherNode(INeuralNode node, double inputSum)
        {
            var receivedValue = node.GetOutput();
            return node.ActivationFunction.CalculateDerivative(receivedValue) * inputSum;
        }

        /// <summary>
        /// Вычисляет ошибку между эталонным выходом и полученным.
        /// </summary>
        /// <param name="expectedOutput">Контейнер с эталонной информацией.</param>
        /// <param name="receivedOutput">Контейнер с полученной информацией.</param>
        /// <returns>Вычисленная ошибка.</returns>
        private double CalculateError(NeuralOutputData expectedOutput, NeuralOutputData receivedOutput)
        {
            return _errorFunction.Calculate(expectedOutput, receivedOutput);
        }

        /// <summary>
        /// Применяет изменение весов на нейронную сеть.
        /// </summary>
        /// <param name="synapseCorrectionTable">Таблица корректировки весов.</param>
        private void ApplyWeightCorrection(Dictionary<INeuralSynapse, SynapseCorrectionInfo> synapseCorrectionTable)
        {
            foreach (var kvp in synapseCorrectionTable)
            {
                var synapse = kvp.Key;
                var newWeight = kvp.Value.NewLinkWeight;

                synapse.ChangeWeight(newWeight);
            }
        }
    }
}
