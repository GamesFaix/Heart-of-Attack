using System;

namespace HOA.To
{

    public class Watch : TokenComponent
    {
        public Stat Initiative { get; private set; }

        public bool Skipped { get; set; }

        public Watch(Unit thisToken, int i = 0)
            : base (thisToken)
        {
            Initiative = Stat.Initiative(thisToken, i);
            Skipped = false;
        }

        public override string ToString() { return ThisToken + "'s Watch"; }
    }
}
