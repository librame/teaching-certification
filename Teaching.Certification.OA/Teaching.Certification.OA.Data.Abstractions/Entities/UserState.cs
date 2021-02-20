#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    public class UserState : AbstractIdentifier<int>, INaming, ILoggable
    {
        public virtual string? Name { get; set; }


        public virtual LogDescriptor ToLog(string? userId = null)
        {
            return new LogDescriptor
            {
                UserId = userId,
                AssocId = Id.ToString(),
                Name = Name
            };
        }

    }
}
