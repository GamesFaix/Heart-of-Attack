using UnityEngine;

namespace HOA {

	public static class ImageLoader {
		
		public static Texture2D yellowBtn;
		public static Texture2D redBtn;

		public static void Load() {
			Thumbs.Load();
			Icons.Load();
			SpriteEffects.Load();
			yellowBtn = Resources.Load("yellowSquare") as Texture2D;
			redBtn = Resources.Load("redBtn") as Texture2D;
		}
	}
}