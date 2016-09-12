using System.Collections.Generic;
using Clif.Abstract;

namespace Clif
{
    public class ConsoleApplication
    {
        private ICommandModuleResolver CommandModuleResolver { get; }
        private ICommandRouteBuilder CommandRouteBuilder { get; }
        private CommandCatalog Catalog { get; }
        private ICommandResolver CommandResolver { get; }

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

        public void Resolve(string[] args)
        {
            CommandResolver.Resolve(args);
        }
    }
}