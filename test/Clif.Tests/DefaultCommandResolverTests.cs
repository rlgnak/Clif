using System;
using System.Collections.Generic;
using Clif.Abstract;
using Moq;
using Xunit;

namespace Clif.Tests
{
    public class DefaultCommandResolverTests
    {
        [Fact]
        public void Should_resolve_command()
        {
            var mockCommand = new Mock<ICommand>();
            var mockCommandCatalog = new Mock<ICommandCatalog>();
            var mockCommandInvoker = new Mock<ICommandInvoker>();

            mockCommandCatalog.Setup(x => x.GetEnumerator()).Returns(() => new List<ICommand> {
                mockCommand.Object
            }.GetEnumerator());

            mockCommand.Setup(x => x.Match(It.IsAny<IEnumerable<string>>()))
                .Returns(() => new CommandResult());

            var defaultCommandResolver = new DefaultCommandResolver(mockCommandCatalog.Object, mockCommandInvoker.Object);

            defaultCommandResolver.Resolve(new List<string> { "fake" });

            mockCommand.Verify(x => x.Match(It.IsAny<IEnumerable<string>>()));
        }

        [Fact]
        public void Should_throw_exception_when_no_commands_are_matched()
        {
            var mockCommand = new Mock<ICommand>();
            var mockCommandCatalog = new Mock<ICommandCatalog>();
            var mockCommandInvoker = new Mock<ICommandInvoker>();

            mockCommandCatalog.Setup(x => x.GetEnumerator()).Returns(() => new List<ICommand> {
                mockCommand.Object
            }.GetEnumerator());

            mockCommand.Setup(x => x.Match(It.IsAny<IEnumerable<string>>()))
                .Returns(() => null);

            var defaultCommandResolver = new DefaultCommandResolver(mockCommandCatalog.Object, mockCommandInvoker.Object);

            Assert.Throws<Exception>(() =>
            {
                defaultCommandResolver.Resolve(new List<string> {"fake"});
            });
        }
    }
}
