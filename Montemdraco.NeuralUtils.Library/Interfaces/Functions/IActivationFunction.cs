namespace Montemdraco.NeuralUtils.Library.Interfaces.Functions
{
    /// <summary>
    /// Интерфейс функции активации.
    /// </summary>
    public interface IActivationFunction : INamedObject
    {
        /// <summary>
        /// Вычисляет значение функции активации.
        /// </summary>
        /// <param name="x">Параметр функции.</param>
        /// <returns>Результат вычисления.</returns>
        double Calculate(double x);

        /// <summary>
        /// Вычисляет значение производной функции активации. 
        /// </summary>
        /// <param name="x">Параметр функции.</param>
        /// <returns>Результат вычисления.</returns>
        double CalculateDerivative(double x);
    }
}
