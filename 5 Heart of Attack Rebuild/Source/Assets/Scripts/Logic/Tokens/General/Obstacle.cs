using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA 
{

    public partial class Obstacle : Token
    {
        public Obstacle(ITokenCreator creator, Species species)
            : base(creator, species)
        {
            Remains = Species.None;
        }
	}
}