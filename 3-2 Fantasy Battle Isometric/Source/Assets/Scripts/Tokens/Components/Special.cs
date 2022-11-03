using UnityEngine;
using System.Collections.Generic;
using System;

namespace HOA {
	public enum EType {UNIT, OB, KING, TRAM, DEST, REM, HEART, CELL, TOKEN}

	public class Special {
		List<EType> types;

		public Special (EType c) {types = new List<EType> {c}; }
		public Special (List<EType> c) {types = c;}

		public void Set (EType c) {types = new List<EType> {c};}
		public void Set (List<EType> c) {types = c;}
		
		public List<EType> Value {get {return types;} }
		
		public bool Is (EType c){
			if (types.Contains(c)) {return true;}
			return false;
		}
		
		public void Add (EType c) {if (!types.Contains(c)) {types.Add(c);} }
		public void Remove (EType c) {if (types.Contains(c)) {types.Remove(c);} }

		public int Count {get {return types.Count;} }

		public EType this[int i] {get { return types[i];} }

		public MyEnumerator GetEnumerator() {return new MyEnumerator(this);}
		
		public class MyEnumerator {
			int n;
			Special buffer;
			public MyEnumerator(Special input) {buffer = input; n = -1;}
			public bool MoveNext() {n++; return (n < buffer.Count);}
			public EType Current {get {return buffer[n];} }
		}

		public void Display (Panel p) {
			Rect box;
			int specials = Enum.GetValues(typeof(EType)).Length - 1;

			for (int i=0; i<specials; i++) {
				if (Is((EType)i)) {
					box = p.Box(p.LineH);
					if (GUI.Button(box, "")) {
						//if (GUIInspector.RightClick) {
							TipInspector.Inspect(Tip.Special((EType)i));
						//}
					}
					GUI.Box(box, Icons.Special((EType)i), p.s);
					p.NudgeX();
				}
			}
		}

		public static Special Cell {get {return new Special(EType.CELL);} }
		public static Special Unit {get {return new Special(EType.UNIT);} }
		public static Special UnitDest {get {return new Special(new List<EType> {EType.UNIT, EType.DEST, EType.REM});} }
		public static Special Dest {get {return new Special(EType.DEST);} }
		public static Special DestRem {get {return new Special(new List<EType> {EType.DEST, EType.REM});} }
	}
}