using System;
using System.Linq;
using Mono.Cecil;

namespace Cecil.XmlDocNames.Tests.Internal
{
    internal static class AssemblyDefinitionExtensions
    {
        public static TypeDefinition GetTypeThatStartsWith(this AssemblyDefinition @this, string prefix)
            => @this.Modules.SelectMany(m => m.GetTypes())
                .First(t => t.Name.StartsWith(prefix, StringComparison.Ordinal));
    }
}