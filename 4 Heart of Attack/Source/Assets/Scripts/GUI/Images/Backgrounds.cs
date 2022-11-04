using UnityEngine;

namespace HOA.Textures {

	public static class Backgrounds {
		
		public static Texture2D Wood { get; private set; }
        public static Texture2D WoodLarge { get; private set; }
        public static Texture2D Parchment { get; private set; }

        static Backgrounds() 
        {
            Core.Load += Load;
        }

		public static void Load(object sender, LoadEventArgs args) 
        {
			Wood = Resources.Load("Images/Textures/GUI/woodDark") as Texture2D;
            WoodLarge = Resources.Load("Images/Textures/GUI/woodLarge") as Texture2D;
			Parchment = Resources.Load("Images/Textures/GUI/parchment") as Texture2D;
		}
	}
}