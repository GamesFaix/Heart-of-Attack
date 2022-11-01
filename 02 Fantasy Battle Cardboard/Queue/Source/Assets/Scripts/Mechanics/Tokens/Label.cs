using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Tokens {
	public enum TTYPE {
		NONE,
		KATA,CARA,MAWT,KABU,HSIL,
		DEMO,MEIN,MINE,PANO,DECI,HSTE,
		ROOK,SMAS,CONF,ASHE,BATT,GARG,HSTO,
		GRIZ,TALO,META,ULTR,HFIR,
		REVO,PIEC,APER,REPR,OLDT,HBRA,
		LICH,BEES,MYCO,MART,BLAC,WEBB,HSLK,
		PRIS,AREN,PRIE,DREA,HGLA,
		RECY,NECR,MOUT,MONO,HBLO,
		CORP,TREE
	}

	public class Label {
		TTYPE code;
		string name = "";
		bool unique;
		char instance;
		string fullName;
		int owner;
		Token parent;

		public Label (Token t, TTYPE c, int own = 0, bool uni=false){
			parent = t;
			owner = own;
			code = c;
			name = "";
			unique = uni;

			codeNames.TryGetValue(code,out name);
			if(!unique){instance = NextAvailableInstance();}
			else {instance = ' ';}
			fullName = name+" "+instance;
		}

		public char Instance() {return instance;}
		public string Name() {return name;}
		public string FullName() {return fullName;}
		public TTYPE Code() {return code;}

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

		public static Dictionary<TTYPE,string> codeNames = new Dictionary<TTYPE,string>{
			{TTYPE.NONE,""},
			
			{TTYPE.KATA, "Katandroid"},
			{TTYPE.CARA, "Carapace Invader"},
			{TTYPE.MAWT, "MAWTH"},
			{TTYPE.KABU, "Kabutomachine"},
			{TTYPE.HSIL, "Silicon Heart of Attack"},
			
			{TTYPE.DEMO, "Demolitia"},
			{TTYPE.MEIN, "Mein Schutz"},
			{TTYPE.MINE, "Mine"},
			{TTYPE.PANO, "Panopticannon"},
			{TTYPE.DECI, "Decimatrix"},
			{TTYPE.HSTE, "Steel Heart of Attack"},
			
			{TTYPE.ROOK, "Rook"},
			{TTYPE.SMAS, "Smashbuckler"},
			{TTYPE.CONF, "Conflagragon"},
			{TTYPE.ASHE, "Ashes"},
			{TTYPE.BATT, "Battering Rambuchet"},
			{TTYPE.GARG, "Gargoliath"},
			{TTYPE.HSTO, "Stone Heart of Attack"},

			{TTYPE.GRIZ, "Grizzly Elder"},
			{TTYPE.TALO, "Taloned Scout"},
			{TTYPE.META, "Metaterrainean"},
			{TTYPE.ULTR, "Ultratherium"},
			{TTYPE.HFIR, "Fir Heart of Attack"},
			
			{TTYPE.REVO, "Revolving Tom"},
			{TTYPE.PIEC, "Piecemaker"},
			{TTYPE.APER, "Aperture"},
			{TTYPE.REPR, "Reprospector"},
			{TTYPE.OLDT, "Old Three Hands"},
			{TTYPE.HBRA, "Brass Heart of Attack"},

			{TTYPE.LICH, "Lichenthrope"},
			{TTYPE.BEES, "Beesassin"},
			{TTYPE.MYCO, "Mycolonist"},
			{TTYPE.MART, "Martian Man Trap"},
			{TTYPE.BLAC, "Black Winnow"},
			{TTYPE.WEBB, "Web"},
			{TTYPE.HSLK, "Silk Heart of Attack"},

			{TTYPE.PRIS, "Prism Guard"},
			{TTYPE.AREN, "Arena Non Sensus"},
			{TTYPE.PRIE, "Priest of Naja"},
			{TTYPE.DREA, "Dream Reaver"},
			{TTYPE.HGLA, "Glass Heart of Attack"},

			{TTYPE.RECY, "Recyclops"},	
			{TTYPE.NECR, "Necrochancellor"},
			{TTYPE.MOUT, "Mouth of the Underworld"},
			{TTYPE.MONO, "Monolith"},
			{TTYPE.HBLO, "Blood Heart of Attack"},
			
			{TTYPE.CORP, "Corpse"},
			{TTYPE.TREE, "Tree"}
		};
		public static Dictionary<TTYPE,string>.ValueCollection Names(){
			return codeNames.Values;
		}
		public static Dictionary<TTYPE,string>.KeyCollection Codes(){
			return codeNames.Keys;
		}
		public static bool IsCode (string str, out TTYPE code){
			try {
				code = (TTYPE)Enum.Parse(typeof(TTYPE),str);
				return true;
			}
			catch (ArgumentException){
				code = TTYPE.NONE;
				GameLog.Debug("Invalid token name input, cannot parse to TTYPE.");
				return false;
			}
		}
		public static string CodeToString (TTYPE code){
			return codeNames[code];

		}
	}
}
