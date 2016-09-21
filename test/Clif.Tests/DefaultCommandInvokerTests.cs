using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Clif.Abstract;
using Clif.MatchResults;
using Xunit;

namespace Clif.Tests
{
    public class DefaultCommandInvokerTests
    {
        [Fact]
        public void Should_invoke_a_command_with_no_parameters_or_flags()
        {
            var commandResult = new CommandResult
            {
                MatchResults = new List<IMatchResult>
                {
                    new MatchResult()
                },
                OptionalMatchResults = new List<IMatchResult>()
            };

            dynamic parameterResults = null;
            dynamic flagResults = null;
            var command = new Command("command", (parameters, flags) => {
                parameterResults = parameters;
                flagResults = flags;
            });

            var commandInvoker = new DefaultCommandInvoker();

            commandInvoker.Invoke(commandResult, command);

            Assert.NotNull(parameterResults);
            Assert.Empty((ExpandoObject)parameterResults);
            Assert.NotNull(flagResults);
            Assert.Empty((ExpandoObject)flagResults);
        }

        [Fact]
        public void Should_invoke_a_command_with_parameters()
        {
            var commandResult = new CommandResult
            {
                MatchResults = new List<IMatchResult>
                {
                    new MatchResult(),
                    new NamedValueMatchResult("test", "value")
                },
                OptionalMatchResults = new List<IMatchResult>()
            };

            dynamic parameterResults = null;
            dynamic flagResults = null;
            var command = new Command("command {test}", (parameters, flags) => {
                parameterResults = parameters;
                flagResults = flags;
            });

            var commandInvoker = new DefaultCommandInvoker();

            commandInvoker.Invoke(commandResult, command);

            Assert.NotNull(parameterResults);
            Assert.Equal(1, ((ExpandoObject)parameterResults).Count());
            Assert.Equal("value", parameterResults.test);
            Assert.NotNull(flagResults);
            Assert.Empty((ExpandoObject)flagResults);
        }

        [Fact]
        public void Should_invoke_a_command_with_flags()
        {
            var commandResult = new CommandResult
            {
                MatchResults = new List<IMatchResult>
                {
                    new MatchResult()
                },
                OptionalMatchResults = new List<IMatchResult>
                {
                    new NamedValueMatchResult("option", "value")
                }
            };

            dynamic parameterResults = null;
            dynamic flagResults = null;
            var command = new Command("command [-o|option]", (parameters, flags) => {
                parameterResults = parameters;
                flagResults = flags;
            });

            var commandInvoker = new DefaultCommandInvoker();

            commandInvoker.Invoke(commandResult, command);


            Assert.NotNull(parameterResults);
            Assert.Empty((ExpandoObject)parameterResults);
            Assert.NotNull(flagResults);
            Assert.Equal(1, ((ExpandoObject)flagResults).Count());
            Assert.Equal("value", flagResults.option);
        }

        [Fact]
        public void Should_invoke_a_command_with_parameters_and_flags()
        {
            var commandResult = new CommandResult
            {
                MatchResults = new List<IMatchResult>
                {
                    new MatchResult(),
                    new NamedValueMatchResult("test", "value")
                },
                OptionalMatchResults = new List<IMatchResult>
                {
                    new NamedValueMatchResult("option", "value")
                }
            };

            dynamic parameterResults = null;
            dynamic flagResults = null;
            var command = new Command("command [-o|option]", (parameters, flags) => {
                parameterResults = parameters;
                flagResults = flags;
            });

            var commandInvoker = new DefaultCommandInvoker();

            commandInvoker.Invoke(commandResult, command);

            Assert.NotNull(parameterResults);
            Assert.Equal(1, ((ExpandoObject)parameterResults).Count());
            Assert.Equal("value", parameterResults.test);
            Assert.NotNull(flagResults);
            Assert.Equal(1, ((ExpandoObject)flagResults).Count());
            Assert.Equal("value", flagResults.option);
        }

        [Fact]
        public void Should_invoke_a_command_with_multiple_parameters_and_multiple_flags()
        {
            var commandResult = new CommandResult
            {
                MatchResults = new List<IMatchResult>
                {
                    new MatchResult(),
                    new NamedValueMatchResult("a", "1"),
                    new NamedValueMatchResult("b", "2"),
                    new NamedValueMatchResult("c", "3")
                },
                OptionalMatchResults = new List<IMatchResult>
                {
                    new NamedValueMatchResult("x", "7"),
                    new NamedValueMatchResult("y", "8"),
                    new NamedValueMatchResult("z", "9")
                }
            };

            dynamic parameterResults = null;
            dynamic flagResults = null;
            var command = new Command("command {a} {b} {c} [-x|x] [-y|y] [-z|z]", (parameters, flags) => {
                parameterResults = parameters;
                flagResults = flags;
            });

            var commandInvoker = new DefaultCommandInvoker();

            commandInvoker.Invoke(commandResult, command);

            Assert.NotNull(parameterResults);
            Assert.Equal(3, ((ExpandoObject)parameterResults).Count());
            Assert.Equal("1", parameterResults.a);
            Assert.Equal("2", parameterResults.b);
            Assert.Equal("3", parameterResults.c);
            Assert.NotNull(flagResults);
            Assert.Equal(3, ((ExpandoObject)flagResults).Count());
            Assert.Equal("7", flagResults.x);
            Assert.Equal("8", flagResults.y);
            Assert.Equal("9", flagResults.z);
        }

    }
}
