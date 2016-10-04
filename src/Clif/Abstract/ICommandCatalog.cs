using System.Collections.Generic;

namespace Clif.Abstract
{
    /// <summary>
    /// A basic class that represents a collection of <see cref="ICommand"/>
    /// </summary>
    public interface ICommandCatalog : IEnumerable<ICommand>
    {
        /// <summary>
        /// Adds a module to the <see cref="ICommandCatalog"/>
        /// </summary>
        /// <param name="module"></param>
        void AddModule(CommandModule module);
    }
}