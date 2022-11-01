using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class UnitFactory {
	
	public static void Add(string code, out string fullName, bool log=true) {
		fullName="";
		Unit u = new Unit(code);
		TurnQueue.units.Add(u);
		if (log) {GameLog.Out("Created " +u+".");}
		fullName = u.FullName();
	}

	public static void Add(string code, bool log=true) {
		Unit u = new Unit(code);
		TurnQueue.units.Add(u);
		if (log) {GameLog.Out("Created " +u+".");}
	}

	public static void Add(string[] codes, bool log=true){
		string fns="";
		foreach (string s in codes){
			string fn;
			Add(s,out fn, false);
			fns+=(fn+", ");
		}
		char[] trimChars = {' ',','};
		fns = fns.TrimEnd(trimChars);
		if (log) {GameLog.Out("Created "+fns+".)");}
	}

	public static void Delete(string fullName, bool log=true) {
		foreach (Unit u in TurnQueue.units){
			if (u.FullName() == fullName) {
				if (log) {GameLog.Out("Killed "+u+".");}
				TurnQueue.units.Remove(u);
				return;
			}
		}
		GameLog.Debug("UnitFactory: Cannot kill "+fullName+". Unit does not exist."); 
	}
	

	

	static string[] gearp = new string[4] {"KABU","MAWT","CARA","KATA"};
	static string[] newrep = new string[4] {"DECI","PANO","MEIN","DEMO"};
	static string[] torrid= new string[6] {"GARG","BATT","CONF","ASHE","SMAS","ROOK"};
	static string[] forgot = new string[4] {"ULTR","META","TALO","GRIZ"};
	static string[] chrono = new string[4] {"OLDT","REPR","PIEC","REVO"};
	static string[] psycho = new string[5] {"BLAC","MART","MYCO","BEES","LICH"};
	static string[] psilent = new string[4] {"DREA","PRIE","AREN","PRIS"};
	static string[] voidoid = new string[4] {"MONO","MOUT","NECR","RECY"};
	static string[][] factions = new string[9][] {new string[0], gearp, newrep, torrid, forgot, chrono, psycho, psilent, voidoid};

	public static string[] Faction(int i){
		if (i>0 && i<=8){return factions[i];}
		else{
			GameLog.Debug("UnitFactory: Attempt to lookup invalid faction.");
			return new string[0];
		}

	}

	public static string[] kings = new string[8] {"KABU","DECI","GARG","ULTR","OLDT","BLAC","DREA","MONO"};













}
