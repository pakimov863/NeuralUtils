using System.Collections.Generic;
using Montemdraco.NeuralUtils.Library.Interfaces.Functions;

namespace Montemdraco.NeuralUtils.Library.Interfaces.Net
{
    /// <summary>
    /// Интерфейс узла нейронной сети.
    /// </summary>
    public interface INeuralNode : INamedObject
    {
        /// <summary>
        /// Получает значение, показывающее, является ли данный узел входным.
        /// </summary>
        bool IsInputNode { get; }

        /// <summary>
        /// Получает значение, показывающее, является ли данный узел выходным.
        /// </summary>
        bool IsOutputNode { get; }

        /// <summary>
        /// Получает функцию активации узла нейронной сети.
        /// </summary>
        IActivationFunction ActivationFunction { get; }

        /// <summary>
        /// Добавляет новое значение на вход узла.
        /// </summary>
        /// <param name="input">Значение на входе узла.</param>
        void AddInputValue(double input);

        /// <summary>
        /// Очищает последние сохраненные узлом значения.
        /// </summary>
        void ClearCachedValues();

        /// <summary>
        /// Получает выход узла.
        /// </summary>
        /// <returns>Выходное значение узла.</returns>
        double GetOutput();

        /// <summary>
        /// Получает коллекцию узлов, расположенных слева от текущего.
        /// </summary>
        /// <returns>Коллекция узлов.</returns>
        IEnumerable<INeuralNode> GetPreviousNodes();

        /// <summary>
        /// Получает коллекцию узлов, расположенных справа от текущего.
        /// </summary>
        /// <returns>Коллекция узлов.</returns>
        IEnumerable<INeuralNode> GetNextNodes();

        /// <summary>
        /// Получает коллекцию связей на уровень слева.
        /// </summary>
        /// <returns>Коллекция синапсов.</returns>
        IEnumerable<INeuralSynapse> GetPreviousLinks();

        /// <summary>
        /// Получает коллекцию связей на уровень справа.
        /// </summary>
        /// <returns>Коллекция синапсов.</returns>
        IEnumerable<INeuralSynapse> GetNextLinks();

        /// <summary>
        /// Запускает обработку данных.
        /// </summary>
        /// <returns>Результат обработки.</returns>
        double Run();
    }
}
