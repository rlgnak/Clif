using System;

namespace Clif.Sample.Modules
{
    public class ExampleModule : CommandModule
    {
        public ExampleModule()
        {
            Command["example"] = (parameters, flags) =>
            {
                Console.WriteLine("This is an example of a basic command.");
            };

            Command["example {name}"] = (parameters, flags) =>
            {
                Console.WriteLine($"This is an example of a command that takes a parameter. The value of the paramater is {parameters.name}");
            };

            Command["example [-o|opiton]"] = (parameters, flags) =>
            {
                Console.WriteLine($"This is an example of how to pass flags to a command. The flag value is {flags.opiton}");
            };
            
            Command["example {name} [-o|opiton]"] = (parameters, flags) =>
            {
                Console.WriteLine($"This is an example of how to pass flags and parameters to a command. The flag value is {flags.opiton}. The value of the paramater is {parameters.name}.");
            };
        }
    }
}
