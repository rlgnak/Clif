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

        private ConsoleApplication ConsoleApplication { get; }

        /// <summary>
        /// Executes command based on provided args
        /// </summary>
        /// <param name="args"></param>
        public void Resolve(string[] args)
        {
            ConsoleApplication.Resolve(args);
        }
    }
}