using System;
using System.Collections.Generic;

namespace HOA.Ab {
	
    public delegate void Adjustment (Ability a, Args arg);


    public static class Adjustments 
    {
        public static Adjustment None = (a, arg) => { };

        public static Adjustment Range0 = (a, arg) => { a.Aims[0].range = arg.ranges[0]; };
        public static Adjustment Range1 = (a, arg) => { a.Aims[1].range = arg.ranges[1]; };

        public static Adjustment Filter0 = (a, arg) => { a.Aims[0].filter = arg.filter; };

        public static Adjustment Body0 = (a, arg) => { a.Aims[0].body = () => Ref.Tokens.templates[arg.species]; };

        public static Adjustment SpeciesName(string basicName) { return (a, arg) => { a.name = basicName + " " + arg.species; }; }

        public static Adjustment SelectionCount0 = (a, arg) => { a.Aims[0].selectionCount = arg.range; };

        public static Adjustment FocusRangeBoost0 = (a, arg) =>
        {
            int bonus = arg.damage * arg.user.Focus;
            a.Aims[0].range = Range.b(arg.range.min, (byte)(arg.range.max + bonus));
        };
	}
}