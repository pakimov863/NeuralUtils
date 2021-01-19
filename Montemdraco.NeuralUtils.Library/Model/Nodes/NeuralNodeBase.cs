using System;
using System.Collections.Generic;
using System.Linq;
using Montemdraco.NeuralUtils.Library.Interfaces.Functions;
using Montemdraco.NeuralUtils.Library.Interfaces.Net;
using Montemdraco.NeuralUtils.Library.Model.Synapses;

namespace Montemdraco.NeuralUtils.Library.Model.Nodes
{
    /// <summary>
    /// Базовый класс для нейрона.
    /// </summary>
    public abstract class NeuralNodeBase : INeuralNodeBuilder
    {
        /// <summary>
        /// Коллекция синапсов, ассоциированных с данным узлом.
        /// </summary>
        protected List<INeuralSynapse> _linkedSynapses;

        /// <summary>
        /// Коллекция входных значений узла.
        /// </summary>
        protected List<double> _inputValues;

        /// <summary>
        /// Выходное значение узла.
        /// </summary>
        protected double _outputValue;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="NeuralNodeBase"/>.
        /// </summary>
        /// <param name="activationFunction"></param>
        protected NeuralNodeBase(IActivationFunction activationFunction)
        {
            ActivationFunction = activationFunction;

            _linkedSynapses = new List<INeuralSynapse>();
            _inputValues = new List<double>();
        }

        /// <summary>
        /// Получает или задает значение, показывающее, является ли данный узел входным.
        /// </summary>
        public bool IsInputNode { get; protected set; }

        /// <summary>
        /// Получает или задает значение, показывающее, является ли данный узел выходным.
        /// </summary>
        public bool IsOutputNode { get; protected set; }
        
        /// <inheritdoc />
        public IActivationFunction ActivationFunction { get; private set; }

        /// <inheritdoc />
        public string Name { get; private set; }

        /// <inheritdoc />
        public virtual void Initialize(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = Guid.NewGuid().ToString("N");
            }

            Name = name;
        }

        /// <inheritdoc />
        public virtual void AddNextNode(INeuralNodeBuilder node)
        {
            AddNextNode(node, 0);
        }

        /// <inheritdoc />
        public virtual void AddNextNode(INeuralNodeBuilder node, double weight)
        {
            var w = new SynapseWeight(weight);
            AddNextNode(node, w);
        }

        /// <inheritdoc />
        public virtual void AddNextNode(INeuralNodeBuilder node, SynapseWeight weight)
        {
            if (_linkedSynapses.Any(e => e.RightNode.Name.Equals(node.Name)))
            {
                return;
            }

            _linkedSynapses.Add(new DefaultNeuralSynapse(this, node, weight));
            node.AddPrevNode(this, weight);
        }

        /// <inheritdoc />
        public virtual void AddPrevNode(INeuralNodeBuilder node)
        {
            AddPrevNode(node, 0);
        }

        /// <inheritdoc />
        public virtual void AddPrevNode(INeuralNodeBuilder node, double weight)
        {
            var w = new SynapseWeight(weight);
            AddPrevNode(node, w);
        }

        /// <inheritdoc />
        public virtual void AddPrevNode(INeuralNodeBuilder node, SynapseWeight weight)
        {
            if (_linkedSynapses.Any(e => e.LeftNode.Name.Equals(node.Name)))
            {
                return;
            }

            _linkedSynapses.Add(new DefaultNeuralSynapse(node, this, weight));
            node.AddNextNode(this, weight);
        }

        /// <inheritdoc />
        public virtual void AddInputValue(double input)
        {
            _inputValues.Add(input);
        }

        /// <inheritdoc />
        public virtual void ClearCachedValues()
        {
            _inputValues.Clear();
            _outputValue = double.NaN;
        }

        /// <inheritdoc />
        public virtual double GetOutput()
        {
            return _outputValue;
        }

        /// <inheritdoc />
        public virtual IEnumerable<INeuralNode> GetNextNodes()
        {
            return GetNextLinks().Select(e => e.RightNode);
        }

        /// <inheritdoc />
        public virtual IEnumerable<INeuralNode> GetPreviousNodes()
        {
            return GetPreviousLinks().Select(e => e.LeftNode);
        }

        /// <inheritdoc />
        public virtual IEnumerable<INeuralSynapse> GetNextLinks()
        {
            return _linkedSynapses
                .Where(e => e.LeftNode.Name.Equals(Name, StringComparison.InvariantCulture));
        }

        /// <inheritdoc />
        public virtual IEnumerable<INeuralSynapse> GetPreviousLinks()
        {
            return _linkedSynapses
                .Where(e => e.RightNode.Name.Equals(Name, StringComparison.InvariantCulture));
        }

        /// <inheritdoc />
        public abstract double Run();
    }
}
