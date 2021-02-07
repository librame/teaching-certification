#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    public class Department : AbstractIdentifier<int>
    {
        public virtual int BranchId { get; set; }

        public virtual string? PrincipalId { get; set; }

        public virtual string? Name { get; set; }

        public virtual string? Phone { get; set; }

        public virtual string? Mobile { get; set; }

        public virtual string? Fax { get; set; }
    }
}
