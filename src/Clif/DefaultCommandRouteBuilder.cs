using System;
using System.Text.RegularExpressions;
using Clif.Abstract;
using Clif.Segments;

namespace Clif
{
    /// <summary>
    /// A basic class for parsing commands
    /// </summary>
    public class DefaultCommandRouteBuilder : ICommandRouteBuilder
    {
        /// <summary>
        /// Parses a <see cref="ICommandRoute"/> 
        /// </summary>
        /// <param name="commandRoute"></param>
        public void ParseRoute(ICommandRoute commandRoute)
        {
            foreach (var segmentText in commandRoute.CommandTemplate.Split(' '))
            {
                if (new Regex(@"^\w+$").IsMatch(segmentText))
                {
                    commandRoute.AddSegment(new ConstantSegment(segmentText));
                    continue;
                }

                if (new Regex(@"^{\w+}$").IsMatch(segmentText))
                {
                    commandRoute.AddSegment(new VariableSegement(segmentText));
                    continue;
                }

                if (new Regex(@"^\[-\w+\|\w+\]$").IsMatch(segmentText))
                {
                    commandRoute.AddOptionalSegment(new FlagValueSegment(segmentText));
                    commandRoute.AddOptionalSegment(new FlagSegment(segmentText));
                    continue;
                }

                throw new Exception($"Invalid route detected {commandRoute.CommandTemplate}");
            }
        }
    }
}
