using System.CommandLine.Facilitator.Infra;
using System.Reflection;

namespace System.CommandLine.Facilitator.Factories
{
    internal class ArgumentFactory
    {
        private readonly PropertyInfo _prop;
        private readonly ArgumentAttribute? _att;

        /// <summary>
        /// Returns true if property has an argument attribute
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid => !(_att is null) && _prop.CanWrite;

        public ArgumentFactory(PropertyInfo info)
        {
            _prop = info;
            _att = _prop.GetCustomAttribute<ArgumentAttribute>(true);
        }

        public Argument Build()
        {
            if (_att is null) throw new ArgumentNullException("Property doesn't implement ArgumentAttribute");
            if (!_prop.CanWrite) throw new Exception("Argument properties need to be writable");
            if (_att.Arity.Length < 2) throw new ArgumentOutOfRangeException("Arity must have 2 values");

            var name = _prop.Name.PascalToKebabCase();
            var min = _att.Arity[0];
            var max = _att.Arity[1];

            return new Argument(name)
            {
                Description = _att.Description,
                Arity = new ArgumentArity(min, max),
                ArgumentType = _prop.PropertyType,
            };
        }
    }
}
