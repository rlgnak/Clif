using System;
using System.Collections.Generic;
using System.Linq;

namespace Clif
{
    public class CommandModule
    {
        public List<Command> Commands { get; set; } = new List<Command>();

        public Command Command {
            get {
                var command = new Command();
                Commands.Add(command);
                return command;
            }
        }
    }
}
