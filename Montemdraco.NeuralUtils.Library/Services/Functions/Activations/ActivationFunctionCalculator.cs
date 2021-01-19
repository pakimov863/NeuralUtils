using System.Collections.Generic;
using Montemdraco.NeuralUtils.Library.Interfaces.Functions;

namespace Montemdraco.NeuralUtils.Library.Services.Functions.Activations
{
    /// <summary>
    /// калькулятор функций активации.
    /// </summary>
    public class ActivationFunctionCalculator : FunctionCalculatorBase<IActivationFunction>, IActivationFunctionCalculator
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ActivationFunctionCalculator"/>.
        /// </summary>
        /// <param name="funcs">Коллекция функций активации.</param>
        public ActivationFunctionCalculator(IEnumerable<IActivationFunction> funcs)
            : base(funcs)
        {
        }

        /// <inheritdoc />
        public double Calculate(string functionName, double x)
        {
            var func = GetFunctionByName(functionName);
            return func.Calculate(x);
        }

        /// <inheritdoc />
        public double CalculateDerivative(string functionName, double x)
        {
            var func = GetFunctionByName(functionName);
            return func.Calculate(x);
        }
    }
}
