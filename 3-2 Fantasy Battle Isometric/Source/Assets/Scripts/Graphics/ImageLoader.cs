using UnityEngine;

namespace HOA {

	public static class ImageLoader {
		
		public static Texture2D[] wood;

		public static void Load() {
			Thumbs.Load();
			Icons.Load();
			SpriteEffects.Load();

			wood = new Texture2D[2] {
				Resources.Load("Images/Textures/GUI/woodDark") as Texture2D,
				Resources.Load("Images/Textures/GUI/woodLarge") as Texture2D
			};
		}
	}
}