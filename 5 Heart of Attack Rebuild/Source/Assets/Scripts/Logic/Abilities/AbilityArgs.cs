using System;
using System.Linq;
using System.Collections.Generic;
using HOA.Fargo;
using HOA.Stats;
using Species = HOA.Tokens.Species;
using Unit = HOA.Tokens.Unit;

namespace HOA.Abilities
{
    public class AbilityArgs : ClosureArgs, IEquatable<AbilityArgs>
    {
        #region Properties

        ArgTable<FS, Stat> stats;
        ArgTable<FX, string> labels;
        ArgTable<FF, Predicate<IEntity>> filters;
        ArgTable<FE, Ef.Builder> effects;
        ArgTable<FT, IEntity> targets;

        public Species species { get; private set; }
        public Player player {get; private set;}

        #endregion

        #region Constructors

        private AbilityArgs(Unit user, Price price) 
        {
            targets = ArgTable.Target(
                Arg.Target(FT.User, user));
            stats = ArgTable.Stat(
                Arg.Stat(FS.Price, Twin.Price(user, price)));
            labels = ArgTable.Text();
            filters = ArgTable.Filter();
            effects = ArgTable.Effect();

            species = Species.None;
            player = null;
        }

        public AbilityArgs(Unit user, Price price, 
            params Arg<FS, Stat>[] stats)
            : this(user, price)
        { this.stats = new ArgTable<FS, Stat>(stats); }

        public AbilityArgs(Unit user, Price price, 
            Arg<FF, Predicate<IEntity>> filter, 
            params Arg<FS, Stat>[] stats)
            : this(user, price, stats)
        { filters.Add(filter); }

        public AbilityArgs(Unit user, Price price, 
            Species species, 
            params Arg<FS, Stat>[] stats)
            : this(user, price, stats)
        { this.species = species; }

        public AbilityArgs(Unit user, Price price, 
            Arg<FF, Predicate<IEntity>> filter, 
            Species species, 
            params Arg<FS, Stat>[] stats)
            : this(user, price, species, stats)
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

        public Stat this[FS fStat] { get { return stats[fStat]; } }

        public IEntity this[FT fTarget] { get { return targets[fTarget]; } }

        public Predicate<IEntity> this[FF fFilter] { get { return filters[fFilter]; } }

        public string this[FX fText] { get { return labels[fText]; } }

        public Ef.Builder this[FE fEffect] { get { return effects[fEffect]; } }

        #endregion

        public bool Contains(FS fStat) { return stats.Contains(fStat); }

        public bool Contains(FT fTarget) { return targets.Contains(fTarget); }

        public bool Contains(FF fFilter) { return filters.Contains(fFilter); }

        public bool Contains(FE fEffect) { return effects.Contains(fEffect); }

        public bool Contains(FX fText) { return labels.Contains(fText); }
    }
}