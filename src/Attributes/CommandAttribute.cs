namespace System.CommandLine.Facilitator
{
    /// <summary>
    /// Creates a new command
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CommandAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the command name.
        /// If not set class name is used
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the aliases.
        /// </summary>
        /// <value>
        /// The aliases.
        /// </value>
        public string[] Aliases { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Gets or sets the help description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the parent command.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public Type? Parent { get; set; }

        /// <summary>
        /// Gets a value indicating whether unmatched tokens contribute errors to <see cref="ParseResult.Errors"/>>.
        /// </summary>
        public bool TreatUnmatchedTokensAsErrors { get; set; }

        public CommandAttribute() { }
        public CommandAttribute(string name) { Name = name; }
    }
}
