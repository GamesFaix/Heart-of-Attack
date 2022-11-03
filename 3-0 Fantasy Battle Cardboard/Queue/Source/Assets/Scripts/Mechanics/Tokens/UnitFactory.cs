using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Tokens;



public static class UnitFactory {
	static Dictionary<string, Token> tokens = new Dictionary<string, Token>();

	public static bool IsInstance(string str) {
		if (tokens.ContainsKey(str)) {return true;}
		else {return false;}
	}
	public static bool IsInstance(string fullName, out Token t) {
		t = default(Token);
		if (tokens.ContainsKey(fullName)) {
			t = tokens[fullName];
			return true;
		}
		else {return false;}
	}


	public static void Add(TTYPE code, out string fullName, bool log=true) {
		fullName="";
		Token t = new Unit(code);
		tokens.Add(t.FullName(), t);
		if (t is Unit) {TurnQueue.units.Add((Unit)t);}
		if (log) {GameLog.Out("Created " +t+".");}
		fullName = t.FullName();
	}

	public static void Add(TTYPE code, bool log=true) {
		Token t;
		if (code == TTYPE.KATA){t = new Katandroid();}
		else {t = new Unit(code);}
		tokens.Add(t.FullName(), t);
		if (t is Unit) {TurnQueue.units.Add((Unit)t);}
		if (log) {GameLog.Out("Created " +t+".");}
	}

	public static void Delete(List<Token> ts, bool log=true) {
		foreach (Token t in ts){
			if (tokens.ContainsValue(t)){
				tokens.Remove(t.FullName());
				if (log) {GameLog.Out("Killed "+t+".");}
				if (t is Unit) {TurnQueue.units.Remove((Unit)t);}
				return;

			}
			GameLog.Debug("UnitFactory: Cannot kill '"+t+"'. Token does not exist."); 
		}
	}
	

	

	static TTYPE[] gearp = new TTYPE[4] {TTYPE.KABU,TTYPE.MAWT,TTYPE.CARA,TTYPE.KATA};
	static TTYPE[] newrep = new TTYPE[4] {TTYPE.DECI,TTYPE.PANO,TTYPE.MEIN,TTYPE.DEMO};
	static TTYPE[] torrid= new TTYPE[6] {TTYPE.GARG,TTYPE.BATT,TTYPE.CONF,TTYPE.ASHE,TTYPE.SMAS,TTYPE.ROOK};
	static TTYPE[] forgot = new TTYPE[4] {TTYPE.ULTR,TTYPE.META,TTYPE.TALO,TTYPE.GRIZ};
	static TTYPE[] chrono = new TTYPE[4] {TTYPE.OLDT,TTYPE.REPR,TTYPE.PIEC,TTYPE.REVO};
	static TTYPE[] psycho = new TTYPE[5] {TTYPE.BLAC,TTYPE.MART,TTYPE.MYCO,TTYPE.BEES,TTYPE.LICH};
	static TTYPE[] psilent = new TTYPE[4] {TTYPE.DREA,TTYPE.PRIE,TTYPE.AREN,TTYPE.PRIS};
	static TTYPE[] voidoid = new TTYPE[4] {TTYPE.MONO,TTYPE.MOUT,TTYPE.NECR,TTYPE.RECY};
	static TTYPE[][] factions = new TTYPE[9][] {new TTYPE[0], gearp, newrep, torrid, forgot, chrono, psycho, psilent, voidoid};

	public static TTYPE[] Faction(int i){
		if (i>0 && i<=8){return factions[i];}
		else{
			GameLog.Debug("UnitFactory: Attempt to lookup invalid faction.");
			return new TTYPE[0];
		}

	}

	public static TTYPE[] kings = new TTYPE[8] {TTYPE.KABU,TTYPE.DECI,TTYPE.GARG,TTYPE.ULTR,TTYPE.OLDT,TTYPE.BLAC,TTYPE.DREA,TTYPE.MONO};










}
