using UnityEngine; 

namespace HOA.Icons { 
	
	public static class Types {
		
		public static Texture2D unit, obstacle, king, heart, destructible, trample, cell;
		
		public static Texture2D[] types {get {return new Texture2D[7] {unit, obstacle, king, heart, destructible, trample, cell};} }
		
		public static void Load () {
			unit = Load("Unit");
			obstacle = Load("Obstacle");
			king = Load("King");
			heart = Load("Heart");
			destructible = Load("Destructible");
			trample = Load("Trample");
			cell = Load("Cell");
		}
		
		static Texture2D Load (string fileName) {
			return Resources.Load("Images/Icons/Types/"+fileName) as Texture2D;
		}
		
	}
}
