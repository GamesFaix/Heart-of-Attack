using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public class Targeter : MonoBehaviour {
		static bool ready;
		public static bool Ready {get {return ready;}}

		static Action currentAction;
		static List<ITargetable> targets;
		static int steps;
		static int currentStep;

		static bool passable;
		public static bool Passable { get {return passable;} }


		public static void Find (Action a) {
			if (currentAction != default(Action)) {Reset();}
			a.Adjust();

			if (a.Legal()) {

				ready = false;

				currentAction = a;
				//Debug.Log("targeting for "+a.Name);

				targets = new List<ITargetable>();

				steps = a.Aim.Count;
				//Debug.Log("steps: "+steps);
				currentStep = 0;

				StartStep (currentStep);
			}
			else {GameLog.Out("Action cannot be performed. (Unaffordable, used, or restricted.)");}
		}

		static void StartStep (int n) {
			Token actor = currentAction.Actor;
			Aim aim = currentAction.Aim[currentStep];
			Token child = currentAction.ChildTemplate;
			Cell start = actor.Cell;

			AllowPass (currentAction, currentStep);

			if (currentAction is IMultiMove && currentStep > 0) {
				int index = targets.Count-1;
				ITargetable last = targets[index];

				if (last is Cell) {start = (Cell)last;}
				if (last is Token) {start = ((Token)last).Cell;}

				if (start.StopToken(actor)) {
					FinishStep();
					return;
				}

			}

			if (currentAction is ITeleport && currentStep > 0) {
				start = currentAction.Actor.Cell;
				actor = (Token)targets[0];

			}


			//Debug.Log(start);

			if (aim.AimType == EAim.SELF) {FinishStep();}
			else {
				//Debug.Log(aim);
				//if (child!= default(Token)) {Debug.Log(child.ToString());}
				Legalizer.Find(actor, aim, start, child); 
			}

		}

		public static void Select (ITargetable t) {
			if (t.IsLegal()) {
				targets.Add(t);
				//Debug.Log("new target selected: "+t.ToString());
				FinishStep();
			}
		}

		static void AllowPass (Action a, int n) {
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

		public static Action Pending () {
			return currentAction;
		}
		public static string PendingString () {
			string str = "";
			if (currentAction != default(Action)) {
				str = currentAction.Name;
				foreach (ITargetable t in targets) {
					if (t != default(ITargetable)) {
						str += "\n"+t.ToString();
					}
				}
			}
			return str;

		}

		public static void Reset () {
			currentAction.UnAdjust();
			currentAction = default(Action);
			targets = default(List<ITargetable>);
			ready = false;
			steps = 0;
			currentStep =0;
			Board.ClearLegal();
			TokenFactory.ClearLegal();
			passable = false;
		}
	}
}