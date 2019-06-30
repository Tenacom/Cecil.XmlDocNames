using System;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Mono.Cecil;

namespace Cecil.XmlDocNames
{
    public static partial class StringBuilderExtensions
    {
        #region Public API

        /// <summary>
        /// Appends the name of a <see cref="MemberReference">MemberReference</see> to a <see cref="StringBuilder">StringBuilder</see>,
        /// in the same format used by XML documentation.
        /// </summary>
        /// <param name="this">The <see cref="StringBuilder">StringBuilder</see> instance on which this method is called.</param>
        /// <param name="member">The <see cref="MemberReference">MemberReference</see> whose name will be appended to <paramref name="this"/>.</param>
        /// <returns><paramref name="this"/></returns>
        /// <exception cref="InvalidOperationException">
        /// <para><paramref name="member"/> is not an instance of one of the following classes:</para>
        /// <list type="bullet">
        ///   <item>
        ///     <term><see cref="TypeReference"/></term>
        ///     <description>A type reference or definition.</description>
        ///   </item>
        ///   <item>
        ///     <term><see cref="MethodReference"/></term>
        ///     <description>A method reference or definition.</description>
        ///   </item>
        ///   <item>
        ///     <term><see cref="PropertyReference"/></term>
        ///     <description>A property reference or definition.</description>
        ///   </item>
        ///   <item>
        ///     <term><see cref="FieldReference"/></term>
        ///     <description>A field reference or definition.</description>
        ///   </item>
        ///   <item>
        ///     <term><see cref="EventReference"/></term>
        ///     <description>An event reference or definition.</description>
        ///   </item>
        /// </list>
        /// </exception>
        /// <remarks>
        /// <para><paramref name="this"/> MUST NOT be <c>null</c>.</para>
        /// <para><paramref name="member"/> MUST NOT be <c>null</c>.</para>
        /// </remarks>
        [PublicAPI, NotNull]
        public static StringBuilder AppendXmlDocName([NotNull] this StringBuilder @this, [NotNull] MemberReference member)
        {
            switch (member)
            {
                case TypeReference type:
                    return @this.Append("T:").AppendDocNameCore(type);
                case MethodReference method:
                    return @this.Append("M:").AppendDocNameCore(method);
                case PropertyReference property:
                    return @this.Append("P:").AppendDocNameCore(property);
                case FieldReference field:
                    return @this.Append("F:").AppendDocNameCore(field);
                case EventReference @event:
                    return @this.Append("E:").AppendDocNameCore(@event);
                default:
                    throw new InvalidOperationException($"Trying to get the XML doc name for a member of unknown type: {member.GetType().Name} {member.FullName}");
            }
        }

        #endregion

        #region Private API

        [NotNull]
        static StringBuilder StripGenericArity([NotNull] this StringBuilder sb)
        {
            for (var i = sb.Length - 1; i >= 0; i--)
            {
                if (sb[i] != '`')
                    continue;

                sb.Length = i;
                break;
            }

            return sb;
        }

        [NotNull]
        static StringBuilder AppendMemberName([NotNull] this StringBuilder sb, [NotNull] MemberReference member)
        {
            var previousLength = sb.Length;
            sb.Append(member.Name);
            for (var i = previousLength; i < sb.Length; i++)
            {
                var c = sb[i];
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (c)
                {
                    case '.':
                        sb[i] = '#';
                        break;
                    case ',':
                        sb[i] = '@';
                        break;
                    case '<':
                        sb[i] = '{';
                        break;
                    case '>':
                        sb[i] = '}';
                        break;
                }
            }

            return sb;
        }

        [NotNull]
        static StringBuilder AppendArrayDimension([NotNull] this StringBuilder sb, ArrayDimension dimension)
            => dimension.IsSized
                ? sb.Append(dimension.LowerBound).Append(':').Append(dimension.UpperBound)
                : sb;

