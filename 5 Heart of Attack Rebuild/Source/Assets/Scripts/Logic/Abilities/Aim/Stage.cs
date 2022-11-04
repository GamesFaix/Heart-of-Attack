using System;

namespace HOA.Ab.Aim
{
    public partial class Stage : ISourced
    {
        public Source source { get; private set; }
        public Plan plan { get { return source.Last<Plan>(); } }
        public Stage previous
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
        public Pattern pattern;
        public Predicate<IEntity> filter;
        public Range<byte> range;
        public bool inclusive;
        public Range<byte> selectionCount;
        public bool autoSelect;
        
        private Stage(Plan plan)
        {
            source = new Source(plan);
            body = () => { return user; };
            center = () => { return user.Cell; };
            ///pattern mandatory
            filter = Filter.False;
            range = Range.b(0, 1);
            inclusive = false;
            selectionCount = Range.b(1, 1);
            autoSelect = false;
        }

        public Stage(Plan plan, Pattern pattern, Predicate<IEntity> filter, 
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

        public Stage(Plan plan, Pattern pattern, Predicate<IEntity> filter,
           Func<Token> body, Range<byte> range)
            : this(plan, pattern, filter, body, null)
        {
            this.range = range;
        }

        public Set<IEntity> FindOptions() 
        { return pattern(new PatternArgs(user, body(), center(), filter, range, inclusive)); }

    }
}
