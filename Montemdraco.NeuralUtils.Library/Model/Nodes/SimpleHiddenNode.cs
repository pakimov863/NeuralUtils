using System.Linq;
using Montemdraco.NeuralUtils.Library.Interfaces.Functions;

namespace Montemdraco.NeuralUtils.Library.Model.Nodes
{
    /// <summary>
    /// Обычный нейрон скрытого слоя.
    /// </summary>
    public class SimpleHiddenNode : NeuralNodeBase
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SimpleHiddenNode"/>.
        /// </summary>
        /// <param name="activationFunction">Функция активации текущего узла.</param>
        public SimpleHiddenNode(IActivationFunction activationFunction)
            : base(activationFunction)
        {
            IsInputNode = false;
            IsOutputNode = false;
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
