using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Clif
{
    public abstract class Segment
    {
        public string Name { get; set; }
        public string Text { get; set; }

        

        public Segment(string segment)
        {
            Name = segment;
            Text = segment;
        }

        public abstract CommandResult Match(string[] argument);

        public virtual int MatchSegments { get { return 1; } }
        public virtual int Type { get { return 1; } }

        public virtual object Default {  get { return null; } }
    }
}
