using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {
	
	public class Cell : Target {
		public Board Board {get; protected set;}

		public index2 Index {get; protected set;}
		public int X {get {return Index.x;} }
		public int Y {get {return Index.y;} }

		public override string ToString() {return "("+X+","+Y+")";}

		protected Cell () {}

		public Cell (Board board, index2 index) {
			Board = board;
			Index = index;
			CellDisplay.Attach(this);
			links = new TargetGroup();
            TargetClass = TargetClass.Cell();
		}

		public Vector3 Location {get {return Display.gameObject.transform.position;} }

		Token[] tokens = new Token[Enum.GetNames(typeof(Planes)).Length];
		
		public virtual Token Occupant (Planes plane) {return tokens[(int)plane];}
		public virtual TargetGroup Occupants {
			get {
				TargetGroup occupants = new TargetGroup();
				for (byte i=0; i<tokens.Length; i++) {
					if (tokens[i] != null) {occupants.Add(tokens[i]);}
				}
				return occupants;
			}
		}
		public int TokenCount {get {return Occupants.Count;} }
        public bool IsEmpty() { return (TokenCount < 1); }
		
		public bool Occupied (Planes p) { return (tokens[(int)p] != null); }
		public bool Occupied (Plane plane) {
			foreach (Planes p in plane) 
				if (tokens[(int)p] != null) return true;
			return false;
		}
		public bool Contains (TargetClasses tc) {
			foreach (Token t in Occupants)
				if (t.TargetClass[tc]) return true;
			return false;
		}

		public bool Contains (TargetClasses tc, out Token occupant){
			occupant = null;
			foreach (Token t in Occupants) {
				if (t.TargetClass[tc]) {
					occupant = t;
					return true;
				}
			}
			return false;
		}

		public bool Contains (Planes p) {
			foreach (Token t in Occupants)
                if (t.Plane[p]) return true;
			return false;
		}

		public bool Contains (Planes p, out Token occupant){
			occupant = default(Token);
			foreach (Token t in Occupants) {
				if (t.Plane[p]) {
					occupant = t;
					return true;
				}
			}
			return false;
		}

        public bool ContainsOnly(Planes p) { return (Contains(p) && Occupants.Count == 1); }
		
		public virtual void Enter (Token t) {
			foreach (Planes p in t.Plane) 
                tokens[(int)p] = t;
			if (t.Plane[Planes.Sunken]) EnterSunken(t);
            for (int i=sensors.Count-1; i>=0; i--) {
				Sensor s = sensors[i];
				s.OtherEnter(t);
			}
		}

		public virtual void EnterSunken (Token t) {((CellDisplay)Display).EnterSunken(t);}

		public virtual void Exit (Token t) {
			for (int i=0; i<=3; i++)
				if (tokens[i] == t) tokens[i] = default(Token);	
			if (t.Plane[Planes.Sunken]) ExitSunken();
            foreach (Sensor s in sensors) s.OtherExit(t);
		}

		void ExitSunken () {((CellDisplay)Display).ExitSunken();}

		public virtual void Clear () {
			tokens = new Token[Enum.GetNames(typeof(Planes)).Length];
		}

		//Sensors
		List<Sensor> sensors = new List<Sensor>();
	
		public virtual List<Sensor> Sensors () {return sensors;}
		public virtual void AddSensor (Sensor s) {sensors.Add(s);}
		public virtual void RemoveSensor (Sensor s) {
			if (sensors.Contains(s)) {sensors.Remove(s);}
			else {GameLog.Debug("Attempt to remove invalid sensor from cell.");}
		}

		TargetGroup links;

		public void AddLink (Cell cell) { links.Add(cell); }
		public void RemoveLink (Cell cell) { links.Remove(cell); }

		public TargetGroup Neighbors (bool self=false) {
			TargetGroup neighbors = new TargetGroup();
			
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
			neighbors.Add(links);
			return neighbors;
		}

		bool[] stop = new bool[4];

		bool Stop (Planes p) {
			if (p == Planes.Sunken) {return stop[0];}
			if (p == Planes.Ground) {return stop[1];}
			if (p == Planes.Air) {return stop[2];}
			if (p == Planes.Ethereal) {return stop[3];}
			return false;
		}

		public virtual bool StopToken (Token t) {
			foreach (Planes p in t.Plane) {
				if (Stop(p)) {return true;}
			}
			if (Body.CanTrample(t, this)) {return true;}
			return false;
		}

		public virtual void SetStop (Planes p, bool s) {
			if (p == Planes.Sunken) {stop[0] = s;}
			if (p == Planes.Ground) {stop[1] = s;}
			if (p == Planes.Air) {stop[2] = s;}
			if (p == Planes.Ethereal) {stop[3] = s;}
		}
	}
}