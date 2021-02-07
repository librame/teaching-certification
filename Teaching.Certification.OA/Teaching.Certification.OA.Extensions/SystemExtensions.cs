#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Teaching.Certification.OA
{
    /// <summary>
    /// <see cref="System"/> 静态扩展。
    /// </summary>
    public static class SystemExtensions
    {

        #region IsNull

        /// <summary>
        /// 是否为 NULL。
        /// </summary>
        /// <typeparam name="TSource">指定的源类型。</typeparam>
        /// <param name="source">给定的源实例。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsNull<TSource>(this TSource source)
            => null == source;

        /// <summary>
        /// 是否不为 NULL。
        /// </summary>
        /// <typeparam name="TSource">指定的源类型。</typeparam>
        /// <param name="source">给定的源实例。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsNotNull<TSource>(this TSource source)
            => null != source;


        /// <summary>
        /// 是否为 NULL 或空格。
        /// </summary>
        /// <remarks>
        /// 详情参考 <see cref="string.IsNullOrWhiteSpace(string)"/>。
        /// </remarks>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsWhiteSpace(this string str)
            => string.IsNullOrWhiteSpace(str);

        /// <summary>
        /// 是否不为 NULL 或空格。
        /// </summary>
        /// <remarks>
        /// 详情参考 <see cref="string.IsNullOrWhiteSpace(string)"/>。
        /// </remarks>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsNotWhiteSpace(this string str)
            => !string.IsNullOrWhiteSpace(str);


        /// <summary>
        /// 是否为 NULL 或空字符串。
        /// </summary>
        /// <remarks>
        /// 详情参考 <see cref="string.IsNullOrEmpty(string)"/>。
        /// </remarks>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsEmpty(this string str)
            => string.IsNullOrEmpty(str);

        /// <summary>
        /// 是否不为 NULL 或空字符串。
        /// </summary>
        /// <remarks>
        /// 详情参考 <see cref="string.IsNullOrEmpty(string)"/>。
        /// </remarks>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsNotEmpty(this string str)
            => !string.IsNullOrEmpty(str);


        /// <summary>
        /// 是否为 NULL 或空集合。
        /// </summary>
        /// <typeparam name="TSources">指定的源集合类型。</typeparam>
        /// <param name="sources">给定的 <see cref="IEnumerable"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsEmpty<TSources>(this TSources sources)
            where TSources : IEnumerable
        {
            if (sources.IsNull())
                return true;

            if (sources is ICollection collection)
                return collection.Count < 1;

            var enumerator = sources.GetEnumerator();
            return !enumerator.MoveNext();
        }

        /// <summary>
        /// 是否不为 NULL 或空集合。
        /// </summary>
        /// <typeparam name="TSources">指定的源集合类型。</typeparam>
        /// <param name="sources">给定的 <see cref="IEnumerable"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsNotEmpty<TSources>(this TSources sources)
            where TSources : IEnumerable
            => !sources.IsEmpty();


        /// <summary>
        /// 是否为 NULL 或空集合。
        /// </summary>
        /// <remarks>
        /// 详情参考  <see cref="Enumerable.Any{TSource}(IEnumerable{TSource})"/>。
        /// </remarks>
        /// <typeparam name="TSource">指定的源类型。</typeparam>
        /// <param name="sources">给定的 <see cref="IEnumerable{TSource}"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsEmpty<TSource>(this IEnumerable<TSource> sources)
        {
            if (sources.IsNull())
                return true;

            return !sources.Any();
        }

        /// <summary>
        /// 是否不为 NULL 或空集合。
        /// </summary>
        /// <remarks>
        /// 详情参考  <see cref="Enumerable.Any{TSource}(IEnumerable{TSource})"/>。
        /// </remarks>
        /// <typeparam name="TSource">指定的源类型。</typeparam>
        /// <param name="sources">给定的 <see cref="IEnumerable{TSource}"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsNotEmpty<TSource>(this IEnumerable<TSource> sources)
            => !sources.IsEmpty();

        #endregion


        #region NotNull

        /// <summary>
        /// 参数不为空。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="param"/> 为空。
        /// </exception>
        /// <typeparam name="TParam">指定的参数类型。</typeparam>
        /// <param name="param">给定的参数。</param>
        /// <param name="paramName">给定的参数名称。</param>
        /// <returns>返回参数。</returns>
        public static TParam NotNull<TParam>(this TParam? param, string paramName)
        {
            if (param is null)
                throw new ArgumentNullException(paramName);

            return param;
        }

        /// <summary>
        /// 参数不为空或空字符串。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="str"/> 为空或空字符串。
        /// </exception>
        /// <param name="str">给定的字符串。</param>
        /// <param name="paramName">给定的参数名称。</param>
        /// <returns>返回字符串。</returns>
        public static string NotEmpty(this string? str, string paramName)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException($"'{paramName}' is null or empty.");

            return str;
        }

        /// <summary>
        /// 参数不为空、空字符串或空格字符。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="str"/> 为空、空字符串或空格字符。
        /// </exception>
        /// <param name="str">给定的字符串。</param>
        /// <param name="paramName">给定的参数名称。</param>
        /// <returns>返回字符串。</returns>
        public static string NotWhiteSpace(this string? str, string paramName)
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentException($"'{paramName}' is null or empty or white-space characters.");

            return str;
        }

        #endregion

    }
}
