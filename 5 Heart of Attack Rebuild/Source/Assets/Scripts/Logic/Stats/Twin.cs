using System;
using System.Collections.Generic;

namespace HOA.Stats
{

    public class Twin : BiStat
    {
        public sbyte first { get { return primary.current; } }
        public sbyte second { get { return secondary.current; } }

        private Twin(Unit self, string name, sbyte normal, sbyte second)
            : base(self, name, normal, second)
        { }

        public override string ToString() { return string.Format("{0}, {1}", primary, secondary); ; }

        public static Twin Price(Unit self, sbyte energy, sbyte focus)
        { return new Twin(self, "Price", energy, focus); }

        public static Twin Price(Unit self, Price price)
        { return new Twin(self, "Price", price.Energy, price.Focus); }

    }
}