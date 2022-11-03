using UnityEngine; 

namespace HOA { 

	public static class GUIInspectorTask {
		public static Task inspectedAction =  default(Task);

		public static void Arsenal (Unit u, Panel p, Panel super) {
			float btnW = 150;
			
			Rect box;
			if (u == TurnQueue.Top) {
				p.NudgeX();
				if (GUI.Button(p.Box(btnW), "End Turn [0]") || Input.GetKey("0") || Input.GetKey("[0]")) {
					Targeter.Start(new AEnd(u));
					GUIMaster.PlaySound(EGUISound.CLICK);
				}
				p.NextLine();
			}
			
			int i =1;
			foreach (Task a in u.Arsenal) {
				p.NudgeX();
				
				box = p.Box(btnW);
				
				if (a.Legal) {
					if (GUI.Button(box, a.Name+" ["+i+"]") || Input.GetKey(i.ToString()) || Input.GetKey("["+i+"]")) {
						//Debug.Log("button clicked "+a.Name);
						GUIMaster.PlaySound(EGUISound.CLICK);
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
			
			
			Task(new Panel(new Rect (p.X+btnW+10, p.Y, p.W-btnW-25, p.LineH*9), p.LineH, p.s));
			
			p.NextLine();
			
			if (Targeter.Pending != default(Task)) {
				p.NudgeX(); p.NudgeY();
				GUI.Label(p.TallBox(3), "Pending: \n"+Targeter.PendingString());
				
				p.NudgeX();
				if (GUI.Button(p.Box(btnW), "Execute [Space]") || Input.GetKeyUp("space")) {
					Targeter.Execute();
					GUIMaster.PlaySound(EGUISound.CLICK);
				}
				
				if (GUI.Button(p.Box(btnW), "Cancel [Backspace]") || Input.GetKeyUp("backspace")) {
					Targeter.Reset();
					GUIMaster.PlaySound(EGUISound.CLICK);
				}
			}
		}
		
		static void Task (Panel p) {
			GUI.Box (p.FullBox, "");
			if (inspectedAction != default(Task)) {
				inspectedAction.Draw(p);
			}
		}
	}
}
