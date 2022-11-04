using System;


namespace HOA.Abilities
{
    public class AimPatternArgs
    {
        public Token user, body;
        public Cell center;
        public Range range;
        public Predicate<IEntity> filter;
        public bool inclusive;

        public AimPatternArgs(Predicate<IEntity> filter, Token user, Token body = null, Cell center = null,
            Range range = default(Range), bool inclusive = false)
        {
            if (user == null || filter == null)
                throw new ArgumentNullException();

            this.user = user;

            if (body == null)
                this.body = user;
            else
                this.body = body;

            if (center == null && user != null)
                this.center = user.Cell;
            else
                this.center = center;

            this.range = range;
            this.filter = filter;
            this.inclusive = inclusive;
        }

        public static AimPatternArgs AttackNeighbor(Predicate<IEntity> filter, Token user)
        {
            return new AimPatternArgs(filter, user, user, user.Cell, new Range(0,1), false);
        }
    }
}
