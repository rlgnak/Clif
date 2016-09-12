using System.Collections.Generic;

namespace Clif.Abstract
{
    public interface ICommandModuleResolver
    {
        IEnumerable<CommandModule> GetCommandModules();
    }
}