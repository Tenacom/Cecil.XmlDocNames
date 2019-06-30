using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Cecil.XmlDocNames
{
    public static partial class StringBuilderExtensions
    {
        #region Public API

        [NotNull]
        static StringBuilder InvokeForEach<T>([NotNull] this StringBuilder sb, [NotNull] IEnumerable<T> items, [NotNull] string separator, [NotNull] Func<StringBuilder, T, StringBuilder> func)
        {
            var first = true;
            foreach (var item in items)
            {
                if (first)
                    first = false;
                else
                    sb.Append(separator);

                sb = func(sb, item);
            }

            return sb;
        }

        #endregion
    }
}