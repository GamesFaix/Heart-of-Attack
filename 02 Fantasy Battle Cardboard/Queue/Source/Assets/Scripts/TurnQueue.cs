using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TurnQueue {
	
	public static List<Unit> units = new List<Unit>();
	public static Unit FindUnit(string fullName){
		foreach(Unit u in units){
			if (u.fullName == fullName){return u;}
		}
		return default(Unit);
	}
	static Unit Top() {return units[0];}
	static Unit Bottom(){return units[units.Count-1];}
	
	public static void MoveUp(Unit u, int n, bool report=true){
		for (int i=0; i<=(n-1); i++){
			if (u != Top()){
				int index = units.IndexOf(u);
				Unit temp = units[index-1];
				units.Remove(temp);
				units.Insert(index, temp);             
			}
		}
		if (report==true){GameLog.Add(u.fullName+" moved up "+n+" slot(s) in the Queue.",LogIO.OUT);}
	}	
	public static void MoveDown(Unit u, int n){
		for (int i=0; i<=(n-1); i++){
			if (u != Bottom()){
				int index = units.IndexOf(u);
				Unit temp = units[index+1];
				units.Remove(temp);
				units.Insert(index, temp);           	
			}
		}
		GameLog.Add(u.fullName+" moved down "+n+" slot(s) in the Queue.",LogIO.OUT);
	}
	
	public static void Advance(bool log=true){
		Dictionary<Unit, int> stunned = StunnedUnits();
		
		Unit oldTop = Top();
		CorrosionDmg(oldTop);
		oldTop.SetAP(0,false);

		Unit oldBtm = Bottom();
		
		units.Add(oldTop);
		units.RemoveAt(0);
		Top().skipped = false;
		Top().SetAP(Top().TurnAP(),false);
		
		if (oldBtm.IN() < oldTop.IN()){
			int difference = oldTop.IN() - oldBtm.IN();
			for (byte i=0; i<difference; i++){
				int index = units.IndexOf(oldTop)-1;
				if (units[index]==Top()){break;}
				if (units[index].IN() < oldTop.IN()
				&& units[index].skipped == false){
					units[index].skipped = true;
					MoveUp(oldTop,1,false);
				}
				
			}
		}
		HoldStunnedUnits(stunned);
		StunDecrement();
		if(log){GameLog.Add("Turn advanced.", LogIO.OUT);}
	}
	public static void Undo(){
		GameLog.Add("Queue undo not yet implemented.", LogIO.DEBUG);
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
		if(log){GameLog.Add("Queue shuffled.",LogIO.OUT);}
	}
	
	
	static void StunDecrement(){
		foreach (Unit u in units) {
			if (u.STUN() > 0){u.ModSTUN(-1);}
		}
	}
	static Dictionary<Unit, int> StunnedUnits(){
		Dictionary<Unit,int> d = new Dictionary<Unit,int>();
		for (int i=0; i<units.Count; i++){
		     Unit u = units[i];
		     if (u.STUN()>0){d.Add(u,i);}
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

	static void CorrosionDmg(Unit u){
		if (u.COR()>0){
			GameLog.Add(u.fullName+" takes "+u.COR()+" corrision damage.", LogIO.OUT);
			u.ModHP(0-u.COR());	
			u.SetCOR((int)Mathf.Floor(u.COR()/2));	
		}
	}
	
	public static void Reset(){
		units = new List<Unit>();
		GUIStats.Inspect(default(Unit));
		GameLog.Add("Queue cleared.", LogIO.DEBUG);
	}
}
