using System;

namespace HOA.Abilities
{
    public class Aim
    {
        public AimPattern Pattern;
        public Func<AimPatternArgs> Args;
        public Range selectionCount;
        public bool autoSelect;

        private Aim(AimPattern pattern, Func<AimPatternArgs> args, 
            Range selectionCount, bool autoSelect = false)
        {
            Pattern = pattern;
            Args = args;
            this.selectionCount = selectionCount;
            this.autoSelect = autoSelect;
        }

        private Aim(AimPattern pattern, Func<AimPatternArgs> args, bool autoSelect = false)
            : this(pattern, args, new Range(1, 1), false) 
        { }

    }
}
