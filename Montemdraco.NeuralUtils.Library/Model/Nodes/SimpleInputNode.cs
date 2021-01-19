using System.Linq;
using Montemdraco.NeuralUtils.Library.Interfaces.Functions;

namespace Montemdraco.NeuralUtils.Library.Model.Nodes
{
    /// <summary>
    /// Обычный входной нейрон.
    /// </summary>
    public class SimpleInputNode : NeuralNodeBase
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SimpleInputNode"/>.
        /// </summary>
        /// <param name="activationFunction">Функция активации текущего узла.</param>
        public SimpleInputNode(IActivationFunction activationFunction)
            : base(activationFunction)
        {
            IsInputNode = true;
            IsOutputNode = false;
        }

        /// <inheritdoc />
        public override double Run()
        {
            _outputValue = _inputValues.First();
            return _outputValue;
        }
    }
}
