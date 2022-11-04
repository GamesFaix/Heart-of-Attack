using System;

namespace HOA.Abilities
{

    public class TargetSelectionEventArgs : EventArgs
    {
        public Set<IEntity> Selection { get; private set; }
        public bool Cancel { get; private set; }

        public TargetSelectionEventArgs(Set<IEntity> selection, bool cancel = false)
        {
            Selection = selection;
            Cancel = cancel;
        }
    }
}