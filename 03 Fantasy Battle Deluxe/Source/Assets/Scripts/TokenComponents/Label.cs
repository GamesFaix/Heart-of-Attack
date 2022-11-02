using System;
using System.Collections.Generic;
using UnityEngine;
using HOA.Players;

namespace HOA.Tokens.Components {

	public class Label {
		TTYPE code;
		string name = "";
		bool unique;
		char instance;
		string fullName;
		Player owner;
		Token parent;
		
		
		Color[] Colors () {return owner.Colors();}
		
		public Label (Token t, TTYPE c, Source s, bool uni=false, bool temp=false){
			parent = t;
			owner = s.Player();
			code = c;
			name = TokenRef.CodeToString(code);
			unique = uni;
			
			if (temp) {
				instance = 'T';
				fullName = name+" TEMPLATE";
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
		
		public char Instance() {return instance;}
		public string Name() {return name;}
		public string FullName() {return fullName;}
		public TTYPE Code() {return code;}
		public string CodeInst() {return code.ToString()+" "+instance;}
		public bool Unique () {return unique;}
		
		public Player Owner(){return owner;}
		public void SetOwner(Player p, bool log=true){
			owner = p;
			if (log){GameLog.Out(p+" has taken possession of "+parent+".");}
		}


		char NextAvailableInstance(){
			List<Token> likeTokens = new List<Token>();
			
			foreach (Token t in TokenFactory.Tokens()){
				if(t.Name() == name) {likeTokens.Add(t);}				
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
	}
}
