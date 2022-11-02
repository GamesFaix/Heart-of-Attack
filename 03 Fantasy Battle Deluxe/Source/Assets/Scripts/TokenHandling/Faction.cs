using HOA.Tokens;
using System.Collections.Generic;
using UnityEngine;

public class Faction {
	string name;
	TTYPE king;
	List<TTYPE> tokens;
	TTYPE heart;
	Color color1;
	Color color2;
	
	
	public Faction (string newName, TTYPE newKing, List<TTYPE> newTokens, TTYPE newHeart, Color newColor1, Color newColor2) {
		name = newName;
		king = newKing;
		heart = newHeart;
		
		tokens = new List<TTYPE>{king};
		Add(newTokens);
		Add(heart);
		
		color1 = newColor1;
		color2 = newColor2;
	}
	
	public override string ToString () {return name;}
	
	public void Add (TTYPE token) {tokens.Add(token);}
	public void Add (List<TTYPE> newTokens) {
		foreach (TTYPE t in newTokens) {tokens.Add(t);}
	}
	public void SetKing (TTYPE token) {
		if (!tokens.Contains(token)) {tokens.Add(token);}
		king = token;
	}
	public void SetHeart (TTYPE token) {
		if (!tokens.Contains(token)) {tokens.Add(token);}
		heart = token;
	}
	
	public List<TTYPE> Tokens () {return tokens;}
	
	public TTYPE Member (int n) {
		if (n>=0 && n<tokens.Count) {return tokens[n];}
		GameLog.Debug("Faction: Attempt to access invalid token.");
		return TTYPE.NONE;
	}
	
	public int Size () {return tokens.Count;}
	
	public TTYPE King () {return king;}
	public TTYPE Heart () {return heart;}
	
	public Color[] Colors () {
		return new Color[2] {color1, color2};
	}
	
}
