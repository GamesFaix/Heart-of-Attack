using UnityEngine;
using HOA.Tokens;
using HOA.Players;
using System.Collections.Generic;

public static class TurnQueue {
	static HOAQueue<Unit> units = new HOAQueue<Unit>();

	public static int Count {get {return units.Count;}}

	public static void Add (Unit u) {units.Add(u);}
	public static void Remove (Unit u) {units.Remove(u);}	
	
	public static Unit Index (int i) {return units[i];} 
	public static Unit Top {get {return units.Top;} }
	public static Unit Bottom {get {return units.Bottom;} }

	public static void MoveUp(Unit u, int n, bool log=true){
		units.MoveUp(u, n);
		if (log) {GameLog.Out(u+" moved up "+n+" slot(s) in the Queue.");}
	}	
	public static void MoveDown(Unit u, int n, bool log=true){
		units.MoveDown(u, n);
		if (log) {GameLog.Out(u+" moved down "+n+" slot(s) in the Queue.");}
	}
	public static void Shuffle(Source s, bool log=true){
		units.Shuffle();
		if (log) {GameLog.Out(s+" shuffled the Queue.");}
	}
	
	public static void Advance(bool log=true){
		QueueStunner.Find(units);

		Unit oldTop = Top;
		Unit oldBtm = Bottom;

		ResetOldTop(Top);

		units.Remove(Top);
		PrepareNewTop(Top);

		units.Add(oldTop);
		Skip(oldTop, oldBtm);

		QueueStunner.Hold(units);
		QueueStunner.Decrement();

		Referee.ActivePlayer = Top.Owner;
		
		if (log) {PrintAdvance(oldTop, Top);}
	}

	static void ResetOldTop (Unit oldTop) {
		if(oldTop.IsCOR()) {oldTop.DecayCOR();}
		oldTop.SetStat(new Source(), STAT.AP, 0, false);
		oldTop.ResetArsenal();
	}

	static void PrepareNewTop (Unit newTop) {
		newTop.ClearSkip(false);
		newTop.FillAP(false);
	}

	static void Skip (Unit oldTop, Unit oldBtm) {
		if (oldBtm.IN < oldTop.IN){
			int difference = oldTop.IN - oldBtm.IN;
			for (byte i=0; i<difference; i++){
				int index = units.IndexOf(oldTop)-1;
				if (units[index]==Top){break;}
				if (units[index].IN < oldTop.IN
				    && !units[index].IsSkipped()){
					units[index].Skip();
					MoveUp(oldTop,1,false);
				}	
			}
		}
	}

	static void PrintAdvance (Unit oldTop, Unit newTop) {
		string oldName = oldTop.FullName;
		string newName = Top.FullName;
		string oldPlayer = oldTop.Owner.ToString();
		GameLog.Out(oldPlayer+" ended "+oldName+"'s turn. "+newName+" ATTACK!");
	}
	
	public static void Undo(){
		GameLog.Debug("Queue undo not yet implemented.");
	}

	public static void Reset(){
		units = new HOAQueue<Unit>();
		GUIInspector.Inspect(default(Unit));
		GameLog.Debug("Queue cleared.");
	}
}
