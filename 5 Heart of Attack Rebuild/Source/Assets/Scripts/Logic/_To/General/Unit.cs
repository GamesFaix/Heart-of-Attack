using System;
using System.Collections.Generic;
using HOA.To;
using HOA.Ab;
using HOA.Stats;
using HOA.Fargo;

namespace HOA 
{ 

	public partial class Unit : Token
	{
        public To.Rank rank { get; set; }
        
        private Wallet wallet;
        public void Charge(Price p) { wallet.Charge(p); }
        public bool CanAfford(Price p) { return wallet.CanAfford(p); }

        private Watch watch;
        private Vitality vitality;

        public Stat energy { get { return wallet.energy; } }
        public Stat focus { get { return wallet.focus; } }
        public Stat initiative { get { return watch.initiative; } }
        public bool skipped { get { return watch.skipped; } }
        public Stat health { get { return vitality.health; } }
        public Stat defense { get { return vitality.defense; } }
        public ArgTable<FS, Stat> stats 
        { 
            get 
            { 
                return ArgTable.Stat(
                    Arg.Stat(FS.Energy, energy),
                    Arg.Stat(FS.Focus, focus),
                    Arg.Stat(FS.Initiative, initiative),
                    Arg.Stat(FS.Health, health),
                    Arg.Stat(FS.Defense, defense)); 
            } 
        }

        public Arsenal arsenal { get; private set; }

        public Unit (object source, Species species, To.Rank rank, Species remains = Species.Corpse) 
            : base (source, species)
        {
            this.rank = rank;
            Remains = remains;
            wallet = new Wallet(this, 2);
            watch = new Watch(this, 0);
            vitality = new Vitality(this, 1);
            arsenal = new Arsenal(this);
        }

        public void StatAdd(object source, FS statName, sbyte amount, bool secondary = false) 
        {
            if (!secondary)
                stats[statName].Add(amount);
            else
                stats[statName].Add(amount, 1);
        }
        public void StatSet(object source, FS statName, sbyte amount, bool secondary = false) 
        {
            if (!secondary)
                stats[statName].Set(amount);
            else
                stats[statName].Set(amount, 1);
        }
        
        public bool Damage(object source, sbyte n) { return vitality.Damage(n); }

        #region Learn Abilities

        private void Learn(Ability ability, AbilityArgs args)
        {
            if (ability == null || args == null)
                throw new ArgumentNullException();
            AbilityClosure a = new AbilityClosure(this, ability, args);
            arsenal.Add(a);
        }
        private void LearnFocus()
        { 
            Learn(Ref.Abilities.Focus, new AbilityArgs(this, Price.Cheap,
                Arg.Stat(FS.Damage, Scalar.Dam(this, 1))));
        }

        private void LearnMove(sbyte rangeMax)
        {
            Learn(Ref.Abilities.Move, new AbilityArgs(this, Price.Cheap,
              Arg.Stat(FS.Range0, Flex.Rng(this, rangeMax))));
        }

        private void LearnDart(sbyte rangeMax)
        {
            Learn(Ref.Abilities.Dart, new AbilityArgs(this, Price.Cheap,
              Arg.Stat(FS.Range0, Flex.Rng(this, rangeMax))));
        }

        private void LearnStrike(sbyte damage)
        {
            Learn(Ref.Abilities.Strike, new AbilityArgs(this, Price.Cheap,
              Arg.Stat(FS.Damage, Scalar.Dam(this, 16))));
        }

        private void LearnShoot(sbyte damage, sbyte rangeMax)
        {
            Learn(Ref.Abilities.Shoot, new AbilityArgs(this, Price.Cheap,
              Arg.Stat(FS.Range0, Flex.Rng(this, rangeMax)),
              Arg.Stat(FS.Damage, Scalar.Dam(this, damage))));
        }

        private void LearnCreate(Price price, Species species)
        {
            Learn(Ref.Abilities.Create, new AbilityArgs(this, price, species));
        }

        #endregion



        public void OnTurnStart() { }
        public void OnTurnEnd() { }
	}

}
