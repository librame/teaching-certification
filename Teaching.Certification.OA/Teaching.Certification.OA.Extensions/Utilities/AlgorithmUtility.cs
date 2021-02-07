#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;
using System.Security.Cryptography;

namespace Teaching.Certification.OA
{
    /// <summary>
    /// 算法实用工具。
    /// </summary>
    public static class AlgorithmUtility
    {
        private static readonly Lazy<SHA384> _sha384
            = new Lazy<SHA384>(() => SHA384.Create());


        #region SHA384

        /// <summary>
        /// 运行 SHA384。
        /// </summary>
        /// <param name="action">给定的动作。</param>
        public static void RunSha384(Action<SHA384> action)
            => action.NotNull(nameof(action)).Invoke(_sha384.Value);

        /// <summary>
        /// 运行 SHA384，并返回值。
        /// </summary>
        /// <typeparam name="TValue">指定的值类型。</typeparam>
        /// <param name="valueFunc">给定的值方法。</param>
        /// <returns>返回 <typeparamref name="TValue"/>。</returns>
        public static TValue RunSha384<TValue>(Func<SHA384, TValue> valueFunc)
            => valueFunc.NotNull(nameof(valueFunc)).Invoke(_sha384.Value);

        #endregion

    }
}
