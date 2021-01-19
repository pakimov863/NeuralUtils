using Montemdraco.NeuralUtils.Library.Model.Synapses;

namespace Montemdraco.NeuralUtils.Library.Interfaces.Net
{
    /// <summary>
    /// Интерфейс билдера узлов.
    /// </summary>
    public interface INeuralNodeBuilder : INeuralNode
    {
        /// <summary>
        /// Выполняет инициализацию узла нейронной сети.
        /// </summary>
        /// <param name="name">Имя узла.</param>
        void Initialize(string name);

        /// <summary>
        /// Добавляет заданный узел следующим (после текущего).
        /// </summary>
        /// <param name="node">Соединяемый узел.</param>
        void AddNextNode(INeuralNodeBuilder node);

        /// <summary>
        /// Добавляет заданный узел следующим (после текущего).
        /// </summary>
        /// <param name="node">Соединяемый узел.</param>
        /// <param name="weight">Заданный вес синапса.</param>
        void AddNextNode(INeuralNodeBuilder node, double weight);

        /// <summary>
        /// Добавляет заданный узел следующим (после текущего).
        /// </summary>
        /// <param name="node">Соединяемый узел.</param>
        /// <param name="weight">Заданный вес синапса.</param>
        void AddNextNode(INeuralNodeBuilder node, SynapseWeight weight);

        /// <summary>
        /// Добавляет заданный узел предыдущим (перед текущим).
        /// </summary>
        /// <param name="node">Соединяемый узел.</param>
        void AddPrevNode(INeuralNodeBuilder node);

        /// <summary>
        /// Добавляет заданный узел предыдущим (перед текущим).
        /// </summary>
        /// <param name="node">Соединяемый узел.</param>
        /// <param name="weight">Заданный вес синапса.</param>
        void AddPrevNode(INeuralNodeBuilder node, double weight);

        /// <summary>
        /// Добавляет заданный узел предыдущим (перед текущим).
        /// </summary>
        /// <param name="node">Соединяемый узел.</param>
        /// <param name="weight">Заданный вес синапса.</param>
        void AddPrevNode(INeuralNodeBuilder node, SynapseWeight weight);
    }
}
