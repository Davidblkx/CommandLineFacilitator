using System.CommandLine.Facilitator;
using Demo.Commands;
using System;

namespace Demo.Handlers
{
    [HandlerHost]
    class StringHandler
    {
        [Handler(Target = typeof(SplitStringCommand))]
        public static int SplitString(SplitStringCommand c)
        {
            foreach (var s in c.Text.Split(c.Separator))
            { Console.WriteLine(s); }

            return 0;
        }

        [Handler(Target = typeof(JoinStringCommand))]
        public static int JoinString(JoinStringCommand c)
        {
            Console.WriteLine(string.Join(c.Separator, c.Parts));
            return 0;
        }
    }
}
