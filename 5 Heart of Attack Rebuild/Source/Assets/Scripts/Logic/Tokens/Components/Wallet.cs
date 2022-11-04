
namespace HOA.Tokens
{

    public class Wallet : TokenComponent
    {
        public Stat Energy { get; protected set; }
        public Stat Focus { get; protected set; }

        private Wallet(Unit thisToken, Stat energy, Stat focus)
            : base ((Token)thisToken)
        {
            Energy = energy;
            Focus = focus;
        }

        public Wallet(Unit thisToken, int energy = 2)
            : this(thisToken, Stat.Energy(thisToken, energy), Stat.Focus(thisToken)) { }

        public static Wallet InitiativeBoost(Unit thisToken, int energy = 2)
        {
            return new Wallet(thisToken,
                Stat.Energy(thisToken, energy),
                Stat.FocusAddsInitiative(thisToken));
        }

        public static Wallet DefenseBoost(Unit thisToken, int energy, int defenseCap = 4)
        {
            return new Wallet(thisToken,
                Stat.Energy(thisToken, energy),
                Stat.FocusAddsDefense(thisToken, defenseCap));
        }


        public void FillEnergy()
        {
            if (Energy.Max > Energy)
                Energy.Add((Energy.Max - Energy));
        }

        public bool CanAfford(Price p) { return (Energy >= p.Energy && Focus >= p.Focus); }

        public void Charge(Price p)
        {
            Energy.Add(0 - p.Energy);
            Focus.Add(0 - p.Focus);
        }

        public void Refund(Price p)
        {
            Energy.Add(p.Energy);
            Focus.Add(p.Focus);
        }

        public override string ToString() { return ThisToken + "'s Wallet"; }
    }
}
