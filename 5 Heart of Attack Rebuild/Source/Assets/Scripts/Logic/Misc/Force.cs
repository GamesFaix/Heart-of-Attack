using System;
using HOA.Tokens;
using HOA.Abilities;

namespace HOA
{
    public static class Force
    {

        public static ForceEffect Effect { get { return new ForceEffect(); } }

        public class ForceEffect : IEffect
        {
            public ForceEffect() { }
            public Action Process { get; private set; }
            public override string ToString() { return "[Force Effect]"; }
        }

        public static ForceEffectUser EffectUser { get { return new ForceEffectUser(); } }

        public class ForceEffectUser : IEffectUser
        {
            public ForceEffectUser() { }
            public override string ToString() { return "[Force EffectUser]"; }
            public Ability ToAbility() { return null; }
            public IAbilityUser ToAbilityUser() { return null; }
            public ITokenCreator ToTokenCreator() { return null; }
        }

        public static ForceAbilityUser AbilityUser { get { return new ForceAbilityUser(); } }

        public class ForceAbilityUser : IAbilityUser
        {
            public ForceAbilityUser() { }
            public override string ToString() { return "[Force AbilityUser]"; }
            public Token ToToken() { return null; }
            public ITokenCreator ToTokenCreator() { return null; }
        }

        public static ForceTokenCreator TokenCreator { get { return new ForceTokenCreator(); } }

        public class ForceTokenCreator : ITokenCreator
        {
            public ForceTokenCreator() { }
            public override string ToString() { return "[Force TokenCreator]"; }
            public Player ToPlayer() { return null; }
        }
    }
}