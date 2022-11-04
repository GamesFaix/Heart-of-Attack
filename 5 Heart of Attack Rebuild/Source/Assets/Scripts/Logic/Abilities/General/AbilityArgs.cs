using System;
using System.Linq;

namespace HOA.Abilities
{
    public class AbilityArgs : IEquatable<AbilityArgs>
    {
        public string name { get; private set; }
        public Rank rank { get; private set; }
        public Price price { get; private set; }
        
        public int[] values { get; private set; }
        public int value { get { return values.Single(); } }
        public Tokens.Species species { get; private set; }
        public Tokens.Stats stat { get; private set; }
        
        public Description desc;

        public AbilityArgs(string name, Rank rank, Price price, int[] values,
            Tokens.Species species = Tokens.Species.None, 
            Tokens.Stats stat = Tokens.Stats.None
            ) 
        {
            this.name = name;
            this.rank = rank;
            this.price = price;
            this.values = values; 
            this.species = species;
            this.stat = stat;
            this.desc = Scribe.Write("Does stuff.");
        }

        public AbilityArgs(string name, Rank rank, Price price)
            : this(name, rank, price, new int[0]) { }
        public AbilityArgs(string name, Rank rank, Price price, int value)
            : this(name, rank, price, new int[1] { value }) { }
        public AbilityArgs(string name, Rank rank, Price price, int value0, int value1)
            : this(name, rank, price, new int[2] { value0, value1 }) { }
        public AbilityArgs(string name, Rank rank, Price price, int value0, int value1, int value2)
            : this(name, rank, price, new int[3] { value0, value1, value2 }) { }

        public AbilityArgs(string name, Rank rank, Price price, Tokens.Species species)
            : this(name, rank, price, new int[0], species) { }

        public AbilityArgs(string name, Rank rank, Price price, Tokens.Stats stat, int value)
            : this(name, rank, price, new int[1] { value }, Tokens.Species.None, stat) { }

        #region IEquatable

        public bool Equals(AbilityArgs other)
        {
            if (other as object == null)
                return false;
            return (name == other.name
                && rank == other.rank
                && price == other.price
                && desc == other.desc
                && values == other.values
                && stat == other.stat
                && species == other.species);
        }

        public override bool Equals(object other) 
        {
            return (other is AbilityArgs 
                && (other as AbilityArgs).Equals(this));
        }

        public override int GetHashCode()
        {
            Debug.Log("No custom implementation.");
            return base.GetHashCode();
        }

        public static bool operator ==(AbilityArgs a, AbilityArgs b) { return a.Equals(b); }
        public static bool operator !=(AbilityArgs a, AbilityArgs b) { return !a.Equals(b); }

        #endregion

    }
}