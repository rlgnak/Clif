namespace Clif.Abstract
{
    /// <summary>
    /// A Interface for name segements
    /// </summary>
    public interface INamedSegment : ISegment
    {
        /// <summary>
        /// The name of a segment
        /// </summary>
        string Name { get; set; }
    }
}