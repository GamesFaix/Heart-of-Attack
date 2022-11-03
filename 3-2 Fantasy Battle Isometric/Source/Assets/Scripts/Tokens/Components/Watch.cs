using System;
using UnityEngine;

namespace HOA {
	
	public class Watch{

		protected Unit parent;

		public Stat IN {get; protected set;}
		public Stat STUN {get; protected set;}

		protected bool skipped;

		public Watch () {}
		
		public Watch(Unit u, byte i=0){
			parent = u;
			IN = Stat.IN(parent, i);
			STUN = Stat.STUN(parent);
			skipped = false;
		}

		public bool IsStunned() {return STUN > 0;}
		public bool IsSkipped() {return skipped;}

		public void Skip(bool log=true) {
			skipped = true;
			if (log) {GameLog.Out(parent+" has been skipped in the Queue.");}
		}
		public void ClearSkip(bool log=true) {
			skipped = false;
			if (log) {GameLog.Out(parent+" is now skippable.");}
		}

		public virtual void Display (Panel p, float iconSize) {
			IN.Display(new Panel(p.Box(iconSize*2 + 5), p.LineH, p.s), iconSize);

			float x3 = p.x2;

			Rect box;

			if (IsStunned()) {
				x3 = p.x2;
				Rect stunBox = p.Box(iconSize*2 + 5);
				if (GUI.Button(stunBox, "")) {
					if (GUIInspector.RightClick) {TipInspector.Inspect(ETip.STUN);}
				}
				p.x2 = x3;
				p.NudgeX();
				box = p.Box(iconSize);
				GUI.Box(p.Box(iconSize), Icons.Stat(EStat.STUN), p.s);
				p.NudgeX();
				p.NudgeY();
				box = p.Box(iconSize);
				GUI.Label(p.Box(iconSize), STUN+"", p.s);
				p.NudgeY(false);
			}
			else if (IsSkipped()){
				p.NudgeX();
				box = p.Box(iconSize);
				if (GUI.Button(box, "")) {
					if (GUIInspector.RightClick) {TipInspector.Inspect(ETip.SKIP);}
				}
				GUI.Box(box, Icons.SKIP(), p.s);
			}
		}
	}
}
