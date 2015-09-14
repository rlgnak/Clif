using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Clif.Segments
{
    public class OptionValueSegment : Segment
    {
        public static Regex SegmentRegex = new Regex(@"\[((-|\\)\w)\|(\w*)\]");

        protected string MatchText { get; set; }

        public OptionValueSegment(string segment) : base(segment)
        {
            var matches = SegmentRegex.Split(segment);
            MatchText = matches[1];
            Text = matches[3];
            Name = matches[3];
        }
        
        public override int MatchSegments
        {
            get
            {
                return 2;
            }
        }

        public override int Type { get { return 2; } }

        public override CommandResult Match(string[] argument)
        {
            if(argument[0] != MatchText)
            {
                return new CommandResult(false);
            }

            var commandResult = new CommandResult(true);
            commandResult.CapturedOptions[Name] = argument[1];
            return commandResult;
        }
    }
}
