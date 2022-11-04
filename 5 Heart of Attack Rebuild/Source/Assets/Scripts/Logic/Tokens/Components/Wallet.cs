using HOA.To.St;

namespace HOA.To
{

    public class Wallet : TokenComponent
    {
        public Stat<sbyte> energy { get; protected set; }
        public Stat<sbyte> focus { get; protected set; }

        private Wallet(Unit thisToken, Stat<sbyte> energy, Stat<sbyte> focus)
            : base ((Token)thisToken)
        {
            this.energy = energy;
            this.focus = focus;
        }

        public Wallet(Unit thisToken, sbyte energy = 2)
            : this(thisToken, Capped.En(thisToken, energy), Scalar.Fo(thisToken))
        { }

        public static Wallet InitiativeBoost(Unit thisToken, sbyte energy = 2)
        { return new Wallet(thisToken, Capped.En(thisToken, energy), Booster.FoIn(thisToken)); }

        public static Wallet DefenseBoost(Unit thisToken, sbyte energy = 2)
        { return new Wallet(thisToken, Capped.En(thisToken, energy), Booster.FoDef(thisToken)); }

        public void FillEnergy()
        { (energy as Capped<sbyte>).Fill(); }

        public bool CanAfford(Price p) { return (energy >= p.Energy && focus >= p.Focus); }

        public void Charge(Price p)
        {
            System.Func<sbyte, sbyte, sbyte> adder = (sbyte a, sbyte b) => { return (sbyte)(a + b); };
            energy.Add(adder, (sbyte)(0 - p.Energy));
            focus.Add(adder, (sbyte)(0 - p.Focus));
        }

        public void Refund(Price p)
        {
            System.Func<sbyte, sbyte, sbyte> adder = (sbyte a, sbyte b) => { return (sbyte)(a + b); };
            energy.Add(adder, (sbyte)(p.Energy));
            focus.Add(adder, (sbyte)(p.Focus));
        }

        public override string ToString() { return ThisToken + "'s Wallet"; }
    }
}
