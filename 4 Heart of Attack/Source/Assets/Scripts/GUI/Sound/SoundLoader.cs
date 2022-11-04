using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public static class SoundLoader {

		static List<AudioClip> themes;
		public static AudioClip click;
		public static AudioClip inspect;
		public static AudioClip Target;
		
		public static void Load () {
			themes = new List<AudioClip>();
			AddTheme("Gearp");
			AddTheme("NewRep");
			AddTheme("Torridale");
			AddTheme("Grove");
			AddTheme("Chrono");
			AddTheme("Psycho");
			AddTheme("Psilent");
			AddTheme("Void");

			click = LoadSound("GUI/Click");
			inspect = LoadSound("GUI/Inspect");
			Target = LoadSound("GUI/Target");

		}

		static void AddTheme(string fileName) {
			themes.Add(LoadSound("Music/"+fileName));
		}

		static AudioClip LoadSound (string fileName) {
			return Resources.Load("Audio/"+fileName) as AudioClip;
		}

		public static AudioClip Theme (int n) {
			if (n<themes.Count) return themes[n];
			return default(AudioClip);
		}
	}
}