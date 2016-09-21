using System.Collections.Generic;

namespace Clif.Abstract
{
    /// <summary>
    /// Defines the functionality for a command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// The route for this command.
        /// </summary>
        CommandRoute CommandRoute { get; set; }

        /// <summary>
        /// Invokes the command with the provided <paramref name="parameters"/> and <paramref name="flags"/>
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <param name="flags">Flags</param>
        void Invoke(dynamic parameters, dynamic flags);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        /// <returns></returns>
        CommandResult Match(IEnumerable<string> segments);
    }
}