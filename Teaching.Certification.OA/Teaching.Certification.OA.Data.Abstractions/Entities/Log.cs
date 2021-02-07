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
    }
}
