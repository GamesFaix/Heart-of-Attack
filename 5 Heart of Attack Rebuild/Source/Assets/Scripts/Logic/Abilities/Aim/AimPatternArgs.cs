﻿using System;
using System.Collections.Generic;

namespace HOA.Ab 
{
	
    public class AimPatternArgs 
    {
        public Token user, body;
        public Cell center;
        public Predicate<IEntity> filter;
        public Range<byte> range;
        public bool inclusive;

        public AimPatternArgs(Token user, Token body, Cell center, Predicate<IEntity> filter, Range<byte> range, bool inclusive)
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