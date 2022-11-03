using System;
using UnityEngine;

namespace HOA {
	
	public class Watch : IDeepCopyUnit<Watch> {

		protected Unit parent;

		public Stat IN {get; protected set;}
		public Stat STUN {get; protected set;}

		public Watch () {}
		
		public Watch(Unit u, int i=0){
			parent = u;
			IN = new IN(parent, i);
			STUN = new STUN(parent);
		}

		public Watch DeepCopy (Unit parent) {return new Watch (parent, IN);}

		public bool IsStunned() {return STUN > 0;}

		public virtual void Display (Panel p, float iconSize) {
			IN.Display(new Panel(p.Box(iconSize*2 + 5), p.LineH, p.s), iconSize);

			float x3 = p.x2;

			if (IsStunned()) {
				x3 = p.x2;
				Rect stunBox = p.Box(iconSize*2 + 5);
				if (GUI.Button(stunBox, "")) {
					if (GUIInspector.RightClick) {TipInspector.Inspect(ETip.STUN);}
				}
				p.x2 = x3;
				p.NudgeX();
				GUI.Box(p.IconBox, Icons.Effects.stun, p.s);
				p.NudgeX();
				p.NudgeY();
				GUI.Label(p.IconBox, STUN+"", p.s);
				p.NudgeY(false);
			}
		}
	}
}
