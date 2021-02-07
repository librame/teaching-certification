#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    public class Role : AbstractIdentifier<int>
    {
        public virtual string? Name { get; set; }

        public virtual string? Descr { get; set; }
    }
}
