using System;
using System.Collections.Generic;
using HOA.To;
using HOA.Ab;
using HOA.St;

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
        public Bundle stats { get { return new Bundle(energy, focus, initiative, health, defense); } }

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

        public void StatAdd(object source, string statName, sbyte n, bool secondary = false) 
        {
            if (!secondary)
                stats[statName].Add(n);
            else
            {
                if (stats[statName] is Capped)
                    ((Capped)stats[statName]).AddCap(n);
                if (stats[statName] is Flex)
                    ((Flex)stats[statName]).AddMin(n);
            }
        }
        public void StatSet(object source, string statName, sbyte n, bool secondary = false) 
        {
            if (!secondary)
                stats[statName].Set(n);
            else
            {
                if (stats[statName] is Capped)
                    ((Capped)stats[statName]).SetCap(n);
                if (stats[statName] is Flex)
                    ((Flex)stats[statName]).SetMin(n);
            }
        }
        
        public bool Damage(object source, sbyte n) { return vitality.Damage(n); }

        #region Learn Abilities

        private void Learn(Ability ability, Ab.Args args)
        {
            if (ability == null || args == null)
                throw new ArgumentNullException();
            Ab.Closure a = new Ab.Closure(this, ability, args);
            arsenal.Add(a);
        }
        private void LearnFocus()
        { Learn(Ref.Abilities.Focus, new Ab.Args(this, Price.Cheap, Scalar.Dam(this, 1))); }

        private void LearnMove(sbyte rangeMax)
        { Learn(Ref.Abilities.Move, new Ab.Args(this, Price.Cheap, Flex.Rng(this, rangeMax))); }

        private void LearnDart(sbyte rangeMax)
        { Learn(Ref.Abilities.Dart, new Ab.Args(this, Price.Cheap, Flex.Rng(this, rangeMax))); }

        private void LearnStrike(sbyte damage)
        { Learn(Ref.Abilities.Strike, new Ab.Args(this, Price.Cheap, Scalar.Dam(this, 16))); }

        private void LearnShoot(sbyte damage, sbyte rangeMax)
        { Learn(Ref.Abilities.Shoot, new Ab.Args(this, Price.Cheap, Flex.Rng(this, rangeMax), Scalar.Dam(this, damage))); }

        private void LearnCreate(Price price, Species species)
        { Learn(Ref.Abilities.Create, new Ab.Args(this, price, species)); }

        #endregion



        public void OnTurnStart() { }
        public void OnTurnEnd() { }
	}

}
