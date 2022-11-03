using UnityEngine; 

namespace HOA { 

	public class HealthHalfDodge : Health {
		
		public HealthHalfDodge (Unit u, int hp=0, int def=0){
			parent = u;
			HP = new HP(parent, hp);
			DEF = new DEF(parent, def);
		}

		public new HealthHalfDodge DeepCopy (Unit parent) {return new HealthHalfDodge(parent, HP.Max, DEF);}
		
		public override bool Damage (Source source, int n, bool log=true) {
			int flip = DiceCoin.Throw(source, EDice.COIN);
			if (flip == 1) {return base.Damage(source, n, log);}
			else {
				GameLog.Out(source.ToString()+" tried to damage "+parent.ToString()+" and missed.");
				return false;
			}
		}
		
		public override void Display (Panel p, float iconSize) {
			HP.Display (new Panel(p.Box(iconSize +95), p.LineH, p.s), iconSize);
			Rect defBox = p.Box(iconSize*2+5);
			
			if (DEF > 0) {DEF.Display(new Panel(defBox, p.LineH, p.s), iconSize);}
			
			p.NudgeX(); p.NudgeX();p.NudgeY();
			GUI.Label(p.Box(200), "50% chance of taking no damage.");
		}
	}
}
