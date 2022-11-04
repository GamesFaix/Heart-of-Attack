using UnityEngine; 

namespace HOA { 

	public class HealthDEFCap : Health {
		public int cap;
		
		public HealthDEFCap (Unit parent, int hp=0, int def=0, int defCap = 255) : base (parent, hp) {
			this.cap = defCap;
			DEF = Stat.Defense(parent, def, defCap);
		}

		public new HealthDEFCap DeepCopy (Unit parent) {return new HealthDEFCap (parent, HP.Max, DEF, cap);}

        public override void Draw(Panel p) { InspectorInfo.HealthDEFCap(this, p); }
	}
}
