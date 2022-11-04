using UnityEngine;

namespace HOA.Textures {

	public static class Backgrounds {
		
		public static Texture2D Wood { get; private set; }
        public static Texture2D WoodLarge { get; private set; }
        public static Texture2D Parchment { get; private set; }

		public static void Load(object sender, LoadEventArgs args) 
        {
			Wood = Resources.Load("Images/Textures/Backgrounds/Wood") as Texture2D;
            WoodLarge = Resources.Load("Images/Textures/Backgrounds/WoodLarge") as Texture2D;
			Parchment = Resources.Load("Images/Textures/Backgrounds/Parchment") as Texture2D;
		}
	}
}