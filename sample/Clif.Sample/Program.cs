using Microsoft.Extensions.DependencyInjection;

namespace Clif.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new ConsoleApplicationBuilder()
                .UseStartup<Program>()
                .Build()
                .Resolve(args);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            
        }

        public void Configure()
        {

        }
    }
}
