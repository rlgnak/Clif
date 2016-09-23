namespace Clif.Abstract
{
    /// <summary>
    /// A basic class for representing a segment
    /// </summary>
    public interface ISegment
    {
        /// <summary>
        /// Determines if a string matches a segment
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        IMatchResult Match(string piece);
    }
}