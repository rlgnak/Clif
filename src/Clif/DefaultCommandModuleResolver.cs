using System;
using System.Collections.Generic;
using Clif.Abstract;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;

namespace Clif
{
    public class DefaultCommandModuleResolver : ICommandModuleResolver
    {
        private IServiceProvider ServiceProvider { get; }

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