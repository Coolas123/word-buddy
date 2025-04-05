using System.Reflection;

namespace Presentation
{
    public sealed class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
