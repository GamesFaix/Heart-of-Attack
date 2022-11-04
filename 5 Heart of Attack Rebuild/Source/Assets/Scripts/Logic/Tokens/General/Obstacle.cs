using System;
using System.Collections.Generic;

namespace HOA.Tokens
{

    public partial class Obstacle : Token
    {
        public Obstacle(object source, Species species)
            : base(source, species)
        {
            Remains = Species.None;
        }
	}
}