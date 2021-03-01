using System.Collections.Generic;
using System.CommandLine.Facilitator.Factories;
using System.Linq;
using System.Reflection;

namespace System.CommandLine.Facilitator.Infra
{
    /// <summary>
    /// Helper to load types from assembly
    /// </summary>
    internal static class AssemblyExtensions
    {
        /// <summary>
        /// Loads the command types.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public static TypeCollection LoadCommandTypes(this Assembly assembly)
        {
            var root = assembly.LoadRootCommand();
            var cmd = assembly.LoadCommandFactories();
            var handlers = assembly.LoadHandlerFactories();
            return new TypeCollection(root, cmd, handlers);
        }

        /// <summary>
        /// Loads the root command.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>null if no type implements <see cref="RootCommandAttribute"/> </returns>
        public static RootCommandFactory? LoadRootCommand(this Assembly assembly)
            => assembly
                .LoadTypesForAttribute<RootCommandAttribute>()
                .FirstOrDefault()?.RootCommandFactory();

        /// <summary>
        /// Loads the command factories.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public static IEnumerable<CommandFactory> LoadCommandFactories(this Assembly assembly)
            => assembly
                .LoadTypesForAttribute<CommandAttribute>()
                .Select(e => new CommandFactory(e));

        /// <summary>
        /// Loads the handler factories.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public static IEnumerable<HandlerFactory> LoadHandlerFactories(this Assembly assembly)
            => assembly
                .LoadTypesForAttribute<HandlerHostAttribute>()
                .Select(e => new HandlerFactory(e));

        public static IEnumerable<Type> LoadTypesForAttribute<T>(this Assembly assembly)
            where T : Attribute
            => assembly.GetTypes()
                .Where(e => e.GetCustomAttribute<T>() is not null);
    }

    /// <summary>
    /// Collection of types extracted from assembly
    /// </summary>
    internal record TypeCollection(RootCommandFactory? RootCommand, IEnumerable<CommandFactory> Commands, IEnumerable<HandlerFactory> Handlers);
}
