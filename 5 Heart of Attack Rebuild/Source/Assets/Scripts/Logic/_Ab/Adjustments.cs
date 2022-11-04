using System;
using System.Collections.Generic;
using HOA.St;

namespace HOA.Ab {
	
    public delegate void Adjustment (Ability a, Args arg);


    public static class Adjustments 
    {
        public static Adjustment Filter0 = (a, arg) => { a.Aims[0].filter = arg.filter; };

        public static Adjustment Body0 = (a, arg) => { a.Aims[0].body = () => Ref.Tokens.templates[arg.species]; };

        public static Adjustment SpeciesName(string basicName) { return (a, arg) => { a.name = basicName + " " + arg.species; }; }

        public static Adjustment FocusRangeBoost0 = (a, arg) =>
        {
            sbyte bonus = (sbyte)(arg.stat["Boost"] * arg.user.focus);
            Flex range = arg.stat["Range0"] as Flex;

            a.Aims[0].range = Range.sb(range.Min, (sbyte)(range.Current + bonus));
        };


        public static Adjustment Standard = (a, arg) =>
        {
            if (arg.stat.Contains("Range0"))
                a.Aims[0].range = (Range<sbyte>)(arg.stat["Range0"] as Flex);
            if (arg.stat.Contains("Range1"))
                a.Aims[1].range = (Range<sbyte>)(arg.stat["Range1"] as Flex);

            if (arg.stat.Contains("Select0"))
                a.Aims[0].selectionCount = (Range<sbyte>)(arg.stat["Select0"] as Flex);
            if (arg.stat.Contains("Select1"))
                a.Aims[1].selectionCount = (Range<sbyte>)(arg.stat["Select1"] as Flex);
        };
    }
}