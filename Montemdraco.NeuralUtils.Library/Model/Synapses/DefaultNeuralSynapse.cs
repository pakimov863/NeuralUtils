using Montemdraco.NeuralUtils.Library.Interfaces.Net;

namespace Montemdraco.NeuralUtils.Library.Model.Synapses
{
    /// <summary>
    /// Модель обычного синапса.
    /// </summary>
    public class DefaultNeuralSynapse : INeuralSynapse
    {
        /// <summary>
        /// Контейнер веса текущего синапса.
        /// </summary>
        private readonly SynapseWeight _synapseWeight;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DefaultNeuralSynapse"/>.
        /// </summary>
        /// <param name="leftNode">Узел в начале связи.</param>
        /// <param name="rightNode">Узел в конце связи.</param>
        /// <param name="weight">Вес синапса.</param>
        public DefaultNeuralSynapse(INeuralNode leftNode, INeuralNode rightNode, SynapseWeight weight)
        {
            LeftNode = leftNode;
            RightNode = rightNode;
            _synapseWeight = weight;
        }

        ///<inheritdoc />
        public INeuralNode LeftNode { get; private set; }

        ///<inheritdoc />
        public INeuralNode RightNode { get; private set; }

        ///<inheritdoc />
        public double CurrentWeight => _synapseWeight.Weight;

        ///<inheritdoc />
        public void ChangeWeight(double newWeight)
        {
            _synapseWeight.Weight = newWeight;
        }
    }
}
