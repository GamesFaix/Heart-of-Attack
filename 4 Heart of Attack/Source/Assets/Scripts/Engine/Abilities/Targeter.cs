using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public static class Targeter {
		public static bool Ready {get; private set;}
		public static bool Passable {get; private set;}
		public static Ability Pending {get; private set;}
		static TargetGroup Targets;
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
			Targets = new TargetGroup();
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
			if (Pending.recursiveMove && step > 0) {start = (Cell)(Targets[Targets.Count-1]);}

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
					int index = Targets.Count-1;
					Target last = Targets[index];

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
				actor = (Token)Targets[0];
			}

			if (aim.Trajectory == ETraj.RADIAL) {
				Token Target1 = (Token)Targets[step-1];
				if (!Find(aim, actor, Target1.Body.Cell, null)) {NoLegalTargets();}
			}
			else {
				if (!Find(aim, actor, start, child)) {NoLegalTargets();}
			}
		}

		static bool Find (Aim aim, Token actor, Cell center=null, Token other=null) {
			TargetGroup Targets = aim.Targets(actor, center, other);
			Targets.Legalize();
			return (Targets.Count>0 ? true : false);
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
				Targets.Add(t);
				FinishStep();
			}
		}

		public static void Execute () {
			if (Passable || Ready) {Pending.Execute(Targets);}
			else {GameLog.Out("Please finish selecting Targets.");}
		}

		static void NoLegalTargets () {
			GameLog.Out("No legal Targets.");
			Reset();
		}

		public static string PendingString () {
			string str = "";
			if (Pending != default(Ability)) {
				str = Pending.Name;
				foreach (Target t in Targets) {
					if (t != default(Target)) {
						str += "\n"+t.ToString();
					}
				}
			}
			return str;

		}
	}
}