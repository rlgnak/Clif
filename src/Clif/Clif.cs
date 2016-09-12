using System;

namespace Clif
{
    ///<summary>
    /// Legacy support. 
    ///</summary>
    public class Clif
    {
        
        ///<summary>
        /// Legacy support. 
        ///</summary>
        public Clif(Func<Type, object> activatorFunction = null)
        {
            //TODO implement old activator function

            ConsoleApplication = new ConsoleApplicationBuilder()
                .Build();
        }

        public ConsoleApplication ConsoleApplication { get; set; }

        public void Resolve(string[] args)
        {
            ConsoleApplication.Resolve(args);
        }
    }
}