using UnityEngine;

namespace HOA {

	public abstract class Timer {

		public string Name {get; protected set;}
		public abstract string Desc {get;}

		public Source Source {get; protected set;}
		public Unit Parent {get; protected set;}

		public int Turns {get; protected set;}

		public void Tick () {
			Turns--;
			if (Turns == 0) {
				Activate();
				if (Turns == 0) {Parent.timers.Remove(this);}
			}
		}

		public abstract void Activate ();

		public Texture2D Icon {get {return Icons.Other.timer;} }

		public void Display (Panel p, float iconSize) {
			Rect box = p.IconBox;

			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.TIMER);}
			GUI.Box(box, Icon, p.s);

			p.NudgeY();
			GUI.Label(p.Box(100), Name);
			p.NudgeY(false);
			
			p.NudgeX();
			p.NudgeY();
			GUI.Label(p.Box(250), Desc);
		}
	}
}