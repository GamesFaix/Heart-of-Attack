using System;
using System.Linq;
using HOA.St;
using System.Collections.Generic;

namespace HOA.Ab
{
    public class Args : ClosureArgs, IEquatable<Args>
    {
        #region Properties

        public Unit user { get; private set; }
        public Price price { get; private set; }
        public Bundle stat { get; private set; }
        public Dictionary<string, string> str { get; private set; }

        public To.Species species { get; private set; }
        public Player player {get; private set;}

        public Predicate<IEntity>[] filters { get; private set; }
        public Predicate<IEntity> filter { get { return filters.Single(); } }

        public Ef.Builder[] effects { get; private set; }
        public Ef.Builder effect { get { return effects.Single(); } }

        #endregion

        #region Constructors

        private Args(Unit user, Price price) 
        {
            this.user = user;
            this.price = price;

            stat = new Bundle();
            species = To.Species.None;
            str = new Dictionary<string, string>();
            player = null;
            filters = new Predicate<IEntity>[0];
            effects = new Ef.Builder[0];
        }

        public Args(Unit user, Price price, params Stat[] stats)
            : this(user, price)
        { stat = new Bundle(stats); }

        public Args(Unit user, Price price, Predicate<IEntity> filter, params Stat[] stats)
            : this(user, price, stats)
        { filters = new Predicate<IEntity>[1] { filter }; }

        public Args(Unit user, Price price, To.Species species, params Stat[] stats)
            : this(user, price, stats)
        { this.species = species; }

        public Args(Unit user, Price price, Predicate<IEntity> filter, To.Species species, params Stat[] stats)
            : this(user, price, species, stats)
        { filters = new Predicate<IEntity>[1] { filter }; }

        #endregion

        #region IEquatable

        public bool Equals(Args other)
        {
            if (other as object == null)
                return false;
            return (user == other.user
                && price == other.price
                && stat == other.stat
                && str == other.str
                && species == other.species
                && player == other.player
                
                && filters == other.filters
                && effects == other.effects);
        }

        public override bool Equals(object other) 
        {
            return (other is Args
                && (other as Args).Equals(this));
        }

        public override int GetHashCode()
        {
            Log.Debug("No custom implementation.");
            return base.GetHashCode();
        }

        public static bool operator ==(Args a, Args b) { return a.Equals(b); }
        public static bool operator !=(Args a, Args b) { return !a.Equals(b); }

        #endregion
        
    }
}