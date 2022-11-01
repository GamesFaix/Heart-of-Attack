using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class UnitFactory {
	
	public static string Add(string code) {
		Unit u = new Unit(code);
		TurnQueue.units.Add(u);
		return u.fullName;
	}
	public static bool Delete(string fullName) {
		foreach (Unit u in TurnQueue.units){
			if (u.fullName == fullName) {
				TurnQueue.units.Remove(u);
				return true;
			}
		}
		return false;
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
		{"MONO", "Monolith"}
	};
	
	public static Dictionary<string,string>.ValueCollection Names(){
		return CodeNames.Values;
	}
	public static Dictionary<string,string>.KeyCollection Codes(){
		return CodeNames.Keys;
	}
}
