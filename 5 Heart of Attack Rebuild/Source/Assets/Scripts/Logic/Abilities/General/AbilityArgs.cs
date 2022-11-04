using System;
using System.Linq;

namespace HOA.Ab
{
    public class AbilityArgs : ClosureArgs, IEquatable<AbilityArgs>
    {
        #region Properties

        public Unit user { get; private set; }
        public Price price { get; private set; }
        public int damage { get; private set; }
        public Range<byte>[] ranges { get; private set; }
        public Range<byte> range { get { return ranges.Single(); } }

        public int[] values { get; private set; }
        public int value { get { return values.Single(); } }
        public To.Species species { get; private set; }
        public To.Stats stat { get; private set; }
        public Player player {get; private set;}

        public Predicate<IEntity>[] filters { get; private set; }
        public Predicate<IEntity> filter { get { return filters.Single(); } }

        public EffectConstructor[] effects { get; private set; }
        public EffectConstructor effect { get { return effects.Single(); } }

        #endregion

        #region Constructors

        private AbilityArgs(Unit user, Price price) 
        {
            this.user = user;
            this.price = price;

            damage = 0;
            ranges = new Range<byte>[0];
            values = new int[0]; 
            species = To.Species.None;
            stat = To.Stats.None;
            player = null;
            filters = new Predicate<IEntity>[0];
            effects = new EffectConstructor[0];
        }

        public AbilityArgs(Unit user, Price price, Range<byte> range)
            : this(user, price)
        { ranges = new Range<byte>[1] { range }; }

        public AbilityArgs(Unit user, Price price, int damage)
            : this(user, price)
        { this.damage = damage; }

        public AbilityArgs(Unit user, Price price, Range<byte> range, int damage)
            : this(user, price, range)
        { this.damage = damage; }

        public AbilityArgs(Unit user, Price price, Predicate<IEntity> filter, int damage)
            : this(user, price, damage)
        { filters = new Predicate<IEntity>[1] { filter }; }

        public AbilityArgs(Unit user, Price price, Range<byte> range, Predicate<IEntity> filter, int damage)
            : this(user, price, range, damage)
        { filters = new Predicate<IEntity>[1] { filter }; }

        public AbilityArgs(Unit user, Price price, Range<byte> range0, Predicate<IEntity> filter, Range<byte> range1)
            : this(user, price)
        {
            ranges = new Range<byte>[2] { range0, range1 };
            filters = new Predicate<IEntity>[1] { filter };
        }


        public AbilityArgs(Unit user, Price price, To.Species species)
            : this(user, price)
        { this.species = species; }

        public AbilityArgs(Unit user, Price price, Range<byte> range, To.Species species)
            : this(user, price, range)
        { this.species = species; }

        public AbilityArgs(Unit user, Price price, Predicate<IEntity> filter, To.Species species)
            : this(user, price, species)
        { filters = new Predicate<IEntity>[1] { filter }; }

        #endregion

        #region IEquatable

        public bool Equals(AbilityArgs other)
        {
            if (other as object == null)
                return false;
            return (user == other.user
                && price == other.price
                && damage == other.damage
                && values == other.values
                && ranges == other.ranges
                && stat == other.stat
                && species == other.species
                && player == other.player
                
                && filters == other.filters
                && effects == other.effects);
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
        
    }
}