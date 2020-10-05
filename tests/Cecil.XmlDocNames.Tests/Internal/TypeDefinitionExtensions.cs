// -----------------------------------------------------------------------------------
// Copyright (C) Riccardo De Agostini and Tenacom. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
//
// Part of this file may be third-party code, distributed under a compatible license.
// See THIRD-PARTY-NOTICES file in the project root for third-party copyright notices.
// -----------------------------------------------------------------------------------

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