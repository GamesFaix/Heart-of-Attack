using System;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Abilities;

namespace HOA 
{ 

	public partial class Unit : Token
	{
        public Rank rank { get; set; }
        
        private Wallet wallet;
        public void Charge(Price p) { wallet.Charge(p); }
        public bool CanAfford(Price p) { return wallet.CanAfford(p); }

        private Watch watch;
        private Vitality vitality;

        public Stat Energy { get { return wallet.Energy; } }
        public Stat Focus { get { return wallet.Focus; } }
        public Stat Initiative { get { return watch.Initiative; } }
        public bool Skipped { get { return watch.Skipped; } }
        public Stat Health { get { return vitality.Health; } }
        public Stat Defense {get {return vitality.Defense;}}

        public Arsenal arsenal { get; private set; }

        public Unit (ITokenCreator creator, Species species, Rank rank, Species remains = Species.Corpse) 
            : base (creator, species)
        {
            this.rank = rank;
            Remains = remains;
            wallet = new Wallet(this, 2);
            watch = new Watch(this, 0);
            vitality = new Vitality(this, 1);
            arsenal = new Arsenal(this);
        }

        public void StatAdd(IEffect effect, Stats stat, int n) { }
        public void StatAddMax(IEffect effect, Stats stat, int n) { }
        public void StatSet(IEffect effect, Stats stat, int n) { }
        public void StatSetMax(IEffect effect, Stats stat, int n) { }
        public bool Damage(IEffect effect, int n) { return vitality.Damage(n); }

        public void OnTurnStart() { }
        public void OnTurnEnd() { }
	}

}
