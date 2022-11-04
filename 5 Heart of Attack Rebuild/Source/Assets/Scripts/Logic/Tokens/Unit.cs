using HOA.Abilities;
using Ledger = HOA.Collections.Ledger<HOA.Abilities.Ability, HOA.Abilities.AbilityArgs>;

namespace HOA.Tokens
{ 

	public partial class Unit : Token, IEntity
	{
        public readonly UnitRank rank;
        readonly StatSheet stats;
        internal readonly Arsenal arsenal;
        
        internal Unit (object source, Species species, Plane plane,
            UnitRank rank, StatSheetArgs stats, 
            AbilityTuple[] basicAbilities, Ledger specialAbilities,
            TokenFlags flags = TokenFlags.None, Species remains = Species.Corpse) 
            : base (source, species, plane, flags, remains)
        {
            this.rank = rank;
            this.stats = new StatSheet(this, stats);
            arsenal = new Arsenal(this, basicAbilities);
            arsenal.Add(specialAbilities);
        }

        internal Unit(
            object source, Species species, Plane plane,
            UnitRank rank, StatSheetArgs stats, 
            AbilityTuple[] basicAbilities, Ledger specialAbilities,
            Species remains)
            : this(source, species, plane, rank, stats, 
            basicAbilities, specialAbilities, TokenFlags.None, remains)
        { }

        public void OnTurnStart() { }
        public void OnTurnEnd() { }
	}

}
