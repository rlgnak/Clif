using System.Collections.Generic;

namespace Clif.Abstract
{
    /// <summary>
    ///     Stores information about the declared route
    /// </summary>
    public interface ICommandRoute
    {
        /// <summary>
        ///     Adds a segment to this <see cref="CommandRoute" />
        /// </summary>
        /// <param name="segment"></param>
        void AddSegment(ISegment segment);

        /// <summary>
        ///     Adds an optional segment to this <see cref="CommandRoute" />
        /// </summary>
        /// <param name="segment"></param>
        void AddOptionalSegment(ISegment segment);

        /// <summary>
        ///     Checks if this <see cref="CommandRoute" /> matches the
        ///     provided <paramref name="command" />.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>
        ///     <see cref="CommandResult" /> if it matches otherwise it returns null.
        /// </returns>
        CommandResult Match(IEnumerable<string> command);

        /// <summary>
        ///     The template register for this command
        /// </summary>
        string CommandTemplate { get; set; }
    }
}