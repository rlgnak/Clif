using System;
using Clif.Abstract;
using Clif.MatchResults;
using Moq;
using Xunit;

namespace Clif.Tests
{
    public class CommandRouteTests
    {
        [Fact]
        public void Should_match_segment()
        {
            var command = "test".Split(' ');
            var commandRoute = new CommandRoute("test");

            var matchResult = new MatchResult();
            var mockSegment = new Mock<ISegment>();

            mockSegment.Setup(x => x.Match("test")).Returns(() => matchResult);

            commandRoute.AddSegment(mockSegment.Object);

            var result = commandRoute.Match(command);

            Assert.NotNull(result);
            Assert.Equal(1, result.MatchResults.Count);
            Assert.Equal(0, result.OptionalMatchResults.Count);
            Assert.True(result.MatchResults.Contains(matchResult));
        }

        [Fact]
        public void Should_match_multiple_segments()
        {
            var command = "test value".Split(' ');
            var commandRoute = new CommandRoute("test {value}");

            var matchResult = new MatchResult();
            var namedMatchResult = new NamedValueMatchResult("value", "value");
            var mockSegment = new Mock<ISegment>();
            var mockVariableSegment = new Mock<ISegment>();

            mockSegment.Setup(x => x.Match("test")).Returns(() => matchResult);
            mockVariableSegment.Setup(x => x.Match("value")).Returns(() => namedMatchResult);

            commandRoute.AddSegment(mockSegment.Object);
            commandRoute.AddSegment(mockVariableSegment.Object);

            var result = commandRoute.Match(command);

            Assert.NotNull(result);
            Assert.Equal(2, result.MatchResults.Count);
            Assert.Equal(0, result.OptionalMatchResults.Count);
            Assert.True(result.MatchResults.Contains(matchResult));
            Assert.True(result.MatchResults.Contains(namedMatchResult));
        }
        
        [Fact]
        public void Should_match_segment_and_flag()
        {
            var command = "test -f".Split(' ');
            var commandRoute = new CommandRoute("test [-f|flag]");

            var matchResult = new MatchResult();
            var namedMatchResult = new NamedValueMatchResult("flag", true);
            var mockSegment = new Mock<ISegment>();
            var mockOptionalSegment = new Mock<ISegment>();

            mockSegment.Setup(x => x.Match("test")).Returns(() => matchResult);
            mockOptionalSegment.Setup(x => x.Match("-f")).Returns(() => namedMatchResult);

            commandRoute.AddSegment(mockSegment.Object);
            commandRoute.AddOptionalSegment(mockOptionalSegment.Object);

            var result = commandRoute.Match(command);

            Assert.NotNull(result);
            Assert.Equal(1, result.MatchResults.Count);
            Assert.Equal(1, result.OptionalMatchResults.Count);
            Assert.True(result.MatchResults.Contains(matchResult));
            Assert.True(result.OptionalMatchResults.Contains(namedMatchResult));
        }

        [Fact]
        public void Should_match_segment_and_flag_with_value()
        {
            var command = "test -f test".Split(' ');
            var commandRoute = new CommandRoute("test [-f|flag]");

            var matchResult = new MatchResult();
            var namedMatchResult = new NamedValueMatchResult("flag", "test");
            var mockSegment = new Mock<ISegment>();
            var mockOptionalSegment = new Mock<ISegment>();

            mockSegment.Setup(x => x.Match("test")).Returns(() => matchResult);
            mockOptionalSegment.Setup(x => x.Match("-f test")).Returns(() => namedMatchResult);

            commandRoute.AddSegment(mockSegment.Object);
            commandRoute.AddOptionalSegment(mockOptionalSegment.Object);

            var result = commandRoute.Match(command);

            Assert.NotNull(result);
            Assert.Equal(1, result.MatchResults.Count);
            Assert.Equal(1, result.OptionalMatchResults.Count);
            Assert.True(result.MatchResults.Contains(matchResult));
            Assert.True(result.OptionalMatchResults.Contains(namedMatchResult));
        }
    }
}
