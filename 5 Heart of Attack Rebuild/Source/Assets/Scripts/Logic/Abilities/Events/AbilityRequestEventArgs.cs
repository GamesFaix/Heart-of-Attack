using System;

namespace HOA.Abilities
{

    public class AbilityRequestEventArgs : EventArgs
    {
        public object source { get; private set; }
        public Ability ability { get; private set; }
        public AbilityArgs args { get; private set; }
        public bool cancel { get; private set; }

        public AbilityRequestEventArgs(Ability ability, AbilityArgs args, bool cancel = false)
        {
            this.source = source;
            this.ability = ability;
            this.args = args;
            this.cancel = cancel;
        }
    }
}