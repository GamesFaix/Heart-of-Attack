using System;
using System.Collections.Generic;

namespace HOA.Abilities
{
    public partial class Effect : ClosedAction<EffectArgs>, IEffect
    {
        public string Name { get; private set; }
        public override string ToString() { return Name; }

        public Action Process { get { return Invoke; } }
        
        public IEffectUser User { get; private set; }
        Token userToken { get { return User.ToAbilityUser().ToToken(); } }
        Unit userUnit { get { return User.ToAbilityUser().ToToken() as Unit; } }

        public EffectSequence Sequence { get; set; }

        private Effect(string name, IEffectUser user, EffectArgs args)
            : base (args)
        {
            if (name == "" || user == null || args == null)
                throw new ArgumentNullException();
            Name = name;
            User = user;
        }
        
        
    }
}
