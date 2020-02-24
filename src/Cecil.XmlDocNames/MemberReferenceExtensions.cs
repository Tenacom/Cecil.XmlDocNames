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
        #region Public API

        /// <summary>
        /// Gets the name of a <see cref="MemberReference">MemberReference</see> in the same format used by XML documentation.
        /// </summary>
        /// <param name="this">The <see cref="MemberReference">MemberReference</see> instance on which this method is called.</param>
        /// <returns>A <see cref="string">string</see> containing the member's name  in the same format used by XML documentation.</returns>
        /// <exception cref="InvalidOperationException">
        /// <para><paramref name="this"/> is not an instance of one of the following classes:</para>
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
        /// </remarks>
        [PublicAPI, NotNull, CLSCompliant(false)]
        public static string GetXmlDocName([NotNull] this MemberReference @this)
            => new StringBuilder().AppendXmlDocName(@this).ToString();

        #endregion
    }
}