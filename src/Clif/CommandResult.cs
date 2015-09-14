using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clif
{
    public class CommandResult
    {
        public IDictionary<string, object> CapturedArguments { get; private set; } = new Dictionary<string, object>();
        public IDictionary<string, object> CapturedOptions { get; private set; } = new Dictionary<string, object>();

        public bool Matches { get; private set; }

        public CommandResult(bool matches)
        {
            Matches = matches;
        }
    }
}
