using System;
using System.CommandLine.Facilitator.Factories;

namespace System.CommandLine.Facilitator.Infra
{
    internal static class TypeExtensions
    {
        public static RootCommandFactory RootCommandFactory(this Type type)
            => new RootCommandFactory(type);
    }
}
