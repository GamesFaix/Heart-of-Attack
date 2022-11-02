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

		public static void Find (Action a) {
			if (currentAction != default(Action)) {Reset();}
			a.Adjust();

			if (a.Legal()) {

				ready = false;

				currentAction = a;
				//Debug.Log(a.Name);

				targets = new List<ITargetable>();

				steps = a.Aim.Count;
				currentStep = 0;

				StartStep (currentStep);
			}
			else {GameLog.Out("Action cannot be performed. (Unaffordable, used, or restricted.)");}
		}

		static void StartStep (int n) {
			Token actor = currentAction.Actor;
			Aim aim = currentAction.Aim[currentStep];
			Token child = currentAction.ChildTemplate;

			if (aim.AimType == EAim.SELF) {FinishStep();}
			else {
				//Debug.Log(aim);
				if (child!= default(Token)) {Debug.Log(child.ToString());}
				Legalizer.Find(actor, aim, child); }
		}

		public static void Select (ITargetable t) {
			if (t.IsLegal()) {
				targets.Add(t);
				//Debug.Log("new target selected: "+t.ToString());
				FinishStep();
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
		}
	}
}