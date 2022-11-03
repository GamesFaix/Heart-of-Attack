using UnityEngine;

namespace HOA {
	
	public abstract class Sensor {
		protected Token parent;
		protected Cell cell;

		public Cell Cell {get {return cell;} }
		public Token Parent {get {return parent;} }

		protected abstract string Desc {get;}

		protected Plane planesToStop = null;
		protected void Stop (Cell cell) {
			if (planesToStop != null) {
				foreach (EPlane plane in planesToStop.Value) {
					cell.SetStop(plane, true);
				}
			}
		}
		protected void ReleaseStop (Cell cell) {
			if (planesToStop != null) {
				foreach (EPlane plane in planesToStop.Value) {
					cell.SetStop(plane, false);
				}
			}
		}

		protected abstract bool IsTrigger (Token trigger);

		public void Enter (Cell c) {
			cell = c;
			Stop(c);
			foreach (Token t in c.Occupants) {if (IsTrigger(t)) {EnterEffects(t);} }
		}
		public void Exit () {
			ReleaseStop(cell);
			foreach (Token t in cell.Occupants) {if (IsTrigger(t)) {ExitEffects(t);} }
		}
		public void OtherEnter (Token t) {if (IsTrigger(t)) {OtherEnterEffects(t);} }
		public void OtherExit (Token t) {if (IsTrigger(t)) {OtherExitEffects(t);} }

		protected virtual void EnterEffects (Token t) {}
		protected virtual void ExitEffects (Token t) {}
		protected virtual void OtherEnterEffects (Token t) {}
		protected virtual void OtherExitEffects (Token t) {}

		public void Delete () {
			Exit();
			cell.RemoveSensor(this);
		}

		public abstract override string ToString ();
		public virtual void Display (Panel p) {
			p.NudgeX();
			Rect box = p.IconBox;
			if (GUI.Button(box, "")) {TipInspector.Inspect(ETip.SENSOR);}
			GUI.Box(box, Icons.SENSOR(), p.s);
			p.NudgeX();
			p.NudgeX();
			GUI.Box(p.Box(0.9f), parent.ID.FullName, p.s);
			p.NextLine();
			p.NudgeX();
			p.NudgeX();
			GUI.Label(p.TallBox(0.9f,3), Desc);
		}
	}
}