using System;

namespace HOA.Ab
{

    public class TargetSelectionRequestEventArgs : EventArgs
    {
        public Set<IEntity> options { get; private set; }
        public Range<byte> selectionCount { get; private set; }

        public TargetSelectionRequestEventArgs(Set<IEntity> options, Range<byte> selectionCount)
        {
            if (options == null || options.Count < 1
                || selectionCount.max < 1)
                throw new ArgumentNullException();
            this.options = options;
            this.selectionCount = selectionCount;
        }
    }
}