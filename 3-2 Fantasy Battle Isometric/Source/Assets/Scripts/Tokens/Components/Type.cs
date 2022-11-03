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
	}
}