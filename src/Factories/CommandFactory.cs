using System.CommandLine.Facilitator.Infra;
using System.CommandLine.Invocation;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CommandLineFacilitator.Tests")]
namespace System.CommandLine.Facilitator.Factories
{
    /// <summary>
    /// Builds a command from a type
    /// </summary>
    internal class CommandFactory
    {
        private readonly Type _t;
        private readonly CommandAttribute? _att;
        private readonly MethodInfo? _handlerInfo;

        public Type? Parent => _att?.Parent;
        public Type Type => _t;
        public CommandAttribute? Attribute => _att;

        public bool IsValid => _att is not null;

        public CommandFactory(Type type)
        { 
            _t = type;
            _att = _t.GetCustomAttributes(
                typeof(CommandAttribute), true)
                .FirstOrDefault() as CommandAttribute;
            _handlerInfo = _t.GetMethods().FirstOrDefault(
                e => e.GetCustomAttribute<HandlerAttribute>() is not null);
        }

        /// <summary>
        /// Builds the specified command.
        /// </summary>
        /// <param name="instance">The instance that contains the handler.</param>
        /// <param name="methodInfo">The methodInfo for the handler.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Object doesn't implement CommandAttribute</exception>
        public Command Build(object? instance = null, MethodInfo? methodInfo = null) 
        {
            if (_att is null) throw new ArgumentNullException("Object doesn't implement CommandAttribute");
            var method = methodInfo ?? _handlerInfo;

            // Load default name
            var defName = _t.Name.PascalToKebabCase();

            //Create command
            var cmd = new Command(_att.Name ?? defName, _att.Description)
            {
                TreatUnmatchedTokensAsErrors = _att.TreatUnmatchedTokensAsErrors
            };

            // Add Aliases
            foreach (var a in _att.Aliases)
                cmd.AddAlias(a);

            // Add arguments and options
            foreach (var p in _t.GetProperties())
            {
                var arg = new ArgumentFactory(p);
                if (arg.IsValid)
                {
                    cmd.AddArgument(arg.Build());
                    continue;
                }

                var opt = new OptionFactory(p);
                if (opt.IsValid)
                    cmd.AddOption(opt.Build());
            }

            if (instance is not null && method is not null)
                cmd.Handler = CommandHandler.Create(method, instance);

            return cmd;
        }

        public static bool TypeIsValidCommand(Type type)
            => new CommandFactory(type).IsValid;
    }
}
