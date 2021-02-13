#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;

namespace Teaching.Certification.OA.Data
{
    public class Attachment : AbstractIdentifier<int>, ILoggable
    {
        public virtual int DocumentId { get; set; }

        public virtual int DocumentCategoryId { get; set; }

        public virtual string? Name { get; set; }

        public virtual string? Path { get; set; }

        public virtual long Length { get; set; }

        public virtual DateTime CreatedTime { get; set; }


        public virtual LogDescriptor ToLog(string? userId = null)
        {
            return new LogDescriptor
            {
                UserId = userId,
                AssocId = Id.ToString(),
                Name = Name,
                Descr = $"{nameof(DocumentId)}={DocumentId},{nameof(DocumentCategoryId)}={DocumentCategoryId},{nameof(Path)}={Path},{nameof(Length)}={Length},{nameof(CreatedTime)}={CreatedTime}"
            };
        }

    }
}
