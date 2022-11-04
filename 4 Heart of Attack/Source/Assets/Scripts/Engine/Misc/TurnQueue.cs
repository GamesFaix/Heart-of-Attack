using UnityEngine;
using System;

namespace HOA {

	public static class TurnQueue 
    {
        static TokenSet units;

		public static void Add (Unit u) 
        {
            units.Add(u); 
            Skip(u);
        }
		public static void Remove (Unit u) 
        {
            bool flag = false;
            if (u == Top) 
                flag = true;
            units.Remove(u);
            if (flag) 
                Initialize();
        }
        
        public static int Count { get { return units.Count; } }
        public static bool Contains(Unit u) { return units.Contains(u); }
        public static Unit Index(int i) { return units[i] as Unit; }
		public static int IndexOf (Unit u) {return units.IndexOf(u);}
        
        public static Unit Top { get { return units[0] as Unit; } }
        public static Unit Bottom { get { return units[Count - 1] as Unit; } }

		public static void Shuffle() {units.Shuffle();}

        public static void Shift(Unit u, int distance)
        {
            if (Contains(u))
            {
                int magnitude = Math.Abs(distance);
                for (int i = 0; i < magnitude; i++)
                {
                    int index = IndexOf(u);

                    if (index > 0 && distance < 0)
                    {
                        Unit temp = Index(index - 1);
                        Remove(temp);
                        units.Insert(index, temp);
                    }

                    else if (index < Count - 1 && distance > 0)
                    {
                        Unit temp = Index(index + 1);
                        Remove(temp);
                        units.Insert(index, temp);
                    }
                }
            }
            else
                throw new Exception("Queue does not contain " + u + ".");

        }

		public static void Advance()
        {
			Unit oldTop = Top;
            oldTop.OnEndTurn();
            units.Remove(oldTop);
            Unit newTop = Top;

            if (oldTop.HP > 0) 
            {
				units.Add(oldTop);
				Skip(oldTop);
			}

            TurnChangePublish(oldTop, newTop);

            Initialize();
            Referee.ActivePlayer = Top.Owner;
			if (!Top.Owner.Alive) 
                Advance();
		}
        
        static void Skip(Unit oldTop)
        {
            int i = oldTop.IN;

            while (i > 1)
            {
                int index = units.IndexOf(oldTop);
                if (index > 1)
                {
                    Unit other = units[index - 1] as Unit;
                    int oldTopRandom = UnityEngine.Random.Range(0, i);
                    int otherRandom = UnityEngine.Random.Range(0, other.IN);
                    if (oldTopRandom > otherRandom)
                    {
                        Shift(oldTop, -1);
                        i--;
                    }
                    else i = 0;
                }
                else i = 0;
            }
        }
        
        public static void Initialize() { Top.OnStartTurn(); }

		public static void Reset ()
        {
            units = new TokenSet();
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