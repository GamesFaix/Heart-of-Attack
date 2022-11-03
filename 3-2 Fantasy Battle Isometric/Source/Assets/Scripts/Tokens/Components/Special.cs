using UnityEngine;
using System.Collections.Generic;
using System;

namespace HOA {
	public enum ESpecial {UNIT, OB, KING, TRAM, DEST, REM, HEART, CELL, TOKEN}

	public class Special : IDeepCopy<Special> {

		List<ESpecial> types;

		public Special (ESpecial c) {types = new List<ESpecial> {c}; }
		public Special (List<ESpecial> c) {types = c;}

		public void Set (ESpecial c) {types = new List<ESpecial> {c};}
		public void Set (List<ESpecial> c) {types = c;}
		
		public List<ESpecial> Value {get {return types;} }

		public Special DeepCopy () {return new Special(Value);}

		public bool Is (ESpecial c){
			if (types.Contains(c)) {return true;}
			return false;
		}
		
		public void Add (ESpecial c) {if (!types.Contains(c)) {types.Add(c);} }
		public void Remove (ESpecial c) {if (types.Contains(c)) {types.Remove(c);} }

		public int Count {get {return types.Count;} }

		public ESpecial this[int i] {get { return types[i];} }

		public MyEnumerator GetEnumerator() {return new MyEnumerator(this);}
		
		public class MyEnumerator {
			int n;
			Special buffer;
			public MyEnumerator(Special input) {buffer = input; n = -1;}
			public bool MoveNext() {n++; return (n < buffer.Count);}
			public ESpecial Current {get {return buffer[n];} }
		}

		public void Display (Panel p) {
			Rect box;
			int specials = Enum.GetValues(typeof(ESpecial)).Length - 1;

			for (int i=0; i<specials; i++) {
				if (Is((ESpecial)i)) {
					box = p.Box(p.LineH);
					if (GUI.Button(box, "")) {
						//if (GUIInspector.RightClick) {
							TipInspector.Inspect(Tip.Special((ESpecial)i));
						//}
					}
					GUI.Box(box, Icons.Special((ESpecial)i), p.s);
					p.NudgeX();
				}
			}
		}

		public static Special None {get {return new Special(new List<ESpecial>());} }
		public static Special Cell {get {return new Special(ESpecial.CELL);} }
		public static Special Unit {get {return new Special(ESpecial.UNIT);} }
		public static Special UnitDest {get {return new Special(new List<ESpecial> {ESpecial.UNIT, ESpecial.DEST, ESpecial.REM});} }
		public static Special Dest {get {return new Special(ESpecial.DEST);} }
		public static Special DestRem {get {return new Special(new List<ESpecial> {ESpecial.DEST, ESpecial.REM});} }
		public static Special Rem {get {return new Special(ESpecial.REM);} }
		public static Special Token {get {return new Special(new List<ESpecial> {ESpecial.TOKEN});} }
	}
}