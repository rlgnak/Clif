using System.Collections.Generic;

namespace Clif.Abstract
{
    public interface ICommandResolver
    {
        void Resolve(IEnumerable<string> command);
    }
}