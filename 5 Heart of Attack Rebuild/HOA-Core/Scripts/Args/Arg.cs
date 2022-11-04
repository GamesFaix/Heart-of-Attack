namespace HOA.Args
{
    public class Arg<TKey, TValue>
    {
        public readonly TKey key;
        public readonly TValue value;

        public Arg(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }
    }

    public static partial class Arg
    {
        public static Arg<RN, sbyte> Num(RN key, sbyte value)
        { return new Arg<RN, sbyte>(key, value); }

        public static Arg<RL, string> Text(RL key, string value)
        { return new Arg<RL, string>(key, value); }

        public static Arg<RO, bool> Option(RO key, bool value)
        { return new Arg<RO, bool>(key, value); }
    }
}