# Clif

Clif is a framework for creating command line applications

```csharp
public class Module : CommandMoudle
{
	public Module()
	{
		Command["greet {name}"] = (parameters, flags) =>
        {
            Console.WriteLine($"Hello {parameters.name}");
        };
	}	
}
```

Compile and run!