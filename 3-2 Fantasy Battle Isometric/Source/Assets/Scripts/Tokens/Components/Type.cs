using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	public enum EType {UNIT, OB, KING, TRAM, DEST, REM, HEART, CELL}

	public class Type {
		List<EType> types;

		public Type (EType c) {types = new List<EType> {c}; }
		public Type (List<EType> c) {types = c;}

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
			Type buffer;
			public MyEnumerator(Type input) {buffer = input; n = -1;}
			public bool MoveNext() {n++; return (n < buffer.Count);}
			public EType Current {get {return buffer[n];} }
		}

		public static Type Unit {get {return new Type(EType.UNIT);} }
		public static Type UnitDest {get {return new Type(new List<EType> {EType.UNIT, EType.DEST, EType.REM});} }
		public static Type Dest {get {return new Type(EType.DEST);} }
		public static Type DestRem {get {return new Type(new List<EType> {EType.DEST, EType.REM});} }
	}
}