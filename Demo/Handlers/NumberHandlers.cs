using System;
using System.CommandLine.Facilitator;
using Demo.Commands;
using Demo.Services;

namespace Demo.Handlers
{
    [HandlerHost]
    class NumberHandler
    {
        private readonly ICalculations _calc;

        public NumberHandler(ICalculations c) { _calc = c; }

        [Handler(Target = typeof(NumberCommands))]
        public void Print()
        {
            Console.WriteLine("Number is at:" + _calc.Get());
        }
    }

    [HandlerHost]
    class NumberAddHandler
    {
        private readonly ICalculations _calc;

        public NumberAddHandler(ICalculations c) { _calc = c; }

        [Handler(Target = typeof(AddNumberCommand))]
        public void Add(AddNumberCommand c)
        {
            foreach (var n in c.Number)
                _calc.Sum(n);
            Console.WriteLine("Number is at:" + _calc.Get());
        }
    }

    [HandlerHost]
    class NumberSubtractHandler
    {
        private readonly ICalculations _calc;

        public NumberSubtractHandler(ICalculations c) { _calc = c; }

        [Handler(Target = typeof(SubtractNumberCommand))]
        public void Add(SubtractNumberCommand c)
        {
            foreach (var n in c.Number)
                _calc.Subtract(n);
            Console.WriteLine("Number is at:" + _calc.Get());
        }
    }
}
