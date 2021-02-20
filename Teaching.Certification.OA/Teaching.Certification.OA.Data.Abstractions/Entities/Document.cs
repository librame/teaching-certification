#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;

namespace Teaching.Certification.OA.Data
{
    public class Document : AbstractParentIdentifier<int>, INaming, ILoggable
    {
        public virtual int CategoryId { get; set; }

        public virtual string? OwnerId { get; set; }

        public virtual string? Name { get; set; }

        public virtual string? Descr { get; set; }

        public virtual string? Path { get; set; }

        public virtual long Length { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        public virtual DocumentStatus Status { get; set; }


        public virtual LogDescriptor ToLog(string? userId = null)
        {
            return new LogDescriptor
            {
                UserId = userId,
                AssocId = Id.ToString(),
                Name = Name,
                Descr = $"{nameof(CategoryId)}={CategoryId},{nameof(OwnerId)}={OwnerId},{nameof(Descr)}={Descr},{nameof(Path)}={Path},{nameof(Length)}={Length},{nameof(CreatedTime)}={CreatedTime},{nameof(Status)}={Status}"
            };
        }

    }
}
