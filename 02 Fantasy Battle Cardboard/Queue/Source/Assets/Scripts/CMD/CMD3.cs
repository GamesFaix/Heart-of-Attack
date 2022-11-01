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
				if (input[0] == "+"){u.ModIN(n);}
				if (input[0] == "-"){u.ModIN(0-n);}
			}
		}
		else {GameLog.Add(name+"IN change requires operator (+ - =) and number.", LogIO.DEBUG);}
	}
	static void ModHP(Unit u, string[] input){
		if (input.Length >= 2){
			int n;
			if(Int32.TryParse(input[1], out n)){
				if (input[0] == "="){u.SetHP(n);}
				if (input[0] == "+"){u.ModHP(n);}
				if (input[0] == "-"){u.ModHP(0-n);}
			}
		}
		else {GameLog.Add(name+"HP change requires operator (+ - =) and number.", LogIO.DEBUG);}
	}
	static void ModMHP(Unit u, string[] input){
		if (input.Length >= 2){
			int n;
			if(Int32.TryParse(input[1], out n)){
				if (input[0] == "="){u.SetMHP(n);}
				if (input[0] == "+"){u.ModMHP(n);}
				if (input[0] == "-"){u.ModMHP(0-n);}	
			}
		}
		else {GameLog.Add(name+"MHP change requires operator (+ - =) and number.", LogIO.DEBUG);}
	}
	static void ModDEF(Unit u, string[] input){
		if (input.Length >= 2){
			int n;
			if(Int32.TryParse(input[1], out n)){
				if (input[0] == "="){u.SetDEF(n);}
				if (input[0] == "+"){u.ModDEF(n);}
				if (input[0] == "-"){u.ModDEF(0-n);}	
			}
		}
		else {GameLog.Add(name+"DEF change requires operator (+ - =) and number.", LogIO.DEBUG);}
	}
	static void Damage(Unit u, string[] input){
		if (input.Length >= 1){
			int n;
			if(Int32.TryParse(input[0], out n)){
				u.Damage(n);
			}
		}
		else {GameLog.Add(name+"DMG requires a value.", LogIO.DEBUG);}
	}


	static void ModSTUN(Unit u, string[] input){
		if (input.Length >= 2){
			int n;
			if(Int32.TryParse(input[1], out n)){
				if (input[0] == "="){u.SetSTUN(n);}
				if (input[0] == "+"){u.ModSTUN(n);}
				if (input[0] == "-"){u.ModSTUN(0-n);}	
			}
		}
		else {GameLog.Add(name+"STUN change requires operator (+ - =) and number.", LogIO.DEBUG);}
	}
	static void ModCOR(Unit u, string[] input){
		if (input.Length >= 2){
			int n;
			if(Int32.TryParse(input[1], out n)){
				if (input[0] == "="){u.SetCOR(n);}
				if (input[0] == "+"){u.ModCOR(n);}
				if (input[0] == "-"){u.ModCOR(0-n);}	
			}
		}
		else {GameLog.Add(name+"COR change requires operator (+ - =) and number.", LogIO.DEBUG);}
	}
	static void ModAP(Unit u, string[] input){
		if (input.Length >= 2){
			int n;
			if(Int32.TryParse(input[1], out n)){
				if (input[0] == "="){u.SetAP(n);}
				if (input[0] == "+"){u.ModAP(n);}
				if (input[0] == "-"){u.ModAP(0-n);}	
			}
		}
		else {GameLog.Add(name+"AP change requires operator (+ - =) and number.", LogIO.DEBUG);}
	}
	static void ModFP(Unit u, string[] input){
		if (input.Length >= 2){
			int n;
			if(Int32.TryParse(input[1], out n)){
				if (input[0] == "="){u.SetFP(n);}
				if (input[0] == "+"){u.ModFP(n);}
				if (input[0] == "-"){u.ModFP(0-n);}	
			}
		}
		else {GameLog.Add(name+"FP change requires operator (+ - =) and number.", LogIO.DEBUG);}
	}
	static void ModOwner(Unit u, string[] input){
		if (input.Length >= 1){
			int n;
			if(Int32.TryParse(input[0], out n)) {u.SetOwner(n);}
		}
		else {GameLog.Add(name+"Owner change requires a number.", LogIO.DEBUG);}
	}
}
