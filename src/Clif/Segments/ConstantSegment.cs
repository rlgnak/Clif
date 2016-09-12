using System;
using Clif.Abstract;
using Clif.MatchResults;

namespace Clif.Segments
{
    public class ConstantSegment : ISegment
    {
        public ConstantSegment(string segmentText)
        {
            SegmentText = segmentText;
        }

        public string SegmentText { get; set; }

        public IMatchResult Match(string piece)
        {
            return SegmentText.Equals(piece, StringComparison.CurrentCultureIgnoreCase) ? new MatchResult() : null;
        }
    }
}