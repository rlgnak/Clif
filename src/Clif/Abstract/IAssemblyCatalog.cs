using System.Collections.Generic;
using System.Reflection;

namespace Clif.Abstract
{
    /// <summary>
    /// Defines the functionality of an assembly catalog.
    /// </summary>
    public interface IAssemblyCatalog
    {
        /// <summary>
        /// Returns assemblies
        /// </summary>
        /// <returns></returns>
        IReadOnlyCollection<Assembly> GetAssemblies();
    }
}