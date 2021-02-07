#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;

namespace Teaching.Certification.OA.Data
{
    public class Document : AbstractParentIdentifier<int>
    {
        public virtual int CategoryId { get; set; }

        public virtual string? OwnerId { get; set; }

        public virtual string? Name { get; set; }

        public virtual string? Descr { get; set; }

        public virtual string? Path { get; set; }

        public virtual long Length { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        public virtual DocumentStatus Status { get; set; }
    }
}
