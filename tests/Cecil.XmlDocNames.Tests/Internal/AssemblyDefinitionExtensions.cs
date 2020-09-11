// -----------------------------------------------------------------------------------
// Copyright (C) Tenacom. All rights reserved.
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
    internal static class AssemblyDefinitionExtensions
    {
        public static TypeDefinition GetTypeThatStartsWith(this AssemblyDefinition @this, string prefix)
            => @this.Modules.SelectMany(m => m.GetTypes())
                .First(t => t.Name.StartsWith(prefix, StringComparison.Ordinal));
    }
}