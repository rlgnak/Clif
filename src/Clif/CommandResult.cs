using System.Collections.Generic;
using Clif.Abstract;

namespace Clif
{
    /// <summary>
    /// Basic class used command result matches
    /// </summary>
    public class CommandResult
    {
        /// <summary>
        /// Macthed results for required segements
        /// </summary>
        public List<IMatchResult> MatchResults { get; set; }

        /// <summary>
        /// Matched results for optonal segements
        /// </summary>
        public List<IMatchResult> OptionalMatchResults { get; set; }
    }
}