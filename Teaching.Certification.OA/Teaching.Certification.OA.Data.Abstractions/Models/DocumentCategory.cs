#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    public class DocumentCategory
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Extension { get; set; }

        public virtual string Icon { get; set; }
    }
}
