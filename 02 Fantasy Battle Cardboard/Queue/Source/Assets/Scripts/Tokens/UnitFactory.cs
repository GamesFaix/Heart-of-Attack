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
		{"LAUG", "Laughing Owl"},
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
}
