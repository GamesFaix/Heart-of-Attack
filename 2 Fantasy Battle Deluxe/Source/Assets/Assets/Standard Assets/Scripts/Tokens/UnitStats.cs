using UnityEngine;
using System.Collections;

namespace FBI.Tokens {
	public enum Composition {NONE, BIO, MECH, CYB, ETH}

	public class UnitStats {
		public Composition comp;
		public byte hp;
		public byte mhp;
		public byte def;
		public sbyte init;
		public byte ap;
		public byte fp;
		public byte skipped;
		
		public UnitStats(){
			comp = Composition.NONE;
			hp = 0;
			mhp = 0;
			def = 0;
			init = 0;
			ap = 0;
			fp = 0;
			skipped = 0;
		}
	}
}