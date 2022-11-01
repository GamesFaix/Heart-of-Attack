using UnityEngine;
using System;
using System.Collections;

public static partial class CMD {
	//Input, Vocab list, &
	//Top-level interpreter command methods

	static string name = "CMD: ";

	public static void New(string input){
		GameLog.Add(input,LogIO.IN);
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

		//display commands
		else if (Head(input) == "GUI"){
			input = Tail(input);
			if (Head(input) == "LOG"){LogDisplay(Tail(input)); return;}
		}
		//unit modify commands
		else if (FNead(input)!=""){UnitCommands(input);}

		else {GameLog.Add(name+input+" is not a valid command.",LogIO.DEBUG);}
	}
	
	static void Start(string[] input){
		TurnQueue.Reset();
		while(input.Length>0){
			Create(input);
			input = Tail(input);
		}
		TurnQueue.Shuffle(false);
		TurnQueue.Advance(false);
		GameLog.Add("New game ready.", LogIO.DEBUG);
	}

	static void Create(string[] input){
		string tryName = String.Join(" ",input);
		string code;
		if (IsCode(tryName, out code)){
			UnitFactory.Add(code);
		}
		else {GameLog.Add(name+"Cannot create "+tryName+". Invalid unit name.",LogIO.DEBUG);}
	}
	static void Kill(string[] input){
		string fn;
		if (FNead(input)!=""){
			fn = FNead(input);
			UnitFactory.Delete(fn);

		}
		else {GameLog.Add(name+"Cannot kill "+input+". Unit does not exist.", LogIO.DEBUG);}
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
				GameLog.Add("Replaced "+oldFN+" with "+newFN+".", LogIO.OUT);
			}
			else {GameLog.Add(name+"Cannot replace "+oldFN+" with "+tryNewFN+". Invalid unit name.",LogIO.DEBUG);}
		}
		else {GameLog.Add(name+"Cannot replace "+input+".  Unit does not exist.", LogIO.DEBUG);}
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
		GameLog.Add(name+"Capture requires two player numbers.", LogIO.DEBUG);
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
	

	
	static void LogDisplay (string[] input){
		if (Head(input) == "IN"){ 
			GUILog.inLog = !GUILog.inLog;
			GameLog.Add("Input log display toggled.", LogIO.DEBUG);
			return;
		}
		else if (Head(input) == "OUT"){ 
			GUILog.outLog = !GUILog.outLog;
			GameLog.Add("Output log display toggled.", LogIO.DEBUG);
			return;
		}
		else if (Head(input) == "DEBUG"){ 
			GUILog.debugLog = !GUILog.debugLog;
			GameLog.Add("Debug log display toggled.", LogIO.DEBUG);
			return;
		}
		else if (Head(input) == "HIDE"){ 
			GUILog.Hide();
			GameLog.Add("Log hidden.", LogIO.DEBUG);
			return;
		}
		else if (Head(input) == "SHOW"){ 
			GUILog.Show();
			GameLog.Add("Log unhidden.", LogIO.DEBUG);
			return;
		}
		else {GameLog.Add("CMD: Invalid LogDisplay command.", LogIO.DEBUG);}
	}

	static void Reset(){
		TurnQueue.Reset();
		GameLog.Reset();
	}
}
