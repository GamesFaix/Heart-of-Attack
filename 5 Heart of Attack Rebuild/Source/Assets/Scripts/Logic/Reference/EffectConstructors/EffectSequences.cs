namespace HOA.Abilities
{
    public partial class EffectSequence
    {
        public static EffectSequence Detonate(IEffectUser user, EffectArgs args)
        {
            EffectSequence e = new EffectSequence("Detonate", user, args);
            e.AddToEnd(Effect.Detonate2(user, args));
            return e;
        }

        public static EffectSequence Explosion(IEffectUser user, EffectArgs args)
        {
            Debug.Log("Not implemented.");
            return null;
        }
    }
}