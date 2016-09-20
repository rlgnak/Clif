using System.Collections.Generic;

namespace Clif.Abstract
{
    /// <summary>
    /// A basic class used for resolving command modules
    /// </summary>
    public interface ICommandModuleResolver
    {
        /// <summary>
        /// Returns instance of all command modules resiger in the assembly
        /// </summary>
        /// <returns></returns>
        IEnumerable<CommandModule> GetCommandModules();
    }
}