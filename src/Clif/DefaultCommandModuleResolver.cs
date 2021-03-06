﻿using System;
using System.Collections.Generic;
using Clif.Abstract;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Clif
{
    /// <summary>
    /// Scans the current assembly for classes that extend <see cref="CommandModule"/>
    /// </summary>
    public class DefaultCommandModuleResolver : ICommandModuleResolver
    {
        private IServiceProvider ServiceProvider { get; }
        private IAssemblyCatalog AssemblyCatalog { get; set; }

        /// <summary>
        /// Constructs a <see cref="DefaultCommandModuleResolver"/>
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="assemblyCatalog"></param>
        public DefaultCommandModuleResolver(IServiceProvider serviceProvider, IAssemblyCatalog assemblyCatalog)
        {
            ServiceProvider = serviceProvider;
            AssemblyCatalog = assemblyCatalog;
        }

        private IEnumerable<Type> GetModules()
        {
            var types = AssemblyCatalog.GetAssemblies().SelectMany(x => x.GetTypes());
            return types.Where(x => x.GetTypeInfo().IsClass && !x.GetTypeInfo().IsAbstract && x.GetTypeInfo().IsSubclassOf(typeof(CommandModule)));
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