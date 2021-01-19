using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Montemdraco.NeuralUtils.Library.Interfaces.Net;
using Montemdraco.NeuralUtils.Library.Model;
using Montemdraco.NeuralUtils.Library.Model.Net;
using Montemdraco.NeuralUtils.Library.Model.Nodes;
using Montemdraco.NeuralUtils.Library.Model.Teachers;
using Montemdraco.NeuralUtils.Library.Services.Functions.Activations;
using Montemdraco.NeuralUtils.Library.Services.Functions.Errors;
using Montemdraco.NeuralUtils.Library.Services.Teachers;

namespace Montemdraco.NeuralUtils
{
    public class NeuralRandomSolver
    {
        private Random rndTest;

        public NeuralRandomSolver()
        {
            rndTest = new Random(675);
        }

        public void Run()
        {
            var net = CreateNet();
            var lessons = CreateLessons(100000, 10);
            var lessonsForTrain = lessons.Take(lessons.Count - 100).ToList();
            var lessonsForTest = lessons.Skip(lessons.Count - 100).ToList();

            var err = new MeanSquareErrorFunction();
            var teacher = new BackPropagationTeacher(err);
            teacher.SetNeuralNet(net);
            teacher.Initialize(0.7, 0.4);
            teacher.AddLessonRange(lessonsForTrain);
            teacher.Teach(1);

            var expectedData = lessonsForTest.Select(e => e.ExpectedOutput.OutputContainer.First().Value).ToList();
            var receivedData = new List<double>();
            foreach (var lesson in lessonsForTest)
            {
                net.SetInput(lesson.ExpectedInput);
                net.Run();

                var netOut = net.GetOutput();
                receivedData.Add(netOut.OutputContainer.First().Value);
            }

            var deltas = expectedData.Zip(receivedData, (d, d1) => d - d1).ToList();
            SaveFile(receivedData, expectedData);
        }

        private void SaveFile(List<double> received, List<double> expected)
        {
            var sb = new StringBuilder();
            foreach (var d in received)
            {
                sb.Append(d).Append(";");
            }

            sb.AppendLine();
            foreach (var d in expected)
            {
                sb.Append(d).Append(";");
            }

            var filePath = @"file.csv";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            File.WriteAllText(filePath, sb.ToString());
        }

        private INeuralNet CreateNet()
        {
            var net = new FeedForwardNeuralNet();
            var act = new HyperbolicTangentActivationFunction();
            var rnd = new Random(1);

            //// Output.
            var outputNodes = new List<INeuralNode>();
            for (var i = 1; i <= 1; i++)
            {
                var n = new SimpleOutputNode(act);
                n.Initialize("output" + i);
                outputNodes.Add(n);
            }

            //// Layer 1.
            var hiddenLayerNodes = new List<INeuralNode>();
            for (var i = 1; i <= 10; i++)
            {
                var n = new SimpleHiddenNode(act);
                n.Initialize("hidden" + i);

                foreach (var node in outputNodes)
                {
                    n.AddNextNode((INeuralNodeBuilder)node, rnd.NextDouble());
                }

                hiddenLayerNodes.Add(n);
            }

            /*var bias1 = new SimpleBiasNode(act);
            bias1.Initialize("bias1");
            foreach (var node in outputNodes)
            {
                if (node is SimpleBiasNode)
                {
                    continue;
                }

                bias1.AddNextNode((INeuralNodeBuilder)node, rnd.NextDouble());
            }
            hiddenLayerNodes.Add(bias1);*/

            //// Inputs.
            var inputNodes = new List<INeuralNode>();
            for (var i = 1; i <= 10; i++)
            {
                var n = new SimpleInputNode(act);
                n.Initialize("input" + i);

                foreach (var node in hiddenLayerNodes)
                {
                    n.AddNextNode((INeuralNodeBuilder)node, rnd.NextDouble());
                }

                inputNodes.Add(n);
            }

            /*var bias2 = new SimpleBiasNode(act);
            bias2.Initialize("bias2");
            foreach (var node in hiddenLayerNodes)
            {
                if (node is SimpleBiasNode)
                {
                    continue;
                }

                bias2.AddNextNode((INeuralNodeBuilder)node, rnd.NextDouble());
            }
            inputNodes.Add(bias2);*/

            //// Build.
            inputNodes.AddRange(hiddenLayerNodes);
            inputNodes.AddRange(outputNodes);
            net.SetNodes(inputNodes);

            return net;
        }

        private List<LessonData> CreateLessons(int totalNumbersCount, int inputsCount)
        {
            var numbersList = new List<double>();
            for (var i = 0; i < totalNumbersCount; i++)
            {
                var generated = 1.0 / rndTest.Next(1, 1001);
                if (!double.IsNormal(generated))
                {
                    throw new Exception("Generated number is not valid.");
                }

                numbersList.Add(generated);
            }

            var lessons = new List<LessonData>();
            for (var i = 0; i < totalNumbersCount - inputsCount; i++)
            {
                var inp = new NeuralInputData();
                for (var j = 1; j <= inputsCount; j++)
                {
                    inp.Add("input" + j, numbersList[i + j - 1]);
                }

                var outp = new NeuralOutputData();
                outp.OutputContainer.Add("output1", numbersList[i + inputsCount]);

                var l = new LessonData(inp, outp);
                lessons.Add(l);
            }

            return lessons;
        }
    }
}
