using System;
using System.Linq;
using Montemdraco.NeuralUtils.Library.Interfaces.Functions;
using Montemdraco.NeuralUtils.Library.Model;

namespace Montemdraco.NeuralUtils.Library.Services.Functions.Errors
{
    /// <summary>
    /// Arctan функция активации.
    /// </summary>
    public class ArctanErrorFunction : IErrorFunction
    {
        /// <inheritdoc />
        public string Name => "Arctan";

        /// <inheritdoc />
        public double Calculate(NeuralOutputData expected, NeuralOutputData obtained)
        {
            var expectedContainer = expected.OutputContainer.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value).ToList();
            var obtainedContainer = obtained.OutputContainer.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value).ToList();
            var expectedCount = expectedContainer.Count;
            var obtainedCount = obtainedContainer.Count;

            if (expectedCount != obtainedCount)
            {
                throw new ArgumentException($"Array must have some lengths: {expectedCount} and {obtainedCount}");
            }

            if (expectedCount == 0 || obtainedCount == 0)
            {
                throw new ArgumentException("Array length must be more than 0.");
            }

            var result = expectedContainer.Zip(obtainedContainer, (exp, obt) => Math.Pow(Math.Tan(exp - obt), -2)).Sum();
            return result / expectedCount;
        }
    }
}
