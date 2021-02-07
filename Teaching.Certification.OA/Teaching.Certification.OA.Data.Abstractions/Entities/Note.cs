#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;

namespace Teaching.Certification.OA.Data
{
    public class Note : AbstractIdentifier<int>
    {
        public virtual string? CreatorId { get; set; }

        public virtual string? Title { get; set; }

        public virtual string? Descr { get; set; }

        public virtual DateTime CreatedTime { get; set; }
    }
}
