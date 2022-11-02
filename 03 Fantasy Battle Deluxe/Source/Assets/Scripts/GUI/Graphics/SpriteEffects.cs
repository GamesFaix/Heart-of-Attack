using UnityEngine;

public enum EFFECT {NONE, SHOW, BIRTH, DEATH, DMG, STATUP, STATDOWN, FIRE, EXP, LASER, COR, STUN, HEADS, TAILS}

public static class SpriteEffects {

	static Texture2D show;
	static Texture2D birth;
	static Texture2D death;
	static Texture2D damage;
	static Texture2D statup;
	static Texture2D statdown;
	static Texture2D fire;
	static Texture2D exp;
	static Texture2D laser;
	static Texture2D cor;
	static Texture2D stun;
	static Texture2D heads;
	static Texture2D tails;

	public static void Load() {
		show = Resources.Load("Effects/show") as Texture2D;
		birth = Resources.Load("Effects/birth") as Texture2D;
		death = Resources.Load("Effects/death") as Texture2D;
		damage = Resources.Load("Effects/damage") as Texture2D;
		statup = Resources.Load("Effects/statup") as Texture2D;
		statdown = Resources.Load("Effects/statdown") as Texture2D;
		fire = Resources.Load("Effects/fire") as Texture2D;
		exp = Resources.Load("Effects/explosion") as Texture2D;
		laser = Resources.Load("Effects/laser") as Texture2D;
		cor = Resources.Load("Effects/corrosion") as Texture2D;
		stun = Resources.Load("Effects/stun") as Texture2D;
		heads = Resources.Load("Effects/heads") as Texture2D;
		tails = Resources.Load("Effects/tails") as Texture2D;
	}

	public static Texture2D Effect (EFFECT e) {
		switch (e) {
			case EFFECT.SHOW: return show;
			case EFFECT.BIRTH: return birth;
			case EFFECT.DEATH: return death;
			case EFFECT.DMG: return damage;
			case EFFECT.STATUP: return statup;
			case EFFECT.STATDOWN: return statdown;
			case EFFECT.FIRE: return fire;
			case EFFECT.EXP: return exp;
			case EFFECT.LASER: return laser;
			case EFFECT.COR: return cor;
			case EFFECT.STUN: return stun;
			case EFFECT.HEADS: return heads;
			case EFFECT.TAILS: return tails;
			default: return default(Texture2D);
		}
	}
}
