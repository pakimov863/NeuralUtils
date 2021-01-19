using System.Collections.Generic;
using Montemdraco.NeuralUtils.Library.Interfaces.Functions;
using Montemdraco.NeuralUtils.Library.Model;

namespace Montemdraco.NeuralUtils.Library.Services.Functions.Errors
{
    /// <summary>
    /// Калькулятор функций рассчета ошибки.
    /// </summary>
    public class ErrorFunctionCalculator : FunctionCalculatorBase<IErrorFunction>, IErrorFunctionCalculator
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ErrorFunctionCalculator"/>.
        /// </summary>
        /// <param name="funcs">Коллекция функций рассчета ошибки.</param>
        public ErrorFunctionCalculator(IEnumerable<IErrorFunction> funcs)
            : base(funcs)
        {
        }

        /// <inheritdoc />
        public double Calculate(string functionName, NeuralOutputData expected, NeuralOutputData obtained)
        {
            var func = GetFunctionByName(functionName);
            return func.Calculate(expected, obtained);
        }
    }
}
