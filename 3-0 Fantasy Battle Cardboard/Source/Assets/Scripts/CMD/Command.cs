using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Tokens;

public class Command {
	string input;
	string[] words;

	public Command (string str){
		input = str.ToUpper();
		char[] seperators = new char[1] {' '};
		words = input.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
	}

	public override string ToString() {return input;}
	public int Length() {return words.Length;}
	public bool Blank() {
		if (words.Length>1) {return true;}
		return false;
	}

	public string Head (){
		if (words.Length==0) {return "";}
		return words[0];
	}
	public string Second(){
		if (words.Length < 2) {return "";}
		return words[1];
	}

	public Command Tail (){
		if (words.Length>1){	
			string[] tailWords = new string[(words.Length-1)];
			Array.Copy(words, 1, tailWords, 0, words.Length-1);
			return new Command(String.Join(" ",tailWords));
		}
		else {return new Command("");}
	}

	public List<TTYPE> THead(){
		List<TTYPE> tokens = new List<TTYPE>();
		for (int i=0; i<Length(); i++){
			string word = words[i];
			TTYPE code;
			if (Label.IsCode(word, out code)) {tokens.Add(code);}
			else {break;}
		}
		return tokens;
	}
	public List<TTYPE> THead(out int nextIndex){
		List<TTYPE> tokens = new List<TTYPE>();
		nextIndex = 0;
		for (int i=0; i<Length(); i++){
			string word = words[i];
			TTYPE code;
			if (Label.IsCode(word, out code)) {tokens.Add(code);}
			else {nextIndex = i; break;}
		}
		return tokens;
	}
	public bool IsTHead() {
		if (THead().Count > 0) {return true;}
		return false;
	}
	public Command TTail(){
		if (IsTHead()) {
			int startIndex = 0;
			THead(out startIndex);
			string[] tailWords = new string[Length()-startIndex];
			Array.Copy(words, startIndex, tailWords, 0, tailWords.Length-1);
			return new Command(String.Join(" ",tailWords));
		}
		else {return new Command("");}
	}

	public List<Token> IHead(){
		List<Token> instances = new List<Token>();
		for (int i=0; i<Length(); i++){
			string word = words[i];
			Token instance;
			if (UnitFactory.IsInstance(word, out instance)) {instances.Add(instance);}
			else {break;}
		}
		return instances;
	}
	public List<Token> IHead(out int nextIndex){
		List<Token> instances = new List<Token>();
		nextIndex = 0;
		for (int i=0; i<Length(); i++){
			string word = words[i];
			Token instance;
			if (UnitFactory.IsInstance(word, out instance)) {instances.Add(instance);}
			else {nextIndex = i; break;}
		}
		return instances;
	}
	public bool IsIHead(){
		if (IHead().Count > 0) {return true;}
		return false;
	}
	public Command ITail(){
		if (IsIHead()) {
			int startIndex = 0;
			IHead(out startIndex);
			string[] tailWords = new string[Length()-startIndex];
			Array.Copy(words, startIndex, tailWords, 0, tailWords.Length-1);
			return new Command(String.Join(" ",tailWords));
		}
		else {return new Command("");}
	}

	bool IsLetter(string s){
		if ((s.Length == 1) && Char.IsLetter(s[0])){return true;}
		return false;
	}
	
	char NextLetter(out int index){
		index = (-1);
		for (int i=0; i<words.Length; i++){
			if (IsLetter(words[i])){
				index = i;
				return words[i][0];
			}
		}
		return (' ');
	}



}
