#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// 父标识符接口。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型（兼容各种引用与值类型标识）。</typeparam>
    public interface IParentIdentifier<TId> : IIdentifier<TId>
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// 父标识。
        /// </summary>
        TId? ParentId { get; set; }
    }
}
