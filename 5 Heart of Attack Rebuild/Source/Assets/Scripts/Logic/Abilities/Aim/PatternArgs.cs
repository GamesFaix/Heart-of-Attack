using System;
using System.Collections.Generic;

namespace HOA.Ab.Aim
{
	
    public class PatternArgs 
    {
        public Token user, body;
        public Cell center;
        public Predicate<IEntity> filter;
        public Range<sbyte> range;
        public bool inclusive;

        public PatternArgs(Token user, Token body, Cell center, Predicate<IEntity> filter, Range<sbyte> range, bool inclusive)
        {
            this.user = user;
            this.body = body;
            this.center = center;
            this.filter = filter;
            this.range = range;
            this.inclusive = inclusive;
        }

	}
}