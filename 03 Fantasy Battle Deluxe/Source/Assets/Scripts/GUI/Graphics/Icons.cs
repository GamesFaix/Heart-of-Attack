using UnityEngine;
using System.Collections;

public static class Icons {
	
	static Texture2D hp;
	static Texture2D def;
	static Texture2D init;
	static Texture2D cor;
	static Texture2D stun;
	static Texture2D ap;
	static Texture2D fp;
	
	static Texture2D air;
	static Texture2D eth;
	static Texture2D gnd;
	static Texture2D dest;
	static Texture2D rem;
	static Texture2D tram;
	static Texture2D sunk;
	static Texture2D king;
	static Texture2D heart;
	
	public static void Load() {
		hp = Resources.Load("Icons/HP") as Texture2D;
		def = Resources.Load("Icons/DEF") as Texture2D;
		init = Resources.Load("Icons/IN") as Texture2D;
		cor = Resources.Load("Icons/COR") as Texture2D;
		stun = Resources.Load("Icons/STUN") as Texture2D;
		ap = Resources.Load("Icons/AP") as Texture2D;
		fp = Resources.Load("Icons/FP") as Texture2D;
		
		air = Resources.Load("Icons/AIR") as Texture2D;
		eth = Resources.Load("Icons/ETH") as Texture2D;
		gnd = Resources.Load("Icons/GND") as Texture2D;
		dest = Resources.Load("Icons/DEST") as Texture2D;
		rem = Resources.Load("Icons/REM") as Texture2D;
		sunk = Resources.Load("Icons/SUNK") as Texture2D;
		tram = Resources.Load("Icons/TRAM") as Texture2D;
		king = Resources.Load("Icons/KING") as Texture2D;
		heart = Resources.Load("Icons/HEART") as Texture2D;
		
	}
	
	public static Texture2D HP() {return hp;}
	public static Texture2D DEF() {return def;}
	public static Texture2D IN() {return init;}
	public static Texture2D AP() {return ap;}
	public static Texture2D FP() {return fp;}
	public static Texture2D COR() {return cor;}
	public static Texture2D STUN() {return stun;}
	public static Texture2D AIR() {return air;}
	public static Texture2D GND() {return gnd;}
	public static Texture2D ETH() {return eth;}
	public static Texture2D SUNK() {return sunk;}
	public static Texture2D DEST() {return dest;}
	public static Texture2D TRAM() {return tram;}
	public static Texture2D REM() {return rem;}
	public static Texture2D KING() {return king;}
	public static Texture2D HEART() {return heart;}



}
