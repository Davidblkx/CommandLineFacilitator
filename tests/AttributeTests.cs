using CommandLineFacilitator.Tests.TestModels;
using System.CommandLine.Facilitator.Factories;
using Xunit;

namespace CommandLineFacilitator.Tests
{
    public class AttributeTests
    {
        [Fact]
        public void TestCommandAttribute()
        {
            var factory = new CommandFactory(typeof(TestCommandStatic));
            Assert.True(factory.IsValid);

            var att = factory.Attribute;
            Assert.NotNull(att);
            Assert.Equal("test-static", att?.Name);
            Assert.Contains(att?.Aliases, s => s == "a1");
            Assert.Contains(att?.Aliases, s => s == "a2");
        }

        [Fact]
        public void TestArgumentAttribute()
        {
            var att = TestCommandStatic.GetArgumentAttribute();
            Assert.NotNull(att);
            Assert.Equal(0, att.Arity[0]);
            Assert.Equal(1, att.Arity[1]);
            Assert.Equal(TestCommandStatic.ARG_1_DESC, att.Description);
        }

        [Fact]
        public void TestOptionAttribute()
        {
            var att = TestCommandStatic.GetOptionAttribute();
            Assert.NotNull(att);
            Assert.Contains(att.Aliases, s => s ==TestCommandStatic.OP_1_ALIAS);
            Assert.Equal(TestCommandStatic.OP_1_ARG_NAME, att.ArgumentName);
            Assert.Equal(TestCommandStatic.OP_1_DESC, att.Description);
        }
    }
}
