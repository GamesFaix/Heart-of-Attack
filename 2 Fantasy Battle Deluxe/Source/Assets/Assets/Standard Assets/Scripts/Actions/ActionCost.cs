using UnityEngine;
using System.Collections;

namespace FBI.Actions {

	public struct ActionCost {
		public byte ap;
		public byte fp;
		public bool special;
		public string desc;
		
		public ActionCost(byte a, byte f, bool s = false, string d = ""){
			ap = a;
			fp = f;
			special = s;
			desc = d;
		}
	}
}