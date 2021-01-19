using Montemdraco.NeuralUtils.Library.Model;

namespace Montemdraco.NeuralUtils.Library.Interfaces.Functions
{
    /// <summary>
    /// Интерфейс калькулятора функций вычисления ошибки.
    /// </summary>
    public interface IErrorFunctionCalculator
    {
        /// <summary>
        /// Вычисляет ошибку между ожидаемыми и полученными результатами.
        /// </summary>
        /// <param name="functionName">Имя функции.</param>
        /// <param name="expected">Контейнер ожидаемых результатов.</param>
        /// <param name="obtained">Контейнер полученных результатов.</param>
        /// <returns>Результат вычисления.</returns>
        double Calculate(string functionName, NeuralOutputData expected, NeuralOutputData obtained);
    }
}
