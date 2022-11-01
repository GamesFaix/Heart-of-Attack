using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TurnQueue {
	
	public static List<Unit> units = new List<Unit>();
	static Unit Top() {return units[0];}
	static Unit Bottom(){return units[units.Count-1];}
	
	public static void MoveUp(Unit unit, int n){
		for (int i=0; i<=(n-1); i++){
			if (unit != Top()){
				int index = units.IndexOf(unit);
				Unit temp = units[index-1];
				units.Remove(temp);
				units.Insert(index, temp);             
			}
		}
	}
	
	public static void MoveDown(Unit unit, int n){
		for (int i=0; i<=(n-1); i++){
			if (unit != Bottom()){
				int index = units.IndexOf(unit);
				Unit temp = units[index+1];
				units.Remove(temp);
				units.Insert(index, temp);           	
			}
		}
	}
	
	public static void Advance(){
		Dictionary<Unit, int> stunned = StunnedUnits();
		
		Unit oldTop = Top();
		CorrosionDmg(oldTop);
		
		Unit oldBtm = Bottom();
		
		units.Add(oldTop);
		units.RemoveAt(0);
		Top().skipped = false;
		
		
		if (oldBtm.init < oldTop.init){
			int difference = oldTop.init - oldBtm.init;
			for (byte i=0; i<difference; i++){
				int index = units.IndexOf(oldTop)-1;
				if (units[index]==Top()){break;}
				if (units[index].init < oldTop.init
				&& units[index].skipped == false){
					units[index].skipped = true;
					MoveUp(oldTop,1);
				}
				
			}
		}
		HoldStunnedUnits(stunned);
		StunDecrement();
	}
	public static void UndoAdvance(){
		
	
	}
	
	static void StunDecrement(){
		foreach (Unit u in units) {
			if (u.stun > 0){u.stun--;}
		}
	}
	static Dictionary<Unit, int> StunnedUnits(){
		Dictionary<Unit,int> d = new Dictionary<Unit,int>();
		for (int i=0; i<units.Count; i++){
		     Unit u = units[i];
		     if (u.stun>0){d.Add(u,i);}
		}
		return d;
	}
	static void HoldStunnedUnits(Dictionary<Unit,int> stunned){
		foreach (Unit sU in stunned.Keys){
			for (int i=0; i<units.Count; i++){
				if (units[i] == sU){
					if (i != stunned[sU]){
						units.Remove(sU);
						units.Insert(stunned[sU], sU);
					}
				}
			}
		}
	}
	static void CorrosionDmg(Unit u){
		if (u.cor>0){
			u.ModHP("-",u.cor);	
			u.cor = (int)Mathf.Floor(u.cor/2);	
		}
	}
	
	
	
	
	public static Unit FindUnit(string fullName){
		foreach(Unit u in units){
			if (u.fullName == fullName){return u;}
		}
		return default(Unit);
	}
	
	
	public static void Reset(){
		units = new List<Unit>();
		
	}
}
