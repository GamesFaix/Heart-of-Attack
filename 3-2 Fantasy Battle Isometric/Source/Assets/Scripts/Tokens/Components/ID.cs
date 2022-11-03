using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public class ID {
		Player owner;
		public Player Owner {
			get {return owner;} 
			set {owner = value;}
		}

		EToken code;
		public EToken Code {get {return code;} }

		string name = "";
		public string Name {get {return name;} }
		char instance;
		public char Instance {get {return instance;} }
		string fullName;
		public string FullName {get {return fullName;} }

		public string CodeInst {get {return code.ToString()+" "+instance;} }

		bool unique;
		public bool Unique {get {return unique;} }


		public ID (Token t, EToken c, Source s, bool uni=false, bool temp=false){
			owner = s.Player;
			code = c;
			name = TokenRef.CodeToString(code);
			unique = uni;
			
			if (temp) {
				instance = 'T';
				fullName = name;//+" TEMPLATE";
			}
				
			else if(!unique){
				instance = NextAvailableInstance();
				fullName = name+" "+instance;
			}
			else {
				instance = 'Z';
				fullName = name;
			}
		}
		
		char NextAvailableInstance(){
			List<Token> likeTokens = new List<Token>();
			
			foreach (Token t in TokenFactory.Tokens){
				if(t.ID.Name == name) {likeTokens.Add(t);}				
			}		
			
			bool[] letterTaken = new bool[10] {
				false, false, false, false, false, 
				false, false, false, false, false};
			
			foreach (Token t in likeTokens){
				if (t.ID.Instance == 'A'){letterTaken[0] = true;}
				if (t.ID.Instance == 'B'){letterTaken[1] = true;}	
				if (t.ID.Instance == 'C'){letterTaken[2] = true;}
				if (t.ID.Instance == 'D'){letterTaken[3] = true;}
				if (t.ID.Instance == 'E'){letterTaken[4] = true;}
				if (t.ID.Instance == 'F'){letterTaken[5] = true;}
				if (t.ID.Instance == 'G'){letterTaken[6] = true;}
				if (t.ID.Instance == 'H'){letterTaken[7] = true;}
				if (t.ID.Instance == 'I'){letterTaken[8] = true;}
				if (t.ID.Instance == 'J'){letterTaken[9] = true;}
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
	}
}
