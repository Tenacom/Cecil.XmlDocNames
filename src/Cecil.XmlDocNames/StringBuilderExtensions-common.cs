// -----------------------------------------------------------------------------------
// Copyright (C) Tenacom. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
//
// Part of this file may be third-party code, distributed under a compatible license.
// See THIRD-PARTY-NOTICES file in the project root for third-party copyright notices.
// -----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Cecil.XmlDocNames
{
    /// <content>
    /// XML documentation for this class is in StringBuilderExtensions.cs.
    /// </content>
    public static partial class StringBuilderExtensions
    {
        [NotNull]
        private static StringBuilder InvokeForEach<T>(this StringBuilder sb, IEnumerable<T> items, [NotNull] string separator, [NotNull] Func<StringBuilder, T, StringBuilder> func)
        {
            if (sb == null)
            {
                throw new ArgumentNullException(nameof(sb));
            }

            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var first = true;
            foreach (var item in items)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    _ = sb.Append(separator);
                }

                sb = func(sb, item);
            }

            return sb;
        }
    }
}