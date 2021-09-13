// -----------------------------------------------------------------------------------
// Copyright (C) Riccardo De Agostini and Tenacom. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
//
// Part of this file may be third-party code, distributed under a compatible license.
// See THIRD-PARTY-NOTICES file in the project root for third-party copyright notices.
// -----------------------------------------------------------------------------------

using System.IO;
using System.Reflection;
using Mono.Cecil;

namespace Cecil.XmlDocNames.Tests.Internal
{
    internal static class TestUtility
    {
        public static AssemblyDefinition ReadExampleAssembly()
        {
            var location = Assembly.GetExecutingAssembly().Location;
            var assemblyPath = Path.Combine(Path.GetDirectoryName(location)!, "ExampleAssembly.dll");

            var readerParameters = new ReaderParameters
            {
                ReadingMode = ReadingMode.Deferred,
                ReadWrite = false,
            };

            return AssemblyDefinition.ReadAssembly(assemblyPath, readerParameters);
        }
    }
}
