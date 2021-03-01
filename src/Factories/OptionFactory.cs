using System.CommandLine.Facilitator.Infra;
using System.Linq;
using System.Reflection;

namespace System.CommandLine.Facilitator.Factories
{
    internal class OptionFactory
    {
        private readonly PropertyInfo _prop;
        private readonly OptionAttribute? _att;

        /// <summary>
        /// Returns true if property has an argument attribute
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid => !(_att is null) && _prop.CanWrite;

        public OptionFactory(PropertyInfo info)
        {
            _prop = info;
            _att = _prop.GetCustomAttribute<OptionAttribute>(true);
        }

        public Option Build()
        {
            if (_att is null) throw new ArgumentNullException("Property doesn't implement OptionAttribute");
            if (!_prop.CanWrite) throw new Exception("Option properties need to be writable");

            var name = _prop.Name.ToOptionName();

            var opt = new Option(name)
            {
                Description = _att.Description,
                AllowMultipleArgumentsPerToken = _att.AllowMultipleArgumentsPerToken,
                IsRequired = _att.IsRequired,
                Name = name
            };

            // Set property argument
            if (_prop.PropertyType != typeof(bool))
                opt.Argument = new Argument(_att.ArgumentName ?? name) { 
                    ArgumentType = _prop.PropertyType };

            _att.Aliases.ToList().ForEach(a => opt.AddAlias(a));

            return opt;
        }
    }
}
