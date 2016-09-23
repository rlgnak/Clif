using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Clif.Abstract;
using Microsoft.Extensions.DependencyModel;

namespace Clif
{
    /// <summary>
    /// Defines the functionality of an assembly catalog.
    /// </summary>
    public class DefaultAssemblyCatalog : IAssemblyCatalog
    {
        /// <summary>
        /// Returns assemblies
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<Assembly> GetAssemblies()
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            var dependencyContext = DependencyContext.Load(entryAssembly);
            var names = dependencyContext.GetDefaultAssemblyNames();
            return names.Select(info => Assembly.Load(new AssemblyName(info.Name))).ToArray();
        }
    }
}