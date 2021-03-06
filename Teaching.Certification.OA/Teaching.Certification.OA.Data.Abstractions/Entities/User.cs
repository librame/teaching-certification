﻿#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    public class User : AbstractIdentifier<string>, INaming, ILoggable
    {
        public virtual string? RoleId { get; set; }

        public virtual int StateId { get; set; }
            = 1;

        public virtual int DepartmentId { get; set; }

        public virtual UserGender Gender { get; set; }
            = UserGender.Male;

        public virtual string? Name { get; set; }

        public virtual string? PasswordHash { get; set; }

        public virtual string? Descr { get; set; }


        public virtual LogDescriptor ToLog(string? userId = null)
        {
            return new LogDescriptor
            {
                UserId = userId,
                AssocId = Id,
                Name = Name,
                Descr = $"{nameof(RoleId)}={RoleId},{nameof(StateId)}={StateId},{nameof(DepartmentId)}={DepartmentId},{nameof(Gender)}={Gender},{nameof(PasswordHash)}={PasswordHash}"
            };
        }

    }
}
