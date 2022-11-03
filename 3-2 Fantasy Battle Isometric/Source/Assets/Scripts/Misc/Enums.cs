namespace HOA {

	public enum EToken {
		TREE,
		KATA, CARA, MAWT, KABU, HSIL,
		DEMO, MINE, MEIN, PANO, DECI, HSTE,
		ROOK, SMAS, CONF, ASHE, BATT, GARG, HSTO,
		GRIZ, TALO, META, ULTR, HFIR,
		REVO, APER, PIEC, REPR, OLDT, HBRA,
		BEES, MYCO, MART, LICH, WEBB, BLAC, HSLK,
		PRIS, AREN, PRIE, DREA, HGLA,
		RECY, NECR, GATE, MONO, HBLO,
		CORP, ROCK, HILL, MNTN, WATR, LAVA,
		NONE
	}

	public enum EStat {HP, MHP, DEF, IN, AP, FP, STUN, COR}

	public enum EEffect {NONE, SHOW, BIRTH, DEATH, DMG, STATUP, STATDOWN, 
		FIRE, EXP, LASER, COR, STUN, HEADS, TAILS, DESTRUCT, SHUFFLE, ADVANCE,
		CORRODE, WATERLOG, INCINERATE, STICK, DETONATE, BURROW, FLY, WALK, TELEPORT,
		GETHEART}

	public enum ETask {
		Create, CreateArc, CreateMan,
		MoveLine, MovePath, MoveMan,
		Strike, Shoot, Volley, Rage, Sting,
		LichEvolve,

		End, Focus,
		OldtHour, OldtMin, OldtSec, ReprMine, ReprSlam, ReprBomb, PiecHeal, RevoQuick,
		KabuTele, KabuLaser, MawtBomb, MawtLaser, CaraShock, CaraDis, KataSprint,
		UltrBlast, UltrThrow, UltrMeta, MetaConsume, TaloGust, GrizHeal,
		DeciMortar, DeciMove, PanoCannon, PanoPierce, MeinDetonate, DemoPlant, DemoThrow,
		DreaTele, DreaBeam, PrieShove, ArenLeech, ArenDonate, PrisRefract,
		BlacWeb, BlacLich, MartMove, MartGrow, MartWhip, MycoDonate, MycoSeed, MycoSpore, BeesBlow, LichFeed, 
		GargLand, GargFly, GargWhip, GargRook, GargPetrify, BattCocktail, BattFling, ConfFire, ConfStrike, AsheArise, SmasFlail, SmasSlam, RookRebuild,
		MonoField, MonoAltar, MonoRecycle, GateFeast, GateBurrow, NecrTouch, NecrTele, RecyCannibal, RecyBurst
	}
}