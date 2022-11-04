using System;
using HOA.Stats;
using HOA.Args;
using EB = HOA.Effects.EffectBuilder;
using Pred = System.Predicate<HOA.IEntity>;

namespace HOA
{
    public static partial class Arg
    {
        public static Arg<RS, Stat> Stat(RS key, Stat value)
        { return new Arg<RS, Stat>(key, value); }

        public static Arg<RT, IEntity> Target(RT key, IEntity value)
        { return new Arg<RT, IEntity>(key, value); }

        public static Arg<RF, FilterBuilder> Filter(RF key, FilterBuilder value)
        { return new Arg<RF, FilterBuilder>(key, value); }

        public static Arg<RE, EB> Effect(RE key, EB value)
        { return new Arg<RE, EB>(key, value); }

    }
}