using System.Collections.Generic;
using System.CommandLine.Builder;
using System.CommandLine.Facilitator.Factories;
using System.CommandLine.Facilitator.Infra;
using System.Linq;
using System.Reflection;

namespace System.CommandLine.Facilitator
{
    public sealed class CommandLineFacilitator
    {
        private readonly List<CommandFactory> _cmd = new();
        private readonly List<HandlerFactory> _handler = new();
        private readonly List<Action<Type, Command>> _cmdMiddleware = new();
        private readonly List<Action<RootCommand>> _rootMiddleware = new();
        private RootCommandFactory? _rootFactory;
        private Func<RootCommand?> _rootBuilder;

        /// <summary>
        /// Gets or sets the type activator.
        /// </summary>
        /// <value>
        /// The type activator.
        /// </value>
        public ICustomActivator TypeActivator { get; set; }

        public CommandLineFacilitator()
        {
            TypeActivator = new DefaultActivator();
            _rootBuilder = () => _rootFactory?.Build();
        }

        public CommandLineFacilitator(ICustomActivator activator)
        {
            TypeActivator = activator;
            _rootBuilder = () => _rootFactory?.Build();
        }

        /// <summary>
        /// Adds the entry assembly or executing assembly.
        /// </summary>
        /// <returns></returns>
        public CommandLineFacilitator AddCurrentAssembly()
            => AddFromAssembly(Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly());

        /// <summary>
        /// Adds commands from assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public CommandLineFacilitator AddFromAssembly(Assembly assembly)
        {
            var res = assembly.LoadCommandTypes();
            if (res.RootCommand?.IsValid ?? false)
                _rootFactory = res.RootCommand;

            foreach (var c in res.Commands)
                if (c.IsValid)
                {
                    _cmd.Add(c);
                    TypeActivator.Register(c.Type);
                }

            foreach (var h in res.Handlers)
                if (h.IsValid)
                {
                    _handler.Add(h);
                    TypeActivator.Register(h.Type);
                }

            return this;
        }

        /// <summary>
        /// Adds the root command.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        /// <exception cref="RootCommandFactory">type</exception>
        public CommandLineFacilitator AddRootCommand(Type type)
        {
            var root = new RootCommandFactory(type);
            if (!root.IsValid) throw new Exception($"Type {type.FullName} isn't a valid root command");
            _rootFactory = root;
            return this;
        }

        /// <summary>
        /// Adds the root command generator.
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns></returns>
        public CommandLineFacilitator AddRootCommand(Func<RootCommand> func)
        {
            _rootBuilder = func;
            return this;
        }

        /// <summary>
        /// Adds the command from type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        /// <exception cref="CommandFactory">type</exception>
        public CommandLineFacilitator AddCommand(Type type)
        {
            var cmd = new CommandFactory(type);
            if (!cmd.IsValid)
                throw new Exception($"Type {type.FullName} isn't a valid command");
            _cmd.Add(cmd);
            return this;
        }

        /// <summary>
        /// Adds a action where you could apply changes to the generated commands.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public CommandLineFacilitator AddCommandMod(Action<Type, Command> action)
        {
            _cmdMiddleware.Add(action);
            return this;
        }

        /// <summary>
        /// Adds a action where you could apply changes to the generated RootCommand.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public CommandLineFacilitator AddRootCommandMod(Action<RootCommand> action)
        {
            _rootMiddleware.Add(action);
            return this;
        }

        /// <summary>
        /// Builds the root command and add all commands and handlers.
        /// </summary>
        /// <returns></returns>
        public RootCommand BuildRootCommand()
        {
            var cache = new Dictionary<Type, Command>();
            var root = _rootBuilder() ?? new RootCommand();

            foreach (var factory in _cmd)
                if (BuildCommand(factory, cache) is Command c && factory.Parent is null)
                    root.AddCommand(c);

            _rootMiddleware.ForEach(m => m(root));

            return root;
        }

        /// <summary>
        /// Gets the command line builder.
        /// </summary>
        /// <returns></returns>
        public CommandLineBuilder GetCommandLineBuilder()
            => new CommandLineBuilder(BuildRootCommand());

        private Command BuildCommand(CommandFactory factory, Dictionary<Type, Command> cache)
        {
            if (cache.ContainsKey(factory.Type))
                return cache[factory.Type];

            // Load handler
            var handler = _handler.FirstOrDefault(
                e => e.Target == factory.Type)?
                .Build(TypeActivator);
            // Get instance, if handler is null try to load it self
            var instance = handler?.instance ?? TypeActivator.GetInstance(factory.Type);

            // Build command
            var cmd = factory.Build(instance, handler?.method);

            // Call all middle-ware
            _cmdMiddleware.ForEach(action => action(factory.Type, cmd));

            // If has parent, add it
            if (factory.Parent is Type t)
            {
                var parent = BuildCommand(_cmd.First(e => e.Type == t), cache);
                parent.AddCommand(cmd);
            }

            // Update cache
            cache[factory.Type] = cmd;
            return cmd;
        }

        public static CommandLineFacilitator Create()
            => new CommandLineFacilitator();
        public static CommandLineFacilitator Create(ICustomActivator activator)
            => new CommandLineFacilitator(activator);
    }
}
