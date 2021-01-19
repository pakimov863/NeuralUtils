using System.Linq;
using Montemdraco.NeuralUtils.Library.Interfaces.Functions;

namespace Montemdraco.NeuralUtils.Library.Model.Nodes
{
    /// <summary>
    /// Обычный выходной нейрон.
    /// </summary>
    public class SimpleOutputNode : NeuralNodeBase
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SimpleOutputNode"/>.
        /// </summary>
        /// <param name="activationFunction">Функция активации текущего узла.</param>
        public SimpleOutputNode(IActivationFunction activationFunction)
            : base(activationFunction)
        {
            IsInputNode = false;
            IsOutputNode = true;
        }

        /// <inheritdoc />
        public override double Run()
        {
            var inputSum = _inputValues.Sum();
            _outputValue = ActivationFunction.Calculate(inputSum);
            return _outputValue;
        }
    }
}
