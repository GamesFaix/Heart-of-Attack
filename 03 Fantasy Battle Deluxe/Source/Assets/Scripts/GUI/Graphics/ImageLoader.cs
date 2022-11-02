using UnityEngine;

namespace HOA {

	public static class ImageLoader {
		
		public static Texture2D yellowBtn;
		public static Texture2D redBtn;
		public static Texture2D[] cells;
		public static Texture2D[] wood;

		public static void Load() {
			Thumbs.Load();
			Icons.Load();
			SpriteEffects.Load();
			yellowBtn = Resources.Load("yellowSquare") as Texture2D;
			redBtn = Resources.Load("redBtn") as Texture2D;

			cells = new Texture2D[2] {
				Resources.Load("Textures/blackCell") as Texture2D,
				Resources.Load("Textures/whiteCell") as Texture2D

			};

			wood = new Texture2D[2] {
				Resources.Load("Textures/woodDark") as Texture2D,
				Resources.Load("Textures/woodLarge") as Texture2D
			};
		}
	}
}