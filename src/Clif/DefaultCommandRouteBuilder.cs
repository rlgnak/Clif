using System;
using System.Text.RegularExpressions;
using Clif.Abstract;
using Clif.Segments;

namespace Clif
{
    public class DefaultCommandRouteBuilder : ICommandRouteBuilder
    {
        public void ParseRoute(CommandRoute commandRoute)
        {
            foreach (var segmentText in commandRoute.CommandTemplate.Split(' '))
            {
                var segment = GetSegment(segmentText);

                if (segment != null)
                {
                    commandRoute.AddSegment(segment);
                    continue;
                }

                var optionalSegment = GetOptionalSegment(segmentText);

                if (optionalSegment != null)
                {
                    commandRoute.AddOptionalSegment(optionalSegment);
                    continue;
                }

                throw new Exception($"Invalid route detected {commandRoute.CommandTemplate}");
            }
        }

        private ISegment GetSegment(string segmentText)
        {
            if (new Regex(@"^\w+$").IsMatch(segmentText))
            {
                return new ConstantSegment(segmentText);
            }

            if (new Regex(@"^{\w+}$").IsMatch(segmentText))
            {
                return new VariableSegement(segmentText);
            }

            return null;
        }

        private ISegment GetOptionalSegment(string segmentText)
        {
            if (new Regex(@"^\[-\w+\|\w+\]$").IsMatch(segmentText))
            {
                return new FlagSegment(segmentText);
            }

            return null;
        }
    }
}
