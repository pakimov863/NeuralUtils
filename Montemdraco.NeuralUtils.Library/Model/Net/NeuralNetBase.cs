using System;
using System.Collections.Generic;
using System.Linq;
using Montemdraco.NeuralUtils.Library.Interfaces.Net;

namespace Montemdraco.NeuralUtils.Library.Model.Net
{
    /// <summary>
    /// Базовый класс для нейронных сетей.
    /// </summary>
    public abstract class NeuralNetBase : INeuralNetBuilder
    {
        /// <summary>
        /// Коллекция узлов нейронной сети.
        /// </summary>
        protected List<INeuralNode> _nodeCollection;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="NeuralNetBase"/>.
        /// </summary>
        protected NeuralNetBase()
        {
            _nodeCollection = new List<INeuralNode>();
        }

        /// <summary>
        /// Получает коллекцию узлов нейронной сети.
        /// </summary>
        public IReadOnlyList<INeuralNode> Nodes => _nodeCollection;

        ///<inheritdoc />
        public void SetInput(NeuralInputData input)
        {
            if (input == null || !input.InputContainer.Any())
            {
                throw new ArgumentException("Input container is empty.");
            }

            ClearNodesCache();

            var inputNodes = _nodeCollection
                .Where(e => e.IsInputNode)
                .ToList();

            foreach(var kvp in input.InputContainer)
            {
                var inputName = kvp.Key;
                var inputValue = kvp.Value;
                var node = inputNodes.FirstOrDefault(e => e.Name.Equals(inputName));
                if (node == null)
                {
                    throw new Exception(string.Format("Input node with name ({0}) not found.", inputName));
                }

                node.ClearCachedValues();
                node.AddInputValue(inputValue);
            }
        }

        ///<inheritdoc />
        public NeuralOutputData GetOutput()
        {
            var result = new NeuralOutputData();

            var outputNodes = _nodeCollection
                .Where(e => e.IsOutputNode);

            foreach(var node in outputNodes)
            {
                result.OutputContainer.Add(node.Name, node.GetOutput());
            }

            return result;
        }

        ///<inheritdoc />
        public abstract void Run();

        ///<inheritdoc />
        public virtual void SaveState(string filePath)
        {
            throw new NotImplementedException();

            /*var sw = new StreamWriter(filePath, false);

            sw.Write(GetName());
            sw.Write("|");
            sw.Write(ActivationFunction.GetName());
            foreach (var node in Nodes)
            {
                sw.Write("|");
                sw.Write(node.Identifier);
                sw.Write(":");
                sw.Write(node.GetNodeType());
                foreach (var synapse in node.NodeSynapses)
                {
                    sw.Write(":");
                    sw.Write(synapse.LinkWeight);
                    sw.Write("_");
                    sw.Write(synapse.LinkedTo.Identifier);
                    sw.Write("_");
                    sw.Write(synapse.LinkedFrom.Identifier);
                }
            }

            sw.Close();*/
        }

        ///<inheritdoc />
        public virtual void LoadState(string filePath)
        {
            throw new NotImplementedException();

            /*if (!File.Exists(filePath))
                throw new FileNotFoundException($"File [{filePath}] not found.");

            var rawContent = File.ReadAllText(filePath);
            var rawArray = rawContent.Split('|');
            if (rawArray.Length < 4)
                throw new InvalidInitializationException("Incorrect file structure.");*/
        }

        ///<inheritdoc />
        public virtual void SetNodes(IList<INeuralNode> nodes)
        {
            _nodeCollection = nodes.ToList();
        }

        /// <summary>
        /// Очищает кэш всех узлов нейронной сети.
        /// </summary>
        private void ClearNodesCache()
        {
            foreach(var node in _nodeCollection)
            {
                node.ClearCachedValues();
            }
        }
    }
}
