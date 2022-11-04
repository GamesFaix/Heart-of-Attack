using System;
using System.Collections.Generic;
using HOA.Stats;
using HOA.Args;
using Unit = HOA.Tokens.Unit;

namespace HOA.Abilities {
	
    public delegate void Adjustment (AbilityClosure a);


    public static class Adjustments 
    {
        public static Adjustment Body0 = (a) => { a.ability.Aims[0].body = () => Content.Tokens.templates[a.args.species]; };

        public static Adjustment SpeciesName(string basicName) { return (a) => { a.ability.name = basicName + " " + a.args.species; }; }

        public static Adjustment FocusRangeBoost0 = (a) =>
        {
            sbyte bonus = (sbyte)(a.args[RS.Boost] * (a.args[RT.User] as Unit).focus);
            Flex range = a.args[RS.Range0] as Flex;

            a.ability.Aims[0].range = Range.sb(range[0], (sbyte)(range[1] + bonus));
        };


        public static Adjustment Standard = (a) =>
        {
            if (a.args.Contains(RS.Range0))
                a.ability.Aims[0].range = (Range<sbyte>)(a.args[RS.Range0] as Flex);
            if (a.args.Contains(RS.Range1))
                a.ability.Aims[1].range = (Range<sbyte>)(a.args[RS.Range1] as Flex);
            if (a.args.Contains(RS.Range2))
                a.ability.Aims[2].range = (Range<sbyte>)(a.args[RS.Range1] as Flex);

            if (a.args.Contains(RS.Select0))
                a.ability.Aims[0].selectionCount = (Range<sbyte>)(a.args[RS.Select0] as Flex);
            if (a.args.Contains(RS.Select1))
                a.ability.Aims[1].selectionCount = (Range<sbyte>)(a.args[RS.Select1] as Flex);
            if (a.args.Contains(RS.Select2))
                a.ability.Aims[2].selectionCount = (Range<sbyte>)(a.args[RS.Select2] as Flex);

            if (a.args.Contains(RF.Filter0))
                a.ability.Aims[0].filter = a.args[RF.Filter0](a);
            if (a.args.Contains(RF.Filter1))
                a.ability.Aims[1].filter = a.args[RF.Filter1](a);
            if (a.args.Contains(RF.Filter2))
                a.ability.Aims[2].filter = a.args[RF.Filter2](a);
        };
    }
}