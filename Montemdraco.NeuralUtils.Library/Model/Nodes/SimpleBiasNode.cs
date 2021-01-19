using Montemdraco.NeuralUtils.Library.Interfaces.Functions;

namespace Montemdraco.NeuralUtils.Library.Model.Nodes
{
    /// <summary>
    /// Обычный нейрон смещения.
    /// </summary>
    public class SimpleBiasNode : NeuralNodeBase
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SimpleBiasNode"/>.
        /// </summary>
        /// <param name="activationFunction">Функция активации текущего узла.</param>
        public SimpleBiasNode(IActivationFunction activationFunction)
            : base(activationFunction)
        {
            IsInputNode = false;
            IsOutputNode = false;
        }

        /// <inheritdoc />
        public override double Run()
        {
            _outputValue = 1.0;
            return _outputValue;
        }
    }
}
