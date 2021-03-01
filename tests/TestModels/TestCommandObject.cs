using System.CommandLine.Facilitator;
using Xunit;

using static CommandLineFacilitator.Tests.TestModels.TestCommandStatic;

namespace CommandLineFacilitator.Tests.TestModels
{
    [Command("test-object")]
    class TestCommandObjectInput
    {
        [Argument(Arity = new int[] { 0, 1 }, Description = ARG_1_DESC)]
        public int Argument1 { get; set; } = 10;

        [Option(
            Aliases = new string[] { OP_1_ALIAS },
            AllowMultipleArgumentsPerToken = false,
            ArgumentName = OP_1_ARG_NAME,
            Description = OP_1_DESC,
            IsRequired = false)]
        public bool Option1 { get; set; } = false;

        [Handler]
        public static int Handler(TestCommandObjectInput input)
        {
            Assert.Equal(ARG_1_EXPECTED, input.Argument1);
            Assert.True(input.Option1);
            return OBJECT_RESULT;
        }

        public const int OBJECT_RESULT = 368;
    }
}
