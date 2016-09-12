using System.Collections.Generic;
using Clif.Abstract;

namespace Clif
{
    public class CommandResult
    {
        public List<IMatchResult> MatchResults { get; set; }

        public List<IMatchResult> OptionalMatchResults { get; set; }
    }
}