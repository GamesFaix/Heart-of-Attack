using UnityEngine; 

namespace HOA.Icons { 
	
	public static class Stats {
		
		public static Texture2D health, defense, initiative, energy, focus;
		
		public static Texture2D[] stats {get {return new Texture2D[5] {health, defense, initiative, energy, focus};} }

		public static void Load () {
			health = Load("Health");
			defense = Load("Defense");
			initiative = Load("Initiative");
			energy = Load("Energy");
			focus = Load("Focus");
		}

		public static Texture2D Stat (EStat stat) {
			switch (stat) {
			case EStat.HP: return health;
			case EStat.MHP: return health;
			case EStat.DEF: return defense;
			case EStat.IN: return initiative;
			case EStat.AP: return energy;
			case EStat.FP: return focus;
			case EStat.STUN: return Icons.Effects.stun;
			default: return null;
			}
		}

		
		static Texture2D Load (string fileName) {
			return Resources.Load("Images/Icons/Stats/"+fileName) as Texture2D;
		}
		
	}
}
