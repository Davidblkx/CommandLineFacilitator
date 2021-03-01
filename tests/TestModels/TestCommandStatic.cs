using System.CommandLine.Facilitator;
using System.Reflection;
using Xunit;

namespace CommandLineFacilitator.Tests.TestModels
{
    [Command("test-static", Aliases = new string[] { "a1", "a2" })]
    class TestCommandStatic
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
        public static int Handler(int argument1, bool option1)
        {
            Assert.Equal(ARG_1_EXPECTED, argument1);
            Assert.True(option1);
            return RESULT;
        }

        public static ArgumentAttribute GetArgumentAttribute()
        {
            var prop = typeof(TestCommandStatic).GetProperty(nameof(Argument1));
            return prop?.GetCustomAttribute<ArgumentAttribute>() ??
                throw new System.ArgumentNullException();
        }

        public static OptionAttribute GetOptionAttribute()
        {
            var prop = typeof(TestCommandStatic).GetProperty(nameof(Option1));
            return prop?.GetCustomAttribute<OptionAttribute>() ??
                throw new System.ArgumentNullException();
        }

        public const string ARG_1_DESC = "Arg1 description";
        public const int ARG_1_EXPECTED = 123;

        public const string OP_1_ALIAS = "-o";
        public const string OP_1_ARG_NAME = "option";
        public const string OP_1_DESC = "option_desc";

        public const int RESULT = 147;
    }
}
