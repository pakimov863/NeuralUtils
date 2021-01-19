using System.Collections.Generic;

namespace Montemdraco.NeuralUtils.Library.Model
{
    /// <summary>
    /// Контейнер выходных данных из нейронной сети.
    /// </summary>
    public class NeuralOutputData
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="NeuralOutputData"/>.
        /// </summary>
        public NeuralOutputData()
        {
            OutputContainer = new Dictionary<string, double>();
        }

        /// <summary>
        /// Получает коллекцию входных данных нейронной сети.
        /// </summary>
        public Dictionary<string, double> OutputContainer { get; }
    }
}
