using System.Collections.Generic;
using System.Dynamic;
using Clif.Abstract;
using Clif.MatchResults;

namespace Clif
{
    /// <summary>
    /// A basic class used for invoking commands
    /// </summary>
    public class DefaultCommandInvoker : ICommandInvoker
    {
        /// <summary>
        /// Invokes a <see cref="Command"/> given a <see cref="CommandResult"/>
        /// </summary>
        /// <param name="commandResult"></param>
        /// <param name="command"></param>
        public void Invoke(CommandResult commandResult, ICommand command)
        {
            var flags = ListResultsToExpando(commandResult.OptionalMatchResults);
            var paramters = ListResultsToExpando(commandResult.MatchResults);

            command.Invoke(paramters, flags);
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