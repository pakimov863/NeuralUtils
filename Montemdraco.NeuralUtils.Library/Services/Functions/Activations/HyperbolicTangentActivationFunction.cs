using System;
using Montemdraco.NeuralUtils.Library.Interfaces.Functions;

namespace Montemdraco.NeuralUtils.Library.Services.Functions.Activations
{
    /// <summary>
    /// Гиперболический тангенс.
    /// </summary>
    /// <remarks>
    /// Диапазон значений [-1, 1].
    /// </remarks>
    public class HyperbolicTangentActivationFunction : IActivationFunction
    {
        /// <inheritdoc/>
        public string Name => "HyperbolicTangent";

        /// <inheritdoc/>
        public double Calculate(double x)
        {
            return (Math.Exp(2 * x) - 1) / (Math.Exp(2 * x) + 1);
        }

        /// <inheritdoc/>
        public double CalculateDerivative(double x)
        {
            return 1 - Math.Pow(x, 2);
        }
    }
}
