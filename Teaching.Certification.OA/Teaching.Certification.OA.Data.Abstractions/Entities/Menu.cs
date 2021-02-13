#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    public class Menu : AbstractParentIdentifier<string>, IRanking<float>, ILoggable
    {
        public virtual string? Name { get; set; }

        public virtual string? Url { get; set; }

        public virtual float Rank { get; set; }


        public virtual LogDescriptor ToLog(string? userId = null)
        {
            return new LogDescriptor
            {
                UserId = userId,
                AssocId = Id.ToString(),
                Name = Name,
                Descr = $"{nameof(Url)}={Url},{nameof(Rank)}={Rank}"
            };
        }

    }
}
