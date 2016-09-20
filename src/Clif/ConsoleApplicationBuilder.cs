using Clif.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace Clif
{
    /// <summary>
    /// A builder for <see cref="ConsoleApplication"/>
    /// </summary>
    public class ConsoleApplicationBuilder
    {
        /// <summary>
        /// Specify the assembly containing the startup type to be used by the console application
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void UseStartup<T>()
        {
            //TODO implement
        }
        
        /// <summary>
        /// Builds a <see cref="ConsoleApplication"/> which runs the console application
        /// </summary>
        /// <returns></returns>
        public ConsoleApplication Build()
        {
            var services = new ServiceCollection();

            services.AddSingleton<CommandCatalog>();
            services.AddSingleton<ICommandRouteBuilder, DefaultCommandRouteBuilder>();
            services.AddSingleton<ICommandResolver, DefaultCommandResolver>();
            services.AddSingleton<ICommandModuleResolver, DefaultCommandModuleResolver>();

            //TODO call startup ConfigureServices if needed

            var serviceProvider = services.BuildServiceProvider();

            return ActivatorUtilities.CreateInstance<ConsoleApplication>(serviceProvider);
        }
    }
}