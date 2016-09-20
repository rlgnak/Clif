using System.Collections.Generic;

namespace Clif.Abstract
{
    /// <summary>
    /// Basic class used to resolve commands
    /// </summary>
    public interface ICommandResolver
    {
        /// <summary>
        /// Resolves a command given list of strings
        /// </summary>
        /// <param name="segments"></param>
        void Resolve(IEnumerable<string> segments);
    }
}