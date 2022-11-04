namespace HOA.Ab
{
    public partial class EffectSequence
    {
        public static EffectSequence Detonate(object source, EffectArgs args)
        {
            EffectSequence e = new EffectSequence(source, "Detonate", args);
            e.AddToEnd(Effect.Detonate2(source, args));
            return e;
        }

        public static EffectSequence Explosion(object source, EffectArgs args)
        {
            Log.Debug("Not implemented.");
            return null;
        }
    }
}