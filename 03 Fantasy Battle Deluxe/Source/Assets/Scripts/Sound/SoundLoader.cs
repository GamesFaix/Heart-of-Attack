using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public static class SoundLoader {

		static List<AudioClip> themes;
		public static AudioClip click;
		public static AudioClip inspect;
		public static AudioClip target;
		static Dictionary<EEffect, AudioClip> effects;

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
			target = LoadSound("GUI/Target");

			effects = new Dictionary<EEffect, AudioClip>();
			
//			Add(EEffect.SHOW, "show");
			AddEffect(EEffect.BIRTH, "Birth");
			AddEffect(EEffect.DEATH, "Death");
			AddEffect(EEffect.DMG, "Punch");
			AddEffect(EEffect.DESTRUCT, "Destruct");
			AddEffect(EEffect.STATUP, "StatUp");
			AddEffect(EEffect.STATDOWN, "StatDown");
			AddEffect(EEffect.FIRE, "Fire");
			AddEffect(EEffect.SHUFFLE, "Shuffle");
			AddEffect(EEffect.ADVANCE, "Advance");
			AddEffect(EEffect.CORRODE, "Corrode");
			AddEffect(EEffect.WATERLOG, "Waterlog");
			AddEffect(EEffect.INCINERATE, "Incinerate");
			AddEffect(EEffect.LASER, "Laser");
			AddEffect(EEffect.TAILS, "Tails");
			AddEffect(EEffect.STICK, "Stick");
			AddEffect(EEffect.DETONATE, "Detonate");
			AddEffect(EEffect.EXP, "Explosion");
			AddEffect(EEffect.FLY, "Fly");
			AddEffect(EEffect.WALK, "Walk");
			AddEffect(EEffect.BURROW, "Burrow");
			AddEffect(EEffect.TELEPORT, "Teleport");
			AddEffect(EEffect.GETHEART, "GetHeart");



		}

		static void AddTheme(string fileName) {
				themes.Add(LoadSound("Music/"+fileName));
		}

		static AudioClip LoadSound (string fileName) {
			return Resources.Load("Audio/"+fileName) as AudioClip;
		}

		public static AudioClip Theme (int n) {
			if (n<themes.Count) {
				return themes[n];
			}
			return default(AudioClip);

		}
		static void AddEffect (EEffect e, string fileName) {effects.Add(e, LoadSound("Effects/"+fileName));}
		

		public static AudioClip Effect (EEffect e) {return effects[e];}

	}


}