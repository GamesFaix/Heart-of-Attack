using System;
using System.Collections.Generic;

namespace HOA.Stats
{
    public class Flex : Stat 
    {
        public Flex(sbyte min, sbyte max)
            : base(null, min, max)
        { }

        public static implicit operator Range<sbyte>(Flex flex) { return Range.sb(flex.values[0], flex.values[1]); }
        
        public override string ToString() { return string.Format("{0}-to-{1}", values[0], values[1]); ; }

    }
}