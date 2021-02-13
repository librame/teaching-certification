#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;

namespace Teaching.Certification.OA.Data
{
    public class Schedule : AbstractIdentifier<int>, ILoggable
    {
        public virtual string? CreatorId { get; set; }

        public virtual string? Title { get; set; }

        public virtual string? Address { get; set; }

        public virtual string? Descr { get; set; }

        public virtual DateTime BeginTime { get; set; }

        public virtual DateTime EndTime { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        public virtual ScheduleScope Scope { get; set; }


        public virtual LogDescriptor ToLog(string? userId = null)
        {
            return new LogDescriptor
            {
                UserId = userId,
                AssocId = Id.ToString(),
                Name = Title,
                Descr = $"{nameof(CreatorId)}={CreatorId},{nameof(Address)}={Address},{nameof(Descr)}={Descr},{nameof(BeginTime)}={BeginTime},{nameof(EndTime)}={EndTime},{nameof(CreatedTime)}={CreatedTime},{nameof(Scope)}={Scope}"
            };
        }

    }
}
