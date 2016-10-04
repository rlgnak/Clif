using System;
using Clif.Abstract;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Clif
{
    /// <summary>
    /// A builder for <see cref="ConsoleApplication"/>
    /// </summary>
    public class ConsoleApplicationBuilder
    {
        private IServiceProvider ServiceProvider { get; }

        private StartupMethods StartupMethods { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ConsoleApplicationBuilder()
        {
            ServiceProvider = new ServiceCollection().BuildServiceProvider();
        }

        /// <summary>
        /// Specify the assembly containing the startup type to be used by the console application
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public ConsoleApplicationBuilder UseStartup<T>()
        {
            //TODO setup environment names
            StartupMethods = StartupLoader.LoadMethods(ServiceProvider, typeof(T), "");
            return this;
        }


        /// <summary>
        /// Builds a <see cref="ConsoleApplication"/> which runs the console application
        /// </summary>
        /// <returns></returns>
        public ConsoleApplication Build()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IAssemblyCatalog, DefaultAssemblyCatalog>();
            services.AddSingleton<ICommandCatalog, CommandCatalog>();
            services.AddSingleton<ICommandRouteBuilder, DefaultCommandRouteBuilder>();
            services.AddSingleton<ICommandResolver, DefaultCommandResolver>();
            services.AddSingleton<ICommandModuleResolver, DefaultCommandModuleResolver>();
            services.AddSingleton<ICommandInvoker, DefaultCommandInvoker>();

            StartupMethods?.ConfigureServicesDelegate(services);

            var serviceProvider = services.BuildServiceProvider();

            return ActivatorUtilities.CreateInstance<ConsoleApplication>(serviceProvider);
        }
    }
}