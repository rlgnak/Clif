using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Clif.Arguments
{
    public class LiteralArgument : Argument
    {
        public static Regex SegmentRegex = new Regex(@"\[(-|\\)\w\|(\w*)\]");

        public LiteralArgument(string segment) : base(segment)
        {

        }

        public override CommandResult Match(string[] argument)
        {
            return new CommandResult(Text.Equals(argument[0], StringComparison.OrdinalIgnoreCase));
        }
    }
}
