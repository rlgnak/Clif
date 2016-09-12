using System;
using System.Collections.Generic;
using System.Dynamic;
using Clif.Abstract;
using Clif.MatchResults;

namespace Clif
{
    public class DefaultCommandResolver : ICommandResolver
    {
        private CommandCatalog Catalog { get; }

        public DefaultCommandResolver(CommandCatalog catalog)
        {
            Catalog = catalog;
        }

        public void Resolve(IEnumerable<string> segments)
        {
            var catalogEnumerator = Catalog.GetEnumerator();

            using (catalogEnumerator)
            {
                CommandResult commandResult = null;

                while (catalogEnumerator.MoveNext())
                {
                    commandResult = catalogEnumerator.Current.CommandRoute.Match(segments);

                    if (commandResult != null)
                    {
                        break;
                    }
                }

                if (commandResult == null)
                {
                    throw new Exception("Command Not Found");
                }

                var flags = ListResultsToExpando(commandResult.OptionalMatchResults);
                var paramters = ListResultsToExpando(commandResult.MatchResults);

                catalogEnumerator.Current.Invoke(paramters, flags);
            }
        }
        
        private static ExpandoObject ListResultsToExpando(IEnumerable<IMatchResult> matchResults)
        {
            var expando = new ExpandoObject();
            var expandoDictionary = (ICollection<KeyValuePair<string, object>>)expando;

            foreach (var matchResult in matchResults)
            {
                if (matchResult is NamedValueMatchResult)
                {
                    var nameValueMatchResult = matchResult as NamedValueMatchResult;
                    expandoDictionary.Add(new KeyValuePair<string, object>(nameValueMatchResult.Name,
                        nameValueMatchResult.Value));
                }
            }

            return expando;
        }
    }
}
