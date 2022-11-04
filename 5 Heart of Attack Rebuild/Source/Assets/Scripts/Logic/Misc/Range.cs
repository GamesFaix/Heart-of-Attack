using System;

namespace HOA
{
    /// <summary>
    /// Always inclusive
    /// </summary>
    public struct Range
    {
        public short min, max;

        public Range(short min, short max)
        {
            this.min = min;
            this.max = max;
        }


        public bool Contains(int i) { return (i >= min && i <= max); }

        public override string ToString() { return min + "-to-" + max; }

    }
}