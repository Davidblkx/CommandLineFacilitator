using System.Threading.Tasks;

namespace System.CommandLine.Facilitator
{
    /// <summary>
    /// Used to create instances from a type
    /// </summary>
    public interface ICustomActivator
    {
        /// <summary>
        /// Registers the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        void Register(Type type);

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        object? GetInstance(Type type);
    }
}
