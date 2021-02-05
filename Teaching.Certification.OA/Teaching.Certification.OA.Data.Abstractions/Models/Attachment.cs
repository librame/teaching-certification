#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;

namespace Teaching.Certification.OA.Data
{
    public class Attachment
    {
        public virtual int Id { get; set; }

        public virtual int DocumentId { get; set; }

        public virtual int DocumentCategoryId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Path { get; set; }

        public virtual long Length { get; set; }

        public virtual DateTime CreatedTime { get; set; }
    }
}
