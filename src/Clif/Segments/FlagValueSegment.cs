using System.Text.RegularExpressions;
using Clif.Abstract;
using Clif.MatchResults;

namespace Clif.Segments
{
    /// <summary>
    /// A basic class for representing a segment match
    /// </summary>
    public class FlagValueSegment : ISegment, INamedSegment
    {
        private static readonly Regex FlagRegex = new Regex(@"^\[-(\w+)\|(\w+)\]$");

        /// <summary>
        /// Constructs a <see cref="ConstantSegment"/>
        /// </summary>
        /// <param name="segmentText"></param>
        public FlagValueSegment(string segmentText)
        {
            var match = FlagRegex.Match(segmentText);
            var flag = match.Groups[1].Value;

            Name = match.Groups[2].Value;
            FlagVariableMatchRegex = new Regex($@"^-{flag} (\w+)$");
        }

        private Regex FlagVariableMatchRegex { get; }

        /// <summary>
        /// Segment name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Determines if a string matches a segment
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        public IMatchResult Match(string piece)
        {
            if (FlagVariableMatchRegex.IsMatch(piece))
            {
                return new NamedValueMatchResult(Name, "value");
            }

            return null;
        }
    }
}