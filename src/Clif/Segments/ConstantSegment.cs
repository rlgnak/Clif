using System;
using Clif.Abstract;
using Clif.MatchResults;

namespace Clif.Segments
{
    /// <summary>
    /// A basic class for rpersenting a segment match
    /// </summary>
    public class ConstantSegment : ISegment
    {
        /// <summary>
        /// Constructs a <see cref="ConstantSegment"/>
        /// </summary>
        /// <param name="segmentText"></param>
        public ConstantSegment(string segmentText)
        {
            SegmentText = segmentText;
        }

        private string SegmentText { get; }

        /// <summary>
        /// Determines if a string matches a segment
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        public IMatchResult Match(string piece)
        {
            return SegmentText.Equals(piece, StringComparison.CurrentCultureIgnoreCase) ? new MatchResult() : null;
        }
    }
}