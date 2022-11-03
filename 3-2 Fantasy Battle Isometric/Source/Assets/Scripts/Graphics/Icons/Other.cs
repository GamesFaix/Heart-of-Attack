using UnityEngine; 

namespace HOA.Icons { 
	
	public static class Other {
		
		public static Texture2D timer, sensor, onDeath;
		
		public static Texture2D[] effects {get {return new Texture2D[3] {timer, sensor, onDeath};} }
		
		public static void Load () {
			timer = Load("Timer");
			sensor = Load("Sensor");
			onDeath = Load("OnDeath");
		}
		
		static Texture2D Load (string fileName) {
			return Resources.Load("Images/Icons/Other/"+fileName) as Texture2D;
		}
		
	}
}
