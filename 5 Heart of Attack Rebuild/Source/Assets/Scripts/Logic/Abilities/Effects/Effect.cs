using System;
using System.Collections.Generic;

namespace HOA.Abilities
{
    public partial class Effect : IEffect
    {
        public string Name { get; private set; }
        public IEffectUser User { get; private set; }

        Token userToken { get { return User.ToAbilityUser().ToToken(); } }
        Unit userUnit { get { return User.ToAbilityUser().ToToken() as Unit; } }

        public EffectArgs Args { get; private set; }
        public Action Process { get; private set; }

        public EffectSequence Sequence { get; set; }

        private Effect(string name, IEffectUser user, EffectArgs args)
        {
            if (name == "" || user == null || args == null)
                throw new ArgumentNullException();
            Name = name;
            User = user;
            Args = args;
        }
        /*
        private Effect(string name, IEffectUser user, EffectArgs args, Action process)
            : this (name, user, args)
        {
            if (process == null)
                throw new ArgumentNullException();
            Process = process;
        }
        */
        public override string ToString() { return Name; }



    }
}
