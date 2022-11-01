using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static partial class Console {
	//Unit stat mod methods

	static void UnitCommands(Command input){
		List<Token> ts = input.IHead();
		Command iTail = input.ITail();
		
		if (iTail.Head() == "UP")  {Up(ts, iTail.Tail()); return;}				
		if (iTail.Head() == "DOWN"){Down(ts, iTail.Tail()); return;}
		
		if (iTail.Head() == "IN")  {ModIN(ts, iTail.Tail());}
		if (iTail.Head() == "HP")  {ModHP(ts, iTail.Tail());}
		if (iTail.Head() == "MHP") {ModMHP(ts, iTail.Tail());}
		if (iTail.Head() == "DEF") {ModDEF(ts, iTail.Tail());}
		if (iTail.Head() == "DMG") {Damage(ts, iTail.Tail());}
		if (iTail.Head() == "STUN"){ModSTUN(ts,iTail.Tail());}
		if (iTail.Head() == "COR") {ModCOR(ts, iTail.Tail());}
		if (iTail.Head() == "AP")  {ModAP(ts, iTail.Tail());}
		if (iTail.Head() == "FP")  {ModFP(ts, iTail.Tail());}
		if (iTail.Head() == "OWNER")  {ModOwner(ts, iTail.Tail());}

	}
	
	
	static void ModIN(List<Token> ts, Command input){
		if (input.Length() > 1){
			foreach (Token t in ts){
				if (t is Unit) {
					Unit u = (Unit)t;
					int n;
					if(Int32.TryParse(input.Second(), out n)){
						if (input.Head() == "="){u.SetIN(n);}
						if (input.Head() == "+"){u.AddIN(n);}
						if (input.Head() == "-"){u.AddIN(0-n);}
					}
				}
				else {GameLog.Debug("Console: Only units have IN.");}
			}
		}
		else {GameLog.Debug("Console: IN change requires operator (+ - =) and number.");}
	}
	static void ModHP(List<Token> ts, Command input){
		if (input.Length() > 1){
			foreach (Token t in ts){
				if (t is Unit) {
					Unit u = (Unit)t;
					int n;
					if(Int32.TryParse(input.Second(), out n)){
						if (input.Head() == "="){u.SetHP(n);}
						if (input.Head() == "+"){u.AddHP(n);}
						if (input.Head() == "-"){u.AddHP(0-n);}
					}
				}
				else {GameLog.Debug("Console: Only units have HP.");}
			}
		}
		else {GameLog.Debug("Console: HP change requires operator (+ - =) and number.");}
	}
	static void ModMHP(List<Token> ts, Command input){
		if (input.Length() > 1){
			foreach (Token t in ts){
				if (t is Unit) {
					Unit u = (Unit)t;
					int n;
					if(Int32.TryParse(input.Second(), out n)){
						if (input.Head() == "="){u.SetMaxHP(n);}
						if (input.Head() == "+"){u.AddMaxHP(n);}
						if (input.Head() == "-"){u.AddMaxHP(0-n);}	
					}
				}
				else {GameLog.Debug("Console: Only units have MHP.");}
			}
		}
		else {GameLog.Debug("Console: MHP change requires operator (+ - =) and number.");}
	}
	static void ModDEF(List<Token> ts, Command input){
		if (input.Length() > 1){
			foreach (Token t in ts){
				if (t is Unit) {
					Unit u = (Unit)t;
					int n;
					if(Int32.TryParse(input.Second(), out n)){
						if (input.Head() == "="){u.SetDEF(n);}
						if (input.Head() == "+"){u.AddDEF(n);}
						if (input.Head() == "-"){u.AddDEF(0-n);}	
					}
				}
				else {GameLog.Debug("Console: Only units have DEF.");}
			}
		}
		else {GameLog.Debug("Console: DEF change requires operator (+ - =) and number.");}
	}
	static void Damage(List<Token> ts, Command input){
		if (!input.Blank()){
			foreach (Token t in ts){
				if (t is Unit) {
					Unit u = (Unit)t;
					int n;
					if(Int32.TryParse(input.Head(), out n)){
						u.Damage(n);
					}
				}
				else {GameLog.Debug("Console: Only units take damage.");}
			}
		}
		else {GameLog.Debug("Console: DMG requires a value.");}
	}


	static void ModSTUN(List<Token> ts, Command input){
		if (input.Length() > 1){
			foreach (Token t in ts){
				if (t is Unit) {
					Unit u = (Unit)t;
					int n;
					if(Int32.TryParse(input.Second(), out n)){
						if (input.Head() == "="){u.SetStun(n);}
						if (input.Head() == "+"){u.AddStun(n);}
						if (input.Head() == "-"){u.AddStun(0-n);}	
					}
				}
				else {GameLog.Debug("Console: Only units can be stunned.");}
			}
		}
		else {GameLog.Debug("Console: STUN change requires operator (+ - =) and number.");}
	}
	static void ModCOR(List<Token> ts, Command input){
		if (input.Length() > 1){
			foreach (Token t in ts){
				if (t is Unit) {
					Unit u = (Unit)t;
					int n;
					if(Int32.TryParse(input.Second(), out n)){
						if (input.Head() == "="){u.SetCOR(n);}
						if (input.Head() == "+"){u.AddCOR(n);}
						if (input.Head() == "-"){u.AddCOR(0-n);}	
					}
				}
				else {GameLog.Debug("Console: Only units can be corroded.");}
			}
		}
		else {GameLog.Debug("Console: COR change requires operator (+ - =) and number.");}
	}
	static void ModAP(List<Token> ts, Command input){
		if (input.Length() > 1){
			foreach (Token t in ts){
				if (t is Unit) {
					Unit u = (Unit)t;
					int n;
					if(Int32.TryParse(input.Second(), out n)){
						if (input.Head() == "="){u.SetAP(n);}
						if (input.Head() == "+"){u.AddAP(n);}
						if (input.Head() == "-"){u.AddAP(0-n);}	
					}
				}
				else {GameLog.Debug("Console: Only units have AP.");}
			}
		}
		else {GameLog.Debug("Console: AP change requires operator (+ - =) and number.");}
	}
	static void ModFP(List<Token> ts, Command input){
		if (input.Length() > 1){
			foreach (Token t in ts){
				if (t is Unit) {
					Unit u = (Unit)t;
					int n;
					if(Int32.TryParse(input.Second(), out n)){
						if (input.Head() == "="){u.SetFP(n);}
						if (input.Head() == "+"){u.AddFP(n);}
						if (input.Head() == "-"){u.AddFP(0-n);}	
					}
				}
				else {GameLog.Debug("Console: Only units have FP.");}
			}
		}
		else {GameLog.Debug("Console: FP change requires operator (+ - =) and number.");}
	}
	static void ModOwner(List<Token> ts, Command input){
		if (!input.Blank()){
			foreach (Token t in ts){
				int n;
				if(Int32.TryParse(input.Head(), out n)) {t.SetOwner(n);}
				else {GameLog.Debug("Console: Owner change requires a number.");}
			}
		}
	}
}
