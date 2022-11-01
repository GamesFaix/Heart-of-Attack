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
		if (input[0] == "+"){
			TurnQueue.Advance();
			return "Turn advanced.";
		}
		else if (input[0] == "-"){
			TurnQueue.UndoAdvance();
			return "Turn advance undone. !!Not yet implemented!!";
		}
		else if (input[0] == "RESET"){
			TurnQueue.Reset();
			return "Game reset.";
		}

		else if (input[0] == "ADD"){
			string tryName = String.Join(" ",input,1,(input.Length-1));
			if (ValidCode(tryName)){
				string uName = UnitFactory.Add(TextToCode(tryName));
				return "Created " +uName+ ".";
			}
			else {return "ERROR: Cannot create "+tryName+". Invalid unit name.";} 
		}
		
		else if (input[0] == "KILL"){
			string fn;
			if (ValidFullName(input,1)){
				fn = GetFullName(input,1);
				UnitFactory.Delete(fn);
				return "Killed "+fn+".";
			}
			return GetFullName(input,1);
		}	
		
		
		//unit modify commands
		else if (ValidFullName(input,0)){
			string fn = GetFullName(input,0);
			Unit u = TurnQueue.FindUnit(fn);
			
			string[] restOfInput = RemoveFullName(input);
			if (CheckError(restOfInput[0])){return restOfInput[0];}
			
			if (restOfInput[0] == "UP"){
				int magnitude = 1;
				if(restOfInput.Length > 1) {Int32.TryParse(restOfInput[1], out magnitude);}
				TurnQueue.MoveUp(u,magnitude);
				return fn+" moved up "+magnitude+" slot(s) in the Queue.";
			}
			else if (restOfInput[0] == "DOWN"){
				int magnitude = 1;
				if(restOfInput.Length > 1){Int32.TryParse(restOfInput[1], out magnitude);}
				TurnQueue.MoveDown(u,magnitude);
				return fn+" moved down "+magnitude+" slot(s) in the Queue.";	
			}
			
			else if (restOfInput[0] == "IN"){
				if (restOfInput.Length >= 3){
					int magnitude;
					if(Int32.TryParse(restOfInput[2], out magnitude)){
						if (restOfInput[1] == "="){
							u.init=magnitude;
							return fn+"'s initiative set to "+u.init;
						}
						if (restOfInput[1] == "+"){
							u.init+=magnitude;
							return fn+"'s initiative +"+magnitude+", IN = "+u.init;
						}
						if (restOfInput[1] == "-"){
							u.init-=magnitude;
							return fn+"'s initiative -"+magnitude+", IN = "+u.init;
						}	
					}
					return "ERROR: Initiative change requires number.";
				}
				return "ERROR: Initiative change requires operator (+ - =) and number.";
			}
			else if (restOfInput[0] == "HP"){
				if (restOfInput.Length >= 3){
					int magnitude;
					if(Int32.TryParse(restOfInput[2], out magnitude)){
						if (restOfInput[1] == "="){
							u.ModHP("=",magnitude);
							return fn+"'s HP set to "+u.HPFraction();
						}
						if (restOfInput[1] == "+"){
							u.ModHP("+",magnitude);
							return fn+"'s HP +"+magnitude+", HP = "+u.HPFraction();
						}
						if (restOfInput[1] == "-"){
							u.ModHP("-",magnitude);
							return fn+"'s HP -"+magnitude+", HP = "+u.HPFraction();
						}	
					}
					return "ERROR: HP change requires number.";
				}
				return "ERROR: HP change requires operator (+ - =) and number.";
			}
			else if (restOfInput[0] == "MHP"){
				if (restOfInput.Length >= 3){
					int magnitude;
					if(Int32.TryParse(restOfInput[2], out magnitude)){
						if (restOfInput[1] == "="){
							u.ModMHP("=",magnitude);
							return fn+"'s MHP set to "+u.HPFraction();
						}
						if (restOfInput[1] == "+"){
							u.ModMHP("+",magnitude);
							return fn+"'s MHP +"+magnitude+", HP = "+u.HPFraction();
						}
						if (restOfInput[1] == "-"){
							u.ModMHP("-",magnitude);
							return fn+"'s MHP -"+magnitude+", HP = "+u.HPFraction();
						}	
					}
					return "ERROR: MHP change requires number.";
				}
				return "ERROR: MHP change requires operator (+ - =) and number.";
			}
			else if (restOfInput[0] == "DEF"){
				if (restOfInput.Length >= 3){
					int magnitude;
					if(Int32.TryParse(restOfInput[2], out magnitude)){
						if (restOfInput[1] == "="){
							u.def=magnitude;
							return fn+"'s defense set to "+u.def;
						}
						if (restOfInput[1] == "+"){
							u.def+=magnitude;
							return fn+"'s defense +"+magnitude+", DEF = "+u.def;
						}
						if (restOfInput[1] == "-"){
							u.def-=magnitude;
							return fn+"'s defense -"+magnitude+", DEF = "+u.def;
						}	
					}
					return "ERROR: Defense change requires number.";
				}
				return "ERROR: Defense change requires operator (+ - =) and number.";
			}	
			else if (restOfInput[0] == "STUN"){
				if (restOfInput.Length >= 3){
					int magnitude;
					if(Int32.TryParse(restOfInput[2], out magnitude)){
						if (restOfInput[1] == "="){
							u.stun=magnitude;
							return fn+"'s stun counters set to "+u.stun+".";
						}
						if (restOfInput[1] == "+"){
							u.stun+=magnitude;
							return fn+" +"+magnitude+" stun counters. ("+u.stun+" total)";
						}
						if (restOfInput[1] == "-"){
							u.stun-=magnitude;
							return fn+" -"+magnitude+" stun counters. ("+u.stun+" total)";
						}	
					}
					return "ERROR: Stun change requires number.";
				}
				return "ERROR: Stun change requires operator (+ - =) and number.";
			}	
			else if (restOfInput[0] == "COR"){
				if (restOfInput.Length >= 3){
					int magnitude;
					if(Int32.TryParse(restOfInput[2], out magnitude)){
						if (restOfInput[1] == "="){
							u.cor=magnitude;
							return fn+"'s corrosion counters set to "+u.cor+".";
						}
						if (restOfInput[1] == "+"){
							u.cor+=magnitude;
							return fn+" +"+magnitude+" corrosion counters. ("+u.cor+" total)";
						}
						if (restOfInput[1] == "-"){
							u.cor-=magnitude;
							return fn+" -"+magnitude+" corrosion counters. ("+u.cor+" total)";
						}	
					}
					return "ERROR: Corrosion change requires number.";
				}
				return "ERROR: Corrosion change requires operator (+ - =) and number.";
			}	
			return "Selected "+fn+".";
		}			
		return "ERROR: "+input+" is not a valid command.";
	}
	
	

	
	static string TextToCode(string text){
		string firstFour = text.Substring(0,4);
		foreach (string code in UnitFactory.Codes()){
			if (firstFour == code){return firstFour;}
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
			return "ERROR: "+tryName+" "+tryInstance+" does not currently exist.";	
		}	
		return "ERROR: "+tryName+" is not a valid unit name.";
	}
	static bool ValidFullName(string[] input, int start){
		string fn = GetFullName(input, start);
		if (CheckError(fn)){return false;}
		return true;
	}
	
	static string[] RemoveFullName(string[] input){
		for(int i=0; i<input.Length; i++){
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
	
}
