// -----------------------------------------------------------------------------------
// Copyright (C) Tenacom. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
//
// Part of this file may be third-party code, distributed under a compatible license.
// See THIRD-PARTY-NOTICES file in the project root for third-party copyright notices.
// -----------------------------------------------------------------------------------

using System;
using System.Text;
using JetBrains.Annotations;
using Mono.Cecil;

namespace Cecil.XmlDocNames
{
    /// <summary>
    /// Provides extension methods for <see cref="MemberReference">MemberReference</see>.
    /// </summary>
    public static class MemberReferenceExtensions
    {
        /// <summary>
        /// Gets the name of a <see cref="MemberReference">MemberReference</see> in the same format used by XML documentation.
        /// </summary>
        /// <param name="this">The <see cref="MemberReference">MemberReference</see> instance on which this method is called.</param>
        /// <returns>A <see cref="string">string</see> containing the member's name in the same format used by XML documentation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">
        /// <para><paramref name="this"/> is not an instance of one of the following classes:</para>
        /// <list type="bullet">
        ///   <item><term><c>Mono.Cecil.TypeReference</c></term></item>
        ///   <item><term><c>Mono.Cecil.MethodReference</c></term></item>
        ///   <item><term><c>Mono.Cecil.PropertyReference</c></term></item>
        ///   <item><term><c>Mono.Cecil.FieldReference</c></term></item>
        ///   <item><term><c>Mono.Cecil.EventReference</c></term></item>
        /// </list>
        /// </exception>
        [PublicAPI]
        [CLSCompliant(false)]
        public static string GetXmlDocName(this MemberReference @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            return new StringBuilder().AppendXmlDocName(@this).ToString();
        }
    }
}