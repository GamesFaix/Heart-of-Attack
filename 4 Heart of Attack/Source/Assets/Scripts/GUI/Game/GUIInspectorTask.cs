using UnityEngine; 

namespace HOA { 

	public static class GUIInspectorAbility {
		public static Ability inspectedAction =  default(Ability);

		public static void Arsenal (Unit u, Panel p, Panel super) {
			float btnW = 150;
			
			Rect box;
			if (u == TurnQueue.Top) {
				p.NudgeX();
				if (GUI.Button(p.Box(btnW), "End Turn [0]") || Input.GetKey("0") || Input.GetKey("[0]")) {
					Targeter.Start(HOA.Ability.End(u));
					GUIMaster.PlaySound(GUISounds.Click);
				}
				p.NextLine();
			}
			
			int i =1;
			foreach (Ability a in u.Arsenal) {
				p.NudgeX();
				
				box = p.Box(btnW);

				string message;
				if (a.Legal(out message)) {
					if (GUI.Button(box, a.Name+" ["+i+"]") || Input.GetKey(i.ToString()) || Input.GetKey("["+i+"]")) {
						GUIMaster.PlaySound(GUISounds.Click);
						Targeter.Start(a);
					}
				}
				else {
					Color oldColor = p.s.normal.textColor;
					p.s.normal.textColor = Color.gray;
					GUI.Box (box, a.Name);
					
					p.s.normal.textColor = oldColor;
				}
				
				if (box.Contains(GUIInspector.MousePos())) {inspectedAction = a;}
				p.NextLine();
				i++;
			}
			
			
			Ability(new Panel(new Rect (p.X+btnW+10, p.Y, p.W-btnW-25, p.LineH*9), p.LineH, p.s));
			
			p.NextLine();
			
			if (Targeter.Pending != default(Ability)) {
				p.NudgeX(); p.NudgeY();
				GUI.Label(p.TallWideBox(3), "Pending: \n"+Targeter.PendingString());
				
				p.NudgeX();
				if (GUI.Button(p.Box(btnW), "Execute [Space]") || Input.GetKeyUp("space")) {
					Targeter.Execute();
					GUIMaster.PlaySound(GUISounds.Click);
				}
				
				if (GUI.Button(p.Box(btnW), "Cancel [Backspace]") || Input.GetKeyUp("backspace")) {
					Targeter.Reset();
					GUIMaster.PlaySound(GUISounds.Click);
				}
			}
		}
		
		static void Ability (Panel p) {
			GUI.Box (p.FullBox, "");
			if (inspectedAction != default(Ability)) {
				inspectedAction.Draw(p);
			}
		}
	}
}
