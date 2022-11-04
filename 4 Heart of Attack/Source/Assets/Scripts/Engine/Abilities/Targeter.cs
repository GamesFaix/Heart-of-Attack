using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public static class Targeter {
		public static bool Ready {get; private set;}
		public static bool Passable {get; private set;}
		public static Ability Pending {get; private set;}
		static TargetGroup targets;
		static List<Aim> aims;
		static int steps, step;

		public static void Start (Ability Ability) {
			if (Illegal(Ability)) {return;}
			Reset();
			Pending = Ability;
			Pending.Adjust();
			aims = CopyAims(Pending.Aims);
			steps = aims.Count;
			StartStep();
		}

		static bool Illegal (Ability Ability) {
			string message;
			if (!Ability.Legal(out message)) {
				GameLog.Out(message);
				return true;
			}
			return false;
		}

		public static void Reset () {
			if (Pending != null) {
				Pending.Unadjust();
				Pending = null;
			}
			targets = new TargetGroup();
			Ready = false;
			Passable = false;
			steps = 0;
			step = 0;
			Game.ClearLegal();
		}

		static List<Aim> CopyAims (List<Aim> original) {
			List<Aim> copy = new List<Aim>();
			foreach (Aim a in original) {copy.Add(a.DeepCopy());}
			return copy;
		}

		static void StartStep () {
			Aim aim = aims[step];
			if (aim.Trajectory == ETraj.SELF) {
				FinishStep();
				return;
			}
			Token actor = Pending.Parent;
			Cell start = null;
			if (actor != null) {start = actor.Body.Cell;}
			Token child = Pending.Template;
			if (Pending.recursiveMove && step > 0) {start = (Cell)(targets[targets.Count-1]);}

			if (Pending.multiMove) {
				if (step == 0) {
					int range = aim.Range;
					for (int i=0; i<range; i++) {
						aims.Add(Aim.MoveNeighbor());
						steps++;
					}
					FinishStep();
					return;
				}
				else if (step > 1) {
					Passable = true;
					int index = targets.Count-1;
					Target last = targets[index];

					if (last is Cell) {start = (Cell)last;}
					else if (last is Token) {start = ((Token)last).Body.Cell;}

					if (start.StopToken(actor)) {
						FinishStep();
						return;
					}
				}
			}

			else if (Pending.multiTarget && step > 0) {Passable = true;}

			else if (Pending.teleport && step > 0) {
				start = Pending.Parent.Body.Cell;
				actor = (Token)targets[0];
			}

			if (aim.Trajectory == ETraj.RADIAL) {
				Token target1 = (Token)targets[step-1];
				if (!Find(aim, actor, target1.Body.Cell, null)) {NoLegalTargets();}
			}
			else {
				if (!Find(aim, actor, start, child)) {NoLegalTargets();}
			}
		}

		static bool Find (Aim aim, Token actor, Cell center=null, Token other=null) {
			TargetGroup targets = aim.Targets(actor, center, other);
			targets.Legalize();
			return (targets.Count>0 ? true : false);
		}

		static void FinishStep () {
			Game.ClearLegal();
			if (step < steps-1) {
				step++;
				StartStep();
			}
			else {Ready = true;}
		}

		public static void Select (Target t) {
			if (t.Legal) {
				targets.Add(t);
				FinishStep();
			}
		}

		public static void Execute () {
			if (Passable || Ready) {Pending.Execute(targets);}
			else {GameLog.Out("Please finish selecting targets.");}
		}

		static void NoLegalTargets () {
			GameLog.Out("No legal targets.");
			Reset();
		}

		public static string PendingString () {
			string str = "";
			if (Pending != default(Ability)) {
				str = Pending.Name;
				foreach (Target t in targets) {
					if (t != default(Target)) {
						str += "\n"+t.ToString();
					}
				}
			}
			return str;

		}
	}
}