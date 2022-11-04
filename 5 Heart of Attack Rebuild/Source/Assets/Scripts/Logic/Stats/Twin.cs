using System;
using System.Collections.Generic;

namespace HOA.Stats
{

    public class Twin : Stat
    {

        private Twin(sbyte val0, sbyte val1)
            : base(null, val0, val1)
        { }

        public override string ToString() { return string.Format("{0}, {1}", values[0], values[1]); ; }

        public static Twin Price(Tokens.Unit self, Price price)
        { return new Twin(price.Energy, price.Focus); }

    }
}