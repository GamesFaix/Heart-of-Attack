using System;

namespace HOA.Ab
{

    public class AbilityRequestEventArgs : EventArgs
    {
        public AbilityClosure closure { get; private set; }
        public Ability ability { get { return closure.ability; } }
        public AbilityArgs args { get { return closure.args; } }
        public bool cancel { get; private set; }

        public AbilityRequestEventArgs(AbilityClosure closure, bool cancel = false)
        {
            this.closure = closure;
            this.cancel = cancel;
        }
    }
}