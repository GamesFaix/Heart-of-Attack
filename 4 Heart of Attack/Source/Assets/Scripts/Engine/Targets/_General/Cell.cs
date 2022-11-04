using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {
	
	public class Cell : Target
    {

        #region //Properties

        public Board Board {get; protected set;}
        public TokenSet Occupants { get; protected set; }
		public index2 Index {get; protected set;}
		public int X {get {return Index.x;} }
		public int Y {get {return Index.y;} }
        public Plane Stop { get; set; }
        public Vector3 Location { get { return Display.gameObject.transform.position; } }
        public List<Sensor> Sensors { get; set; }
        public CellSet Links { get; set; }

        #endregion

        public override string ToString() {return "("+X+","+Y+")";}

		protected Cell () {}

		public Cell (Board board, index2 index) {
			Board = board;
			Index = index;
			CellDisplay.Attach(this);
			Links = new CellSet();
            Stop = Plane.None;
            Occupants = new TokenSet();
            Sensors = new List<Sensor>();
		}

        public Plane Occupied
        {
            get
            {
                Plane p = Plane.None;
                foreach (Token t in Occupants)
                    p = p | t.Plane;
                return p;
            }
        }

		public virtual void Enter (Token t) {
            Occupants.Add(t);
            if ((t.Plane & Plane.Sunken) == Plane.Sunken) 
                EnterSunken(t);
            for (int i=Sensors.Count-1; i>=0; i--) 
                Sensors[i].OtherEnter(t);
		}

		public virtual void EnterSunken (Token t) {((CellDisplay)Display).EnterSunken(t);}

		public virtual void Exit (Token t) {
            Occupants.Remove(t);            
			if ((t.Plane & Plane.Sunken) == Plane.Sunken) 
                ExitSunken();
            foreach (Sensor s in Sensors) 
                s.OtherExit(t);
		}

		void ExitSunken () {((CellDisplay)Display).ExitSunken();}

		public CellSet Neighbors (bool self=false) {
            CellSet neighbors = new CellSet();
			
			foreach (int2 dir in Direction.Directions) {
				Cell neighbor;
				index2 index;
				if (index2.Safe((int2)Index + dir, out index)) {
					if (Board.HasCell(index, out neighbor)) {
						neighbors.Add(neighbor);
					}
				}
			}
			if (self) {neighbors.Add(this);}
			neighbors.Add(Links);
			return neighbors;
		}

        
		public virtual bool StopToken (Token t) {
            if ( (t.Plane & Plane.All) != Plane.None) return true;
			if (Body.CanTrample(t, this)) return true;
			return false;
		}
	}
}