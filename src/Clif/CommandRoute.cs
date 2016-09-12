using System;
using System.Collections.Generic;
using Clif.Abstract;

namespace Clif
{
    /// <summary>
    /// Stores information about the declared route
    /// </summary>
    public class CommandRoute
    {
        /// <summary>
        /// Initalizes a new instance of <see cref="CommandRoute"/>
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
        /// The template regiester for this command
        /// </summary>
        public string CommandTemplate { get; set; }

        /// <summary>
        /// The list of segments
        /// </summary>
        private List<ISegment> Segments { get; } = new List<ISegment>();

        /// <summary>
        /// The list of optional segments
        /// </summary>
        private List<ISegment> OptionalSegments { get; } = new List<ISegment>();

        /// <summary>
        /// Adds a segment to this <see cref="CommandRoute"/>
        /// </summary>
        /// <param name="segment"></param>
        public void AddSegment(ISegment segment)
        {
            Segments.Add(segment);
        }

        /// <summary>
        /// Adds an optional segment to this <see cref="CommandRoute"/>
        /// </summary>
        /// <param name="segment"></param>
        public void AddOptionalSegment(ISegment segment)
        {
            OptionalSegments.Add(segment);
        }

        /// <summary>
        /// Checks if this <see cref="CommandRoute"/> matches the 
        /// provided <paramref name="command"/>.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>
        /// <see cref="CommandResult"/> if it matches otherwise it returns null.
        /// </returns>
        public CommandResult Match(IEnumerable<string> command)
        {
            var results = new List<IMatchResult>();
            var optionalResults = new List<IMatchResult>();

            var commandEnumerator = command.GetEnumerator();
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

                        var match = segmentEnumerator.Current?.Match(commandEnumerator.Current);

                        if (match == null)
                        {
                            return null;
                        }
                        results.Add(match);
                    }
                }
                
                while (commandEnumerator.MoveNext())
                {
                    IMatchResult result = null;
                    foreach (var optionalSegment in OptionalSegments)
                    {
                        result = optionalSegment.Match(commandEnumerator.Current);
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
            }

            return new CommandResult
            {
                MatchResults = results,
                OptionalMatchResults = optionalResults
            };
        }
    }
}