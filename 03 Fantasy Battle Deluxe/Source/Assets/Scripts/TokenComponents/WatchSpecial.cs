using System;
using HOA.Players;
using UnityEngine;

namespace HOA.Tokens.Components {
	
	public class WatchOldt : Watch {
		public WatchOldt (Unit u, int i=0){
			parent = u;
			init = i;
			stun = 0;
			skipped = false;
			cor = 0;
		}
		
		public override int IN () {
			return init + (Mathf.Min(5, parent.FP()));
		}
	}
}
