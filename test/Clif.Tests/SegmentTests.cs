using Clif.MatchResults;
using Clif.Segments;
using Xunit;

namespace Clif.Tests
{
    public class SegmentTests
    {
        [Theory]
        [InlineData("test", "test")]
        [InlineData("test123", "test123")]
        [InlineData("123", "123")]
        [InlineData("Test", "Test")]
        [InlineData("Test", "test")]
        public void Should_match_constant_segment(string segmentText, string commandText)
        {
            var segment = new ConstantSegment(segmentText);
            Assert.NotNull(segment.Match(commandText));
        }

        [Theory]
        [InlineData("test", "pizza")]
        [InlineData("123", "pizza")]
        [InlineData("Test", "pizza")]
        [InlineData("Test", "-f")]
        [InlineData("Test", "-f pizza")]
        public void Should_not_match_constant_segment(string segmentText, string commandText)
        {
            var segment = new ConstantSegment(segmentText);
            Assert.Null(segment.Match(commandText));
        }

        [Theory]
        [InlineData("[-f|flag]", "-f")]
        [InlineData("[-f|flag]", "-f test")]
        [InlineData("[-t|flag]", "-t test")]
        [InlineData("[-t|flag]", "-t 123")]
        [InlineData("[-t|flag]", "-t test123")]
        [InlineData("[-t|flag]", "-t 123test")]
        public void should_match_flag_segment(string segmentText, string commandText)
        {
            var segment = new FlagSegment(segmentText);
            Assert.NotNull(segment.Match(commandText));
        }

        [Theory]
        [InlineData("[-f|flag]", "-t")]
        [InlineData("[-t|flag]", "-f")]
        [InlineData("[-f|flag]", "-t test")]
        [InlineData("[-t|flag]", "-f test")]
        public void Should_not_match_flag_segment(string segmentText, string commandText)
        {
            var segment = new FlagSegment(segmentText);
            Assert.Null(segment.Match(commandText));
        }

        [Theory]
        [InlineData("{bob}", "squiggle")]
        [InlineData("{bob}", "123")]
        [InlineData("{bob}", "123squiggle")]
        [InlineData("{bob}", "squiggle123")]
        public void should_match_variable_segment(string segmentText, string commandText)
        {
            var segment = new VariableSegement(segmentText);
            Assert.NotNull(segment.Match(commandText));
        }

        [Theory]
        [InlineData("{bob}", "-s")]
        [InlineData("{bob}", "-s squiggle")]
        public void Should_not_match_variable_segment(string segmentText, string commandText)
        {
            var segment = new VariableSegement(segmentText);
            Assert.Null(segment.Match(commandText));
        }

        [Fact]
        public void FlagSegment_without_value_should_return_true()
        {
            var segment = new FlagSegment("[-f|flag]");
            var match = segment.Match("-f");
            Assert.IsType<NamedValueMatchResult>(match);
            Assert.Equal(true, (bool) ((NamedValueMatchResult) match).Value);
        }

        [Fact]
        public void FlagSegment_with_value_should_return_value()
        {
            var segment = new FlagSegment("[-f|flag]");
            var match = segment.Match("-f value");
            Assert.IsType<NamedValueMatchResult>(match);
            Assert.Equal("value", (string)((NamedValueMatchResult)match).Value);
        }

        [Fact]
        public void VariableSegment_should_return_value()
        {
            var segment = new VariableSegement("{test}");
            var match = segment.Match("value");
            Assert.IsType<NamedValueMatchResult>(match);
            Assert.Equal("value", (string)((NamedValueMatchResult)match).Value);
        }

        [Fact]
        public void FlagSegment_should_be_named()
        {
            var segment = new FlagSegment("[-f|flag]");
            var match = segment.Match("-f value");
            Assert.IsType<NamedValueMatchResult>(match);
            Assert.Equal("flag", ((NamedValueMatchResult)match).Name);
        }

        [Fact]
        public void VariableSegment_should_be_named()
        {
            var segment = new VariableSegement("{test}");
            var match = segment.Match("value");
            Assert.IsType<NamedValueMatchResult>(match);
            Assert.Equal("test", ((NamedValueMatchResult)match).Name);
        }
    }
}
