using System.CommandLine.Facilitator;

namespace CommandLineFacilitator.Tests.TestModels
{
    internal abstract class TestBaseCommand<T> where T : new()
    {
        protected abstract int Value { get; }

        [Handler]
        public int HandleCommand(T _)
        {
            return Value;
        }
    }
}
