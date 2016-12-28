using System;
using System.Collections.Generic;
using System.Linq;
using Clif.Abstract;
using Clif.MatchResults;

namespace Clif
{
    /// <summary>
    ///     Stores information about the declared route
    /// </summary>
    public class CommandRoute : ICommandRoute
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="CommandRoute" />
        /// </summary>
        /// <param name="commandTemplate"></param>
        public CommandRoute(string commandTemplate)
        {
            if (string.IsNullOrEmpty(commandTemplate))
            {
                throw new ArgumentException($"{nameof(CommandTemplate)} must be specified", nameof(commandTemplate));
            }

            CommandTemplate = commandTemplate;
        }

        /// <summary>
        ///     The list of segments
        /// </summary>
        private List<ISegment> Segments { get; } = new List<ISegment>();

        /// <summary>
        ///     The list of optional segments
        /// </summary>
        private List<INamedSegment> OptionalSegments { get; } = new List<INamedSegment>();

        /// <summary>
        ///     The template register for this command
        /// </summary>
        public string CommandTemplate { get; set; }

        /// <summary>
        ///     Adds a segment to this <see cref="CommandRoute" />
        /// </summary>
        /// <param name="segment"></param>
        public void AddSegment(ISegment segment)
        {
            Segments.Add(segment);
        }

        /// <summary>
        ///     Adds an optional segment to this <see cref="CommandRoute" />
        /// </summary>
        /// <param name="segment"></param>
        public void AddOptionalSegment(INamedSegment segment)
        {
            OptionalSegments.Add(segment);
        }

        /// <summary>
        ///     Checks if this <see cref="CommandRoute" /> matches the
        ///     provided <paramref name="command" />.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>
        ///     <see cref="CommandResult" /> if it matches otherwise it returns null.
        /// </returns>
        public CommandResult Match(IEnumerable<string> command)
        {
            var results = new List<IMatchResult>();
            var optionalResults = new List<IMatchResult>();

            var commandEnumerator = ListToPairList(command).GetEnumerator();
            var segmentEnumerator = Segments.GetEnumerator();

            using (commandEnumerator)
            {
                using (segmentEnumerator)
                {
                    while (segmentEnumerator.MoveNext())
                    {
                        if (!commandEnumerator.MoveNext())
                        {
                            //not match, the number of commands is less than the number of segments
                            return null;
                        }

                        var match = segmentEnumerator.Current?.Match(commandEnumerator.Current.Item1);

                        if (match == null)
                        {
                            return null;
                        }
                        results.Add(match);
                    }
                }

                while (commandEnumerator.MoveNext())
                {
                    var current = commandEnumerator.Current;

                    IMatchResult result = null;
                    foreach (var optionalSegment in OptionalSegments)
                    {
                        //verify this isn't the last piece
                        if (current.Item2 != null)
                        {
                            result = optionalSegment.Match($"{current.Item1} {current.Item2}");
                            if (result != null)
                            {
                                //skip ahead by one because we matched both the current and next command piece
                                commandEnumerator.MoveNext();
                                break;
                            }
                        }

                        result = optionalSegment.Match(current.Item1);
                        if (result != null)
                        {
                            break;
                        }
                    }

                    if (result == null)
                    {
                        return null;
                    }

                    optionalResults.Add(result);
                }

                //add all missing optional segments with null values
                foreach (var optionalSegment in OptionalSegments)
                {
                    if (optionalResults.All(x => (x as NamedValueMatchResult)?.Name != optionalSegment.Name))
                    {
                        optionalResults.Add(new NamedValueMatchResult(optionalSegment.Name, null));
                    }
                }

            }

            return new CommandResult
            {
                MatchResults = results,
                OptionalMatchResults = optionalResults
            };
        }

        private IEnumerable<Tuple<string, string>> ListToPairList(IEnumerable<string> list)
        {
            using (var enumerator = list.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    yield break;
                }

                var current = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    var previous = current;
                    current = enumerator.Current;
                    yield return Tuple.Create(previous, current);
                }

                yield return Tuple.Create(current, default(string));
            }
        }
    }
}