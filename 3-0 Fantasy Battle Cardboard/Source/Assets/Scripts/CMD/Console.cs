using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Tokens;

public static partial class Console {
	//Input, Vocab list, &
	//Top-level interpreter command methods
	
	public static void Submit(string text){
		GameLog.In(text);
		Command input = new Command(text);
		Parse(input);
		GUILog.ScrollToBottom();
	}
	

	
	static void Parse(Command input){
		//queue commands
		if (input.Head() == "+"){TurnQueue.Advance(); return;}
		else if (input.Head() == "-"){TurnQueue.Undo(); return;}
		else if (input.Head() == "SHUFFLE"){TurnQueue.Shuffle(); return;}
		else if (input.Head() == "RESET"){Reset(); return;}
		else if (input.Head() == "SHIFT"){Shift(input.Tail()); return;}

		//creation/destruction commands
		else if (input.Head() == "START"){Start(input.Tail()); return;}
		else if (input.Head() == "CREATE" || input.Head() == "C"){Create(input.Tail()); return;}
		else if (input.Head() == "KILL" || input.Head() == "K"){Kill(input.Tail()); return;}	
		else if (input.Head() == "REPLACE" || input.Head() == "R"){Replace(input.Tail()); return;}
		else if (input.Head() == "CAPTURE"){Capture(input.Tail()); return;}

		//unit modify commands
		else if (input.IsTHead()){UnitCommands(input);}

		else {GameLog.Debug("Console: '"+input+"' is not a valid command.");}
	}
	
	static void Start(Command input){
		TurnQueue.Reset();
		while(!input.Blank()){
			Create(input);
			input = input.Tail();
		}
		TurnQueue.Shuffle(false);
		TurnQueue.Advance(false);
		GameLog.Out("New game ready.");
	}

	static void Create(Command input){
		string tryName = input.ToString();
		TTYPE code;
		if (Label.IsCode(tryName, out code)){
			UnitFactory.Add(code);
		}
		else {GameLog.Debug("Console: Cannot create '"+tryName+"'. Invalid token name.");}
	}
	static void Kill(Command input){
		if (input.IsIHead()){
			UnitFactory.Delete(input.IHead());
		}
		else {GameLog.Debug("Console: Cannot kill '"+input+"'. Token does not exist.");}
	}
	static void Replace(Command input){
		if (input.IsIHead()){
			string tryNewFN = input.ITail().ToString();
			TTYPE code;
			if (Label.IsCode(tryNewFN, out code)){
				string newFN;
				UnitFactory.Add(code, out newFN);
				UnitFactory.Delete(input.IHead());
				GameLog.Out("Replaced "+input.IHead()+" with "+newFN+".");
			}
			else {GameLog.Debug("Console: Cannot replace '"+input.IHead()+"' with '"+tryNewFN+"'. Invalid unit name.");}
		}
		else {GameLog.Debug("Console: Cannot replace '"+input+"'.  Unit does not exist.");}
	}

	static void Capture(Command input){
		if (!input.Blank()){
			int captive;
			int captor;
			if (Int32.TryParse(input.Head(), out captive)
			&& Int32.TryParse(input.Second(), out captor)){
				Roster.Capture(captive,captor);
				return;
			}
		}
		GameLog.Debug("Console: Capture requires two player numbers.");
	}

	static void Shift(Command input){UnitCommands(input);}


	static void Up(List<Token> ts, Command input){
		int n = 1;
		if(!input.Blank()) {Int32.TryParse(input.Head(), out n);}
		foreach (Token t in ts){
			if (t is Unit){TurnQueue.MoveUp((Unit)t,n);}
			else {GameLog.Debug("Console: Cannot move non-Unit in Queue.");}
		}
	}
	static void Down(List<Token> ts, Command input){
		int n = 1;
		if(!input.Blank()) {Int32.TryParse(input.Head(), out n);}
		foreach (Token t in ts){
			if (t is Unit){TurnQueue.MoveDown((Unit)t,n);}
			else {GameLog.Debug("Console: Cannot move non-Unit in Queue.");}
		}
	}

	static void Reset(){
		TurnQueue.Reset();
		GameLog.Reset();
	}
}
