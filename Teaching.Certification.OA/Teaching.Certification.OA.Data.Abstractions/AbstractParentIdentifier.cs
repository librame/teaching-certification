#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// 抽象父标识。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    [NotMapped]
    public abstract class AbstractParentIdentifier<TId> : AbstractIdentifier<TId>, IParentIdentifier<TId>
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// 父标识。
        /// </summary>
        public virtual TId? ParentId { get; set; }
    }
}
