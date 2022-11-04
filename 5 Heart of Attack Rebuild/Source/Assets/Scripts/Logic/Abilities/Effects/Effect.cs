using System;
using System.Collections.Generic;

namespace HOA.Abilities
{
    public class Effect : IEffect
    {
        public string Name { get; private set; }
        public IEffectUser User { get; private set; }
        public EffectArgs Args { get; private set; }
        public Action Process { get; private set; }

        private Effect(string name, IEffectUser user, EffectArgs args, Action process)
        {
            if (name == "" || user == null || args == null || process == null)
                throw new ArgumentNullException();
            Name = name;
            User = user;
            Args = args;
            Process = process;
        }

        public override string ToString() { return Name; }

    }
}
