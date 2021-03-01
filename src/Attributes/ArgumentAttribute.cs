namespace System.CommandLine.Facilitator
{
    /// <summary>
    /// Adds an argument to current command
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ArgumentAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the number of values allowed, by default 0 or more.
        /// </summary>
        /// <value>
        /// The arity.
        /// </value>
        public int[] Arity { get; set; } = { 0, 100_00 };

        /// <summary>
        /// Gets or sets the help description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string? Description { get; set; }
    }
}
