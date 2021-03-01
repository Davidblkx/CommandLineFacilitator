namespace System.CommandLine.Facilitator
{
    /// <summary>
    /// Define that class has command handlers
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public class HandlerHostAttribute : Attribute {}
}
