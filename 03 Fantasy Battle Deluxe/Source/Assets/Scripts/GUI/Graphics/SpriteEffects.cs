using UnityEngine;
using System.Collections.Generic;

public enum EFFECT {NONE, SHOW, BIRTH, DEATH, DMG, STATUP, STATDOWN, FIRE, EXP, LASER, COR, STUN, HEADS, TAILS}

public static class SpriteEffects {

	static Dictionary<EFFECT, Texture2D> effects;

	public static void Load() {
		effects = new Dictionary<EFFECT, Texture2D>();
		
		Add(EFFECT.SHOW, "show");
		Add(EFFECT.BIRTH, "birth");
		Add(EFFECT.DEATH, "death");
		Add(EFFECT.DMG, "damage");
		Add(EFFECT.STATUP, "statup");
		Add(EFFECT.STATDOWN, "statdown");
		Add(EFFECT.FIRE, "fire");
		Add(EFFECT.EXP, "explosion");
		Add(EFFECT.LASER, "laser");
		Add(EFFECT.COR, "corrosion");
		Add(EFFECT.STUN, "stun");
		Add(EFFECT.HEADS, "heads");
		Add(EFFECT.TAILS, "tails");
	}

	static void Add (EFFECT e, string fileName) {effects.Add(e, LoadFile(fileName));}

	static Texture2D LoadFile (string name) {return (Resources.Load("Effects/"+name) as Texture2D);}

	public static Texture2D Effect (EFFECT e) {return effects[e];}
}
