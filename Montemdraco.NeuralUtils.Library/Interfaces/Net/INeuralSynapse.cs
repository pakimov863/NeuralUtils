namespace Montemdraco.NeuralUtils.Library.Interfaces.Net
{
    /// <summary>
    /// Интерфейс синапса между узлами нейронной сети.
    /// </summary>
    public interface INeuralSynapse
    {
        /// <summary>
        /// Получает узел, расположенный в начале связи.
        /// </summary>
        INeuralNode LeftNode { get; }

        /// <summary>
        /// Получает узел, расположенный в конце связи.
        /// </summary>
        INeuralNode RightNode { get; }

        /// <summary>
        /// Получает вес связи.
        /// </summary>
        double CurrentWeight { get; }

        /// <summary>
        /// Изменяет вес связи.
        /// </summary>
        /// <param name="newWeight">Новый вес связи.</param>
        void ChangeWeight(double newWeight);
    }
}
