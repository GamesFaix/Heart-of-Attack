using System;

namespace HOA.Tokens
{ 
    /// <summary> Flag enum of Token planes.  None = 0. </summary>
    [Flags]
    public enum Plane : byte 
    { 
        None = 0, 
        Sunken = 1, 
        Ground = 2, 
        Air = 4, 
        Ethereal = 8,
        Terrain = 16,
        HalfSunk = (Sunken|Ground), 
        Tall = (Ground|Air), 
        All = (Sunken|Ground|Air|Ethereal) 
    }

    /// <summary> Provides set-like functionality to Plane. </summary>
    public static class PlaneExtensionMethods
    {
        /// <summary> Does this Plane contain any of the flags of the argument?  </summary>
        /// <returns>True if any flags match.</returns>
        public static bool ContainsAny (this Plane p, Plane query) 
        {
            return !((p & query) == Plane.None);
        }
        /// <summary> Does the argument contain all the flags of This? </summary>
        /// <returns>True if argument has all of This's flags.</returns>
        public static bool SubsetOf(this Plane p, Plane other)
        {
            return (p & other) == p;
        }

        /// <summary> Does This contain all the argument's flags? </summary>
        /// <returns>True if This has all of argument's flags.</returns>
        public static bool SupersetOf(this Plane p, Plane other)
        {
            return (p & other) == other;
        }

        /// <summary> Does the argument contain all the flags of This, plus some others? </summary>
        /// <returns>True if argument has all of This's flags and more.</returns>
        public static bool ProperSubsetOf(this Plane p, Plane other)
        {
            return (p.SubsetOf(other) && !p.SupersetOf(other));
        }

        /// <summary> Does This contain all the flags of the argument, plus some others? </summary>
        /// <returns>True if This has all of the argument's flags and more.</returns>
        public static bool ProperSupersetOf(this Plane p, Plane other)
        {
            return (p.SupersetOf(other) && !p.SubsetOf(other));
        }

    }
}