#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    public class UserState : AbstractIdentifier<int>
    {
        public virtual string? Name { get; set; }
    }
}
