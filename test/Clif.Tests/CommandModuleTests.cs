using System;
using System.Linq;
using Xunit;

namespace Clif.Tests
{
    public class CommandModuleTests
    {
        [Theory]
        [InlineData("test")]
        [InlineData("test [f|flag]")]
        [InlineData("test {variable}")]
        [InlineData("test {variable} [f|flag]")]
        public void Should_be_able_add_commands(string route)
        {
            var module = new CommandModuleTest();

            module.AddCommand(route, (o, o1) => { /*do nothing*/ });

            Assert.Equal(1, module.Commands.Count());
        }

        [Fact]
        public void Should_be_able_add_multiple_commands()
        {
            var module = new CommandModuleTest();

            module.AddCommand("test", (o, o1) => { /*do nothing*/ });
            module.AddCommand("test [f|flag]", (o, o1) => { /*do nothing*/ });
            module.AddCommand("test {variable}", (o, o1) => { /*do nothing*/ });
            module.AddCommand("test {variable} [f|flag]", (o, o1) => { /*do nothing*/ });

            Assert.Equal(4, module.Commands.Count());
        }

        [Fact]
        public void Should_be_able_add_commands_with_legacy_commmands_property()
        {
            var module = new CommandModuleTest();

            module.LegacyAddCommand("test", (o, o1) => { /*do nothing*/ });

            Assert.Equal(1, module.Commands.Count());
        }

        private class CommandModuleTest : CommandModule
        {
            public void AddCommand(string testRoute, Action<dynamic, dynamic> testAction)
            {
                this[testRoute] = testAction;
            }

            public void LegacyAddCommand(string testRoute, Action<dynamic, dynamic> testAction)
            {
                Command[testRoute] = testAction;
            }
        }
    }
}