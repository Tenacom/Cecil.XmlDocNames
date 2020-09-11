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
using System.Text;
using JetBrains.Annotations;
using Mono.Cecil;

namespace Cecil.XmlDocNames
{
    public static partial class StringBuilderExtensions
    {
        /// <summary>
        /// Appends the name of a <see cref="MemberReference">MemberReference</see> to a <see cref="StringBuilder">StringBuilder</see>,
        /// in the same format used by XML documentation.
        /// </summary>
        /// <param name="this">The <see cref="StringBuilder">StringBuilder</see> instance on which this method is called.</param>
        /// <param name="member">The <see cref="MemberReference">MemberReference</see> whose name will be appended to <paramref name="this"/>.</param>
        /// <returns><paramref name="this"/>, with the XML ID string representation of <paramref name="member"/> appended.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="member"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">
        /// <para><paramref name="member"/> is not an instance of one of the following classes:</para>
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
        public static StringBuilder AppendXmlDocName(this StringBuilder @this, MemberReference member)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            return member switch {
                TypeReference type => @this.Append("T:").AppendDocNameCore(type),
                MethodReference method => @this.Append("M:").AppendDocNameCore(method),
                PropertyReference property => @this.Append("P:").AppendDocNameCore(property),
                FieldReference field => @this.Append("F:").AppendDocNameCore(field),
                EventReference @event => @this.Append("E:").AppendDocNameCore(@event),
                _ => throw new InvalidOperationException($"Trying to get the XML doc name for a member of unknown type: {member.GetType().Name} {member.FullName}"),
            };
        }

        private static StringBuilder StripGenericArity(this StringBuilder sb)
        {
            for (var i = sb.Length - 1; i >= 0; i--)
            {
                if (sb[i] == '`')
                {
                    sb.Length = i;
                    break;
                }
            }

            return sb;
        }

        private static StringBuilder AppendMemberName(this StringBuilder sb, MemberReference member)
        {
            var previousLength = sb.Length;
            sb = sb.Append(member.Name);
            for (var i = previousLength; i < sb.Length; i++)
            {
                var c = sb[i];

                // ReSharper disable once SwitchStatementMissingSomeCases
                sb[i] = c switch {
                    '.' => '#',
                    ',' => '@',
                    '<' => '{',
                    '>' => '}',
                    _ => c,
                };
            }

            return sb;
        }

        private static StringBuilder AppendArrayDimension(this StringBuilder sb, ArrayDimension dimension)
            => dimension.IsSized
                ? sb.Append(dimension.LowerBound).Append(':').Append(dimension.UpperBound)
                : sb;

        private static StringBuilder AppendMethodSignature(this StringBuilder sb, IMethodSignature signature)
        {
            if (signature.HasParameters)
            {
                sb = sb.Append('(')
                    .InvokeForEach(signature.Parameters.Select(p => p.ParameterType), ",", AppendDocNameCore)
                    .Append(')');
            }

            return sb;
        }

        [NotNull]
        private static StringBuilder AppendDocNameCore([NotNull] this StringBuilder sb, [NotNull] TypeReference type)
        {
            // Generic parameter (`1 / ``1)
            // Must be handled before nested types, because generic type parameters
            // have a DeclaringType although they are obviously not nested types.
            if (type is GenericParameter genericParameter)
            {
                return sb.Append(genericParameter.Type == GenericParameterType.Method ? "``" : "`").Append(genericParameter.Position);
            }

            // Nested type
            var declaringType = type.DeclaringType;
            if (declaringType != null)
            {
                sb = AppendDocNameCore(sb, declaringType).Append('.');
            }

            // ReSharper disable TailRecursiveCall
            return type switch {
                PointerType pointerType
                    => sb.AppendDocNameCore(pointerType.ElementType).Append('*'),
                ByReferenceType byReferenceType
                    => sb.AppendDocNameCore(byReferenceType.ElementType).Append('@'),
                PinnedType pinnedType
                    => sb.AppendDocNameCore(pinnedType.ElementType).Append('^'),
                RequiredModifierType requiredModifierType
                    => sb.AppendDocNameCore(requiredModifierType.ElementType)
                        .Append('|')
                        .AppendDocNameCore(requiredModifierType.ModifierType),
                OptionalModifierType optionalModifierType
                    => sb.AppendDocNameCore(optionalModifierType.ElementType)
                        .Append('!')
                        .AppendDocNameCore(optionalModifierType.ModifierType),
                ArrayType arrayType
                    => sb.AppendDocNameCore(arrayType.ElementType)
                        .Append('[')
                        .InvokeForEach(arrayType.Dimensions, ",", AppendArrayDimension)
                        .Append(']'),
                FunctionPointerType functionPointerType
                    => sb.Append("=FUNC:")
                        .AppendDocNameCore(functionPointerType.ReturnType)
                        .AppendMethodSignature(functionPointerType),
                GenericInstanceType genericInstanceType
                    => sb.AppendDocNameCore(genericInstanceType.ElementType)
                        .StripGenericArity()
                        .Append('{')
                        .InvokeForEach(genericInstanceType.GenericArguments, ",", AppendDocNameCore)
                        .Append('}'),
                _ => sb.Append(type.Namespace).Append('.').AppendMemberName(type),
            };

            // ReSharper restore TailRecursiveCall
        }

        [NotNull]
        private static StringBuilder AppendDocNameCore([NotNull] this StringBuilder sb, [NotNull] MethodReference method)
        {
            sb = sb.AppendDocNameCore(method.DeclaringType).Append('.').AppendMemberName(method);

            if (method.HasGenericParameters)
            {
                sb = sb.Append("``").Append(method.GenericParameters.Count);
            }

            sb = AppendMethodSignature(sb, method);

            // Special case for op_Implicit and op_Explicit
            // Quickly bail out if there is not exactly one parameter
            return method.Parameters.Count != 1
                ? sb
                : "op_Implicit".Equals(method.Name, StringComparison.Ordinal)
               || "op_Explicit".Equals(method.Name, StringComparison.Ordinal)
                    ? sb.Append('~').AppendDocNameCore(method.ReturnType)
                    : sb;
        }

        [NotNull]
        private static StringBuilder AppendDocNameCore([NotNull] this StringBuilder sb, [NotNull] PropertyReference property)
        {
            sb = sb.AppendDocNameCore(property.DeclaringType).Append('.').AppendMemberName(property);

            if (property.Parameters.Count > 0)
            {
                sb = sb.Append('(')
                    .InvokeForEach(property.Parameters.Select(p => p.ParameterType), ",", AppendDocNameCore)
                    .Append(')');
            }

            return sb;
        }

        [NotNull]
        private static StringBuilder AppendDocNameCore([NotNull] this StringBuilder sb, [NotNull] MemberReference field)
            => sb.AppendDocNameCore(field.DeclaringType).Append('.').AppendMemberName(field);
    }
}