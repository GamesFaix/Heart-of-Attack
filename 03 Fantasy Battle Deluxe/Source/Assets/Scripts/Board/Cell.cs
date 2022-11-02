using System;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Tokens.Components;
using UnityEngine;

namespace HOA.Map {
	
	public class Cell {
		int x;
		int y;
		
		public Cell (int xx, int yy) {
			x = xx;
			y = yy;
		}
		
		public override string ToString() {return "("+x+","+y+")";}
		public int X() {return x;}
		public int Y() {return y;}
		
		
		Token[] tokens = new Token[Enum.GetNames(typeof(PLANE)).Length];
		
		public Token Occupant (PLANE p) {return tokens[(int)p];}
		public TokenGroup Occupants () {
			TokenGroup tg = new TokenGroup();
			for (int i=0; i<tokens.Length; i++) {
				if (tokens[i] != default(Token)
				&& !tg.Contains(tokens[i])) {	
					tg.Add(tokens[i]);
				}
			}
			return tg;
		}
		public int TokenCount () {return Occupants().Count;}
		public bool IsEmpty () {
			if (TokenCount() < 1) {return true;}
			return false;
		}
		
		public bool Occupied (PLANE p) {
			if (tokens[(int)p] != default(Token)) {return true;}
			return false;
		}
		public bool Occupied (List<PLANE> ps) {
			foreach (PLANE p in ps) {
				if (tokens[(int)p] != default(Token)) {return true;}
			}
			return false;
		}
		
		
		public void Enter (Token t) {
			List<PLANE> planes = t.Plane();
			if (!Occupied(planes)) {
				foreach (PLANE p in planes) {
					tokens[(int)p] = t;
				}
				foreach (Sensor s in sensors) {s.OtherEnter(t);}
			}
			else {GameLog.Debug("Cell: Already contains token in that plane.");}
		}
		
		public void Exit (Token t) {
			for (int i=0; i<=3; i++){
				if (tokens[i] == t) {tokens[i] = default(Token);}	
			}
			foreach (Sensor s in sensors) {s.OtherExit(t);}
		}
		public void Clear () {
			tokens = new Token[Enum.GetNames(typeof(PLANE)).Length];
		}
		
		bool legal = false;
		public bool IsLegal () {return legal;}
		public void Legalize (bool b=true) {legal = b;}
		
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
		
	}
}