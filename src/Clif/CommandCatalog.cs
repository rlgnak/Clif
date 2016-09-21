using System.Collections;
using System.Collections.Generic;
using Clif.Abstract;

namespace Clif
{
    /// <summary>
    /// A basic class that represents a collection of <see cref="ICommand"/>
    /// </summary>
    public class CommandCatalog : ICommandCatalog
    {
        private List<ICommand> Catalog { get; } = new List<ICommand>();

        /// <summary>
        /// Gets enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<ICommand> GetEnumerator()
        {
            return Catalog.GetEnumerator();
        }

        /// <summary>
        /// Gets enumerator.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Catalog.GetEnumerator();
        }

        /// <summary>
        /// Adds a module to the <see cref="Catalog"/>
        /// </summary>
        /// <param name="module"></param>
        public void AddModule(CommandModule module)
        {
            Catalog.AddRange(module.Commands);
        }
    }
}
