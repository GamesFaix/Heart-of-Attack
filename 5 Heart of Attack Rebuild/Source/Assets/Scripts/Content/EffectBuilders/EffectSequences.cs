namespace HOA.Effects
{
    public partial class Sequence
    {
        public static Sequence Detonate(object source, EffectArgs args)
        {
            Sequence e = new Sequence(source, "Detonate", args);
            e.AddToEnd(Effect.Detonate2(source, args));
            return e;
        }

        public static Sequence Explosion(object source, EffectArgs args)
        {
            Log.Debug("Not implemented.");
            return null;
        }
    }
}