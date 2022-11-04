using System;
using System.Linq;

namespace HOA.Abilities
{
    public class AbilityArgs : IEquatable<AbilityArgs>
    {
        public Tokens.Species species { get; private set; }

        public int[] values { get; private set; }
        public int value { get { return values.Single(); } }

        private AbilityArgs() {
            species = Tokens.Species.None;
            values = new int[0];            
        }

        public AbilityArgs(Tokens.Species species)
            : this()
        {this.species = species;}

        public AbilityArgs(params int[] values)
            : this()
        { this.values = values; }


        #region IEquatable

        public bool Equals(AbilityArgs other)
        {
            if (species != other.species)
                return false;
            return true;
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