using System;

namespace HOA.Abilities
{
    public partial class AimStage : ISourced
    {
        public Source source { get; private set; }
        public AimPlan plan { get { return source.Last<AimPlan>(); } }
        public AimStage previous
        {
            get
            {
                int index = plan.IndexOf(this);
                if (index > 0)
                    return plan[index - 1];
                return null;
            }
        }

        public Token user { get { return source.Last<Token>(); } }
        public Func<Token> body;
        public Func<Cell> center;
        public AimPattern pattern;
        public Predicate<IEntity> filter;
        public Range range;
        public bool inclusive;
        public Range selectionCount;
        public bool autoSelect;
        
        private AimStage(AimPlan plan)
        {
            source = new Source(plan);
            body = () => { return user; };
            center = () => { return user.Cell; };
            ///pattern mandatory
            filter = Filter.False;
            range = new Range(0, 1);
            inclusive = false;
            selectionCount = new Range(1, 1);
            autoSelect = false;
        }

        public AimStage(AimPlan plan, AimPattern pattern, Predicate<IEntity> filter, 
            Func<Token> body = null, Func<Cell> center = null)
            : this (plan)
        {
            this.pattern = pattern;
            this.filter = filter;
            if (body != null) 
                this.body = body;
            if (center != null)
                this.center = center;
        }

        public AimStage(AimPlan plan, AimPattern pattern, Predicate<IEntity> filter,
           Func<Token> body, Range range)
            : this(plan, pattern, filter, body, null)
        {
            this.range = range;
        }

        public Set<IEntity> FindOptions() 
        { return pattern(new AimPatternArgs(user, body(), center(), filter, range, inclusive)); }

    }
}
