#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// 保存更改截面接口。
    /// </summary>
    public interface ISaveChangesAspect
    {
        /// <summary>
        /// 前置拦截。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        void PreInject(IAccessor accessor);

        /// <summary>
        /// 后置拦截。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        void PostInject(IAccessor accessor);
    }
}
