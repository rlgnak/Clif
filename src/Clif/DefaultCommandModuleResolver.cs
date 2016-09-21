using System;
using System.Collections.Generic;
using Clif.Abstract;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;

namespace Clif
{
    /// <summary>
    /// Scans the current assembly for classes that extend <see cref="CommandModule"/>
    /// </summary>
    public class DefaultCommandModuleResolver : ICommandModuleResolver
    {
        private IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Constructs a <see cref="DefaultCommandModuleResolver"/>
        /// </summary>
        /// <param name="serviceProvider"></param>
        public DefaultCommandModuleResolver(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        private static IEnumerable<Type> GetModules()
        {
            var assemblyNames = DependencyContext.Default.GetDefaultAssemblyNames();
            var assemblies = assemblyNames.Select(info => Assembly.Load(new AssemblyName(info.Name)));
            var types = assemblies.SelectMany(x => x.GetTypes());
            return types.Where(x => x.GetTypeInfo().IsSubclassOf(typeof(CommandModule)));
        }

        /// <summary>
        /// Returns instance of all command modules registered in the assembly
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CommandModule> GetCommandModules()
        {
            var commands = new List<CommandModule>();

            foreach (var module in GetModules())
            {
                var instance = ActivatorUtilities.CreateInstance(ServiceProvider, module) as CommandModule;
                commands.Add(instance);
            }

            return commands.AsReadOnly();
        }
    }
}