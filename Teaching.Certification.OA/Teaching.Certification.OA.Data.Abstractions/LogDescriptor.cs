#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// 日志描述符。
    /// </summary>
    public record LogDescriptor
    {
        public string? UserId { get; init; }

        public string? AssocId { get; init; }

        public string? Name { get; init; }

        public string? Descr { get; init; }
    }
}
