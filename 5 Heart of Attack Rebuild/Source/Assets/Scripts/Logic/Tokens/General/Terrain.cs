using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA 
{ 

	public partial class Terrain : Token
	{
       
        private Terrain(string name, Species species)
            : base(Force.Player, name, species)
        {
            body = new Body(this, TokenFlags.None, Plane.Terrain); 
        
        
        }




	}

}
