using Montemdraco.NeuralUtils.Library.Interfaces.Functions;

namespace Montemdraco.NeuralUtils.Library.Services.Functions.Activations
{
    /// <summary>
    /// Пороговая функция активации.
    /// </summary>
    /// <remarks>
    /// Диапазон 0, 1.
    /// </remarks>
    public class LeveledActivationFunction : IActivationFunction
    {
        /// <inheritdoc/>
        public string Name => "Leveled";

        /// <inheritdoc />
        public double Calculate(double x)
        {
            return x >= 0 ? 1 : 0;
        }

        /// <inheritdoc />
        public double CalculateDerivative(double x)
        {
            return 1;
        }
    }
}
