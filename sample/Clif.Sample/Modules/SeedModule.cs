using Clif;
using System;

namespace Example.Modules
{
    public class SeedModule : CommandModule
    {
        public SeedModule()
        {
            Command["seed [-f|force]"] = (parameters, flags) =>
            {
                Console.WriteLine($"A {flags.force}");
            };

            Command["generate"] = (parameters, flags) =>
            {
                Console.WriteLine("B");
            };

            Command["generate list"] = (parameters, flags) =>
            {
                Console.WriteLine("C");
            };

            Command["generate {builder}"] = (parameters, flags) =>
            {
                Console.WriteLine($"D {parameters.builder}");
            };

            Command["generate {builder} {index} [-c|count]"] = (parameters, flags) =>
            {
                Console.WriteLine($"E {flags.count}");
            };
        } 
    }
}
