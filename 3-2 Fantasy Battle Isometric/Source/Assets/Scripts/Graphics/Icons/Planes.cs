using UnityEngine; 

namespace HOA.Icons { 

	public static class Planes {

		public static Texture2D sunken, ground, air, ethereal;

		public static Texture2D[] planes {get {return new Texture2D[4] {sunken,ground,air,ethereal};} }

		public static void Load () {
			sunken = Load("Sunken");
			ground = Load("Ground");
			air = Load("Air");
			ethereal = Load("Ethereal");
		}

		static Texture2D Load (string fileName) {
			return Resources.Load("Images/Icons/Planes/"+fileName) as Texture2D;
		}

	}
}
