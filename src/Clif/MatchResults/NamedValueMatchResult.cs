using Clif.Abstract;

namespace Clif.MatchResults
{
    /// <summary>
    /// A basic class for rpersenting a segment match
    /// </summary>
    public class NamedValueMatchResult : IMatchResult
    {
        /// <summary>
        /// Constructs a  <see cref="NamedValueMatchResult"/>
        /// </summary>
        public NamedValueMatchResult(string name, object value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Segment Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Segment Match Value
        /// </summary>
        public object Value { get; set; }
    }
}