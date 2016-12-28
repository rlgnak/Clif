using System.Text.RegularExpressions;
using Clif.Abstract;
using Clif.MatchResults;

namespace Clif.Segments
{
    /// <summary>
    /// A basic class for rpersenting a segment
    /// </summary>
    public class VariableSegement : INamedSegment
    {
        private static readonly Regex VairbaleRegex = new Regex(@"^\{(\w+)\}$");
        private static readonly Regex MatchRegex = new Regex(@"^\w(.*)$");

        /// <summary>
        /// Constructs a <see cref="VariableSegement"/>
        /// </summary>
        /// <param name="segmentText"></param>
        public VariableSegement(string segmentText)
        {
            var match = VairbaleRegex.Match(segmentText);
            Name = match.Groups[1].Value;
        }

        /// <summary>
        /// The name of a segment
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Determines if a string matches a segment
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        public IMatchResult Match(string piece)
        {
            return MatchRegex.IsMatch(piece) ? new NamedValueMatchResult(Name, piece) : null;
        }
    }
}