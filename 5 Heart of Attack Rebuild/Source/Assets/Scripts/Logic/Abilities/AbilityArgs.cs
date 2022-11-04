using System;
using System.Linq;
using System.Collections.Generic;
using HOA.Args;
using HOA.Stats;
using Species = HOA.Tokens.Species;
using Unit = HOA.Tokens.Unit;

namespace HOA.Abilities
{
    public partial class AbilityArgs : IEquatable<AbilityArgs>
    {
        #region Properties

        ArgTable<RS, Stats.Stat> stats;
        ArgTable<RL, string> labels;
        ArgTable<RF, FilterBuilder> filters;
        ArgTable<RE, Effects.EffectBuilder> effects;
        ArgTable<RT, IEntity> targets;

        public Species species { get; private set; }
        public Player player {get; private set;}

        #endregion

        #region Constructors

        private AbilityArgs(Price price) 
        {
            targets = ArgTable.Target();
            stats = ArgTable.Stat(
                Arg.Stat(RS.Price, Twin.Price(price)));
            labels = Args.ArgTable.Text();
            filters = ArgTable.Filter();
            effects = ArgTable.Effect();

            species = Species.None;
            player = null;
        }

        public AbilityArgs(Price price, 
            params Arg<RS, Stat>[] stats)
            : this(price)
        { this.stats = new ArgTable<RS, Stat>(stats); }

        public AbilityArgs(Price price,
            Arg<RF, FilterBuilder> filter, 
            params Arg<RS, Stat>[] stats)
            : this(price, stats)
        { filters.Add(filter); }

        public AbilityArgs(Price price, 
            Species species, 
            params Arg<RS, Stat>[] stats)
            : this(price, stats)
        { this.species = species; }

        public AbilityArgs(Price price,
            Arg<RF, FilterBuilder> filter, 
            Species species, 
            params Arg<RS, Stat>[] stats)
            : this(price, species, stats)
        { filters.Add(filter); }

        #endregion

        #region IEquatable

        public bool Equals(AbilityArgs other)
        {
            if (other as object == null)
                return false;
            return (
                targets == other.targets
                && stats == other.stats
                && labels == other.labels

                && filters == other.filters
                && effects == other.effects

                && species == other.species
                && player == other.player);
                
        }

        public override bool Equals(object other) 
        {
            return (other is AbilityArgs
                && (other as AbilityArgs).Equals(this));
        }

        public override int GetHashCode()
        {
            Log.Debug("No custom implementation.");
            return base.GetHashCode();
        }

        public static bool operator ==(AbilityArgs a, AbilityArgs b) { return a.Equals(b); }
        public static bool operator !=(AbilityArgs a, AbilityArgs b) { return !a.Equals(b); }

        #endregion

        #region Indexers

        public Stat this[RS stat] { get { return stats[stat]; } }

        public IEntity this[RT target] { get { return targets[target]; } }

        public FilterBuilder this[RF filter] { get { return filters[filter]; } }

        public string this[RL label] { get { return labels[label]; } }

        public Effects.EffectBuilder this[RE effect] { get { return effects[effect]; } }

        #endregion

        public bool Contains(RS stat) { return stats.Contains(stat); }

        public bool Contains(RT target) { return targets.Contains(target); }

        public bool Contains(RF filter) { return filters.Contains(filter); }

        public bool Contains(RE effect) { return effects.Contains(effect); }

        public bool Contains(RL label) { return labels.Contains(label); }
    }
}