using System;
using HOA.Stats;

namespace HOA.Fargo
{
    public class Arg<TKey, TValue>
      //  where TKey : Enum
    {
        public readonly TKey key;
        public readonly TValue value;

        public Arg(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }
    }

    public static class Arg
    {
        public static Arg<FN, sbyte> Num(FN key, sbyte value)
        { return new Arg<FN, sbyte>(key, value); }

        public static Arg<FS, Stat> Stat(FS key, Stat value)
        { return new Arg<FS, Stat>(key, value); }

        public static Arg<FX, string> Text(FX key, string value)
        { return new Arg<FX, string>(key, value); }

        public static Arg<FO, bool> Option(FO key, bool value)
        { return new Arg<FO, bool>(key, value); }

        public static Arg<FT, IEntity> Target(FT key, IEntity value)
        { return new Arg<FT, IEntity>(key, value); }

        public static Arg<FF, Predicate<IEntity>> Filter(FF key, Predicate<IEntity> value)
        { return new Arg<FF, Predicate<IEntity>>(key, value); }

        public static Arg<FE, Ef.Builder> Effect(FE key, Ef.Builder value)
        { return new Arg<FE, Ef.Builder>(key, value); }

    }
}