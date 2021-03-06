using System.CommandLine;
using System.CommandLine.Facilitator.Factories;
using CommandLineFacilitator.Tests.TestModels;
using Xunit;

using static System.CommandLine.Facilitator.CommandLineFacilitator;

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

        [Fact]
        public void TestInherance()
        {
            var cmd = Create()
                .AddCommand(typeof(TestInherance1))
                .AddCommand(typeof(TestInherance2))
                .BuildRootCommand();

            var res1 = cmd.Invoke(TestInherance1.EXPTECTED_NAME);
            var res2 = cmd.Invoke(TestInherance2.EXPTECTED_NAME);

            Assert.Equal(TestInherance1.EXPTECTED_VALUE, res1);
            Assert.Equal(TestInherance2.EXPTECTED_VALUE, res2);
        }
    }
}
