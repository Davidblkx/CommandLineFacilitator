using System.Collections.Generic;

namespace System.CommandLine.Facilitator
{
    internal class DefaultActivator : ICustomActivator
    {
        private readonly Dictionary<Type, object> _instances = new();

        public object? GetInstance(Type type)
        {
            if (_instances.ContainsKey(type))
                return _instances[type];

            var i = Activator.CreateInstance(type);
            if (i is null) return null;
            _instances[type] = i;
            return i;
        }

        public void Register(Type type)
        {
            var hasEmptyConst = type.GetConstructor(Type.EmptyTypes) is not null;
            if (!hasEmptyConst) throw new NotImplementedException("Default activator can't handle types without empty constructors");
        }
    }
}
