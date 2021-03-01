using CommandLineFacilitator.Tests.TestModels;
using System.CommandLine;
using System.CommandLine.Facilitator.Factories;
using Xunit;

namespace CommandLineFacilitator.Tests
{
    public class CommandTests
    {
        [Fact]
        public void TestExecuteStaticHandler()
        {
            var factory = new CommandFactory(typeof(TestCommandStatic));
            Assert.True(factory.IsValid);

            var cmd = factory.Build(new TestCommandStatic());
            
            var res1 = cmd.Invoke($"test-static --option1 {TestCommandStatic.ARG_1_EXPECTED}");
            var res2 = cmd.Invoke($"a1 {TestCommandStatic.OP_1_ALIAS} {TestCommandStatic.ARG_1_EXPECTED}");
            var res3 = cmd.Invoke($"a2 {TestCommandStatic.OP_1_ALIAS} {TestCommandStatic.ARG_1_EXPECTED}");

            Assert.Equal(TestCommandStatic.RESULT, res1);
            Assert.Equal(TestCommandStatic.RESULT, res2);
            Assert.Equal(TestCommandStatic.RESULT, res3);
        }

        [Fact]
        public void TestExecuteObject()
        {
            var factory = new CommandFactory(typeof(TestCommandObjectInput));
            Assert.True(factory.IsValid);

            var cmd = factory.Build(new TestCommandObjectInput());

            var res1 = cmd.Invoke($"test-object --option1 {TestCommandStatic.ARG_1_EXPECTED}");

            Assert.Equal(TestCommandObjectInput.OBJECT_RESULT, res1);
        }
    }
}
