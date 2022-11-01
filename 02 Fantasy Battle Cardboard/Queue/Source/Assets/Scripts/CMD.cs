using UnityEngine;
using System;
using System.Collections;

public static class CMD {
	public static string New(string input){
		string[] words = Format(input);
		string output = Parse(words);
		return output;
	}
	
	static string[] Format(string input){
		string allCaps = input.ToUpper();
		string[] separators = new string[]{" "};
		string[] words = allCaps.Split(separators, StringSplitOptions.RemoveEmptyEntries);
		return words;
	}
	
	static string Parse(string[] input){
		if (CAR(input) == "+"){return Advance();}
		if (CAR(input) == "-"){return Undo();}
		if (CAR(input) == "START"){return Start(CDR(input));}
		if (CAR(input) == "SHUFFLE"){return Shuffle();}
		if (CAR(input) == "RESET"){return Reset();}
		if (CAR(input) == "CREATE" || CAR(input) == "C"){return Create(CDR(input));}
		if (CAR(input) == "KILL" || CAR(input) == "K"){return Kill(CDR(input));}	
		if (CAR(input) == "REP" || CAR(input) == "R"){return Replace(input);}
		
		//unit modify commands
		if (ValidFullName(input,0)){
			string fn = GetFullName(input,0);
			Unit u = TurnQueue.FindUnit(fn);
			
			string[] restOfInput = RemoveFullName(input);
			if (CheckError(restOfInput[0])){return restOfInput[0];}
			
			if (restOfInput[0] == "UP"){return Up(u, restOfInput);}				
			if (restOfInput[0] == "DOWN"){return Down(u, restOfInput);}
			
			if (restOfInput[0] == "IN"){return ModIN(u, restOfInput);}
			if (restOfInput[0] == "HP"){return ModHP(u, restOfInput);}
			if (restOfInput[0] == "MHP"){return ModMHP(u, restOfInput);}
			if (restOfInput[0] == "DEF"){return ModDEF(u, restOfInput);}
			if (restOfInput[0] == "STUN"){return ModSTUN(u, restOfInput);}
			if (restOfInput[0] == "COR"){return ModCOR(u, restOfInput);}
			return "Selected "+fn+".";
		}			
		return "ERROR: "+input+" is not a valid command.";
	}
	
	static string TextToCode(string text){
		if (text.Length>=4){
			string firstFour = text.Substring(0,4);
			foreach (string code in UnitFactory.Codes()){
				if (firstFour == code){return firstFour;}
			}
		}
		return "ERROR";
	}
	static bool ValidCode(string text){
		if (CheckError(TextToCode(text))){return false;}
		return true;
	}

	static string GetFullName(string[] input, int start){
		string tryName = String.Join(" ",input,start,(input.Length-1));
		if (ValidCode(tryName)){
			string tryCode = TextToCode(tryName);
			char tryInstance = ' ';
			for (int i=start+1; i<input.Length; i++){
				if (input[i].Length == 1){
					tryInstance = input[i][0];
					break;
				}	
			}
			if (tryInstance == ' '){return "ERROR: Instance must be single letter.";}
			foreach (Unit u in TurnQueue.units){
				if ((u.code == tryCode) && (u.instance == tryInstance)){
					return u.fullName;
				}
			}
			return "ERROR: "+tryName+" does not currently exist.";	
		}	
		return "ERROR: "+tryName+" is not a valid unit name.";
	}
	static bool ValidFullName(string[] input, int start){
		string fn = GetFullName(input, start);
		if (CheckError(fn)){return false;}
		return true;
	}	
	static string[] RemoveFullName(string[] input){
		for(int i=1; i<input.Length; i++){
			if ((input[i].Length == 1) && (Char.IsLetter(input[i][0]))){
				if (input.Length > (i+1)){
					string restOfInput = String.Join(" ", input, (i+1), (input.Length-(i+1)));
					
					string[] sep = new string[1]{" "};
					return restOfInput.Split(sep, StringSplitOptions.RemoveEmptyEntries);
			
				}
				else {return new string[1] {"ERROR: No command specified after unit name."};}
			}
		}
		return new string[1] {"ERROR: No command specified after unit name."};
	}

