using HOA.Stats;

namespace HOA.To
{

    public class Wallet : TokenComponent
    {
        public Capped energy { get; protected set; }
        public Stat focus { get; protected set; }

        private Wallet(Unit thisToken, Capped energy, Stat focus)
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

        public bool CanAfford(Price p) { return (energy >= p.Energy && focus >= p.Focus); }

        public void Charge(Price p)
        {
            energy += (sbyte)(0 - p.Energy);
            focus += (sbyte)(0 - p.Focus);
        }

        public void Refund(Price p)
        {
            energy += p.Energy;
            focus += p.Focus;
        }

        public override string ToString() { return ThisToken + "'s Wallet"; }
    }
}
