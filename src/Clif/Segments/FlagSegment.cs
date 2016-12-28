using System.Text.RegularExpressions;
using Clif.Abstract;
using Clif.MatchResults;

namespace Clif.Segments
{
    /// <summary>
    /// A basic class for representing a segment
    /// </summary>
    public class FlagSegment : INamedSegment
    {
        private static readonly Regex FlagRegex = new Regex(@"^\[-(\w+)\|(\w+)\]$");

        /// <summary>
        /// Constructs a <see cref="ConstantSegment"/>
        /// </summary>
        /// <param name="segmentText"></param>
        public FlagSegment(string segmentText)
        {
            var match = FlagRegex.Match(segmentText);
            var flag = match.Groups[1].Value;

            Name = match.Groups[2].Value;
            FlagMatchRegex = new Regex($@"^-{flag}$");
            FlagVariableMatchRegex = new Regex($@"^-{flag} (\w+)$");
        }

        private Regex FlagMatchRegex { get; }

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
            if (FlagMatchRegex.IsMatch(piece))
            {
                return new NamedValueMatchResult(Name, true);
            }

            if (FlagVariableMatchRegex.IsMatch(piece))
            {
                return new NamedValueMatchResult(Name, "value");
            }

            return null;
        }
    }
}