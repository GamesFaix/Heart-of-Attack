using UnityEngine;
using System;
using System.Collections;

public static partial class CMD {
	//Input, Vocab list, &
	//Top-level interpreter command methods

	static string name = "CMD: ";

	public static void New(string input){
		GameLog.In(input);
		Parse(Format(input));
		GUILog.ScrollToBottom();
	}
	
	static string[] Format(string input){
		string allCaps = input.ToUpper();
		string[] separators = new string[]{" "};
		string[] words = allCaps.Split(separators, StringSplitOptions.RemoveEmptyEntries);
		return words;
	}
	
	static void Parse(string[] input){
		//queue commands
		if (Head(input) == "+"){TurnQueue.Advance(); return;}
		else if (Head(input) == "-"){TurnQueue.Undo(); return;}
		else if (Head(input) == "SHUFFLE"){TurnQueue.Shuffle(); return;}
		else if (Head(input) == "RESET"){Reset(); return;}
		else if (Head(input) == "SHIFT"){Shift(Tail(input)); return;}

		//creation/destruction commands
		else if (Head(input) == "START"){Start(Tail(input)); return;}
		else if (Head(input) == "CREATE" || Head(input) == "C"){Create(Tail(input)); return;}
		else if (Head(input) == "KILL" || Head(input) == "K"){Kill(Tail(input)); return;}	
		else if (Head(input) == "REPLACE" || Head(input) == "R"){Replace(Tail(input)); return;}
		else if (Head(input) == "CAPTURE"){Capture(Tail(input)); return;}

		//unit modify commands
		else if (FNead(input)!=""){UnitCommands(input);}

		else {GameLog.Debug(name+input+" is not a valid command.");}
	}
	
	static void Start(string[] input){
		TurnQueue.Reset();
		while(input.Length>0){
			Create(input);
			input = Tail(input);
		}
		TurnQueue.Shuffle(false);
		TurnQueue.Advance(false);
		GameLog.Out("New game ready.");
	}

	static void Create(string[] input){
		string tryName = String.Join(" ",input);
		string code;
		if (IsCode(tryName, out code)){
			UnitFactory.Add(code);
		}
		else {GameLog.Debug(name+"Cannot create "+tryName+". Invalid unit name.");}
	}
	static void Kill(string[] input){
		string fn;
		if (FNead(input)!=""){
			fn = FNead(input);
			UnitFactory.Delete(fn);

		}
		else {GameLog.Debug(name+"Cannot kill "+input+". Unit does not exist.");}
	}
	static void Replace(string[] input){
		string oldFN;
		if (FNead(input)!=""){
			oldFN = FNead(input);
			string tryNewFN = String.Join(" ",FNail(input));
			string code;
			if (IsCode(tryNewFN, out code)){
				string newFN;
				UnitFactory.Add(code, out newFN);
				UnitFactory.Delete(oldFN);
				GameLog.Out("Replaced "+oldFN+" with "+newFN+".");
			}
			else {GameLog.Debug(name+"Cannot replace "+oldFN+" with "+tryNewFN+". Invalid unit name.");}
		}
		else {GameLog.Debug(name+"Cannot replace "+input+".  Unit does not exist.");}
	}

	static void Capture(string[] input){
		if (input.Length>1){
			int captive;
			int captor;
			if (Int32.TryParse(input[0], out captive)
			&& Int32.TryParse(input[1], out captor)){
				Roster.Capture(captive,captor);
				return;
			}
		}
		GameLog.Debug(name+"Capture requires two player numbers.");
	}

	static void Shift(string[] input){UnitCommands(input);}


	static void Up(Unit u, string[] input){
		int n = 1;
		if(input.Length > 0) {Int32.TryParse(input[0], out n);}
		TurnQueue.MoveUp(u,n);
	}
	static void Down(Unit u, string[] input){
		int n = 1;
		if(input.Length > 0){Int32.TryParse(input[0], out n);}
		TurnQueue.MoveDown(u,n);
	}

	static void Reset(){
		TurnQueue.Reset();
		GameLog.Reset();
	}
}
