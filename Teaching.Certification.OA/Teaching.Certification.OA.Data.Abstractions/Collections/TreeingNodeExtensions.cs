﻿#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// 树形节点静态扩展。
    /// </summary>
    public static class TreeingNodeExtensions
    {
        /// <summary>
        /// 转换为无层级树形节点列表。
        /// </summary>
        /// <typeparam name="T">指定实现 <see cref="IParentIdentifier{TId}"/> 的元素类型。</typeparam>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <param name="nodes">给定的树形节点集合。</param>
        /// <param name="orderedFactory">给定的排序工厂方法（可选）。</param>
        /// <returns>返回无层级树形节点列表。</returns>
        public static IList<TreeingNode<T, TId>> ExportNonstratifiedNodes<T, TId>(IEnumerable<TreeingNode<T, TId>> nodes,
            Func<IEnumerable<TreeingNode<T, TId>>, IOrderedEnumerable<TreeingNode<T, TId>>>? orderedFactory = null)
            where T : IParentIdentifier<TId>
            where TId : IEquatable<TId>
        {
            var nonstratifiedNodes = new List<TreeingNode<T, TId>>();

            if (nodes.IsEmpty())
                return nonstratifiedNodes;

            foreach (var node in nodes)
            {
                nonstratifiedNodes.Add(node);

                // 链式查询
                var lookupNodes = ExportNonstratifiedNodes(node.Children);
                if (lookupNodes.IsNotEmpty())
                {
                    orderedFactory?.Invoke(lookupNodes);
                    nonstratifiedNodes.AddRange(lookupNodes);
                }
            }

            return nonstratifiedNodes;
        }


        /// <summary>
        /// 异步转换为树形节点列表。
        /// </summary>
        /// <typeparam name="T">指定实现 <see cref="IParentIdentifier{TId}"/> 的元素类型。</typeparam>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <param name="items">给定的类型实例集合。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个包含树形节点列表的异步操作。</returns>
        public static Task<IList<TreeingNode<T, TId>>> AsTreeingNodesAsync<T, TId>(this IEnumerable<T> items,
            CancellationToken cancellationToken = default)
            where T : IParentIdentifier<TId>
            where TId : IEquatable<TId>
            => Task.Run(() => items.AsTreeingNodes<T, TId>(), cancellationToken);

        /// <summary>
        /// 转换为树形节点列表。
        /// </summary>
        /// <typeparam name="T">指定实现 <see cref="IParentIdentifier{TId}"/> 的元素类型。</typeparam>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <param name="items">给定的类型实例集合。</param>
        /// <returns>返回树形节点列表。</returns>
        public static IList<TreeingNode<T, TId>> AsTreeingNodes<T, TId>(this IEnumerable<T> items)
            where T : IParentIdentifier<TId>
            where TId : IEquatable<TId>
        {
            if (items.IsEmpty())
                return new List<TreeingNode<T, TId>>();

            // 提取根父标识
            var rootParentId = items.Select(s => s.ParentId).Min();

            return LookupNodes(items, rootParentId);
        }

        private static IList<TreeingNode<T, TId>> LookupNodes<T, TId>(IEnumerable<T> items,
            TId? parentId, int depthLevel = 0)
            where T : IParentIdentifier<TId>
            where TId : IEquatable<TId>
        {
            var nodes = new List<TreeingNode<T, TId>>();

            // 提取父元素集合
#pragma warning disable CS8602 // 解引用可能出现空引用。
            var parents = items.Where(p => p.IsNotNull() && p.ParentId.Equals(parentId));
#pragma warning restore CS8602 // 解引用可能出现空引用。

            if (parents.IsEmpty())
                return nodes;

            foreach (var p in parents)
            {
                var node = new TreeingNode<T, TId>(p, depthLevel);

                var children = LookupNodes(items, p.Id, node.DepthLevel + 1);
                node.Children = children;

                nodes.Add(node);
            }

            return nodes;
        }

    }
}
