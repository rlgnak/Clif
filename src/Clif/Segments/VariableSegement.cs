using System.Text.RegularExpressions;
using Clif.Abstract;
using Clif.MatchResults;

namespace Clif.Segments
{
    public class VariableSegement : ISegment, INamedSegment
    {
        private static readonly Regex VairbaleRegex = new Regex(@"^\{(\w+)\}$");
        private static readonly Regex MatchRegex = new Regex(@"^\w+$");

        public VariableSegement(string segmentText)
        {
            var match = VairbaleRegex.Match(segmentText);
            Name = match.Groups[1].Value;
        }

        public string Name { get; set; }

        public IMatchResult Match(string piece)
        {
            return MatchRegex.IsMatch(piece) ? new NamedValueMatchResult(Name, piece) : null;
        }
    }
}