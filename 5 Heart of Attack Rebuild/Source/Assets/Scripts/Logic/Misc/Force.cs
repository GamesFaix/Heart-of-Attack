using System;

namespace HOA
{
    public static class Force
    {

        public static ForceEffect Effect { get { return new ForceEffect(); } }

        public class ForceEffect : Abilities.IEffect
        {
            public ForceEffect() { }
            public Action Process { get; private set; }
            public override string ToString() { return "[Force Effect]"; }
        }

        public static ForcePlayer Player { get { return new ForcePlayer(); } }

        public class ForcePlayer : IPlayer
        {
            public ForcePlayer() { }
            public override string ToString() { return "[Force Player]"; }
        }
    }
}