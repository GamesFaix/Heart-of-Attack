using System.Collections.Generic;

namespace HOA {

	public static class Targeter {
		public static bool Ready {get; private set;}
		public static bool Passable {get; private set;}
		public static Task Pending {get; private set;}
		static TargetGroup targets;
		static List<Aim> Aim;
		static int steps, step;

		public static void Start (Task task) {
			Reset();
			if (task.Legal) {
				Pending = task;
				Pending.Adjust();
				Aim = new List<HOA.Aim>();
				foreach (HOA.Aim a in Pending.Aim) {
					Aim.Add(a.DeepCopy());
				}
				steps = Aim.Count;
				StartStep();
			}
			else {GameLog.Out("Task unaffordable, used, or restricted.");}
		}

		static void StartStep () {
			Token Parent = Pending.Parent;
			Aim aim = Aim[step];
			Token child = Pending.Template;
			Cell start = null;
			if (!(Pending is IManualFree)) {start = Parent.Body.Cell;}
			if (Pending is IRecursiveTarget && step > 0) {start = (Cell)(targets[targets.Count-1]);}


			if (aim.Trajectory == ETraj.SELF) {
				FinishStep();
				return;
			}

			if (Pending is IMultiMove) {
				if (step == 0) {
					int range = aim.Range;
					for (int i=0; i<range; i++) {
						Aim.Add(HOA.Aim.MoveNeighbor());
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

					if (start.StopToken(Parent)) {
						FinishStep();
						return;
					}
				}
			}

			else if (Pending is IMultiTarget && step > 0) {Passable = true;}

			else if (Pending is ITeleport && step > 0) {
				if (!(Pending is IManualFree)) {start = Pending.Parent.Body.Cell;}
				Parent = (Token)targets[0];
			}

			if (aim.Trajectory == ETraj.RADIAL) {
				Token target1 = (Token)targets[step-1];
				if (!Legalizer.Find(Parent, aim, target1.Body.Cell)) {NoLegalTargets();}
			}
			else {
				if (!Legalizer.Find(Parent, aim, start, child)) {NoLegalTargets();}
			}
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
			if (Pending != default(Task)) {
				str = Pending.Name;
				foreach (Target t in targets) {
					if (t != default(Target)) {
						str += "\n"+t.ToString();
					}
				}
			}
			return str;

		}

		public static void Reset () {
			if (Pending != null) {
				Pending.UnAdjust();
				Pending = default(Task);
			}
			targets = new TargetGroup();
			Ready = false;
			Passable = false;
			steps = 0;
			step = 0;
			Game.ClearLegal();
		}
	}
}