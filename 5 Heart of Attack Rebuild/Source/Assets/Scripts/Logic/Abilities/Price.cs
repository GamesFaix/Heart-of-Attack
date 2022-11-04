using System;

namespace HOA
{
    /// <summary>
    /// Price of an Ability
    /// </summary>
    public struct Price : IComparable<Price>, IEquatable<Price>
    {
        /// <summary>
        /// Energy cost
        /// </summary>
        public sbyte Energy { get; private set; }
        /// <summary>
        /// Focus cost
        /// </summary>
        public sbyte Focus { get; private set; }
        /// <summary>
        /// Energy + Focus
        /// </summary>
        public sbyte Total { get { return (sbyte)(Energy + Focus); } }

        /// <summary>
        /// Create new Price
        /// </summary>
        /// <param name="energy"></param>
        /// <param name="focus"></param>
        public Price(sbyte energy, sbyte focus)
        {
            Energy = energy;
            Focus = focus;
        }

        /// <summary>
        /// Price(0,0)
        /// </summary>
        public static Price Free { get { return new Price(0, 0); } }
        /// <summary>
        /// Price(1,0)
        /// </summary>
        public static Price Cheap { get { return new Price(1, 0); } }
        /// <summary>
        /// Price(1,1)
        /// </summary>
        public static Price One { get { return new Price(1, 1); } }
       
        /// <summary>
        /// Returns "([Energy]E / Focus[F])"
        /// </summary>
        /// <returns></returns>
        public override string ToString() { return "(" + Energy + "E / " + Focus + "F)"; }

        /// <summary>
        /// Compares first based on Total, then by Energy if tied.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Price other)
        {
            if (Total < other.Total) { return -1; }
            else if (Total > other.Total) { return 1; }
            else
            {
                if (Energy > other.Energy) { return -1; }
                else if (Energy < other.Energy) { return 1; }
                else { return 0; }
            }
        }

        /// <summary>
        /// True if Energy and Focus equal
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Price other) { return ((Energy == other.Energy && Focus == other.Focus) ? true : false); }
        /// <summary>
        /// True if obj is Price and Energy & Focus are equal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(System.Object obj)
        {
            if (obj == null) { return false; }
            if (!(obj is Price)) { return false; }
            Price other = (Price)obj;
            return Equals(other);
        }

        /// <summary>
        /// Simple hash
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() { return (Energy << 16) & Focus; }

        /// <summary>
        /// a.Equals(b)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Price a, Price b) { return a.Equals(b); }
        /// <summary>
        /// !a.Equals(b)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Price a, Price b) { return !(a.Equals(b)); }

    }
}
