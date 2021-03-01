using Demo.Services;
using SimpleInjector;
using System;
using System.CommandLine.Facilitator;

namespace Demo
{
    class CustomActivator : ICustomActivator
    {
        private readonly Container container;

        public CustomActivator()
        {
            container = new Container();
            container.Register<ICalculations, Calculations>(Lifestyle.Singleton);
        }

        public object? GetInstance(Type type)
            => container.GetInstance(type);

        public void Register(Type type)
            => container.RegisterSingleton(type, type);
    }
}
