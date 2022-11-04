using System;
using System.Collections.Generic;

namespace HOA.Tokens
{ 

	public partial class Terrain : Token
	{  
        internal Terrain(
            object source, 
            Species species,
            Plane plane)
            : base(source, species, plane, Species.None)
        { }
	}
}
