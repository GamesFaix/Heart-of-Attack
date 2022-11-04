using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA 
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