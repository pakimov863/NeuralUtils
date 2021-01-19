using System.Collections.Generic;

namespace Montemdraco.NeuralUtils.Library.Interfaces.Net
{
    /// <summary>
    /// Интерфейс билдера нейронной сети.
    /// </summary>
    public interface INeuralNetBuilder : INeuralNet
    {
        /// <summary>
        /// Задать узлы нейронной сети.
        /// </summary>
        /// <param name="nodes">Коллекция узлов.</param>
        void SetNodes(IList<INeuralNode> nodes);
    }
}
