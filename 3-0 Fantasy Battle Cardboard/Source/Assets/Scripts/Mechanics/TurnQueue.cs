using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TurnQueue {
	
	public static List<Unit> units = new List<Unit>();
	public static Unit FindUnit(string fullName){
		foreach(Unit u in units){
			if (u.FullName() == fullName){return u;}
		}
		return default(Unit);
	}
	public static int Length(){return units.Count;}

	static Unit Top() {return units[0];}
	static Unit Bottom(){return units[units.Count-1];}
	
	public static void MoveUp(Unit u, int n, bool log=true){
		for (int i=0; i<=(n-1); i++){
			if (u != Top()){
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
			if (u != Bottom()){
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
		
		Unit oldTop = Top();
		if(oldTop.IsCOR()) {oldTop.DecayCOR();}
		oldTop.SetAP(0,false);

		Unit oldBtm = Bottom();
		
		units.Add(oldTop);
		units.RemoveAt(0);
		Top().ClearSkip(false);
		Top().FillAP(false);
		
		if (oldBtm.IN() < oldTop.IN()){
			int difference = oldTop.IN() - oldBtm.IN();
			for (byte i=0; i<difference; i++){
				int index = units.IndexOf(oldTop)-1;
				if (units[index]==Top()){break;}
				if (units[index].IN() < oldTop.IN()
				&& !units[index].IsSkipped()){
					units[index].Skip();
					MoveUp(oldTop,1,false);
				}
				
			}
		}
		HoldStunnedUnits(stunned);
		StunDecrement();
		if (log) {GameLog.Out("Turn advanced.");}
	}
	public static void Undo(){
		GameLog.Debug("Queue undo not yet implemented.");
	}
	
	public static void Shuffle(bool log=true){
		List<Unit> oldUnits = units;
		
		List<Unit> shufUnits = new List<Unit>();
		while (oldUnits.Count>0){
			int rand = (int)Mathf.Round(Random.Range(0,oldUnits.Count));	
			shufUnits.Add(oldUnits[rand]);
			oldUnits.Remove(oldUnits[rand]);
		}
		units = shufUnits;
		if (log) {GameLog.Out("Queue shuffled.");}
	}
	
	
	static void StunDecrement(){
		foreach (Unit u in units) {
			if (u.IsStunned()) {u.AddStun(-1);}
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
