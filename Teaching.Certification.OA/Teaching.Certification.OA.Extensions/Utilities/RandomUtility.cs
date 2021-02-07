#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;
using System.Security.Cryptography;
using System.Threading;

namespace Teaching.Certification.OA
{
    /// <summary>
    /// <see cref="Random"/> 实用工具。
    /// </summary>
    public static class RandomUtility
    {
        private static int _location
            = Environment.TickCount;

        // 支持多线程，各线程维持独立的随机实例
        private static readonly ThreadLocal<Random> _random
            = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _location)));

        // 支持多线程，各线程维持独立的随机数生成器实例
        private static readonly ThreadLocal<RandomNumberGenerator> _generator
            = new ThreadLocal<RandomNumberGenerator>(() => RandomNumberGenerator.Create());


        /// <summary>
        /// 运行伪随机数生成器。
        /// </summary>
        /// <param name="action">给定的动作。</param>
        public static void Run(Action<Random> action)
#pragma warning disable CS8604 // 可能的 null 引用参数。
            => action.NotNull(nameof(action)).Invoke(_random.Value);
#pragma warning restore CS8604 // 可能的 null 引用参数。

        /// <summary>
        /// 运行伪随机数生成器，并返回值。
        /// </summary>
        /// <typeparam name="TValue">指定的值类型。</typeparam>
        /// <param name="valueFunc">给定的值方法。</param>
        /// <returns>返回 <typeparamref name="TValue"/>。</returns>
        public static TValue Run<TValue>(Func<Random, TValue> valueFunc)
#pragma warning disable CS8604 // 可能的 null 引用参数。
            => valueFunc.NotNull(nameof(valueFunc)).Invoke(_random.Value);
#pragma warning restore CS8604 // 可能的 null 引用参数。


        /// <summary>
        /// 运行更具安全性的随机数生成器。
        /// </summary>
        /// <param name="action">给定的动作。</param>
        public static void RunSecurity(Action<RandomNumberGenerator> action)
#pragma warning disable CS8604 // 可能的 null 引用参数。
            => action.NotNull(nameof(action)).Invoke(_generator.Value);
#pragma warning restore CS8604 // 可能的 null 引用参数。

        /// <summary>
        /// 运行更具安全性的随机数生成器，并返回值。
        /// </summary>
        /// <typeparam name="TValue">指定的值类型。</typeparam>
        /// <param name="valueFunc">给定的值方法。</param>
        /// <returns>返回 <typeparamref name="TValue"/>。</returns>
        public static TValue RunSecurity<TValue>(Func<RandomNumberGenerator, TValue> valueFunc)
#pragma warning disable CS8604 // 可能的 null 引用参数。
            => valueFunc.NotNull(nameof(valueFunc)).Invoke(_generator.Value);
#pragma warning restore CS8604 // 可能的 null 引用参数。


        /// <summary>
        /// 生成指定长度的随机数字节数组。
        /// </summary>
        /// <param name="length">给定的字节数组元素长度。</param>
        /// <returns>返回生成的字节数组。</returns>
        public static byte[] GenerateByteArray(int length)
        {
            return RunSecurity(rng =>
            {
                var buffer = new byte[length];
                rng.GetBytes(buffer);

                return buffer;
            });
        }

    }
}
