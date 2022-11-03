using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {
	
	public class AimSeq : IEnumerable<Aim>{
		
		List<Aim> list;

		public AimSeq () {list = new List<Aim>();}
		public AimSeq (Aim a) {list = new List<Aim>{a};}
		public AimSeq (IEnumerable<Aim> a) {list = new List<Aim>(a);}

		public AimSeq Copy {get {return new AimSeq(this);} }

		public void Add (Aim a) {list.Add(a);}
		public void Insert (int i, Aim a) {list.Insert(i, a);}
		public void Remove (Aim a) {list.Remove(a);}
		public bool Contains (Aim a) {return list.Contains(a);}
		public int IndexOf (Aim a) {return list.IndexOf(a);}

		public Aim this[int i] {get {return list[i];} set {list[i] = value;} }

		public int Count {get {return list.Count;} }

		public IEnumerator<Aim> GetEnumerator() {
			for (int i=0; i<Count; i++) {yield return list[i];}
		}
		IEnumerator IEnumerable.GetEnumerator() {return GetEnumerator();}
	
		public static AimSeq operator + (AimSeq a, Aim b) {a.Add(b); return a;}
		public static AimSeq operator - (AimSeq a, Aim b) {a.Remove(b); return a;}



	}
}