	static bool CheckError(string input){
		if ((input.Length >= 5) && (input.Substring(0,5) == "ERROR")){return true;}
		return false;
	}
	
	static string Advance(){
		TurnQueue.Advance();
		return "Turn advanced.";
	}
	static string Undo(){
		TurnQueue.UndoAdvance();
		return "Turn advance undone. !!Not yet implemented!!";		
	}
	static string Start(string[] input){
		Reset();
		while(input.Length>0){
			Create(input);
			input = CDR(input);
		}
		Shuffle();		
		
		return "New game ready.";
	}
	static string Shuffle(){
		TurnQueue.Shuffle();
		return "Queue shuffled.";
	}
	
	static string Reset(){
		TurnQueue.Reset();
		return "Game reset.";
	}
	static string Create(string[] input){
		string tryName = String.Join(" ",input);
		if (ValidCode(tryName)){
			string uName = UnitFactory.Add(TextToCode(tryName));
			return "Created " +uName+ ".";
		}
		else {return "ERROR: Cannot create "+tryName+". Invalid unit name.";} 
	}
	static string Kill(string[] input){
		string fn;
		if (ValidFullName(input,0)){
			fn = GetFullName(input,0);
			UnitFactory.Delete(fn);
			return "Killed "+fn+".";
		}
		return GetFullName(input,0);
	}
	static string Replace(string[] input){
		string oldFN;
		if (ValidFullName(input,1)){
			oldFN = GetFullName(input,1);
			string[] restOfInput = RemoveFullName(input);
			string tryName = String.Join(" ",restOfInput,0,restOfInput.Length);
			if (ValidCode(tryName)){
				string newFN = UnitFactory.Add(TextToCode(tryName));
				UnitFactory.Delete(oldFN);
				return "Replaced "+oldFN+" with "+newFN+".";
			}
			return "ERROR: Cannot replace "+oldFN+" with "+tryName+". Invalid unit name.";
		}
		return GetFullName(input,1);
	}

	static string Up(Unit u, string[] input){
		int magnitude = 1;
		if(input.Length > 1) {Int32.TryParse(input[1], out magnitude);}
		TurnQueue.MoveUp(u,magnitude);
		return u.fullName+" moved up "+magnitude+" slot(s) in the Queue.";
	}
	static string Down(Unit u, string[] input){
		int magnitude = 1;
		if(input.Length > 1){Int32.TryParse(input[1], out magnitude);}
		TurnQueue.MoveDown(u,magnitude);
		return u.fullName+" moved down "+magnitude+" slot(s) in the Queue.";	
	}
	
