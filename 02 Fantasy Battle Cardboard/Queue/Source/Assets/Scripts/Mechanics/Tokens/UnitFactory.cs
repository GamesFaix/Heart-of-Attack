using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class UnitFactory {
	
	public static void Add(string code, out string fullName, bool log=true) {
		fullName="";
		Unit u = new Unit(code);
		TurnQueue.units.Add(u);
		if(log==true){GameLog.Add("Created " +u.name+ " ("+u.instance+").",LogIO.OUT);}
		fullName = u.fullName;
	}

	public static void Add(string code, bool log=true) {
		Unit u = new Unit(code);
		TurnQueue.units.Add(u);
		if(log==true){GameLog.Add("Created " +u.name+ " ("+u.instance+").",LogIO.OUT);}
	}

	public static void Add(string[] codes){
		string fns="";
		foreach (string s in codes){
			string fn;
			Add(s,out fn, false);
			fns+=(fn+", ");
		}
		char[] trimChars = {' ',','};
		fns = fns.TrimEnd(trimChars);
		GameLog.Add("Created "+fns+".)", LogIO.OUT); 
	}

	public static void Delete(string fullName) {
		foreach (Unit u in TurnQueue.units){
			if (u.fullName == fullName) {
				GameLog.Add("Killed "+u.fullName+".",LogIO.OUT);
				TurnQueue.units.Remove(u);
				return;
			}
		}
		GameLog.Add("UnitFactory: Cannot kill "+fullName+". Unit does not exist.", LogIO.DEBUG); 
	}
	
	public static Dictionary<string,string> CodeNames = new Dictionary<string,string>{
		{"",""},

		{"KATA","Katandroid"},
		{"CARA", "Carapace Invader"},
		{"MAWT", "MAWTH"},
		{"KABU", "Kabutomachine"},
		
		{"DEMO", "Demolitia"},
		{"MEIN", "Mein Schutz"},
		{"PANO", "Panopticannon"},
		{"DECI", "Decimatrix"},
		
		{"ROOK", "Rook"},
		{"SMAS", "Smashbuckler"},
		{"CONF", "Conflagragon"},
		{"ASHE", "Ashes"},
		{"BATT", "Battering Rambuchet"},
		{"GARG", "Gargoliath"},
		
		{"GRIZ", "Grizzly Elder"},
		{"TALO", "Taloned Scout"},
		{"META", "Metaterrainean"},
		{"ULTR", "Ultratherium"},
		
		{"REVO", "Revolving Tom"},
		{"PIEC", "Piecemaker"},
		{"REPR", "Reprospector"},
		{"OLDT", "Old Three Hands"},
		
		{"LICH", "Lichenthrope"},
		{"BEES", "Beesassin"},
		{"MYCO", "Mycolonist"},
		{"MART", "Martian Man Trap"},
		{"BLAC", "Black Winnow"},
		
		{"PRIS", "Prism Guard"},
		{"AREN", "Arena Non Sensus"},
		{"PRIE", "Priest of Naja"},
		{"DREA", "Dream Reaver"},
		
		{"RECY", "Recyclops"},	
		{"NECR", "Necrochancellor"},
		{"MOUT", "Mouth of the Underworld"},
		{"MONO", "Monolith"},

		{"CORP", "Corpse"},
		{"TREE", "Tree"}
	};
	
	public static Dictionary<string,string>.ValueCollection Names(){
		return CodeNames.Values;
	}
	public static Dictionary<string,string>.KeyCollection Codes(){
		return CodeNames.Keys;
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
			GameLog.Add("UnitFactory: Attempt to lookup invalid faction.",LogIO.DEBUG);
			return new string[0];
		}

	}

	public static string[] kings = new string[8] {"KABU","DECI","GARG","ULTR","OLDT","BLAC","DREA","MONO"};













}
