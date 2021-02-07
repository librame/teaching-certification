#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    public class Menu : AbstractParentIdentifier<int>, IRanking<float>
    {
        public virtual string? Name { get; set; }

        public virtual string? Url { get; set; }

        public virtual float Rank { get; set; }
    }
}
