using System;
using System.Collections.Generic;
using HOA.Stats;
using HOA.Fargo;
using Unit = HOA.Tokens.Unit;

namespace HOA.Abilities {
	
    public delegate void Adjustment (Ability a, AbilityArgs arg);


    public static class Adjustments 
    {
        public static Adjustment Body0 = (a, arg) => { a.Aims[0].body = () => Content.Tokens.templates[arg.species]; };

        public static Adjustment SpeciesName(string basicName) { return (a, arg) => { a.name = basicName + " " + arg.species; }; }

        public static Adjustment FocusRangeBoost0 = (a, arg) =>
        {
            sbyte bonus = (sbyte)(arg[FS.Boost] * (arg[FT.User] as Unit).focus);
            Flex range = arg[FS.Range0] as Flex;

            a.Aims[0].range = Range.sb(range[0], (sbyte)(range[1] + bonus));
        };


        public static Adjustment Standard = (a, arg) =>
        {
            if (arg.Contains(FS.Range0))
                a.Aims[0].range = (Range<sbyte>)(arg[FS.Range0] as Flex);
            if (arg.Contains(FS.Range1))
                a.Aims[1].range = (Range<sbyte>)(arg[FS.Range1] as Flex);
            if (arg.Contains(FS.Range2))
                a.Aims[2].range = (Range<sbyte>)(arg[FS.Range1] as Flex);

            if (arg.Contains(FS.Select0))
                a.Aims[0].selectionCount = (Range<sbyte>)(arg[FS.Select0] as Flex);
            if (arg.Contains(FS.Select1))
                a.Aims[1].selectionCount = (Range<sbyte>)(arg[FS.Select1] as Flex);
            if (arg.Contains(FS.Select2))
                a.Aims[2].selectionCount = (Range<sbyte>)(arg[FS.Select2] as Flex);

            if (arg.Contains(FF.Filter0))
                a.Aims[0].filter = arg[FF.Filter0];
            if (arg.Contains(FF.Filter1))
                a.Aims[1].filter = arg[FF.Filter1];
            if (arg.Contains(FF.Filter2))
                a.Aims[2].filter = arg[FF.Filter2];
        };
    }
}