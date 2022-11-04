using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA 
{ 

	public partial class Terrain : Token
	{
       
        private Terrain(ITokenCreator creator, Species species)
            : base(creator, species)
        {
        }




	}

}
