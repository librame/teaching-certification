#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    public class DocumentCategory : AbstractIdentifier<int>, ILoggable
    {
        public virtual string? Name { get; set; }

        public virtual string? Extension { get; set; }

        public virtual string? Icon { get; set; }


        public virtual LogDescriptor ToLog(string? userId = null)
        {
            return new LogDescriptor
            {
                UserId = userId,
                AssocId = Id.ToString(),
                Name = Name,
                Descr = $"{nameof(Extension)}={Extension},{nameof(Icon)}={Icon}"
            };
        }

    }
}
