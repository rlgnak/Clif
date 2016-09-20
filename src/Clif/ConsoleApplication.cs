using System.Collections.Generic;
using Clif.Abstract;

namespace Clif
{
    /// <summary>
    /// Repersents a configured console application
    /// </summary>
    public class ConsoleApplication
    {
        private ICommandModuleResolver CommandModuleResolver { get; }
        private ICommandRouteBuilder CommandRouteBuilder { get; }
        private CommandCatalog Catalog { get; }
        private ICommandResolver CommandResolver { get; }

        /// <summary>
        /// Constructs a <see cref="ConsoleApplication"/>
        /// </summary>
        /// <param name="commandModuleResolver"></param>
        /// <param name="commandRouteBuilder"></param>
        /// <param name="catalog"></param>
        /// <param name="commandResolver"></param>
        public ConsoleApplication(ICommandModuleResolver commandModuleResolver, ICommandRouteBuilder commandRouteBuilder, CommandCatalog catalog, ICommandResolver commandResolver)
        {
            CommandModuleResolver = commandModuleResolver;
            CommandRouteBuilder = commandRouteBuilder;
            Catalog = catalog;
            CommandResolver = commandResolver;

            var modules = CommandModuleResolver.GetCommandModules();

            RegisterModules(modules);
        }

        private void RegisterModules(IEnumerable<CommandModule> modules)
        {
            foreach (var module in modules)
            {
                foreach (var command in module.Commands)
                {
                    CommandRouteBuilder.ParseRoute(command.CommandRoute);
                }

                Catalog.AddModule(module);
            }
        }

        /// <summary>
        /// Executes command based on provided args
        /// </summary>
        /// <param name="args"></param>
        public void Resolve(string[] args)
        {
            CommandResolver.Resolve(args);
        }
    }
}