using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public class Targeter : MonoBehaviour {
		static bool ready;
		public static bool Ready {get {return ready;}}

		static Task currentAction;
		static TargetGroup targets;
		static int steps;
		static int currentStep;

		static bool passable;
		public static bool Passable { get {return passable;} }


		public static void Find (Task a) {
			if (currentAction != default(Task)) {Reset();}
			a.Adjust();

			if (a.Legal()) {

				ready = false;

				currentAction = a;
			
				targets = new TargetGroup();

				steps = a.Aim.Count;
				currentStep = 0;

				StartStep (currentStep);
			}
			else {GameLog.Out("Task cannot be performed. (Unaffordable, used, or restricted.)");}
		}

		static void StartStep (int n) {
			Token Parent = currentAction.Parent;
			Aim aim = currentAction.Aim[currentStep];
			Token child = currentAction.Template;
			Cell start = Parent.Body.Cell;

			AllowPass (currentAction, currentStep);

			if (currentAction is IMultiMove && currentStep > 0) {
				int index = targets.Count-1;
				Target last = targets[index];

				if (last is Cell) {start = (Cell)last;}
				if (last is Token) {start = ((Token)last).Body.Cell;}

				if (start.StopToken(Parent)) {
					FinishStep();
					return;
				}

			}

			if (currentAction is ITeleport && currentStep > 0) {
				start = currentAction.Parent.Body.Cell;
				Parent = (Token)targets[0];

			}


			//Debug.Log(start);

			if (aim.Trajectory == ETraj.SELF) {FinishStep();}
			else if (currentAction is AMoveAren) {
				Legalizer.FindArenMove (Parent, aim);
			}
			else {
				//Debug.Log(aim);
				//if (child!= default(Token)) {Debug.Log(child.ToString());}
				Legalizer.Find(Parent, aim, start, child); 
			}

		}

		public static void Select (Target t) {
			if (t.Legal) {
				targets.Add(t);
				//Debug.Log("new target selected: "+t.ToString());
				FinishStep();
			}
		}

		static void AllowPass (Task a, int n) {
			if (a is IMultiMove) {
				if (n >= ((IMultiMove)a).Optional()) {passable = true;}
			}
			if (a is IMultiTarget) {
				if (n >= ((IMultiTarget)a).Optional()) {passable = true;}
			}
		}

		public static void Pass () {
			if (passable) {
				currentAction.Execute(targets);
			}
		}

		static void FinishStep () {
			GUISelectors.Reset();
			if (currentStep < steps-1) {
				currentStep++;
				StartStep(currentStep);
			}
			else {
				//Execute();
				ready = true;
			}
		}

		public static void Execute () {
			currentAction.Execute(targets);

		}

		public static Task Pending () {
			return currentAction;
		}
		public static string PendingString () {
			string str = "";
			if (currentAction != default(Task)) {
				str = currentAction.Name;
				foreach (Target t in targets) {
					if (t != default(Target)) {
						str += "\n"+t.ToString();
					}
				}
			}
			return str;

		}

		public static void Reset () {
			currentAction.UnAdjust();
			currentAction = default(Task);
			targets = new TargetGroup();
			ready = false;
			steps = 0;
			currentStep =0;
			Board.ClearLegal();
			TokenFactory.ClearLegal();
			passable = false;
		}
	}
}