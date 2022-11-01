using UnityEngine;
using System;
using System.Collections;

public static partial class CMD {
	//Unit stat mod methods

	static void UnitCommands(string[] input){
		Unit u = TurnQueue.FindUnit(FNead(input));
		string[] fnail = FNail(input);
		
		if (Head(fnail) == "UP")  {Up(u, Tail(fnail)); return;}				
		if (Head(fnail) == "DOWN"){Down(u, Tail(fnail)); return;}
		
		if (Head(fnail) == "IN")  {ModIN(u, Tail(fnail));}
		if (Head(fnail) == "HP")  {ModHP(u, Tail(fnail));}
		if (Head(fnail) == "MHP") {ModMHP(u, Tail(fnail));}
		if (Head(fnail) == "DEF") {ModDEF(u, Tail(fnail));}
		if (Head(fnail) == "DMG") {Damage(u, Tail(fnail));}
		if (Head(fnail) == "STUN"){ModSTUN(u, Tail(fnail));}
		if (Head(fnail) == "COR") {ModCOR(u, Tail(fnail));}
		if (Head(fnail) == "AP")  {ModAP(u, Tail(fnail));}
		if (Head(fnail) == "FP")  {ModFP(u, Tail(fnail));}
		if (Head(fnail) == "OWNER")  {ModOwner(u, Tail(fnail));}

	}
	
	
	static void ModIN(Unit u, string[] input){
		if (input.Length >= 2){
			int n;
			if(Int32.TryParse(input[1], out n)){
				if (input[0] == "="){u.SetIN(n);}
				if (input[0] == "+"){u.AddIN(n);}
				if (input[0] == "-"){u.AddIN(0-n);}
			}
		}
		else {GameLog.Debug(name+"IN change requires operator (+ - =) and number.");}
	}
	static void ModHP(Unit u, string[] input){
		if (input.Length >= 2){
			int n;
			if(Int32.TryParse(input[1], out n)){
				if (input[0] == "="){u.SetHP(n);}
				if (input[0] == "+"){u.AddHP(n);}
				if (input[0] == "-"){u.AddHP(0-n);}
			}
		}
		else {GameLog.Debug(name+"HP change requires operator (+ - =) and number.");}
	}
	static void ModMHP(Unit u, string[] input){
		if (input.Length >= 2){
			int n;
			if(Int32.TryParse(input[1], out n)){
				if (input[0] == "="){u.SetMaxHP(n);}
				if (input[0] == "+"){u.AddMaxHP(n);}
				if (input[0] == "-"){u.AddMaxHP(0-n);}	
			}
		}
		else {GameLog.Debug(name+"MHP change requires operator (+ - =) and number.");}
	}
	static void ModDEF(Unit u, string[] input){
		if (input.Length >= 2){
			int n;
			if(Int32.TryParse(input[1], out n)){
				if (input[0] == "="){u.SetDEF(n);}
				if (input[0] == "+"){u.AddDEF(n);}
				if (input[0] == "-"){u.AddDEF(0-n);}	
			}
		}
		else {GameLog.Debug(name+"DEF change requires operator (+ - =) and number.");}
	}
	static void Damage(Unit u, string[] input){
		if (input.Length >= 1){
			int n;
			if(Int32.TryParse(input[0], out n)){
				u.Damage(n);
			}
		}
		else {GameLog.Debug(name+"DMG requires a value.");}
	}


	static void ModSTUN(Unit u, string[] input){
		if (input.Length >= 2){
			int n;
			if(Int32.TryParse(input[1], out n)){
				if (input[0] == "="){u.SetStun(n);}
				if (input[0] == "+"){u.AddStun(n);}
				if (input[0] == "-"){u.AddStun(0-n);}	
			}
		}
		else {GameLog.Debug(name+"STUN change requires operator (+ - =) and number.");}
	}
	static void ModCOR(Unit u, string[] input){
		if (input.Length >= 2){
			int n;
			if(Int32.TryParse(input[1], out n)){
				if (input[0] == "="){u.SetCOR(n);}
				if (input[0] == "+"){u.AddCOR(n);}
				if (input[0] == "-"){u.AddCOR(0-n);}	
			}
		}
		else {GameLog.Debug(name+"COR change requires operator (+ - =) and number.");}
	}
	static void ModAP(Unit u, string[] input){
		if (input.Length >= 2){
			int n;
			if(Int32.TryParse(input[1], out n)){
				if (input[0] == "="){u.SetAP(n);}
				if (input[0] == "+"){u.AddAP(n);}
				if (input[0] == "-"){u.AddAP(0-n);}	
			}
		}
		else {GameLog.Debug(name+"AP change requires operator (+ - =) and number.");}
	}
	static void ModFP(Unit u, string[] input){
		if (input.Length >= 2){
			int n;
			if(Int32.TryParse(input[1], out n)){
				if (input[0] == "="){u.SetFP(n);}
				if (input[0] == "+"){u.AddFP(n);}
				if (input[0] == "-"){u.AddFP(0-n);}	
			}
		}
		else {GameLog.Debug(name+"FP change requires operator (+ - =) and number.");}
	}
	static void ModOwner(Unit u, string[] input){
		if (input.Length >= 1){
			int n;
			if(Int32.TryParse(input[0], out n)) {u.SetOwner(n);}
		}
		else {GameLog.Debug(name+"Owner change requires a number.");}
	}
}
