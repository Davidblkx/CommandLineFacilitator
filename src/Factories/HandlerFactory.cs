using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.CommandLine.Facilitator.Factories
{
    internal class HandlerFactory
    {
        private readonly Type _t;
        private readonly Dictionary<Type, MethodInfo> _targets;

        public bool IsValid => _targets.Count > 0;

        public Type Type => _t;

        public HandlerFactory(Type source)
        {
            _t = source;
            var methods = _t.GetMethods()
                .Where(e => e.GetCustomAttribute<HandlerAttribute>() is not null);

            _targets = new();
            foreach (var m in methods)
            {
                var type = m.GetCustomAttribute<HandlerAttribute>()?.Target ?? source;
                _targets[type] = m;
            }
        }

        /// <summary>
        /// Determines whether the specified target has handler.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>
        ///   <c>true</c> if the specified target contains handler; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsTarget(Type target)
            => _targets.ContainsKey(target);

        /// <summary>
        /// Create instance of handler
        /// </summary>
        /// <param name="activator">The activator.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">$"Type {_t.FullName} doesn't implement {nameof(HandlerAttribute)}</exception>
        public (object instance, MethodInfo method)? Build(Type target, ICustomActivator activator)
        {
            if (!ContainsTarget(target))
                throw new NotImplementedException($"Type {_t.FullName} doesn't implement handler for {target.FullName}");

            var method = _targets[target];
            var instance = activator.GetInstance(_t);
            if (instance is null || method is null) return null;

            return (instance, method);
        }
    }
}
