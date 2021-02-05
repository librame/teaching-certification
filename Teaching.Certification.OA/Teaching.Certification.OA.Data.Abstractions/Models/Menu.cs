#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    public class Menu
    {
        public virtual int Id { get; set; }

        public virtual int ParentId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Url { get; set; }

        public virtual int Rank { get; set; }
    }
}
