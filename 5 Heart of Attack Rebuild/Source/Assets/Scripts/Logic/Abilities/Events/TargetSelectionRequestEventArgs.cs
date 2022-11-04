using System;

namespace HOA.Abilities
{

    public class TargetSelectionRequestEventArgs : EventArgs
    {
        public Set<IEntity> options { get; private set; }
        public Range selectionCount { get; private set; }

        public TargetSelectionRequestEventArgs(Set<IEntity> options, Range selectionCount)
        {
            if (options == null || options.Count < 1
                || selectionCount.max < 1)
                throw new ArgumentNullException();
            this.options = options;
            this.selectionCount = selectionCount;
        }
    }
}