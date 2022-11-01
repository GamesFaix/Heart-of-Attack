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
			//TurnQueue.Undo();
			return "Turn advance undone. (NYI)";
		}
		else if (input[0] == "RESET"){
			TurnQueue.Reset();
			return "Game reset.";
		}

		else if (input[0] == "CREATE"){
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
		
		else{ 
			string fn;
			if (ValidFullName(input,0)){
				fn = GetFullName(input,0);
				
				return "Selected "+fn+".";
			}
			return GetFullName(input,0);
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
		if (TextToCode(text)!="ERROR"){return true;}
		return false;
	}

	static string GetFullName(string[] input, int start){
		string tryName = String.Join(" ",input,start,(input.Length-1));
		if (ValidCode(tryName)){
			string tryCode = TextToCode(tryName);
			char tryInstance = ' ';
			for (int i=start+1; i<input.Length; i++){
				if (input[i].Length == 1){tryInstance = input[i][0];}	
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
		string first5 = fn.Substring(0,5);
		if (first5 != "ERROR"){return true;}
		return false;
	}

}
