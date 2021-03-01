namespace System.CommandLine.Facilitator
{
    /// <summary>
    /// Adds an option to current Command
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class OptionAttribute : Attribute
    {
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
        /// Gets or sets a value indicating whether [allow multiple arguments per token].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow multiple arguments per token]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowMultipleArgumentsPerToken { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is required; otherwise, <c>false</c>.
        /// </value>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets the name of the argument to use when is not a bool option.
        /// </summary>
        /// <value>
        /// The name of the argument.
        /// </value>
        public string? ArgumentName { get; set; }
    }
}
