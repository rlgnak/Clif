using Clif.Abstract;

namespace Clif.MatchResults
{
    public class NamedValueMatchResult : IMatchResult
    {
        public NamedValueMatchResult(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }
}