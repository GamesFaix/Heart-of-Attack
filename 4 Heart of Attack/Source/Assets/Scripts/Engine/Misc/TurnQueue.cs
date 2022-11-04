using UnityEngine;
using System;
using System.Collections.Generic;

namespace HOA {

   
	public static class TurnQueue {
		static Group<Unit> units;

		public static int Count {get {return units.Count;} }
		public static bool Contains (Unit u) {return ((units.Contains(u)) ? true : false);}
		public static void Add (Unit u) {units.Add(u); Skip(u);}
		public static void Remove (Unit u) {units.Remove(u);}	
		public static Unit Index (int i) {return units[i];}
		public static int IndexOf (Unit u) {return units.IndexOf(u);}
		public static Unit Top {get {return units[0];} }
		public static Unit Bottom {get {return units[Count-1];} }

		public static void Shuffle() {units.Shuffle();}

		public static void MoveUp (Unit u, int spaces){
			if (Contains(u)) {
				for (int i=0; i<spaces; i++){
					int index = IndexOf(u);
					if (index > 0){
						Unit temp = Index(index-1);
						Remove(temp);
						units.Insert(index, temp);             
					}
				}
			}
			else {Debug.Log("Queue cannot MoveUp item, item not in Queue.");}
		}
		public static void MoveDown (Unit u, int spaces){
			if (Contains(u)) {
				for (int i=0; i<spaces; i++){
					int index = IndexOf(u);
					if (index < Count-1){
						Unit temp = Index(index+1);
						Remove(temp);
						units.Insert(index, temp);           	
					}
				}
			}
			else {Debug.Log("Queue cannot MoveDown item, item not in Queue.");}
		}
		

		public static void Initialize () {PrepareNewTop(Top);}

		public static void Advance(){
			Unit oldTop = Top;
			EffectQueue.Add(Effect.SetStat(Source.Neutral, oldTop, Stats.Energy, 0));
			oldTop.Arsenal.Reset();

			units.Remove(oldTop);
			if (oldTop.HP > 0) {
				units.Add(oldTop);
				Skip(oldTop);
			}

            TurnChangePublish(oldTop, Top);

			Stun();
			PrepareNewTop(Top);
			Referee.ActivePlayer = Top.Owner;
			if (!Top.Owner.Alive) {Advance();}
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

		static void Skip (Unit oldTop) {
			int i = oldTop.IN;

			while (i > 1) {
				int index = units.IndexOf(oldTop);
				if (index > 1) {
					Unit other = units[index-1];
					int oldTopRandom = UnityEngine.Random.Range(0,i);
					int otherRandom = UnityEngine.Random.Range(0,other.IN);
					if (oldTopRandom > otherRandom) {
						MoveUp(oldTop,1);
						i--;
					}
					else {i = 0;}
				}
				else {i = 0;}
			}
		}

		static void Stun () {
			for (int i=Count-1; i>=0; i--) {
				Unit u = units[i];
				if (u.STUN > 0) {
					MoveDown(u, 1);
					u.AddStat(Source.Neutral, Stats.Stun, -1);
				}
			}
		}

		public static void Reset () {
			units = new Group<Unit>();
			GUIInspector.Inspected = null;
		}

        public static event EventHandler<TurnChangeEventArgs> TurnChangeEvent;

        public static void TurnChangePublish(Unit lastUnit, Unit newUnit)
        {
            if (TurnChangeEvent != null) 
            { 
                TurnChangeEvent(null, new TurnChangeEventArgs(lastUnit, newUnit));
                Debug.Log("Unfinished code: TurnChangeEvent sender null.");
            }

        }
	}

    public class TurnChangeEventArgs : EventArgs
    {
        public Unit LastUnit { get; private set; }
        public Unit NewUnit { get; private set; }

        public TurnChangeEventArgs(Unit lastUnit, Unit newUnit)
        {
            LastUnit = lastUnit;
            NewUnit = newUnit;
        }
    }
}