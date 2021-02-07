#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// 树形节点。
    /// </summary>
    /// <typeparam name="T">指定的树形元素类型。</typeparam>
    /// <typeparam name="TId">指定的树形元素标识类型。</typeparam>
    [NotMapped]
    public class TreeingNode<T, TId> : IParentIdentifier<TId>, IEquatable<TreeingNode<T, TId>>
        where T : IParentIdentifier<TId>
        where TId : IEquatable<TId>
    {
        private IList<TreeingNode<T, TId>> _children;


        /// <summary>
        /// 构造一个 <see cref="TreeingNode{T, TId}"/>。
        /// </summary>
        /// <param name="item">给定的项。</param>
        /// <param name="depthLevel">给定的深度等级。</param>
        /// <param name="children">给定的子节点列表。</param>
        public TreeingNode(T item, int depthLevel = 0, IList<TreeingNode<T, TId>>? children = null)
        {
            Item = item;
            DepthLevel = depthLevel;

            _children = children ?? new List<TreeingNode<T, TId>>();
        }


        /// <summary>
        /// 节点项。
        /// </summary>
        public T Item { get; }

        /// <summary>
        /// 深度等级。
        /// </summary>
        public int DepthLevel { get; }


        /// <summary>
        /// 子节点列表。
        /// </summary>
        public IList<TreeingNode<T, TId>> Children
        {
            get
            {
                return _children;
            }
            set
            {
                if (value.IsEmpty())
                    value = new List<TreeingNode<T, TId>>();

                _children = value;
            }
        }


        /// <summary>
        /// 获取节点项的标识。
        /// </summary>
        public TId? Id
        {
            get { return Item.Id; }
            set { Item.Id = value; }
        }

        /// <summary>
        /// 获取节点项的父标识。
        /// </summary>
        public TId? ParentId
        {
            get { return Item.ParentId; }
            set { Item.ParentId = value; }
        }


        /// <summary>
        /// 是否包含指定标识的子节点。
        /// </summary>
        /// <param name="childId">给定的子节点编号。</param>
        /// <returns>返回布尔值。</returns>
        public virtual bool ContainsChild(TId childId)
            => ContainsChild(childId, out _);

        /// <summary>
        /// 是否包含指定标识的子节点。
        /// </summary>
        /// <param name="childId">给定的子节点信号。</param>
        /// <param name="child">输出当前子节点。</param>
        /// <returns>返回布尔值。</returns>
        public virtual bool ContainsChild(TId childId, out TreeingNode<T, TId> child)
        {
#pragma warning disable CS8601 // 可能的 null 引用赋值。
            child = GetChild(childId);
#pragma warning restore CS8601 // 可能的 null 引用赋值。

            return child.IsNotNull();
        }


        /// <summary>
        /// 查找指定编号的子节点。
        /// </summary>
        /// <param name="childId">给定的子节点编号。</param>
        /// <returns>返回当前子节点。</returns>
        public virtual TreeingNode<T, TId>? GetChild(TId childId)
        {
            if (Children.IsEmpty())
                return null;

#pragma warning disable CS8602 // 解引用可能出现空引用。
            return Children.FirstOrDefault(c => Id.Equals(childId));
#pragma warning restore CS8602 // 解引用可能出现空引用。
        }

        /// <summary>
        /// 查询指定父编号的子孙节点列表。
        /// </summary>
        /// <param name="parentId">给定的父编号。</param>
        /// <returns>返回树形节点列表。</returns>
        public virtual IList<TreeingNode<T, TId>>? GetParentChildren(TId parentId)
        {
            if (Children.IsEmpty())
                return null;

#pragma warning disable CS8602 // 解引用可能出现空引用。
            return Children.Where(p => p.ParentId.Equals(parentId)).ToList();
#pragma warning restore CS8602 // 解引用可能出现空引用。
        }


        /// <summary>
        /// 是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="TreeingNode{T, TId}"/>。</param>
        /// <returns>返回布尔值。</returns>
        public virtual bool Equals(TreeingNode<T, TId>? other)
        {
            if (other is null)
                return false;

#pragma warning disable CS8602 // 解引用可能出现空引用。
            return Id.Equals(other.Id) && ParentId.Equals(other.ParentId);
#pragma warning restore CS8602 // 解引用可能出现空引用。
        }

        /// <summary>
        /// 是否相等。
        /// </summary>
        /// <param name="obj">给定的 <see cref="TreeingNode{T, TId}"/>。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object? obj)
            => obj is TreeingNode<T, TId> other && Equals(other);


        /// <summary>
        /// 已重载。
        /// </summary>
        /// <returns>返回此实例的哈希代码。</returns>
        public override int GetHashCode()
        {
#pragma warning disable CS8602 // 解引用可能出现空引用。
            return Id.GetHashCode() ^ ParentId.GetHashCode();
#pragma warning restore CS8602 // 解引用可能出现空引用。
        }


        /// <summary>
        /// 已重载。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string ToString()
            => ToString(node => node.Item.ToString());

        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <param name="toStringFactory">给定的转换方法。</param>
        /// <returns>返回字符串。</returns>
        public virtual string ToString(Func<TreeingNode<T, TId>, string?> toStringFactory)
        {
            if (Children.IsEmpty())
                return string.Empty;

            toStringFactory.NotNull(nameof(toStringFactory));

            var sb = new StringBuilder();

            // Current Node
            sb.Append(toStringFactory.Invoke(this));
            sb.Append(';');

            // Children Nodes
            int i = 0;
            foreach (var child in Children)
            {
                // 链式转换可能存在的子孙节点
                sb.Append(child.ToString(toStringFactory));

                if (i != Children.Count - 1)
                    sb.Append(';');

                i++;
            }

            return sb.ToString();
        }

    }
}
