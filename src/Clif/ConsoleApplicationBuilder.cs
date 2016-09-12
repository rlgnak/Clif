using Clif.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace Clif
{
    public class ConsoleApplicationBuilder
    {
        public void UseStartup<T>()
        {
            //TODO implement
        }
        
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