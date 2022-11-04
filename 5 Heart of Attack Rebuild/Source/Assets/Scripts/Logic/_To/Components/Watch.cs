using System;
using HOA.St;

namespace HOA.To
{

    public class Watch : TokenComponent
    {
        public Stat initiative { get; private set; }

        public bool skipped { get; set; }

        public Watch(Unit thisToken, sbyte initiative = 0)
            : base (thisToken)
        {
            this.initiative = Scalar.In(thisToken, initiative);
            skipped = false;
        }

        public override string ToString() { return ThisToken + "'s Watch"; }
    }
}
