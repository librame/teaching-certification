#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;
using System.Linq;

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// <see cref="IStore{T}"/> 静态扩展。
    /// </summary>
    public static class StoreExtensions
    {
        private const string Unknown = "未知";
        private const string Default = "默认";


        #region INaming

        /// <summary>
        /// 通过名称获取类型实例。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{T}"/>。</param>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回 <typeparamref name="T"/>。</returns>
        public static T? GetByName<T>(this IStore<T> store, string? name)
            where T : class, INaming
        {
            store.NotNull(nameof(store));

#pragma warning disable CS8604 // 可能的 null 引用参数。
            if (name.IsEmpty())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
                return default;
            }

            return store.Queryable.FirstOrDefault(p => p.Name == name);
        }


        /// <summary>
        /// 获取指定 <see cref="int"/> 标识类型的名称，支持验证标识是否为空字符串或默认标识的形式（注：类型需实现 <see cref="INaming"/>）。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{T}"/>。</param>
        /// <param name="idString">给定的标识字符串。</param>
        /// <returns>返回字符串。</returns>
        public static string? GetNameByIdIfNotEmptyOrDefault<T>(this IStore<T> store, string? idString)
            where T : class, INaming
            => store.GetNameByIdIfNotEmptyOrDefault(idString, int.Parse);

        /// <summary>
        /// 获取指定标识的名称，支持验证标识是否为空字符串或默认标识的形式（注：类型需实现 <see cref="INaming"/>）。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{T}"/>。</param>
        /// <param name="idString">给定的标识字符串。</param>
        /// <param name="idConverter">给定的标识转换器。</param>
        /// <returns>返回字符串。</returns>
        public static string? GetNameByIdIfNotEmptyOrDefault<T, TId>(this IStore<T> store, string? idString,
            Func<string, TId> idConverter)
            where T : class, INaming
            where TId : IEquatable<TId>
        {
            store.NotNull(nameof(store));
            idConverter.NotNull(nameof(idConverter));

#pragma warning disable CS8604 // 可能的 null 引用参数。
            if (idString.IsEmpty())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
                return Unknown;
            }

            var id = idConverter.Invoke(idString);

            if (id.Equals(default(TId)))
                return Default;

            return store.GetById(id)?.Name ?? idString;
        }


        /// <summary>
        /// 获取指定 <see cref="string"/> 标识类型的名称，支持验证标识是否为空字符串的形式（注：类型需实现 <see cref="INaming"/>）。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{T}"/>。</param>
        /// <param name="id">给定的标识。</param>
        /// <returns>返回字符串。</returns>
        public static string? GetNameByIdIfNotEmpty<T>(this IStore<T> store, string? id)
            where T : class, INaming
        {
            store.NotNull(nameof(store));

#pragma warning disable CS8604 // 可能的 null 引用参数。
            if (id.IsEmpty())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
                return Unknown;
            }

            return store.GetById(id)?.Name ?? id;
        }


        /// <summary>
        /// 获取指定 <see cref="struct"/> 标识类型的名称，支持验证标识是否为默认标识的形式（注：类型需实现 <see cref="INaming"/>）。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{T}"/>。</param>
        /// <param name="id">给定的标识。</param>
        /// <returns>返回字符串。</returns>
        public static string? GetNameByIdIfNotDefault<T, TId>(this IStore<T> store, TId id)
            where T : class, INaming
            where TId : struct, IEquatable<TId>
        {
            if (id.Equals(default(TId)))
                return Default;

            return store.GetNameById(id);
        }


        /// <summary>
        /// 获取指定标识的名称（注：类型需实现 <see cref="INaming"/>）。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{T}"/>。</param>
        /// <param name="id">给定的标识。</param>
        /// <returns>返回字符串。</returns>
        public static string? GetNameById<T>(this IStore<T> store, object id)
            where T : class, INaming
        {
            store.NotNull(nameof(store));

            return store.GetById(id)?.Name ?? id.ToString();
        }

        #endregion


        #region ITitling

        /// <summary>
        /// 通过标题获取类型实例。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{T}"/>。</param>
        /// <param name="title">给定的标题。</param>
        /// <returns>返回 <typeparamref name="T"/>。</returns>
        public static T? GetByTitle<T>(this IStore<T> store, string? title)
            where T : class, ITitling
        {
            store.NotNull(nameof(store));

#pragma warning disable CS8604 // 可能的 null 引用参数。
            if (title.IsEmpty())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
                return default;
            }

            return store.Queryable.FirstOrDefault(p => p.Title == title);
        }


        /// <summary>
        /// 获取指定 <see cref="int"/> 标识类型的标题，支持验证标识是否为空字符串或默认标识的形式（注：类型需实现 <see cref="ITitling"/>）。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{T}"/>。</param>
        /// <param name="idString">给定的标识字符串。</param>
        /// <returns>返回字符串。</returns>
        public static string? GetTitleByIdIfNotEmptyOrDefault<T>(this IStore<T> store, string? idString)
            where T : class, ITitling
            => store.GetTitleByIdIfNotEmptyOrDefault(idString, int.Parse);

        /// <summary>
        /// 获取指定标识的标题，支持验证标识是否为空字符串或默认标识的形式（注：类型需实现 <see cref="ITitling"/>）。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{T}"/>。</param>
        /// <param name="idString">给定的标识字符串。</param>
        /// <param name="idConverter">给定的标识转换器。</param>
        /// <returns>返回字符串。</returns>
        public static string? GetTitleByIdIfNotEmptyOrDefault<T, TId>(this IStore<T> store, string? idString,
            Func<string, TId> idConverter)
            where T : class, ITitling
            where TId : IEquatable<TId>
        {
            store.NotNull(nameof(store));
            idConverter.NotNull(nameof(idConverter));

#pragma warning disable CS8604 // 可能的 null 引用参数。
            if (idString.IsEmpty())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
                return Unknown;
            }

            var id = idConverter.Invoke(idString);

            if (id.Equals(default(TId)))
                return Default;

            return store.GetById(id)?.Title ?? idString;
        }


        /// <summary>
        /// 获取指定 <see cref="string"/> 标识类型的标题，支持验证标识是否为空字符串的形式（注：类型需实现 <see cref="ITitling"/>）。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{T}"/>。</param>
        /// <param name="id">给定的标识。</param>
        /// <returns>返回字符串。</returns>
        public static string? GetTitleByIdIfNotEmpty<T>(this IStore<T> store, string? id)
            where T : class, ITitling
        {
            store.NotNull(nameof(store));

#pragma warning disable CS8604 // 可能的 null 引用参数。
            if (id.IsEmpty())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
                return Unknown;
            }

            return store.GetById(id)?.Title ?? id;
        }


        /// <summary>
        /// 获取指定 <see cref="struct"/> 标识类型的标题，支持验证标识是否为默认标识的形式（注：类型需实现 <see cref="ITitling"/>）。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{T}"/>。</param>
        /// <param name="id">给定的标识。</param>
        /// <returns>返回字符串。</returns>
        public static string? GetTitleByIdIfNotDefault<T, TId>(this IStore<T> store, TId id)
            where T : class, ITitling
            where TId : struct, IEquatable<TId>
        {
            if (id.Equals(default(TId)))
                return Default;

            return store.GetTitleById(id);
        }


        /// <summary>
        /// 获取指定标识的标题（注：类型需实现 <see cref="ITitling"/>）。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{T}"/>。</param>
        /// <param name="id">给定的标识。</param>
        /// <returns>返回字符串。</returns>
        public static string? GetTitleById<T>(this IStore<T> store, object id)
            where T : class, ITitling
        {
            store.NotNull(nameof(store));

            return store.GetById(id)?.Title ?? id.ToString();
        }

        #endregion

    }
}
