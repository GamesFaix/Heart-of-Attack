namespace HOA.Ef
{
    public partial class Sequence
    {
        public static Sequence Detonate(object source, Args args)
        {
            Sequence e = new Sequence(source, "Detonate", args);
            e.AddToEnd(Effect.Detonate2(source, args));
            return e;
        }

        public static Sequence Explosion(object source, Args args)
        {
            Log.Debug("Not implemented.");
            return null;
        }
    }
}