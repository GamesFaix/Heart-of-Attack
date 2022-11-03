using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	public class Type {
		List<EClass> types;

		public Type (EClass c) {types = new List<EClass> {c}; }
		public Type (List<EClass> c) {types = c;}

		public void Set (EClass c) {types = new List<EClass> {c};}
		public void Set (List<EClass> c) {types = c;}
		
		public List<EClass> Value {get {return types;} }
		
		public bool Is (EClass c){
			if (types.Contains(c)) {return true;}
			return false;
		}
		
		public void Add (EClass c) {if (!types.Contains(c)) {types.Add(c);} }
		public void Remove (EClass c) {if (types.Contains(c)) {types.Remove(c);} }
	}
}