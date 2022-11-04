using UnityEngine;

namespace HOA {

	public static class ImageLoader {
		
		public static Texture2D[] wood;
		public static Texture2D parchment;

		public static void Load() {
			TokenThumbnails.Load();
			Icons.Load();
			
			wood = new Texture2D[2] {
				Resources.Load("Images/Textures/GUI/woodDark") as Texture2D,
				Resources.Load("Images/Textures/GUI/woodLarge") as Texture2D
			};

			parchment = Resources.Load("Images/Textures/GUI/parchment") as Texture2D;
		}
	}
}