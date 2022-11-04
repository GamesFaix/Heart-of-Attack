using UnityEngine; 

namespace HOA { 

	public class HealthHalfDodge : Health, IInspectable {
		
		public HealthHalfDodge (Unit u, int hp=0, int def=0) : base (u, hp, def){ }

		public new HealthHalfDodge DeepCopy (Unit parent) {return new HealthHalfDodge(parent, HP.Max, DEF);}
		
		public override bool Damage (Source source, int n, bool log=true) {
			int flip = DiceCoin.Throw(source, EDice.COIN);
			if (flip == 1) {return base.Damage(source, n, log);}
			else {
				GameLog.Out(source.ToString()+" tried to damage "+Parent+" and missed.");
				return false;
			}
		}

        public override void Draw(Panel p) { InspectorInfo.HealthHalfDodge(this, p); }

	}
}
