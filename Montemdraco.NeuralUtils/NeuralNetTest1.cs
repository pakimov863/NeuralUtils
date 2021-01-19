using System.Collections.Generic;
using System.Linq;
using Montemdraco.NeuralUtils.Library.Interfaces.Net;
using Montemdraco.NeuralUtils.Library.Model;
using Montemdraco.NeuralUtils.Library.Model.Net;
using Montemdraco.NeuralUtils.Library.Model.Nodes;
using Montemdraco.NeuralUtils.Library.Model.Teachers;
using Montemdraco.NeuralUtils.Library.Services.Functions.Activations;
using Montemdraco.NeuralUtils.Library.Services.Functions.Errors;
using Montemdraco.NeuralUtils.Library.Services.Teachers;

namespace Montemdraco.NeuralUtils
{
    public class NeuralNetTest1
    {
        public void Test1()
        {
            var net = new FeedForwardNeuralNet();
            var act = new LogisticActivationFunction();
            var err = new MeanSquareErrorFunction();
            var n1 = new SimpleInputNode(act);
            n1.Initialize("node1");
            var n2 = new SimpleInputNode(act);
            n2.Initialize("node2");
            var n3 = new SimpleHiddenNode(act);
            n3.Initialize("nodeH1");
            var n4 = new SimpleHiddenNode(act);
            n4.Initialize("nodeH2");
            var n5 = new SimpleOutputNode(act);
            n5.Initialize("nodeO1");

            n1.AddNextNode(n3, 0.45);
            n1.AddNextNode(n4, 0.78);
            n2.AddNextNode(n3, -0.12);
            n2.AddNextNode(n4, 0.13);
            n3.AddNextNode(n5, 1.5);
            n4.AddNextNode(n5, -2.3);
            net.SetNodes(new List<INeuralNode> { n1, n2, n3, n4, n5 });

            var outp1 = new NeuralOutputData();
            outp1.OutputContainer.Add("nodeO1", 0);
            var inp1 = new NeuralInputData();
            inp1.Add("node1", 0);
            inp1.Add("node2", 0);
            var outp2 = new NeuralOutputData();
            outp2.OutputContainer.Add("nodeO1", 1);
            var inp2 = new NeuralInputData();
            inp2.Add("node1", 0);
            inp2.Add("node2", 1);
            var outp3 = new NeuralOutputData();
            outp3.OutputContainer.Add("nodeO1", 1);
            var inp3 = new NeuralInputData();
            inp3.Add("node1", 1);
            inp3.Add("node2", 0);
            var outp4 = new NeuralOutputData();
            outp4.OutputContainer.Add("nodeO1", 0);
            var inp4 = new NeuralInputData();
            inp4.Add("node1", 1);
            inp4.Add("node2", 1);

            net.SetInput(inp3);
            net.Run();
            var outp = net.GetOutput();
            var outError = err.Calculate(outp3, outp);

            var les1 = new LessonData(inp1, outp1);
            var les2 = new LessonData(inp2, outp2);
            var les3 = new LessonData(inp3, outp3);
            var les4 = new LessonData(inp4, outp4);
            var lesCollection = new List<LessonData> { les3, les1, les2, les4 };

            var teacher = new BackPropagationTeacher(err);
            teacher.Initialize(0.7, 0.3);
            teacher.AddLessonRange(lesCollection);
            teacher.Teach(10000);

            foreach (var lessonData in lesCollection)
            {
                net.SetInput(lessonData.ExpectedInput);
                net.Run();
                var accruedOut = net.GetOutput().OutputContainer.First().Value;
                var expectedOut = lessonData.ExpectedOutput.OutputContainer.First().Value;
            }
        }
    }
}
