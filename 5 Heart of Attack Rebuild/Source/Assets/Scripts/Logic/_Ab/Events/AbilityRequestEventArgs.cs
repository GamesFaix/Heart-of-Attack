using System;

namespace HOA.Ab
{

    public class AbilityRequestEventArgs : EventArgs
    {
        public Closure closure { get; private set; }
        public Ability ability { get { return closure.ability; } }
        public Args args { get { return closure.args; } }
        public bool cancel { get; private set; }

        public AbilityRequestEventArgs(Closure closure, bool cancel = false)
        {
            this.closure = closure;
            this.cancel = cancel;
        }
    }
}