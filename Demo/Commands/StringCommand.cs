using System;
using System.CommandLine.Facilitator;

namespace Demo.Commands
{
    [Command(
        Name = "string",
        Aliases = new string[]{ "s" },
        Description = "Tools to format strings")]
    class StringCommand 
    {
        [Handler]
        public static void Handle()
        {
            Console.WriteLine("Try call \"string join\" or \"string split\" ");
        }
    }

    [Command(
        Name = "split",
        Description = "split a string",
        Parent = typeof(StringCommand))]
    class SplitStringCommand
    {
        [Option(
            Aliases = new string[] { "-s" },
            AllowMultipleArgumentsPerToken = false,
            ArgumentName = "Separator",
            Description = "Token used to split the string, defaults to '-'",
            IsRequired = false)]
        public string Separator { get; set; } = "-";

        [Argument(
            Arity = new int[] { 1, 1 },
            Description = "Text to split")]
        public string Text { get; set; } = "";
    }

    [Command(
        Name = "join",
        Description = "join a string",
        Parent = typeof(StringCommand))]
    class JoinStringCommand
    {
        [Option(
            Aliases = new string[] { "-s" },
            AllowMultipleArgumentsPerToken = false,
            ArgumentName = "Separator",
            Description = "Token to separate string, defaults to '|'",
            IsRequired = false)]
        public string Separator { get; set; } = "|";

        [Argument(
            Arity = new int[] { 1, 100_000 },
            Description = "string parts to join")]
        public string[] Parts { get; set; } = Array.Empty<string>();
    }
}
