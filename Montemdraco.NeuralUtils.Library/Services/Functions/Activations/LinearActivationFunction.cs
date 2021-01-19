using Montemdraco.NeuralUtils.Library.Interfaces.Functions;

namespace Montemdraco.NeuralUtils.Library.Services.Functions.Activations
{
    /// <summary>
    /// Линейная функция активации.
    /// </summary>
    /// <remarks>
    /// Диапазон значений (-inf, inf).
    /// </remarks>
    public class LinearActivationFunction : IActivationFunction
    {
        /// <inheritdoc/>
        public string Name => "Linear";

        /// <inheritdoc/>
        public double Calculate(double x)
        {
            return x;
        }

        /// <inheritdoc/>
        public double CalculateDerivative(double x)
        {
            return 1;
        }
    }
}
