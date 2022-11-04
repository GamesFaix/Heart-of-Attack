using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public class TokenID {
		
        public string Name {get; private set;}
        public EToken Code {get; private set;}
        public Player Owner {get; set;}
        public char Instance {get; private set;}
		public bool Unique {get; private set;}
        public string FullName {get {return Name + " " + Instance;} }
        public string CodeInst {get {return Code+" "+Instance;} }

		public TokenID (Source source, Token token, EToken code, bool unique=false, bool template=false){
			Owner = source.Player;
			Code = code;
			Name = TokenRef.CodeToString(Code);
			Unique = unique;
			
			if (template) Instance = '*';
			else if(!Unique) Instance = NextAvailableInstance();
			else Instance = '!';
		}

        public TokenID(Token token, EToken code, Source source, bool unique=false, bool template=false) 
            : this(source, token, code, unique, template) { }

		char NextAvailableInstance(){
			List<Token> likeTokens = new List<Token>();
			
			foreach (Token t in TokenFactory.Tokens){
				if(t.ID.Name == Name) {likeTokens.Add(t);}				
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
