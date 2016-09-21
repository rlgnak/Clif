using System;
using System.Collections.Generic;
using System.Dynamic;
using Clif.Abstract;
using Clif.MatchResults;

namespace Clif
{
    /// <summary>
    /// Basic class used to resolve commands
    /// </summary>
    public class DefaultCommandResolver : ICommandResolver
    {
        private ICommandCatalog Catalog { get; }

        private ICommandInvoker CommandInvoker { get; }

        /// <summary>
        /// Constructs a <see cref="DefaultCommandModuleResolver"/>
        /// </summary>
        /// <param name="catalog"></param>
        /// <param name="commandInvoker"></param>
        public DefaultCommandResolver(ICommandCatalog catalog, ICommandInvoker commandInvoker)
        {
            Catalog = catalog;
            CommandInvoker = commandInvoker;
        }

        /// <summary>
        /// Resolves a command given list of strings
        /// </summary>
        /// <param name="segments"></param>
        public void Resolve(IEnumerable<string> segments)
        {
            var catalogEnumerator = Catalog.GetEnumerator();

            using (catalogEnumerator)
            {
                CommandResult commandResult = null;

                while (catalogEnumerator.MoveNext())
                {
                    commandResult = catalogEnumerator.Current.Match(segments);

                    if (commandResult != null)
                    {
                        break;
                    }
                }

                if (commandResult == null)
                {
                    throw new Exception("Command Not Found");
                }

                CommandInvoker.Invoke(commandResult, catalogEnumerator.Current);
            }
        }
    }
}
