using System.CommandLine.Facilitator;

namespace CommandLineFacilitator.Tests.TestModels
{
    [Command("inh1")]
    internal class TestInherance1 : TestBaseCommand<TestInherance1>
    {
        public const int EXPTECTED_VALUE = 9087;
        public const string EXPTECTED_NAME = "inh1";

        protected override int Value => EXPTECTED_VALUE;
    }

    [Command("inh2")]
    internal class TestInherance2 : TestBaseCommand<TestInherance2>
    {
        public const int EXPTECTED_VALUE = 5623;
        public const string EXPTECTED_NAME = "inh2";

        protected override int Value => EXPTECTED_VALUE;
    }
}
