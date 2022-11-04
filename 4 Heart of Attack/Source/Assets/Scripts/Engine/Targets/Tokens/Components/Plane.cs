using System;

namespace HOA {
    
    [Flags]
    public enum Plane : byte 
    { 
        None = 0, 
        Sunken = 1, 
        Ground = 2, 
        Air = 4, 
        Ethereal = 8,
        HalfSunk = (Sunken|Ground), 
        Tall = (Ground|Air), 
        All = (Sunken|Ground|Air|Ethereal) 
    }

    public static class PlaneExtensionMethods
    {
        public static bool ContainsAny (this Plane p, Plane query) 
        {
            return !((p & query) == Plane.None);
        }

        public static void Draw (this Plane plane, Panel p) 
        {
            InspectorInfo.Plane(plane, p);
        }


    }
}