#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Teaching.Certification.OA
{
    /// <summary>
    /// <see cref="System"/> 静态扩展。
    /// </summary>
    public static class SystemExtensions
    {

        /// <summary>
        /// 是否定义特性。
        /// </summary>
        /// <typeparam name="TAttribute">指定的特性类型。</typeparam>
        /// <param name="provider">指定的 <see cref="ICustomAttributeProvider"/>。</param>
        /// <param name="inherit">指定是否搜索该成员的继承链以查找这些特性。</param>
        /// <returns>返回是否已定义此特性的布尔值。</returns>
        public static bool IsDefined<TAttribute>(this ICustomAttributeProvider provider, bool inherit = false)
            where TAttribute : Attribute
            => provider.NotNull(nameof(provider)).IsDefined(typeof(TAttribute), inherit);


        /// <summary>
        /// 遍历元素集合（元素集合为空或空集合则返回，不抛异常）。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="items"/> or <paramref name="action"/> is null.
        /// </exception>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="items">给定的元素集合。</param>
        /// <param name="action">给定的遍历动作。</param>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            items.NotNull(nameof(items));
            action.NotNull(nameof(action));

            foreach (var item in items)
                action.Invoke(item);
        }

        /// <summary>
        /// 遍历元素集合（元素集合为空或空集合则返回，不抛异常）。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="items"/>, <paramref name="action"/> or <paramref name="breakFactory"/> is null.
        /// </exception>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="items">给定的元素集合。</param>
        /// <param name="action">给定的遍历动作。</param>
        /// <param name="breakFactory">给定跳出遍历的动作。</param>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action, Func<T, bool> breakFactory)
        {
            items.NotNull(nameof(items));
            action.NotNull(nameof(action));
            breakFactory.NotNull(nameof(breakFactory));

            foreach (var item in items)
            {
                action.Invoke(item);

                if (breakFactory.Invoke(item))
                    break;
            }
        }


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


        #region IsImplemented

        /// <summary>
        /// 是否可以从目标类型分配。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseType"/> 或 <paramref name="targetType"/> 为空。
        /// </exception>
        /// <remarks>
        /// 详情参考 <see cref="Type.IsAssignableFrom(Type)"/>。
        /// </remarks>
        /// <param name="baseType">给定的基础类型。</param>
        /// <param name="targetType">给定的目标类型。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsAssignableFromTargetType(this Type baseType, Type targetType)
        {
            baseType.NotNull(nameof(baseType));
            targetType.NotNull(nameof(targetType));

            // 对泛型类型定义提供支持
            if (baseType.IsGenericTypeDefinition)
            {
                if (baseType.IsInterface)
                    return targetType.IsImplementedInterfaceType(baseType);

                return targetType.IsImplementedBaseType(baseType);
            }

            return baseType.IsAssignableFrom(targetType);
        }

        /// <summary>
        /// 是否可以分配给基础类型。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseType"/> 或 <paramref name="targetType"/> 为空。
        /// </exception>
        /// <remarks>
        /// 与 <see cref="IsAssignableFromTargetType(Type, Type)"/> 参数相反。
        /// </remarks>
        /// <param name="targetType">给定的目标类型。</param>
        /// <param name="baseType">给定的基础类型。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsAssignableToBaseType(this Type targetType, Type baseType)
            => baseType.IsAssignableFromTargetType(targetType);


        /// <summary>
        /// 是否已实现某个接口类型。
        /// </summary>
        /// <typeparam name="TInterface">指定的接口类型（支持泛型类型定义）。</typeparam>
        /// <param name="type">给定的当前类型。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsImplementedInterfaceType<TInterface>(this Type type)
            => type.IsImplementedInterfaceType(typeof(TInterface), out _);

        /// <summary>
        /// 是否已实现某个接口类型。
        /// </summary>
        /// <typeparam name="TInterface">指定的接口类型（支持泛型类型定义）。</typeparam>
        /// <param name="type">给定的当前类型。</param>
        /// <param name="resultType">输出此结果类型（当接口类型为泛型定义时，可用于得到泛型参数等操作）。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsImplementedInterfaceType<TInterface>(this Type type, out Type resultType)
            => type.IsImplementedInterfaceType(typeof(TInterface), out resultType);

        /// <summary>
        /// 是否已实现某个接口类型。
        /// </summary>
        /// <param name="type">给定的当前类型。</param>
        /// <param name="interfaceType">给定的接口类型（支持泛型类型定义）。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsImplementedInterfaceType(this Type type, Type interfaceType)
            => type.IsImplementedInterfaceType(interfaceType, out _);

        /// <summary>
        /// 是否已实现某个接口类型。
        /// </summary>
        /// <param name="type">给定的当前类型。</param>
        /// <param name="interfaceType">给定的接口类型（支持泛型类型定义）。</param>
        /// <param name="resultType">输出此结果类型（当接口类型为泛型定义时，可用于得到泛型参数等操作）。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsImplementedInterfaceType(this Type type, Type interfaceType, out Type resultType)
        {
            type.NotNull(nameof(type));
            interfaceType.NotNull(nameof(interfaceType));

            var allInterfaceTypes = type.GetInterfaces();

            // 如果判定的接口类型是泛型定义
            if (interfaceType.IsGenericTypeDefinition)
            {
#pragma warning disable CS8601 // 可能的 null 引用赋值。
                resultType = allInterfaceTypes
                    .Where(type => type.IsGenericType)
                    .FirstOrDefault(type => type.GetGenericTypeDefinition() == interfaceType);
#pragma warning restore CS8601 // 可能的 null 引用赋值。

                return resultType.IsNotNull();
            }

#pragma warning disable CS8601 // 可能的 null 引用赋值。
            resultType = allInterfaceTypes.FirstOrDefault(type => type == interfaceType);
#pragma warning restore CS8601 // 可能的 null 引用赋值。

            return resultType.IsNotNull();
        }


        /// <summary>
        /// 是否已实现某个基础（非接口）类型。
        /// </summary>
        /// <typeparam name="TBase">指定的基础类型（支持泛型类型定义）。</typeparam>
        /// <param name="type">给定的当前类型。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsImplementedBaseType<TBase>(this Type type)
            => type.IsImplementedBaseType(typeof(TBase), out _);

        /// <summary>
        /// 是否已实现某个基础（非接口）类型。
        /// </summary>
        /// <typeparam name="TBase">指定的基础类型（支持泛型类型定义）。</typeparam>
        /// <param name="type">给定的当前类型。</param>
        /// <param name="resultType">输出此结果类型（当基础类型为泛型定义时，可用于得到泛型参数等操作）。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsImplementedBaseType<TBase>(this Type type, out Type resultType)
            => type.IsImplementedBaseType(typeof(TBase), out resultType);

        /// <summary>
        /// 是否已实现某个基础（非接口）类型。
        /// </summary>
        /// <param name="type">给定的当前类型。</param>
        /// <param name="baseType">给定的基础类型（支持泛型类型定义）。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsImplementedBaseType(this Type type, Type baseType)
            => type.IsImplementedBaseType(baseType, out _);

        /// <summary>
        /// 是否已实现某个基础（非接口）类型。
        /// </summary>
        /// <param name="type">给定的当前类型。</param>
        /// <param name="baseType">给定的基础类型（支持泛型类型定义）。</param>
        /// <param name="resultType">输出此结果类型（当基础类型为泛型定义时，可用于得到泛型参数等操作）。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsImplementedBaseType(this Type type, Type baseType, out Type resultType)
        {
            baseType.NotNull(nameof(baseType));

            if (baseType.IsInterface)
                throw new NotSupportedException($"The base type '{baseType}' does not support interface.");

            var allBaseTypes = type.GetBaseTypes();

            // 如果判定的基础类型是泛型定义
            if (baseType.IsGenericTypeDefinition)
            {
#pragma warning disable CS8601 // 可能的 null 引用赋值。
                resultType = allBaseTypes
                    .Where(type => type.IsGenericType)
                    .FirstOrDefault(type => type.GetGenericTypeDefinition() == baseType);
#pragma warning restore CS8601 // 可能的 null 引用赋值。

                return resultType.IsNotNull();
            }

#pragma warning disable CS8601 // 可能的 null 引用赋值。
            resultType = allBaseTypes.FirstOrDefault(type => type == baseType);
#pragma warning restore CS8601 // 可能的 null 引用赋值。

            return resultType.IsNotNull();
        }


        /// <summary>
        /// 获取基础类型集合。
        /// </summary>
        /// <param name="type">给定的类型。</param>
        /// <returns>返回 <see cref="IEnumerable{Type}"/>。</returns>
        public static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            type.NotNull(nameof(type));

            // 当前基类（Object 为最顶层基类，接口会直接返回 NULL）
#pragma warning disable CS8600 // 将 null 文本或可能的 null 值转换为不可为 null 类型。
            type = type.BaseType;
#pragma warning restore CS8600 // 将 null 文本或可能的 null 值转换为不可为 null 类型。

            while (type != null)
            {
                yield return type;

#pragma warning disable CS8600 // 将 null 文本或可能的 null 值转换为不可为 null 类型。
                type = type.BaseType;
#pragma warning restore CS8600 // 将 null 文本或可能的 null 值转换为不可为 null 类型。
            }
        }

        #endregion

    }
}
