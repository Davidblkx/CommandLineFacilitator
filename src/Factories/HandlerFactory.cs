using System.Linq;
using System.Reflection;

namespace System.CommandLine.Facilitator.Factories
{
    internal class HandlerFactory
    {
        private readonly Type _t;
        private readonly MethodInfo? _att;

        public bool IsValid => _att is not null;

        public Type Type => _t;

        public Type? Target => _att?
            .GetCustomAttribute<HandlerAttribute>()?
            .Target;

        public HandlerFactory(Type source)
        {
            _t = source;
            _att = _t.GetMethods()
                .FirstOrDefault(e => e.GetCustomAttribute<HandlerAttribute>() is not null);
        }

        /// <summary>
        /// Create instance of handler
        /// </summary>
        /// <param name="activator">The activator.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">$"Type {_t.FullName} doesn't implement {nameof(HandlerAttribute)}</exception>
        public (object instance, MethodInfo method)? Build(ICustomActivator activator)
        {
            if (_att is null)
                throw new ArgumentNullException($"Type {_t.FullName} doesn't implement {nameof(HandlerAttribute)}");

            var instance = activator.GetInstance(_t);
            if (instance is null) return null;

            return (instance, _att);
        }
    }
}
