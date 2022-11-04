using System;
using System.Collections.Generic;
using System.Collections;
using HOA.Stats;
using HOA.Args;
using EB = HOA.Effects.EffectBuilder;

namespace HOA
{
    public static partial class ArgTable
    {
        public static ArgTable<RS, Stat> Stat(params Arg<RS, Stat>[] args)
        { return new ArgTable<RS, Stat>(args); }

        public static ArgTable<RT, IEntity> Target(params Arg<RT, IEntity>[] args)
        { return new ArgTable<RT, IEntity>(args); }

        public static ArgTable<RF, FilterBuilder> Filter(params Arg<RF, FilterBuilder>[] args)
        { return new ArgTable<RF, FilterBuilder>(args); }

        public static ArgTable<RE, EB> Effect(params Arg<RE, EB>[] args)
        { return new ArgTable<RE, EB>(args); }
    }
}