namespace Montemdraco.NeuralUtils.Library.Model.Synapses
{
    /// <summary>
    /// Класс, описывающий вес синапса.
    /// </summary>
    public class SynapseWeight
    {
        /// <summary>
        /// Инициализирует новый объект класса <see cref="SynapseWeight"/>.
        /// </summary>
        public SynapseWeight()
            : this(0)
        {
        }

        /// <summary>
        /// Инициализирует новый объект класса <see cref="SynapseWeight"/>.
        /// </summary>
        /// <param name="weight">Начальный вес синапса.</param>
        public SynapseWeight(double weight)
        {
            Weight = weight;
        }

        /// <summary>
        /// Получает или задает вес синапса.
        /// </summary>
        public double Weight { get; set; }
    }
}
