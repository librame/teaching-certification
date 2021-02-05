#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;

namespace Teaching.Certification.OA.Data
{
    public class Schedule
    {
        public virtual int Id { get; set; }

        public virtual string CreatorId { get; set; }

        public virtual string Title { get; set; }

        public virtual string Address { get; set; }

        public virtual string Descr { get; set; }

        public virtual DateTime BeginTime { get; set; }

        public virtual DateTime EndTime { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        public virtual ScheduleScope Scope { get; set; }
    }
}
