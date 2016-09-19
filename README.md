CLIF (Command Line Interface Framework)
=====================

Clif is a framework for creating command line applications

[![Build Status](https://travis-ci.org/rlgnak/Clif.svg?branch=master)](https://travis-ci.org/rlgnak/Clif)
[![Build status](https://ci.appveyor.com/api/projects/status/hu0l65dl0cacsoa0/branch/master?svg=true)](https://ci.appveyor.com/project/rlgnak/clif/branch/master)

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