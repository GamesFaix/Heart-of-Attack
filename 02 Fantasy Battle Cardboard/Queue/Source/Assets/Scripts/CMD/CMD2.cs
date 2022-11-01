using UnityEngine;
using System;
using System.Collections;
using Tokens;

public static partial class CMD {
	//Core text interpretation methods

	
	static bool IsCode(string text, out string code){
		code = TextToCode(text);
		if (code==""){return false;}
		return true;
	}
	static string TextToCode(string text){
		if (text.Length>=4){
			string firstFour = text.Substring(0,4);
			foreach (string code in Label.Codes()){
				if (firstFour == code){return firstFour;}
			}
		}
		return "";
	}
	static bool IsLetter(string s){
		if ((s.Length == 1) && Char.IsLetter(s[0])){return true;}
		return false;
	}

	static char NextLetter(string[] input, out int index){
		index = (-1);
		for (int i=0; i<input.Length; i++){
			if (IsLetter(input[i])){
				index = i;
				return input[i][0];
			}
		}
		return (' ');
	}
	
	static string Head (string[] input){
		if (input.Length==0){return "";}
		return input[0];
	}
	static string[] Tail (string[] input){
		string[] tail; 
		if (input.Length>1){	
			tail = new string[(input.Length-1)];
			Array.Copy(input, 1, tail, 0, input.Length-1);
		}
		else {tail = new string[0];}
		return tail;
	}

	//if array starts with name and instance, returns full name as single string
	//else return blank
	static string FNead(string[] input){
		string code;
		if (IsCode(String.Join(" ",input),out code)){
			int index;
			char instance = NextLetter(input, out index);
			if (instance!=' '){
				foreach (Unit u in TurnQueue.units){
					if ((u.Code() == code) && (u.Instance() == instance)){

						return u.FullName();
					}
				}
			}
			else {GameLog.Debug(name+String.Join(" ",input)+" does not currently exist.");}	
		}	
		else {GameLog.Debug(name+String.Join(" ",input)+" is not a valid unit name.");}
		return "";
	}
	static string FNead(string[] input, out int tailStart){
		string code;
		tailStart = 0;
		if (IsCode(String.Join(" ",input),out code)){
			int index;
			char instance = NextLetter(input, out index);
			if (instance!=' '){
				foreach (Unit u in TurnQueue.units){
					if ((u.Code() == code) && (u.Instance() == instance)){
						tailStart = index+1;
						return u.FullName();
					}
				}
			}
			else {GameLog.Debug(name+String.Join(" ",input)+" does not currently exist.");}	
		}	
		else {GameLog.Debug(name+String.Join(" ",input)+" is not a valid unit name.");}
		return "";
	}

	//if array starts with name and instance, return all strings after instance
	//else return blank
	static string[] FNail(string[] input){
		int start = 0;
		string fnead = FNead(input, out start);
		if (fnead!=""){
			string[] fnail = new string[input.Length-start];
			if (fnail.Length<1){
				GameLog.Debug(name+"No command specified after unit name.");
				return new string[0];
			}
			Array.Copy(input, start, fnail, 0, input.Length-start);
			return fnail;
		}
		GameLog.Debug(name+"Input does not start with a unit full name.");
		return new string[0];
	}
}
