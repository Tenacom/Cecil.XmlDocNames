// -----------------------------------------------------------------------------------
// Copyright (C) Riccardo De Agostini and Tenacom. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
//
// Part of this file may be third-party code, distributed under a compatible license.
// See THIRD-PARTY-NOTICES file in the project root for third-party copyright notices.
// -----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace Cecil.XmlDocNames
{
    /// <content>
    /// XML documentation for this class is in StringBuilderExtensions.cs.
    /// </content>
    public static partial class StringBuilderExtensions
    {
        private static StringBuilder InvokeForEach<T>(this StringBuilder sb, IEnumerable<T> items, string separator, Func<StringBuilder, T, StringBuilder> func)
        {
            if (sb is null)
            {
                throw new ArgumentNullException(nameof(sb));
            }

            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
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