using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {
	
	public class Cell : ITargetable {
		int x, y;
		public int X {get {return x;} }
		public int Y {get {return y;} }
		public override string ToString() {return "("+x+","+y+")";}

		public Cell (int xx, int yy) {
			x = xx;
			y = yy;
		}

		CellDisplay display;
		public CellDisplay Display {
			get {return display;}
			set {display = value;}
		}

		public Vector3 Location {get {return display.gameObject.transform.position;} }

//		public void SpriteEffect (EEffect e) {Display.Effect(e);}

		Token[] tokens = new Token[Enum.GetNames(typeof(EPlane)).Length];
		
		public Token Occupant (EPlane p) {return tokens[(int)p];}
		public TokenGroup Occupants {
			get {
				TokenGroup tg = new TokenGroup();
				for (int i=0; i<tokens.Length; i++) {
					if (tokens[i] != default(Token)
					&& !tg.Contains(tokens[i])) {	
						tg.Add(tokens[i]);
					}
				}
				return tg;
			}
		}
		public int TokenCount {get {return Occupants.Count;} }
		public bool IsEmpty () {
			if (TokenCount < 1) {return true;}
			return false;
		}
		
		public bool Occupied (EPlane p) {
			if (tokens[(int)p] != default(Token)) {return true;}
			return false;
		}
		public bool Occupied (List<EPlane> ps) {
			foreach (EPlane p in ps) {
				if (tokens[(int)p] != default(Token)) {return true;}
			}
			return false;
		}
		public bool Contains (EClass s) {
			foreach (Token t in Occupants) {
				if (t.Type.Is(s)) {return true;}
			}
			return false;
		}

		public bool Contains (EClass c, out Token occupant){
			occupant = default(Token);
			foreach (Token t in Occupants) {
				if (t.Type.Is(c)) {
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
		
		public void Enter (Token t) {
			foreach (EPlane p in t.Plane.Value) {
				tokens[(int)p] = t;
			}
			if (t.Plane.Is(EPlane.SUNK)) {EnterSunken(t);}

			for (int i=sensors.Count-1; i>=0; i--) {
				Sensor s = sensors[i];
				s.OtherEnter(t);
			}
		}

		public void EnterSunken (Token t) {Display.EnterSunken(t);}

		public void Exit (Token t) {
			for (int i=0; i<=3; i++){
				if (tokens[i] == t) {tokens[i] = default(Token);}	
			}

			if (t.Plane.Is(EPlane.SUNK)) {ExitSunken();}

			foreach (Sensor s in sensors) {s.OtherExit(t);}
		}

		void ExitSunken () {Display.ExitSunken();}

		public void Clear () {
			tokens = new Token[Enum.GetNames(typeof(EPlane)).Length];
		}

		//Sensors
		List<Sensor> sensors = new List<Sensor>();
	
		public List<Sensor> Sensors () {return sensors;}
		public void AddSensor (Sensor s) {sensors.Add(s);}
		public void RemoveSensor (Sensor s) {
			if (sensors.Contains(s)) {sensors.Remove(s);}
			else {GameLog.Debug("Attempt to remove invalid sensor from cell.");}
		}
		
		public CellGroup Neighbors(bool self=false) {
			CellGroup neighbors = new CellGroup();
			
			for (int i=0; i<8; i++) {
				int[] dir = Direction.FromInt(i);
				Cell c;
				if (Board.HasCell(x+dir[0], y+dir[1], out c)) {
					neighbors.Add(c);
				}
			}
			if (self) {neighbors.Add(this);}
			return neighbors;
		}

		//ITargetable
		public void Select (Source s) {GUISelectors.Cell = this;}
		bool legal = false;
		public bool IsLegal() {return legal;}
		public void Legalize (bool l=true) {
			legal = l;
			Display.SetLegal(legal);
		}

		bool[] stop = new bool[4];

		bool Stop (EPlane p) {
			if (p == EPlane.SUNK) {return stop[0];}
			if (p == EPlane.GND) {return stop[1];}
			if (p == EPlane.AIR) {return stop[2];}
			if (p == EPlane.ETH) {return stop[3];}
			return false;
		}

		public bool StopToken (Token t) {
			foreach (EPlane p in t.Plane.Value) {
				if (Stop(p)) {return true;}
			}
			if (t.Body.CanTrample(this)) {return true;}
			return false;
		}

		public void SetStop (EPlane p, bool s) {
			if (p == EPlane.SUNK) {stop[0] = s;}
			if (p == EPlane.GND) {stop[1] = s;}
			if (p == EPlane.AIR) {stop[2] = s;}
			if (p == EPlane.ETH) {stop[3] = s;}
		}
	}
}