using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Clif.Arguments
{
    public class VariableArgument : Argument
    {
        public static Regex SegmentRegex = new Regex(@"\{(\w*)\}");

        public VariableArgument(string segment) : base(segment)
        {
            Name = SegmentRegex.Split(segment)[1];
        }

        public override CommandResult Match(string[] argument)
        {
            var commandResult = new CommandResult(true);

            commandResult.CapturedArguments[Name] = argument[0];

            return commandResult;
        }
    }
}
