using System.Linq;

namespace System.CommandLine.Facilitator.Factories
{
    internal class RootCommandFactory
    {
        private readonly Type _t;
        private readonly RootCommandAttribute? _att;

        public Type Type => _t;

        public bool IsValid => _att is not null;

        public RootCommandFactory(Type t)
        {
            _t = t;
            _att = _t.GetCustomAttributes(
                typeof(RootCommandAttribute), true)
                .FirstOrDefault() as RootCommandAttribute;
        }

        public RootCommand Build()
        {
            // Check attribute exists
            if (_att is null) throw new ArgumentNullException("Object doesn't implement RootCommandAttribute");
            // Create root command
            var root = new RootCommand(_att.Description ?? "");
            // Search in properties for arguments and options
            foreach (var p in _t.GetProperties())
            {
                var arg = new ArgumentFactory(p);
                if (arg.IsValid)
                {
                    root.AddArgument(arg.Build());
                    continue;
                }

                var opt = new OptionFactory(p);
                if (opt.IsValid)
                    root.AddOption(opt.Build());
            }
            return root;
        }
    }
}
