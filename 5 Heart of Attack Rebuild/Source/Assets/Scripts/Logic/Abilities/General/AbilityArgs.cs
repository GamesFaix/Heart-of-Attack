using System;

namespace HOA.Abilities
{
    public class AbilityArgs : IEquatable<AbilityArgs>
    {
        public Tokens.Species species { get; private set; }

        public AbilityArgs(Tokens.Species species)
        {
            this.species = species;


        }

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