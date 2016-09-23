using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clif.Abstract;
using Clif.Segments;
using Moq;
using Xunit;

namespace Clif.Tests
{
    public class DefaultCommandRouteBuilderTests
    {
        [Theory]
        [InlineData("test")]
        [InlineData("test123")]
        [InlineData("123")]
        public void Should_parse_constant_command(string segmentText)
        {
            var mockCommandRoute = new Mock<ICommandRoute>();
            mockCommandRoute.Setup(x => x.CommandTemplate).Returns(segmentText);

            var commandRouteBuilder = new DefaultCommandRouteBuilder();
            
            commandRouteBuilder.ParseRoute(mockCommandRoute.Object);

            mockCommandRoute.Verify(x => x.AddOptionalSegment(It.IsAny<ISegment>()), Times.Never);
            mockCommandRoute.Verify(x => x.AddSegment(It.IsAny<ISegment>()), Times.Once);
            mockCommandRoute.Verify(x => x.AddSegment(It.IsAny<ConstantSegment>()), Times.Once);
        }

        [Theory]
        [InlineData("{name}")]
        public void Should_parse_variable_command(string segmentText)
        {
            var mockCommandRoute = new Mock<ICommandRoute>();
            mockCommandRoute.Setup(x => x.CommandTemplate).Returns(segmentText);

            var commandRouteBuilder = new DefaultCommandRouteBuilder();

            commandRouteBuilder.ParseRoute(mockCommandRoute.Object);

            mockCommandRoute.Verify(x => x.AddOptionalSegment(It.IsAny<ISegment>()), Times.Never);
            mockCommandRoute.Verify(x => x.AddSegment(It.IsAny<ISegment>()), Times.Exactly(1));
            mockCommandRoute.Verify(x => x.AddSegment(It.IsAny<VariableSegement>()), Times.Once);
        }

        [Theory]
        [InlineData("test {name}")]
        [InlineData("test123 {name}")]
        [InlineData("123 {name}")]
        public void Should_parse_constant_and_variable_command(string segmentText)
        {
            var mockCommandRoute = new Mock<ICommandRoute>();
            mockCommandRoute.Setup(x => x.CommandTemplate).Returns(segmentText);

            var commandRouteBuilder = new DefaultCommandRouteBuilder();

            commandRouteBuilder.ParseRoute(mockCommandRoute.Object);

            mockCommandRoute.Verify(x => x.AddOptionalSegment(It.IsAny<ISegment>()), Times.Never);
            mockCommandRoute.Verify(x => x.AddSegment(It.IsAny<ISegment>()), Times.Exactly(2));
            mockCommandRoute.Verify(x => x.AddSegment(It.IsAny<ConstantSegment>()), Times.Once);
            mockCommandRoute.Verify(x => x.AddSegment(It.IsAny<VariableSegement>()), Times.Once);
        }

        [Theory]
        [InlineData("test [-f|flag]")]
        [InlineData("test123 [-f|flag]")]
        [InlineData("123 [-f|flag]")]
        public void Should_parse_constant_and_flag_command(string segmentText)
        {
            var mockCommandRoute = new Mock<ICommandRoute>();
            mockCommandRoute.Setup(x => x.CommandTemplate).Returns(segmentText);

            var commandRouteBuilder = new DefaultCommandRouteBuilder();

            commandRouteBuilder.ParseRoute(mockCommandRoute.Object);

            mockCommandRoute.Verify(x => x.AddOptionalSegment(It.IsAny<ISegment>()), Times.Exactly(2));
            mockCommandRoute.Verify(x => x.AddOptionalSegment(It.IsAny<FlagSegment>()), Times.Once);
            mockCommandRoute.Verify(x => x.AddOptionalSegment(It.IsAny<FlagValueSegment>()), Times.Once);
            mockCommandRoute.Verify(x => x.AddSegment(It.IsAny<ISegment>()), Times.Once);
            mockCommandRoute.Verify(x => x.AddSegment(It.IsAny<ConstantSegment>()), Times.Once);
        }

