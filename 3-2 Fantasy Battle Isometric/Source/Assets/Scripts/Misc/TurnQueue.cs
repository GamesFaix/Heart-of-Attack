using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public static class TurnQueue {
		static HOAQueue<Unit> units = new HOAQueue<Unit>();

		public static int Count {get {return units.Count;}}

		public static bool Contains (Unit u) {
			if (units.Contains(u)) {return true;}
			return false;
		}

		public static void Add (Unit u) {units.Add(u);}
		public static void Remove (Unit u) {
			units.Remove(u);
			//Debug.Log("TUrnQueue removed "+u);
		}	
		
		public static Unit Index (int i) {return units[i];} 
		public static int IndexOf (Unit u) {return units.IndexOf(u);}
		public static Unit Top {get {return units.First;} }
		public static Unit Bottom {get {return units.Last;} }

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

		public static void Initialize () {
			PrepareNewTop(Top);
		}

		public static void Advance(bool log=true){
			QueueStunner.Find(units);

			Unit oldTop = Top;
			Unit oldBtm = Bottom;

			TimerTick(oldTop);

			ResetOldTop(oldTop);

			units.Remove(oldTop);
			PrepareNewTop(Top);

			if (oldTop.HP > 0) {
				units.Add(oldTop);
				Skip(oldTop, oldBtm);
			}

			QueueStunner.Hold(units);
			QueueStunner.Decrement();

			Referee.ActivePlayer = Top.Owner;
			
			if (log) {PrintAdvance(oldTop, Top);}

			if (!Top.Owner.Alive) {Advance();}
		}

		static void TimerTick (Unit oldTop) {
			List<Timer> timers = oldTop.timers;
			if (timers.Count > 0) {
				for (int i=timers.Count-1; i>=0; i--){
					Timer timer = timers[i];
					timer.Tick();
				}
			}

		}


		static void ResetOldTop (Unit oldTop) {
			if(oldTop.IsCOR()) {oldTop.DecayCOR();}
			oldTop.SetStat(new Source(), EStat.AP, 0, false);
			oldTop.ResetArsenal();
		}

		public static void PrepareNewTop (Unit newTop) {
			newTop.ClearSkip(false);
			newTop.FillAP(false);
			GUIInspector.Inspected = newTop;

			Player lastPlayer = Referee.ActivePlayer;
			Referee.ActivePlayer = newTop.Owner;


			if (lastPlayer != Referee.ActivePlayer) {
				Core.Music.clip = Referee.ActivePlayer.Faction.Theme;
				Core.Music.Play();
			}


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
			string oldName = oldTop.ToString();
			string newName = Top.ToString();
			string oldPlayer = oldTop.Owner.ToString();
			GameLog.Out(oldPlayer+" ended "+oldName+"'s turn. "+newName+" ATTACK!");
		}
		
		public static void Undo(){
			GameLog.Debug("Queue undo not yet implemented.");
		}

		public static void Reset(){
			units = new HOAQueue<Unit>();
			GUIInspector.Inspected = default(Unit);
			GameLog.Debug("Queue cleared.");
		}
	}
}