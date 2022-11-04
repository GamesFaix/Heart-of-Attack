using System;
using System.Collections.Generic;
using HOA.Abilities;
using HOA.Stats;
using HOA.Fargo;

namespace HOA.Tokens
{ 

	public partial class Unit : Token, IEntity
	{
        public UnitRank rank { get; set; }
        public StatSheet stats { get; private set; }
        public void Charge(Price p) { stats.Charge(p); }
        public bool CanAfford(Price p) { return stats.CanAfford(p); }
        public Capped energy { get { return stats.energy; } }
        public Scalar focus { get { return stats.focus; } }
        public Scalar initiative { get { return stats.initiative; } }
        public bool skipped { get { return stats.skipped; } }
        public Capped health { get { return stats.health; } }
        public Stat defense { get { return stats.defense; } }
       
        public Arsenal arsenal { get; private set; }

        public Unit (object source, Species species, UnitRank rank, Species remains = Species.Corpse) 
            : base (source, species)
        {
            this.rank = rank;
            Remains = remains;
            stats = new Tokens.StatSheet(this, 0, 0, 0);
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
        
        public bool Damage(object source, sbyte n) { return stats.Damage(n); }

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
            Learn(Content.Abilities.Focus, new AbilityArgs(this, Price.Cheap,
                Arg.Stat(FS.Damage, new Scalar(1))));
        }

        private void LearnMove(sbyte rangeMax)
        {
            Learn(Content.Abilities.Move, new AbilityArgs(this, Price.Cheap,
              Arg.Stat(FS.Range0, new Flex(0, rangeMax))));
        }

        private void LearnDart(sbyte rangeMax)
        {
            Learn(Content.Abilities.Dart, new AbilityArgs(this, Price.Cheap,
              Arg.Stat(FS.Range0, new Flex(0, rangeMax))));
        }

        private void LearnStrike(sbyte damage)
        {
            Learn(Content.Abilities.Strike, new AbilityArgs(this, Price.Cheap,
              Arg.Stat(FS.Damage, new Scalar(damage))));
        }

        private void LearnShoot(sbyte damage, sbyte rangeMax)
        {
            Learn(Content.Abilities.Shoot, new AbilityArgs(this, Price.Cheap,
              Arg.Stat(FS.Range0, new Flex(0, rangeMax)),
              Arg.Stat(FS.Damage, new Scalar(damage))));
        }

        private void LearnCreate(Price price, Species species)
        {
            Learn(Content.Abilities.Create, new AbilityArgs(this, price, species));
        }

        #endregion



        public void OnTurnStart() { }
        public void OnTurnEnd() { }
	}

}
