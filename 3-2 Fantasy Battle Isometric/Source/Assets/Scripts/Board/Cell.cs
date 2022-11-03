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
			links = new CellGroup();
		}

		public Vector3 Location {get {return Display.gameObject.transform.position;} }

		Token[] tokens = new Token[Enum.GetNames(typeof(EPlane)).Length];
		
		public virtual Token Occupant (EPlane plane) {return tokens[(int)plane];}
		public virtual TokenGroup Occupants {
			get {
				TokenGroup occupants = new TokenGroup();
				for (byte i=0; i<tokens.Length; i++) {
					if (tokens[i] != null) {occupants.Add(tokens[i]);}
				}
				return occupants;
			}
		}
		public int TokenCount {get {return Occupants.Count;} }
		public bool IsEmpty () {
			if (TokenCount < 1) {return true;}
			return false;
		}
		
		public bool Occupied (EPlane p) {
			if (tokens[(int)p] != null) {return true;}
			return false;
		}
		public bool Occupied (List<EPlane> ps) {
			foreach (EPlane p in ps) {
				if (tokens[(int)p] != null) {return true;}
			}
			return false;
		}
		public bool Contains (ESpecial s) {
			foreach (Token t in Occupants) {
				if (t.Special.Is(s)) {return true;}
			}
			return false;
		}

		public bool Contains (ESpecial c, out Token occupant){
			occupant = null;
			foreach (Token t in Occupants) {
				if (t.Special.Is(c)) {
					occupant = t;
					return true;
				}
			}
			return false;
		}

		public bool Contains (EPlane p) {
			foreach (Token t in Occupants) {
				if (t.Plane.Is(p)) {return true;}
			}
			return false;
		}

		public bool Contains (EPlane p, out Token occupant){
			occupant = default(Token);
			foreach (Token t in Occupants) {
				if (t.Plane.Is(p)) {
					occupant = t;
					return true;
				}
			}
			return false;
		}

		public bool ContainsOnly (EPlane p) {
			if (Contains(p) && Occupants.Count == 1) {return true;}
			return false;
		}
		
		public virtual void Enter (Token t) {
			foreach (EPlane p in t.Plane.Value) {
				tokens[(int)p] = t;
			}
			if (t.Plane.Is(EPlane.SUNK)) {EnterSunken(t);}

			for (int i=sensors.Count-1; i>=0; i--) {
				Sensor s = sensors[i];
				s.OtherEnter(t);
			}
		}

		public virtual void EnterSunken (Token t) {((CellDisplay)Display).EnterSunken(t);}

		public virtual void Exit (Token t) {
			for (int i=0; i<=3; i++){
				if (tokens[i] == t) {tokens[i] = default(Token);}	
			}

			if (t.Plane.Is(EPlane.SUNK)) {ExitSunken();}

			foreach (Sensor s in sensors) {s.OtherExit(t);}
		}

		void ExitSunken () {((CellDisplay)Display).ExitSunken();}

		public virtual void Clear () {
			tokens = new Token[Enum.GetNames(typeof(EPlane)).Length];
		}

		//Sensors
		List<Sensor> sensors = new List<Sensor>();
	
		public virtual List<Sensor> Sensors () {return sensors;}
		public virtual void AddSensor (Sensor s) {sensors.Add(s);}
		public virtual void RemoveSensor (Sensor s) {
			if (sensors.Contains(s)) {sensors.Remove(s);}
			else {GameLog.Debug("Attempt to remove invalid sensor from cell.");}
		}

		CellGroup links;

		public void AddLink (Cell cell) {
			links.Add(cell);
		}
		public void RemoveLink (Cell cell) {
			links.Remove(cell);
		}

		public CellGroup Neighbors (bool self=false) {
			CellGroup neighbors = new CellGroup();
			
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

		bool Stop (EPlane p) {
			if (p == EPlane.SUNK) {return stop[0];}
			if (p == EPlane.GND) {return stop[1];}
			if (p == EPlane.AIR) {return stop[2];}
			if (p == EPlane.ETH) {return stop[3];}
			return false;
		}

		public virtual bool StopToken (Token t) {
			foreach (EPlane p in t.Plane.Value) {
				if (Stop(p)) {return true;}
			}
			if (Body.CanTrample(t, this)) {return true;}
			return false;
		}

		public virtual void SetStop (EPlane p, bool s) {
			if (p == EPlane.SUNK) {stop[0] = s;}
			if (p == EPlane.GND) {stop[1] = s;}
			if (p == EPlane.AIR) {stop[2] = s;}
			if (p == EPlane.ETH) {stop[3] = s;}
		}
	}
}