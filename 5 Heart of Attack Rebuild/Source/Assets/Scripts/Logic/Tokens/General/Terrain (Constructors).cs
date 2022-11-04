using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA 
{ 

	public partial class Terrain
	{
        public static Terrain None
        {
            get
            {
                return new Terrain("Default terrain", Species.None);
            }
        }


        public static Terrain Sand
        {
            get
            {
               return new Terrain("Sand", Species.Sand);
            }
        }

        public static Terrain Hill
        {
            get
            {
                return new Terrain("Hill", Species.Hill);
            }
        }

        public static Terrain Mountain
        {
            get
            {
                Terrain t = new Terrain("Mountain", Species.Mountain);
                t.body.Plane = (Plane.Ground | Plane.Air | Plane.Terrain);
                return t;
            }
        }
        public static Terrain Water
        {
            get
            {
                Terrain t = new Terrain("Water", Species.Water);
                
                return t;
            }
        }


	}

}
