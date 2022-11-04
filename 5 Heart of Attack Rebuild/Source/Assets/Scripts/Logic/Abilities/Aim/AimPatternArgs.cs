using System;


namespace HOA.Abilities
{
    public class AimPatternArgs
    {
        public Token user, body;
        public Cell center;
        public Tokens.Stat range;
        public Predicate<IEntity> filter;
        public bool inclusive;

        public AimPatternArgs(
            Token user,
            Token body,
            Cell center,
            Tokens.Stat range,
            Predicate<IEntity> filter,
            bool inclusive
            )
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
    }
}
