using System;
using System.Linq;
using Clif.Arguments;
using Clif.Segments;

namespace Clif
{
    public class Command
    {
        public string Route { get; set; }
        public Action<dynamic, dynamic> Action { get; set; }

        public Segment[] Arguments { get; set; }
        public Segment[] Options { get; set; }

        public Action<dynamic, dynamic> this[string command]
        {
            set
            {
                Action = value;
                ParseCommand(command);
            }
        }

        public CommandResult Match(string[] segments)
        {
            var remaindingSegments = segments;
            var finalCommandResult = new CommandResult(true);

            //at a min there has to be enough segments
            //some arguments have multiple segments
            var argumentLength = Arguments.Sum(x => x.MatchSegments);
            if (segments.Length < argumentLength)
            {
                return new CommandResult(false);
            }
            
            var index = 0;
            while (index < argumentLength)
            {
                var argumentMatcher = Arguments[index];
                var matchedSegments = segments.Skip(index).Take(argumentMatcher.MatchSegments).ToArray();

                var argumentResult = argumentMatcher.Match(matchedSegments);
                //at any point if it doesn't match break early
                if (!argumentResult.Matches)
                {
                    return new CommandResult(false);
                }

                //add the arguments
                foreach (var argument in argumentResult.CapturedArguments)
                {
                    finalCommandResult.CapturedArguments.Add(argument);
                }

                index = index + argumentMatcher.MatchSegments;
            }

            //
            while(index < segments.Length)
            {
                var matchFound = false;

                foreach(var optionMatcher in Options)
                {
                    if (index + optionMatcher.MatchSegments > segments.Length)
                    {
                        continue;
                    }

                    var matchedSegments = segments.Skip(index).Take(optionMatcher.MatchSegments).ToArray();
                    var optionResult = optionMatcher.Match(matchedSegments);

                    if (!optionResult.Matches)
                    {
                        continue;
                    }

                    //add the arguments
                    foreach (var option in optionResult.CapturedOptions)
                    {
                        finalCommandResult.CapturedOptions.Add(option);
                    }

                    index = index + optionMatcher.MatchSegments;
                    matchFound = true;
                    break;                  
                }

                if (!matchFound)
                {
                    return new CommandResult(false);
                }
            }

            foreach (var optionMatcher in Options)
            {
                if (!finalCommandResult.CapturedOptions.ContainsKey(optionMatcher.Name))
                {
                    finalCommandResult.CapturedOptions.Add(optionMatcher.Name, optionMatcher.Default);
                }                
            }

            return finalCommandResult;
        }

        private Segment ResolveArgument(string segment)
        {
            if (OptionSegment.SegmentRegex.Match(segment).Success)
            {
                return new OptionSegment(segment);
            }
            
            if (OptionValueSegment.SegmentRegex.Match(segment).Success)
            {
                return new OptionValueSegment(segment);
            }
            
            if (VariableArgument.SegmentRegex.Match(segment).Success)
            {
                return new VariableArgument(segment);
            }

            if (VariableArgument.SegmentRegex.Match(segment).Success)
            {
                return new VariableArgument(segment);
            }

            return new LiteralArgument(segment);
        }

        private void ParseCommand(string command)
        {
            var segments = command.Split(' ');
            var commands = segments.Select(segment => ResolveArgument(segment)).ToArray();

            Options = commands.Where(x => x.Type == 2).ToArray();
            Arguments = commands.Where(x => x.Type == 1).ToArray();
        }
    }
}