	static string ModIN(Unit u, string[] input){
		if (input.Length >= 3){
			int magnitude;
			if(Int32.TryParse(input[2], out magnitude)){
				if (input[1] == "="){
					u.ModIN("=",magnitude);
					return u.fullName+"'s initiative set to "+u.IN();
				}
				if (input[1] == "+"){
					u.ModIN("+",magnitude);
					return u.fullName+"'s initiative +"+magnitude+", IN = "+u.IN();
				}
				if (input[1] == "-"){
					u.ModIN("-",magnitude);
					return u.fullName+"'s initiative -"+magnitude+", IN = "+u.IN();
				}	
			}
			return "ERROR: Initiative change requires number.";
		}
		return "ERROR: Initiative change requires operator (+ - =) and number.";
	}
	static string ModHP(Unit u, string[] input){
		if (input.Length >= 3){
			int magnitude;
			if(Int32.TryParse(input[2], out magnitude)){
				if (input[1] == "="){
					u.ModHP("=",magnitude);
					return u.fullName+"'s HP set to "+u.HPFraction();
				}
				if (input[1] == "+"){
					u.ModHP("+",magnitude);
					return u.fullName+"'s HP +"+magnitude+", HP = "+u.HPFraction();
				}
				if (input[1] == "-"){
					u.ModHP("-",magnitude);
					return u.fullName+"'s HP -"+magnitude+", HP = "+u.HPFraction();
				}	
			}
			return "ERROR: HP change requires number.";
		}
		return "ERROR: HP change requires operator (+ - =) and number.";
	}
	static string ModMHP(Unit u, string[] input){
		if (input.Length >= 3){
			int magnitude;
			if(Int32.TryParse(input[2], out magnitude)){
				if (input[1] == "="){
					u.ModMHP("=",magnitude);
					return u.fullName+"'s MHP set to "+u.HPFraction();
				}
				if (input[1] == "+"){
					u.ModMHP("+",magnitude);
					return u.fullName+"'s MHP +"+magnitude+", HP = "+u.HPFraction();
				}
				if (input[1] == "-"){
					u.ModMHP("-",magnitude);
					return u.fullName+"'s MHP -"+magnitude+", HP = "+u.HPFraction();
				}	
			}
			return "ERROR: MHP change requires number.";
		}
		return "ERROR: MHP change requires operator (+ - =) and number.";
	}
	static string ModDEF(Unit u, string[] input){
		if (input.Length >= 3){
			int magnitude;
			if(Int32.TryParse(input[2], out magnitude)){
				if (input[1] == "="){
					u.ModDEF("=",magnitude);
					return u.fullName+"'s defense set to "+u.DEF();
				}
				if (input[1] == "+"){
					u.ModDEF("+",magnitude);
					return u.fullName+"'s defense +"+magnitude+", DEF = "+u.DEF();
				}
				if (input[1] == "-"){
					u.ModDEF("-",magnitude);
					return u.fullName+"'s defense -"+magnitude+", DEF = "+u.DEF();
				}	
			}
			return "ERROR: Defense change requires number.";
		}
		return "ERROR: Defense change requires operator (+ - =) and number.";
	}
	static string ModSTUN(Unit u, string[] input){
		if (input.Length >= 3){
			int magnitude;
			if(Int32.TryParse(input[2], out magnitude)){
				if (input[1] == "="){
					u.ModSTUN("=",magnitude);
					return u.fullName+"'s stun counters set to "+u.STUN()+".";
				}
				if (input[1] == "+"){
					u.ModSTUN("+",magnitude);
					return u.fullName+"+"+magnitude+" stun counters. ("+u.STUN()+" total)";
				}
				if (input[1] == "-"){
					u.ModSTUN("-",magnitude);
					return u.fullName+" -"+magnitude+" stun counters. ("+u.STUN()+" total)";
				}	
			}
			return "ERROR: Stun change requires number.";
		}
		return "ERROR: Stun change requires operator (+ - =) and number.";
	}
	static string ModCOR(Unit u, string[] input){
		if (input.Length >= 3){
			int magnitude;
			if(Int32.TryParse(input[2], out magnitude)){
				if (input[1] == "="){
					u.ModCOR("=",magnitude);
					return u.fullName+"'s corrosion counters set to "+u.COR()+".";
				}
				if (input[1] == "+"){
					u.ModCOR("+",magnitude);
					return u.fullName+" +"+magnitude+" corrosion counters. ("+u.COR()+" total)";
				}
				if (input[1] == "-"){
					u.ModCOR("-",magnitude);
					return u.fullName+" -"+magnitude+" corrosion counters. ("+u.COR()+" total)";
				}	
			}
			return "ERROR: Corrosion change requires number.";
		}
		return "ERROR: Corrosion change requires operator (+ - =) and number.";
	}
	
	
	static string[] CDR (string[] input){
		string[] cdr; 
		if (input.Length>1){	
			cdr = new string[(input.Length-1)];
			Array.Copy(input, 1, cdr, 0, input.Length-1);
		}
		else {cdr = new string[0];}
		return cdr;
	}
	static string CAR (string[] input){
		return input[0];
	}
}
