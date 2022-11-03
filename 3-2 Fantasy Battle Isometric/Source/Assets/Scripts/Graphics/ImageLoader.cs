using UnityEngine;

namespace HOA {

	public static class ImageLoader {
		
		public static Texture2D[] wood;
		public static Texture2D parchment;

		public static void Load() {
			Thumbs.Load();
			LoadIcons();
			SpriteEffects.Load();

			wood = new Texture2D[2] {
				Resources.Load("Images/Textures/GUI/woodDark") as Texture2D,
				Resources.Load("Images/Textures/GUI/woodLarge") as Texture2D
			};

			parchment = Resources.Load("Images/Textures/GUI/parchment") as Texture2D;
		}

		static void LoadIcons () {
			Icons.Aims.Load();
			Icons.Effects.Load();
			Icons.Planes.Load();
			Icons.Stats.Load();
			Icons.Types.Load();
			Icons.Other.Load();
		}
	}
}