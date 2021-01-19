using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Montemdraco.NeuralUtils.Library.Interfaces.Net;

namespace Montemdraco.NeuralUtils.Library.Helpers
{
    /// <summary>
    /// Компаратор для <see cref="INeuralNode"/>.
    /// </summary>
    public class NeuralNodeEqualityComparer : IEqualityComparer<INeuralNode>
    {
        ///<inheritdoc />
        public bool Equals([AllowNull] INeuralNode x, [AllowNull] INeuralNode y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.Name.Equals(y.Name, System.StringComparison.InvariantCulture);
        }

        ///<inheritdoc />
        public int GetHashCode([DisallowNull] INeuralNode obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
