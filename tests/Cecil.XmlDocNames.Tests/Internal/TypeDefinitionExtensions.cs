using System;
using System.Linq;
using Mono.Cecil;

namespace Cecil.XmlDocNames.Tests.Internal
{
    internal static class TypeDefinitionExtensions
    {
        public static MethodDefinition GetMethodThatStartsWith(this TypeDefinition @this, string prefix)
            => @this.Methods.First(t => t.Name.StartsWith(prefix, StringComparison.Ordinal));

        public static FieldDefinition GetFieldThatStartsWith(this TypeDefinition @this, string prefix)
            => @this.Fields.First(t => t.Name.StartsWith(prefix, StringComparison.Ordinal));

        public static EventDefinition GetEventThatStartsWith(this TypeDefinition @this, string prefix)
            => @this.Events.First(t => t.Name.StartsWith(prefix, StringComparison.Ordinal));

        public static PropertyDefinition GetPropertyThatStartsWith(this TypeDefinition @this, string prefix)
            => @this.Properties.First(t => t.Name.StartsWith(prefix, StringComparison.Ordinal));
    }
}