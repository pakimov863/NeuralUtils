using System;
using System.Collections.Generic;
using System.Linq;
using Montemdraco.NeuralUtils.Library.Helpers;
using Montemdraco.NeuralUtils.Library.Interfaces.Net;

namespace Montemdraco.NeuralUtils.Library.Model.Net
{
    /// <summary>
    /// Нейронная сеть прямого распространения.
    /// </summary>
    public class FeedForwardNeuralNet : NeuralNetBase
    {
        ///<inheritdoc />
        public override void Run()
        {
            var nodes = _nodeCollection
                .Where(e => e.IsInputNode)
                .ToList();

            while (true)
            {
                var nextNodes = new List<INeuralNode>();

                foreach(var node in nodes)
                {
                    var nodeResult = node.Run();
                    if (!double.IsNormal(nodeResult))
                    {
                        throw new Exception("Cannnot calculate node's output.");
                    }

                    foreach(var nextSynapse in node.GetNextLinks())
                    {
                        var nodeInput = nodeResult * nextSynapse.CurrentWeight;
                        if (!double.IsNormal(nodeResult))
                        {
                            throw new Exception("Cannnot calculate next node's input.");
                        }

                        var nextNode = nextSynapse.RightNode;

                        nextNode.AddInputValue(nodeInput);
                        nextNodes.Add(nextNode);
                    }
                }

                nextNodes = nextNodes.Distinct(new NeuralNodeEqualityComparer()).ToList();

                if (!nextNodes.Any())
                {
                    break;
                }

                nodes = nextNodes;
            }
        }
    }
}
