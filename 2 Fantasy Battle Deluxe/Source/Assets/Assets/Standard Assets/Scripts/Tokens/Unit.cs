using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FBI.Tokens;
using FBI.Actions;

public class Unit : MonoBehaviour {
	public Token token;

	public UnitStats stats;
	public List<Action> actions;
		
	public UnitStats GetStats(){return stats;}
	public Timer GetTimer(){return token.GetTimer();}
}
