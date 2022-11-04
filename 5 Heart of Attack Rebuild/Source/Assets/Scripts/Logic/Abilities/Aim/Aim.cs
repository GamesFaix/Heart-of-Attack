using System;

namespace HOA.Abilities
{
    public class AimStage
    {
        public AimPattern Pattern;
        public Func<AimPatternArgs> Args;
        public Range selectionCount;
        public bool autoSelect;

        public AimStage(AimPattern pattern, Func<AimPatternArgs> args, Range selectionCount, bool autoSelect = false)
        {
            Pattern = pattern;
            Args = args;
            this.selectionCount = selectionCount;
            this.autoSelect = autoSelect;
        }

        public AimStage(AimPattern pattern, Func<AimPatternArgs> args, bool autoSelect = false)
            : this(pattern, args, new Range(1, 1), autoSelect) { }

    }
}
