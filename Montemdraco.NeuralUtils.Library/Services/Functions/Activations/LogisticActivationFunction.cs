using System;
using Montemdraco.NeuralUtils.Library.Interfaces.Functions;

namespace Montemdraco.NeuralUtils.Library.Services.Functions.Activations
{
    /// <summary>
    /// Сигмоидная функция активации.
    /// </summary>
    /// <remarks>
    /// Диапазон значений [0, 1].
    /// </remarks>
    public class LogisticActivationFunction : IActivationFunction
    {
        /// <inheritdoc/>
        public string Name => "Logistic";

        /// <inheritdoc/>
        public double Calculate(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        /// <inheritdoc/>
        public double CalculateDerivative(double x)
        {
            return (1 - x) * x;
        }
    }
}
