#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    public class Department : AbstractIdentifier<int>, ILoggable
    {
        public virtual int BranchId { get; set; }

        public virtual string? PrincipalId { get; set; }

        public virtual string? Name { get; set; }

        public virtual string? Phone { get; set; }

        public virtual string? Mobile { get; set; }

        public virtual string? Fax { get; set; }


        public virtual LogDescriptor ToLog(string? userId = null)
        {
            return new LogDescriptor
            {
                UserId = userId,
                AssocId = Id.ToString(),
                Name = Name,
                Descr = $"{nameof(BranchId)}={BranchId},{nameof(PrincipalId)}={PrincipalId},{nameof(Phone)}={Phone},{nameof(Mobile)}={Mobile},{nameof(Fax)}={Fax}"
            };
        }

    }
}
