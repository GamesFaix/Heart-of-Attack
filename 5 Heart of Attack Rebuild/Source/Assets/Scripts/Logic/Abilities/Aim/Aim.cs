using System;

namespace HOA.Abilities
{
    public class Aim
    {
        public AimPattern Pattern;
        public Func<AimPatternArgs> Args;
        public Range selectionCount;
        public Range range;
        public bool autoSelect;

        private Aim(AimPattern pattern, Func<AimPatternArgs> args, Range range,
            Range selectionCount, bool autoSelect = false)
        {
            Pattern = pattern;
            Args = args;
            this.range = range;
            this.selectionCount = selectionCount;
            this.autoSelect = autoSelect;
        }

        private Aim(AimPattern pattern, Func<AimPatternArgs> args, 
            Range range, bool autoSelect = false)
            : this(pattern, args, range, new Range(1, 1), false) 
        { }

    }
}
