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
		Unit oldTop = Top();
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
	}
	
	
	public static void Reset(){
		units = new List<Unit>();
		
		GameObject go = GameObject.Find("GameObject") as GameObject;
		QueueGUI qg = go.GetComponent("QueueGUI") as QueueGUI;
		qg.Reset();
	}
}
