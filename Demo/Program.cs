using System;
using System.CommandLine.Facilitator;
using System.CommandLine;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            var root = CommandLineFacilitator
                .Create(new CustomActivator())
                .AddCurrentAssembly()
                .BuildRootCommand();

            while (true)
            {
                Console.WriteLine("try '--help' or 'exit':");
                var c = Console.ReadLine();
                if (c == "exit" || c is null) break;
                root.Invoke(c);

                Console.WriteLine("\nPress to clear");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
