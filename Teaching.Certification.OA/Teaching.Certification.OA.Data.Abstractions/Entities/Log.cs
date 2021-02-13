#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;

namespace Teaching.Certification.OA.Data
{
    public class Log : AbstractIdentifier<int>
    {
        public virtual string? UserId { get; set; }

        public virtual string? AssocId { get; set; }

        public virtual string? Name { get; set; }

        public virtual string? Descr { get; set; }

        public virtual DateTime CreatedTime { get; set; }


        /// <summary>
        /// 从描述符创建日志。
        /// </summary>
        /// <param name="descriptor">给定的 <see cref="LogDescriptor"/>。</param>
        /// <returns>返回 <see cref="Log"/>。</returns>
        public static Log CreateFromDescriptor(LogDescriptor descriptor)
        {
            descriptor.NotNull(nameof(descriptor));

            return new Log
            {
                UserId = descriptor.UserId,
                AssocId = descriptor.AssocId,
                Name = descriptor.Name,
                Descr = descriptor.Descr,
                CreatedTime = DateTime.Now
            };
        }

    }
}
