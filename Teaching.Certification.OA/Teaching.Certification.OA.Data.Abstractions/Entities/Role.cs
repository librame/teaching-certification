#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    public class Role : AbstractIdentifier<int>, ILoggable
    {
        public virtual string? Name { get; set; }

        public virtual string? Descr { get; set; }


        public virtual LogDescriptor ToLog(string? userId = null)
        {
            return new LogDescriptor
            {
                UserId = userId,
                AssocId = Id.ToString(),
                Name = Name,
                Descr = $"{nameof(Descr)}={Descr}"
            };
        }

    }
}
