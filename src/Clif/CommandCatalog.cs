using System.Collections;
using System.Collections.Generic;

namespace Clif
{
    /// <summary>
    /// 
    /// </summary>
    public class CommandCatalog : IEnumerable<Command>
    {
        private List<Command> Catalog { get; } = new List<Command>();

        /// <summary>
        /// Gets enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Command> GetEnumerator()
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
