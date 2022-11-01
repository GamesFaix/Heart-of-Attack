using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Tokens {
	public class Label {
		string code;
		string name = "";
		char instance;
		string fullName;
		int owner;
		Token parent;

		public Label (Token t, string c, int own = 0){
			parent = t;
			owner = own;
			code = c;
			name = "";
			codeNames.TryGetValue(code,out name);
			instance = NextAvailableInstance();
			fullName = name+" "+instance;
		}

		public char Instance() {return instance;}
		public string Name() {return name;}
		public string FullName() {return fullName;}
		public string Code() {return code;}

		public int Owner(){return owner;}
		public void SetOwner(int o, bool log=true){
			owner=o;
			if (log){GameLog.Out(Roster.Name(o)+" has taken possession of "+parent+".");}
		}


		char NextAvailableInstance(){
			List<Token> likeTokens = new List<Token>();
			
			foreach (Unit u in TurnQueue.units){
				if(u.Name() == name) {likeTokens.Add(u);}				
			}		
			
			bool[] letterTaken = new bool[10] {
				false, false, false, false, false, 
				false, false, false, false, false};
			
			foreach (Token t in likeTokens){
				if (t.Instance() == 'A'){letterTaken[0] = true;}
				if (t.Instance() == 'B'){letterTaken[1] = true;}	
				if (t.Instance() == 'C'){letterTaken[2] = true;}
				if (t.Instance() == 'D'){letterTaken[3] = true;}
				if (t.Instance() == 'E'){letterTaken[4] = true;}
				if (t.Instance() == 'F'){letterTaken[5] = true;}
				if (t.Instance() == 'G'){letterTaken[6] = true;}
				if (t.Instance() == 'H'){letterTaken[7] = true;}
				if (t.Instance() == 'I'){letterTaken[8] = true;}
				if (t.Instance() == 'J'){letterTaken[9] = true;}
			}
			if (letterTaken[0] == false){return 'A';}
			if (letterTaken[1] == false){return 'B';}
			if (letterTaken[2] == false){return 'C';}
			if (letterTaken[3] == false){return 'D';}
			if (letterTaken[4] == false){return 'E';}
			if (letterTaken[5] == false){return 'F';}
			if (letterTaken[6] == false){return 'G';}
			if (letterTaken[7] == false){return 'H';}
			if (letterTaken[8] == false){return 'I';}
			if (letterTaken[9] == false){return 'J';}
			
			return 'Z';
		}

		public static Dictionary<string,string> codeNames = new Dictionary<string,string>{
			{"",""},
			
			{"KATA", "Katandroid"},
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
			return codeNames.Values;
		}
		public static Dictionary<string,string>.KeyCollection Codes(){
			return codeNames.Keys;
		}


	}
}
