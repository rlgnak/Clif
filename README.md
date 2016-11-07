CLIF (Command Line Interface Framework)
=====================

Clif is a framework for creating command line applications

[![Nuget](https://img.shields.io/nuget/v/Clif.svg)](https://www.nuget.org/packages/Clif)

[![Build Status](https://travis-ci.org/rlgnak/Clif.svg?branch=master)](https://travis-ci.org/rlgnak/Clif)
[![Build status](https://ci.appveyor.com/api/projects/status/hu0l65dl0cacsoa0/branch/master?svg=true)](https://ci.appveyor.com/project/rlgnak/clif/branch/master)
[![Coverage Status](https://coveralls.io/repos/github/rlgnak/Clif/badge.svg?branch=master)](https://coveralls.io/github/rlgnak/Clif?branch=master)

```csharp
public class Module : CommandModule
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
