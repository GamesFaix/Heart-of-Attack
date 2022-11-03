using UnityEngine; 

namespace HOA { 

	public class HealthDEFCap : Health {
		int cap;
		
		public HealthDEFCap (Unit parent, int hp=0, int def=0, int defCap = 255) {
			this.parent = parent;
			this.cap = defCap;
			HP = new HP(parent, hp);
			DEF = new DEF(parent, def, defCap);
		}

		public new HealthDEFCap DeepCopy (Unit parent) {return new HealthDEFCap (parent, HP.Max, DEF, cap);}

		public override void Display (Panel p, float iconSize) {
			HP.Display (new Panel(p.Box(iconSize +95), p.LineH, p.s), iconSize);
			Rect box = p.Box(iconSize*2+5);
			
			if (DEF > 0) {DEF.Display(new Panel(box, p.LineH, p.s), iconSize);}
			
			p.NudgeX(); p.NudgeX();
			iconSize = 20;
			GUI.Label(p.Box(30), "(Max");
			GUI.Box(p.Box(iconSize), Icons.Stats.defense, p.s);
			GUI.Label(p.Box(40), "= "+cap+")");
			
		}
	}
}