        [NotNull]
        static StringBuilder AppendMethodSignature([NotNull] this StringBuilder sb, [NotNull] IMethodSignature signature)
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
        static StringBuilder AppendDocNameCore([NotNull] this StringBuilder sb, [NotNull] TypeReference type)
        {
            // Generic parameter (`1 / ``1)
            // Must be handled before nested types, because generic type parameters
            // have a DeclaringType although they are obviously not nested types.
            if (type is GenericParameter genericParameter)
                return sb.Append(genericParameter.Type == GenericParameterType.Method ? "``" : "`").Append(genericParameter.Position);

            // Nested type
            var declaringType = type.DeclaringType;
            if (declaringType != null)
                sb = AppendDocNameCore(sb, declaringType).Append('.');

            // ReSharper disable TailRecursiveCall
            switch (type)
            {
                case PointerType pointerType:
                    // Pointer type
                    return sb.AppendDocNameCore(pointerType.ElementType).Append('*');
                case ByReferenceType byReferenceType:
                    // ByRef type
                    return sb.AppendDocNameCore(byReferenceType.ElementType).Append('@');
                case PinnedType pinnedType:
                    // Pinned type
                    return sb.AppendDocNameCore(pinnedType.ElementType).Append('^');
                case RequiredModifierType requiredModifierType:
                    // Required modifier
                    return sb.AppendDocNameCore(requiredModifierType.ElementType)
                        .Append('|')
                        .AppendDocNameCore(requiredModifierType.ModifierType);
                case OptionalModifierType optionalModifierType:
                    // Optional modifier
                    return sb.AppendDocNameCore(optionalModifierType.ElementType)
                        .Append('!')
                        .AppendDocNameCore(optionalModifierType.ModifierType);
                case ArrayType arrayType:
                    // Array
                    return sb.AppendDocNameCore(arrayType.ElementType)
                        .Append('[')
                        .InvokeForEach(arrayType.Dimensions, ",", AppendArrayDimension)
                        .Append(']');
                case FunctionPointerType functionPointerType:
                    // Function pointer
                    return sb.Append("=FUNC:")
                        .AppendDocNameCore(functionPointerType.ReturnType)
                        .AppendMethodSignature(functionPointerType);
                case GenericInstanceType genericInstanceType:
                    // Generic type instance
                    return sb.AppendDocNameCore(genericInstanceType.ElementType)
                        .StripGenericArity()
                        .Append('{')
                        .InvokeForEach(genericInstanceType.GenericArguments, ",", AppendDocNameCore)
                        .Append('}');
                default:
                    // For other types, including generic type definitions, Cecil already does the right thing.
                    return sb.Append(type.Namespace).Append('.').AppendMemberName(type);
            }
            // ReSharper restore TailRecursiveCall
        }

        [NotNull]
        static StringBuilder AppendDocNameCore([NotNull] this StringBuilder sb, [NotNull] MethodReference method)
        {
            sb = sb.AppendDocNameCore(method.DeclaringType).Append('.').AppendMemberName(method);

            if (method.HasGenericParameters)
                sb = sb.Append("``").Append(method.GenericParameters.Count);

            sb = AppendMethodSignature(sb, method);

            // Special case for op_Implicit and op_Explicit
            // Quickly bail out if there is not exactly one parameter
            if (method.Parameters.Count != 1)
                return sb;

            if ("op_Implicit".Equals(method.Name, StringComparison.Ordinal) || "op_Explicit".Equals(method.Name, StringComparison.Ordinal))
                sb.Append('~').AppendDocNameCore(method.ReturnType);

            return sb;
        }

        [NotNull]
        static StringBuilder AppendDocNameCore([NotNull] this StringBuilder sb, [NotNull] PropertyReference property)
        {
            sb = sb.AppendDocNameCore(property.DeclaringType).Append('.').AppendMemberName(property);

            if (property.Parameters.Count > 0)
                sb = sb.Append('(')
                    .InvokeForEach(property.Parameters.Select(p => p.ParameterType), ",", AppendDocNameCore)
                    .Append(')');

            return sb;
        }

        [NotNull]
        static StringBuilder AppendDocNameCore([NotNull] this StringBuilder sb, [NotNull] MemberReference field)
            => sb.AppendDocNameCore(field.DeclaringType).Append('.').AppendMemberName(field);

        #endregion
    }
}