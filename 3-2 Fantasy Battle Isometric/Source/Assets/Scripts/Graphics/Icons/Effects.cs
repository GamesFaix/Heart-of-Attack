using UnityEngine; 

namespace HOA.Icons { 
	
	public static class Effects {
		
		public static Texture2D damage, fire, explosive, corrosive, laser, stun;
		
		public static Texture2D[] effects {get {return new Texture2D[6] {damage, fire, explosive, corrosive, laser, stun};} }
		
		public static void Load () {
			damage = Load("Damage");
			fire = Load("Fire");
			explosive = Load("Explosive");
			corrosive = Load("Corrosive");
			laser = Load("Laser");
			stun = Load("Stun");
		}
		
		static Texture2D Load (string fileName) {
			return Resources.Load("Images/Icons/Effects/"+fileName) as Texture2D;
		}
		
	}
}
