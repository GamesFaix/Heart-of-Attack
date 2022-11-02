using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Players;

public static class TurnQueue {
	
	static List<Unit> units = new List<Unit>();

	public static int Count {get {return units.Count;}}

	public static void Add(Unit u){
		if (!units.Contains(u)) {units.Add(u);}
		else {GameLog.Debug("TurnQueue: Attempt to duplicate unit in Queue.");}
	}
	public static void Remove(Unit u){
		if (units.Contains(u)) {units.Remove(u);}
		else {GameLog.Debug("TurnQueue: Cannot remove unit. Does not exist.");}
	}	


	public static Unit Index (int i) {
		if (i < Count) {return units[i];}
		return default(Unit);
	}
	
	public static Unit Top {
		get {if (Count > 0) {return units[0];}
			return default(Unit);
		}
	}
	
	static Unit Bottom {
		get {return units[units.Count-1];}
	}

	public static void MoveUp(Unit u, int n, bool log=true){
		for (int i=0; i<=(n-1); i++){
			if (u != Top){
				int index = units.IndexOf(u);
				Unit temp = units[index-1];
				units.Remove(temp);
				units.Insert(index, temp);             
			}
		}
		if (log) {GameLog.Out(u+" moved up "+n+" slot(s) in the Queue.");}
	}	
	public static void MoveDown(Unit u, int n, bool log=true){
		for (int i=0; i<=(n-1); i++){
			if (u != Bottom){
				int index = units.IndexOf(u);
				Unit temp = units[index+1];
				units.Remove(temp);
				units.Insert(index, temp);           	
			}
		}
		if (log) {GameLog.Out(u+" moved down "+n+" slot(s) in the Queue.");}
	}
	
	public static void Advance(bool log=true){
		Dictionary<Unit, int> stunned = StunnedUnits();
		
		Unit oldTop = Top;
		if(oldTop.IsCOR()) {oldTop.DecayCOR();}
		oldTop.SetStat(new Source(), STAT.AP, 0, false);
		oldTop.ResetArsenal();
		
		Unit oldBtm = Bottom;
		
		units.Add(oldTop);
		units.RemoveAt(0);
		Top.ClearSkip(false);
		Top.FillAP(false);
		
		if (oldBtm.IN() < oldTop.IN()){
			int difference = oldTop.IN() - oldBtm.IN();
			for (byte i=0; i<difference; i++){
				int index = units.IndexOf(oldTop)-1;
				if (units[index]==Top){break;}
				if (units[index].IN() < oldTop.IN()
				&& !units[index].IsSkipped()){
					units[index].Skip();
					MoveUp(oldTop,1,false);
				}
				
			}
		}
		HoldStunnedUnits(stunned);
		StunDecrement();
		Player newPlayer = Top.Owner();
		Referee.SetActive(newPlayer);
		
		string oldName = oldTop.FullName();
		string newName = Top.FullName();
		string oldPlayer = oldTop.Owner().ToString();
		if (log) {GameLog.Out(oldPlayer+" ended "+oldName+"'s turn. "+newName+" ATTACK!");}
	}
	
	public static void Undo(){
		GameLog.Debug("Queue undo not yet implemented.");
	}
	
	public static void Shuffle(Source s, bool log=true){
		List<Unit> oldUnits = units;
		
		List<Unit> shufUnits = new List<Unit>();
		while (oldUnits.Count>0){
			int rand = (int)Mathf.Round(Random.Range(0,oldUnits.Count));	
			shufUnits.Add(oldUnits[rand]);
			oldUnits.Remove(oldUnits[rand]);
		}
		units = shufUnits;
		if (log) {GameLog.Out(s+" shuffled the Queue.");}
	}
	
	
	static void StunDecrement(){
		foreach (Unit u in units) {
			if (u.IsStunned()) {u.AddStat(new Source(), STAT.STUN, -1);}
		}
	}
	static Dictionary<Unit, int> StunnedUnits(){
		Dictionary<Unit,int> d = new Dictionary<Unit,int>();
		for (int i=0; i<units.Count; i++){
		     Unit u = units[i];
		     if (u.IsStunned()) {d.Add(u,i);}
		}
		return d;
	}
	static void HoldStunnedUnits(Dictionary<Unit,int> stunned){
		foreach (Unit sU in stunned.Keys){
			for (int i=0; i<units.Count; i++){
				if (units[i] == sU){
					if (i != stunned[sU]){
						units.Remove(sU);
						if (stunned[sU]!=0){units.Insert(stunned[sU], sU);}
						else {units.Add(sU);}
					}
				}
			}
		}
	}

	public static void Reset(){
		units = new List<Unit>();
		GUIInspector.Inspect(default(Unit));
		GameLog.Debug("Queue cleared.");
	}
}
