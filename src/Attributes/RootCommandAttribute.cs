namespace System.CommandLine.Facilitator
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RootCommandAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the help description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string? Description { get; set; }

        /// <summary>
        /// Gets a value indicating whether unmatched tokens contribute errors to <see cref="ParseResult.Errors"/>>.
        /// </summary>
        public bool TreatUnmatchedTokensAsErrors { get; set; }
    }
}
