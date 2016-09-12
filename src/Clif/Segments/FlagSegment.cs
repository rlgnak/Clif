using System.Text.RegularExpressions;
using Clif.Abstract;
using Clif.MatchResults;

namespace Clif.Segments
{
    /// <summary>
    /// </summary>
    public class FlagSegment : ISegment, INamedSegment
    {
        private static readonly Regex FlagRegex = new Regex(@"^\[-(\w+)\|(\w+)\]$");

        public FlagSegment(string piece)
        {
            var match = FlagRegex.Match(piece);
            var flag = match.Groups[1].Value;

            Name = match.Groups[2].Value;
            FlagMatchRegex = new Regex($@"^-{flag}$");
            FlagVariableMatchRegex = new Regex($@"^-{flag} (\w+)$");
        }

        public Regex FlagMatchRegex { get; set; }

        public Regex FlagVariableMatchRegex { get; set; }

        public string Name { get; set; }

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