namespace Montemdraco.NeuralUtils.Library.Interfaces.Functions
{
    /// <summary>
    /// Интерфейс калькулятора для функций активации.
    /// </summary>
    public interface IActivationFunctionCalculator
    {
        /// <summary>
        /// Вычисляет значение функции активации.
        /// </summary>
        /// <param name="functionName">Имя функции.</param>
        /// <param name="x">Параметр функции.</param>
        /// <returns>Результат вычисления.</returns>
        double Calculate(string functionName, double x);

        /// <summary>
        /// Вычисляет значение производной функции активации. 
        /// </summary>
        /// <param name="functionName">Имя функции.</param>
        /// <param name="x">Параметр функции.</param>
        /// <returns>Результат вычисления.</returns>
        double CalculateDerivative(string functionName, double x);
    }
}
