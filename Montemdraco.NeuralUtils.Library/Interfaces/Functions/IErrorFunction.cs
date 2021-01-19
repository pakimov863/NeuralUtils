using Montemdraco.NeuralUtils.Library.Model;

namespace Montemdraco.NeuralUtils.Library.Interfaces.Functions
{
    /// <summary>
    /// Интерфейс функции расчета ошибки.
    /// </summary>
    public interface IErrorFunction : INamedObject
    {
        /// <summary>
        /// Вычисляет ошибку между ожидаемыми и полученными результатами.
        /// </summary>
        /// <param name="expected">Контейнер ожидаемых результатов.</param>
        /// <param name="obtained">Контейнер полученных результатов.</param>
        /// <returns>Результат вычисления.</returns>
        double Calculate(NeuralOutputData expected, NeuralOutputData obtained);
    }
}
