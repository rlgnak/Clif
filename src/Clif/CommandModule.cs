using System;
using System.Collections.Generic;

namespace Clif
{
    /// <summary>
    ///     Basic class for defining routes and actions
    /// </summary>
    public abstract class CommandModule
    {
        private readonly List<Command> commands = new List<Command>();

        /// <summary>
        ///     Returns all routes for this module.
        /// </summary>
        public IEnumerable<Command> Commands => commands.AsReadOnly();

        /// <summary>
        ///     Declares a route. This is provided for backwards compatibility.
        /// </summary>
        public CommandModule Command => this;

        /// <summary>
        ///     Delcares a route with paramaters and flags
        /// </summary>
        public Action<dynamic, dynamic> this[string i]
        {
            set { commands.Add(new Command(i, value)); }
        }
    }
}