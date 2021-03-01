namespace System.CommandLine.Facilitator
{
    /// <summary>
    /// Defines method that handles current command
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class HandlerAttribute : Attribute 
    {
        /// <summary>
        /// Gets or sets the target to handle.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        public Type? Target { get; set; }
    }
}
