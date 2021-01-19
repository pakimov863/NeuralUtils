namespace Montemdraco.NeuralUtils.Library.Interfaces.Teachers
{
    /// <summary>
    /// Интерфейс учителя "Обратного распространения ошибки".
    /// </summary>
    public interface IBackPropagationTeacher
    {
        /// <summary>
        /// Выполняет инициализацию учителя.
        /// </summary>
        /// <param name="epsilonValue">Скорость обучения.</param>
        /// <param name="alphaValue">Момент обучения.</param>
        void Initialize(double epsilonValue, double alphaValue);
    }
}
