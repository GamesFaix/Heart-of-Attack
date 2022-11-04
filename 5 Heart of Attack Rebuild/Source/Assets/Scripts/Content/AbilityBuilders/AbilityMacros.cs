using System;
using HOA.Stats;
using HOA.Args;
using HOA.Abilities;
using A = HOA.Content.Abilities;
using R = HOA.Abilities.AbilityArgs;
using Tuple = HOA.Abilities.AbilityTuple;

namespace HOA.Content
{
    public static partial class AbilityMacros
    {
        internal static Tuple Move(sbyte rangeMax)
        {
            return new Tuple(
                A.Move, new R(Price.Cheap,
                Arg.Stat(RS.Range0, new Flex(0, rangeMax))));
        }

        internal static Tuple Dart(sbyte rangeMax)
        {
            return new Tuple(
                A.Dart, new R(Price.Cheap,
                Arg.Stat(RS.Range0, new Flex(0, rangeMax))));
        }

        internal static Tuple Focus()
        {
            return new Tuple(
                A.Focus, new R(Price.Cheap,
                Arg.Stat(RS.Damage, new Scalar(1))));
        }

        internal static Tuple Strike(sbyte damage)
        {
            return new Tuple(
                A.Strike, new R(Price.Cheap,
                Arg.Stat(RS.Damage, new Scalar(damage))));
        }

        internal static Tuple Shoot(sbyte rangeMax, sbyte damage)
        {
            return new Tuple(
                A.Shoot, new AbilityArgs(Price.Cheap,
                Arg.Stat(RS.Range0, new Flex(0, rangeMax)),
                Arg.Stat(RS.Damage, new Scalar(damage))));
        }

        internal static Tuple Create(Price price, HOA.Tokens.Species species)
        {
            return new Tuple(
                A.Create, new R(price,
                species));
        }

    }
}