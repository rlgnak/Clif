using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Clif.Segments
{
    public class OptionSegment : Segment
    {
        public static Regex SegmentRegex = new Regex(@"\[(-|\\)\w\]");

        protected string MatchText { get; set; }

        public OptionSegment(string segment) : base(segment)
        {
            MatchText = segment;
        }

        public override CommandResult Match(string[] argument)
        {
            var result = MatchText.Equals(argument[0], StringComparison.OrdinalIgnoreCase);
            var commandResult = new CommandResult(result);

            if (result)
            {
                commandResult.CapturedOptions[MatchText] = true;
            }

            return commandResult;
        }


        public override int Type { get { return 2; } }
    }
}
