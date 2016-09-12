CLIF (Command Line Interface Framework)
=====================

Clif is a framework for creating command line applications

[![Build Status](https://travis-ci.org/rlgnak/Clif.svg?branch=master)](https://travis-ci.org/rlgnak/Clif)

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