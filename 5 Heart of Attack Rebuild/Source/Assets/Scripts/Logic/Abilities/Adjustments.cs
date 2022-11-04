using System;
using System.Collections.Generic;
using HOA.To.St;

namespace HOA.Ab {
	
    public delegate void Adjustment (Ability a, Args arg);


    public static class Adjustments 
    {
        public static Adjustment None = (a, arg) => { };

        public static Adjustment Range0 = (a, arg) => { a.Aims[0].range = (Range<sbyte>)(arg["Range0"] as Flex); };
        public static Adjustment Range1 = (a, arg) => { a.Aims[1].range = (Range<sbyte>)(arg["Range1"] as Flex); };

        public static Adjustment Filter0 = (a, arg) => { a.Aims[0].filter = arg.filter; };

        public static Adjustment Body0 = (a, arg) => { a.Aims[0].body = () => Ref.Tokens.templates[arg.species]; };

        public static Adjustment SpeciesName(string basicName) { return (a, arg) => { a.name = basicName + " " + arg.species; }; }

        public static Adjustment SelectionCount0 = (a, arg) => { a.Aims[0].selectionCount = (Range<sbyte>)(arg["Selections0"] as Flex); };

        public static Adjustment FocusRangeBoost0 = (a, arg) =>
        {
            sbyte bonus = (sbyte)(arg["Boost"] * arg.user.focus);
            Flex range = arg["Range0"] as Flex;

            a.Aims[0].range = Range.sb(range.Min, (sbyte)(range.Current + bonus));
        };
	}
}