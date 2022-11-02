using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public static class SpriteEffects {

		static Dictionary<EEffect, Texture2D> effects;

		public static void Load() {
			effects = new Dictionary<EEffect, Texture2D>();
			
			Add(EEffect.SHOW, "show");
			Add(EEffect.BIRTH, "birth");
			Add(EEffect.DEATH, "death");
			Add(EEffect.DMG, "damage");
			Add(EEffect.STATUP, "statup");
			Add(EEffect.STATDOWN, "statdown");
			Add(EEffect.FIRE, "fire");
			Add(EEffect.EXP, "explosion");
			Add(EEffect.LASER, "laser");
			Add(EEffect.COR, "corrosion");
			Add(EEffect.STUN, "stun");
			Add(EEffect.HEADS, "heads");
			Add(EEffect.TAILS, "tails");
			Add(EEffect.DESTRUCT, "destruct");
			
			Add(EEffect.CORRODE, "corrosion");
			Add(EEffect.WATERLOG, "waterlog");
			Add(EEffect.INCINERATE, "incinerate");
			Add(EEffect.STICK, "stick");
			Add(EEffect.DETONATE, "detonate");
			Add(EEffect.BURROW, "burrow");

		}

		static void Add (EEffect e, string fileName) {effects.Add(e, LoadFile(fileName));}

		static Texture2D LoadFile (string name) {return (Resources.Load("Effects/"+name) as Texture2D);}

		public static Texture2D Effect (EEffect e) {return effects[e];}
	}
}
