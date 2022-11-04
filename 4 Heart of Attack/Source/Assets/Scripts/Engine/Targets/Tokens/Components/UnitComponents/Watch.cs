using System;
using UnityEngine;

namespace HOA {
	
	public class Watch : TokenComponent, IDeepCopyUnit<Watch>, IInspectable {

		public Stat IN {get; protected set;}
		public Stat STUN {get; protected set;}

		protected bool skipped;

		public Watch(Unit parent, int i=0) : base (parent) {
			IN = Stat.Initiative(parent, i);
			STUN = Stat.Stun(parent, 0);
			skipped = false;
		}

		public Watch DeepCopy (Unit parent) {return new Watch (parent, IN);}

		public bool IsStunned() {return STUN > 0;}
		public bool IsSkipped() {return skipped;}

		public void Skip(bool log=true) {
			skipped = true;
			if (log) {GameLog.Out(Parent+" has been skipped in the Queue.");}
		}
		public void ClearSkip(bool log=true) {
			skipped = false;
			if (log) {GameLog.Out(Parent+" is now skippable.");}
		}

		public override void Draw (Panel p) {InspectorInfo.Watch(this, p);}
	}
}
