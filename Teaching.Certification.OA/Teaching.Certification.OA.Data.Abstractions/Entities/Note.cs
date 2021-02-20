#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;

namespace Teaching.Certification.OA.Data
{
    public class Note : AbstractIdentifier<int>, ITitling, ILoggable
    {
        public virtual string? CreatorId { get; set; }

        public virtual string? Title { get; set; }

        public virtual string? Descr { get; set; }

        public virtual DateTime CreatedTime { get; set; }


        public virtual LogDescriptor ToLog(string? userId = null)
        {
            return new LogDescriptor
            {
                UserId = userId,
                AssocId = Id.ToString(),
                Name = Title,
                Descr = $"{nameof(CreatorId)}={CreatorId},{nameof(Descr)}={Descr},{nameof(CreatedTime)}={CreatedTime}"
            };
        }

    }
}
