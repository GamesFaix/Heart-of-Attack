using System.Collections.Generic;
using HOA.Actions;
using UnityEngine;

namespace HOA {

	public static class Targeter {
		public static bool ready {get; private set;}
		public static Task pending {get; private set;}
		static TargetGroup targets;
		static AimSeq aims;
		static int step;

		public static void Start (Task task) {
			if (Illegal(task)) {return;}
			Reset();
			pending = task;
			pending.Adjust();
			aims = pending.aims.Copy;
			StartStep();
		}

		static bool Illegal (Task task) {
			string message;
			if (!task.Legal(out message)) {
				GameLog.Out(message);
				return true;
			}
			return false;
		}

		public static void Reset () {
			if (pending != null) {
				pending.UnAdjust();
				pending = null;
			}
			targets = new TargetGroup();
			ready = false;
			step = 0;
			Game.ClearLegal();
		}

		static void StartStep () {
			Aim aim = aims[step];
			aim.Extend(aim, aims);
			if (aim.optional) {ready = true;}

			if (aim.trajectory == Trajectory.Self) {
				FinishStep();
				return;
			}

			Token actor = pending.parent;
			Cell start = null;
			if (actor != null) {start = actor.Body.Cell;}
			Token child = pending.template;

			if (aim.recursiveTarget) {
				Target last = targets[targets.Count-1];
				if (last is Cell) {start = (Cell)last;}
				else if (last is Token) {start = ((Token)last).Body.Cell;}

				if (start.StopToken(actor)) {
					FinishStep();
					return;
				}
			}

			else if (pending is IMultiTarget && step > 0) {ready = true;}

			else if (pending is ITeleport && step > 0) {
				start = pending.parent.Body.Cell;
				actor = (Token)targets[0];
			}

			if (aim.trajectory == Trajectory.Radial) {
				Token target1 = (Token)targets[step-1];
				if (!Find(aim, actor, target1.Body.Cell, null)) {NoLegalTargets();}
			}
			else {
				if (!Find(aim, actor, start, child)) {NoLegalTargets();}
			}
		}

		static bool Find (Aim aim, Token actor, Cell center=null, Token other=null) {
			TargetGroup targets = aim.Find(actor, center, other);
			targets.Legalize();
			return (targets.Count>0 ? true : false);
		}

		static void FinishStep () {
			Game.ClearLegal();
			if (step < aims.Count) {
				step++;
				StartStep();
			}
			else {ready = true;}
		}

		public static void Select (Target t) {
			if (t.Legal) {
				targets.Add(t);
				FinishStep();
			}
		}

		public static void Execute () {
			if (ready) {pending.Execute(targets);}
			else {GameLog.Out("Please finish selecting targets.");}
		}

		static void NoLegalTargets () {
			GameLog.Out("No legal targets.");
			Reset();
		}

		public static string PendingString () {
			string str = "";
			if (pending != default(Task)) {
				str = pending.name;
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