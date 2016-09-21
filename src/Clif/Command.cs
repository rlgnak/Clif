using System;
using System.Collections.Generic;
using Clif.Abstract;

namespace Clif
{
    /// <summary>
    /// Defines the functionality for a command.
    /// </summary>
    public class Command : ICommand
    {
        /// <summary>
        /// Initialize a new instance of <see cref="Command"/> type.
        /// </summary>
        /// <param name="commandTemplate"></param>
        /// <param name="action"></param>
        public Command(string commandTemplate, Action<dynamic, dynamic> action)
        {
            CommandRoute = new CommandRoute(commandTemplate);
            Action = action;
        }

        /// <summary>
        /// The action that is excuted if the command is called.
        /// </summary>
        private Action<dynamic, dynamic> Action { get; }

        /// <summary>
        /// The route for this command.
        /// </summary>
        public CommandRoute CommandRoute { get; set; }

        /// <summary>
        /// Invokes the command with the provided <paramref name="parameters"/> and <paramref name="flags"/>
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <param name="flags">Flags</param>
        public void Invoke(dynamic parameters, dynamic flags)
        {
            Action.Invoke(parameters, flags);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        /// <returns></returns>
        public CommandResult Match(IEnumerable<string> segments)
        {
            return CommandRoute.Match(segments);
        }
    }
}