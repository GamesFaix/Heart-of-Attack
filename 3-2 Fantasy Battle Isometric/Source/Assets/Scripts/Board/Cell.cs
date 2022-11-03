using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {
	
	public class Cell : Target {
		public Board Board {get; protected set;}

		public index2 Index {get; protected set;}
		public int X {get {return Index.x;} }
		public int Y {get {return Index.y;} }

		public Plane stop {get; set;}

		public override string ToString() {return "("+X+","+Y+")";}

		protected Cell () {}

		public Cell (Board board, index2 index) {
			Board = board;
			Index = index;
			CellDisplay.Attach(this);
			links = new CellGroup();
			stop = new Plane(false,false,false,false);
		}

		public Vector3 Location {get {return Display.gameObject.transform.position;} }

		Token[] tokens = new Token[Plane.count];
		
		public virtual Token Occupant (byte i) {return tokens[i];}
		public virtual TokenGroup Occupants {
			get {
				TokenGroup occupants = new TokenGroup();
				for (byte i=0; i<Plane.count; i++) {
					if (tokens[i] != null) {occupants.Add(tokens[i]);}
				}
				return occupants;
			}
		}

		public bool Occupied (int i, out Token occupant) {
			occupant = null;
			if (i < tokens.Length && i >= 0) {
				occupant = tokens[i];
				if (occupant != null) {return true;}
			}
			return false;
		}

		public bool Occupied (Plane p) {
			for (byte i=0; i<Plane.count; i++) {
				if (p.planes[i] && tokens[i]!=null) {return true;}
			}
			return false;
		}



		public int TokenCount {get {return Occupants.Count;} }
		public bool IsEmpty () {return (TokenCount < 1 ? true : false);}

		public virtual void Enter (Token t) {
			for (byte i=0; i<Plane.count; i++) {
				if (t.Plane.planes[i]) {tokens[i] = t;}
			}
			if (t.Plane.sunken) {EnterSunken(t);}

			for (int i=sensors.Count-1; i>=0; i--) {
				Sensor s = sensors[i];
				s.OtherEnter(t);
			}
		}

		public virtual void EnterSunken (Token t) {((CellDisplay)Display).EnterSunken(t);}

		public virtual void Exit (Token t) {
			for (byte i=0; i<Plane.count; i++){
				if (tokens[i] == t) {tokens[i] = null;}	
			}
			if (t.Plane.sunken) {ExitSunken();}
			foreach (Sensor s in sensors) {s.OtherExit(t);}
		}

		void ExitSunken () {((CellDisplay)Display).ExitSunken();}

		public virtual void Clear () {tokens = new Token[Plane.count];}

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

		public virtual bool StopToken (Token t) {
			for (byte i=0; i<Plane.count; i++) {
				if (stop.planes[i] && t.Plane.planes[i]) {return true;}
			}
			if (Body.CanTrample(t, this)) {return true;}
			return false;
		}
	}
}