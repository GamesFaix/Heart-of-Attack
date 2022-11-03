using UnityEngine; 

namespace HOA.Icons { 
	
	public static class Aims {
		
		public static Texture2D neighbor, path, line, arc, radial, free, self;
		
		public static Texture2D[] aims {get {return new Texture2D[7] {
					neighbor, path, line, arc, radial, free, self};} }
		
		public static void Load () {
			neighbor = Load("Neighbor");
			path = Load("Path");
			line = Load("Line");
			arc = Load("Arc");
			radial = Load("Radial");
			free = Load("Free");
			self = Load("Self");
		}
		
		static Texture2D Load (string fileName) {
			return Resources.Load("Images/Icons/Aims/"+fileName) as Texture2D;
		}
		
	}
}
