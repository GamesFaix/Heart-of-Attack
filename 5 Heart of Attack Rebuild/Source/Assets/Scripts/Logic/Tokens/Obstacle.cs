using System;
using System.Collections.Generic;

namespace HOA.Tokens
{

    public partial class Obstacle : Token
    {
        internal Obstacle(
            object source, 
            Species species,
            Plane plane = Plane.Ground,
            TokenFlags flags = TokenFlags.None,
            Species remains = Species.None)
            : base(source, species, plane, flags, remains)
        {
        }

        internal Obstacle(
            object source,
            Species species,
            Plane plane,
            Species remains)
            : this(source, species, plane, TokenFlags.None, remains)
        { }
	}
}