        [Theory]
        [InlineData("test {name} [-f|flag]")]
        [InlineData("test123 {name} [-f|flag]")]
        [InlineData("123 {name} [-f|flag]")]
        public void Should_parse_constant_variable_and_flag_command(string segmentText)
        {
            var mockCommandRoute = new Mock<ICommandRoute>();
            mockCommandRoute.Setup(x => x.CommandTemplate).Returns(segmentText);

            var commandRouteBuilder = new DefaultCommandRouteBuilder();

            commandRouteBuilder.ParseRoute(mockCommandRoute.Object);

            mockCommandRoute.Verify(x => x.AddOptionalSegment(It.IsAny<ISegment>()), Times.Exactly(2));
            mockCommandRoute.Verify(x => x.AddOptionalSegment(It.IsAny<FlagSegment>()), Times.Once);
            mockCommandRoute.Verify(x => x.AddOptionalSegment(It.IsAny<FlagValueSegment>()), Times.Once);
            mockCommandRoute.Verify(x => x.AddSegment(It.IsAny<ISegment>()), Times.Exactly(2));
            mockCommandRoute.Verify(x => x.AddSegment(It.IsAny<ConstantSegment>()), Times.Once);
            mockCommandRoute.Verify(x => x.AddSegment(It.IsAny<VariableSegement>()), Times.Once);
        }
        
        [Theory]
        [InlineData("test {a}", 1, 1, 0)]
        [InlineData("test {a} {b}", 1, 2, 0)]
        [InlineData("test {a} {b} {c}", 1, 3, 0)]
        [InlineData("test {a} {b} {c} {d}", 1, 4, 0)]
        [InlineData("test {a} {b} {c} {d} {e}", 1, 5, 0)]
        [InlineData("test {a} {b} {c} {d} {e} {f}", 1, 6, 0)]
        [InlineData("test [-x|x]", 1, 0, 1)]
        [InlineData("test [-x|x] [-y|y]", 1, 0, 2)]
        [InlineData("test [-x|x] [-y|y] [-z|z]", 1, 0, 3)]
        [InlineData("test {a} [-x|x]", 1, 1, 1)]
        [InlineData("test {a} {b} [-x|x] [-y|y]", 1, 2, 2)]
        [InlineData("test {a} {b} {c} [-x|x] [-y|y] [-z|z]", 1, 3, 3)]
        [InlineData("test {a} [-x|x]", 1, 1, 1)]
        [InlineData("test bob {a} {b} [-x|x] [-y|y]", 2, 2, 2)]
        [InlineData("test bob jim {a} {b} {c} [-x|x] [-y|y] [-z|z]", 3, 3, 3)]
        public void Should_parse_multiple_variables_constants_and_flags(string segmentText, int constantCount, int variableCount, int flagCount)
        {
            var mockCommandRoute = new Mock<ICommandRoute>();
            mockCommandRoute.Setup(x => x.CommandTemplate).Returns(segmentText);

            var commandRouteBuilder = new DefaultCommandRouteBuilder();

            commandRouteBuilder.ParseRoute(mockCommandRoute.Object);

            mockCommandRoute.Verify(x => x.AddOptionalSegment(It.IsAny<ISegment>()), Times.Exactly(flagCount * 2));
            mockCommandRoute.Verify(x => x.AddOptionalSegment(It.IsAny<FlagSegment>()), Times.Exactly(flagCount));
            mockCommandRoute.Verify(x => x.AddOptionalSegment(It.IsAny<FlagValueSegment>()), Times.Exactly(flagCount));
            mockCommandRoute.Verify(x => x.AddSegment(It.IsAny<ISegment>()), Times.Exactly(variableCount + constantCount));
            mockCommandRoute.Verify(x => x.AddSegment(It.IsAny<ConstantSegment>()), Times.Exactly(constantCount));
            mockCommandRoute.Verify(x => x.AddSegment(It.IsAny<VariableSegement>()), Times.Exactly(variableCount));
        }

        [Theory]
        [InlineData("test [[garbage]]")]
        [InlineData("test [f]")]
        [InlineData("test {}")]
        [InlineData("test []")]
        [InlineData("test [-f]")]
        [InlineData("test [-f|]")]
        [InlineData("test [-f|flag")]
        [InlineData("test -f|flag]")]
        [InlineData("test {{}}")]
        [InlineData("test {test")]
        [InlineData("test test}")]
        [InlineData("test {test space}")]
        [InlineData("test [-f|flag space]")]
        [InlineData("test {test-dash}")]
        [InlineData("test [-f|flag-dash]")]
        public void Should_throw_an_exception_for_invalid_commands(string segmentText)
        {
            var mockCommandRoute = new Mock<ICommandRoute>();
            mockCommandRoute.Setup(x => x.CommandTemplate).Returns(segmentText);

            var commandRouteBuilder = new DefaultCommandRouteBuilder();

            Assert.Throws<Exception>(() => {
                commandRouteBuilder.ParseRoute(mockCommandRoute.Object);
            });
        }
    }
}
