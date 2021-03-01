using System;
using System.CommandLine.Facilitator;

namespace Demo.Commands
{
    [Command(
        Name = "number",
        Aliases = new string[] { "n" },
        Description = "print current value")]
    class NumberCommands { }

    [Command(
        Name = "add",
        Description = "add to current value",
        Parent = typeof(NumberCommands))]
    class AddNumberCommand
    {
        [Argument(Description = "Numbers to add, by default 1")]
        public int[] Number { get; set; } = Array.Empty<int>();
    }

    [Command(
        Name = "sub",
        Description = "subtract to current value",
        Parent = typeof(NumberCommands))]
    class SubtractNumberCommand
    {
        [Argument(Description = "Numbers to subtract, by default 1")]
        public int[] Number { get; set; } = Array.Empty<int>();
    }
}
