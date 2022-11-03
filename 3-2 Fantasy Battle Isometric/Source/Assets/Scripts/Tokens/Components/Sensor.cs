using UnityEngine;

namespace HOA {
	
	public abstract class Sensor {
		protected Token parent;
		protected Cell cell;

		public Cell Cell {get {return cell;} }
		public Token Parent {get {return parent;} }

		protected abstract string Desc {get;}

		protected Plane planesToStop;
		protected void Stop (Cell cell) {
			Plane plane = cell.stop;
			for (byte i=0; i<Plane.count; i++) {
				if (planesToStop.planes[i]) {plane.planes[i] = true;}
			}
			cell.stop = plane;
		}

		protected void ReleaseStop (Cell cell) {
			Plane plane = cell.stop;
			for (byte i=0; i<Plane.count; i++) {
				if (planesToStop.planes[i]) {plane.planes[i] = false;}
			}
			cell.stop = plane;
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
			GUI.Box(box, Icons.Other.sensor, p.s);
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