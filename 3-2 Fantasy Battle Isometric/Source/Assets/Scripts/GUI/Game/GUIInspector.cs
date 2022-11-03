using UnityEngine;
using System.Collections.Generic;
using HOA;

public class GUIInspector : MonoBehaviour {

	static Panel panel;
	
	static Target inspected = default(Target);

	public static Target Inspected {
		get {return inspected;}
		set {
			inspected = value;
			inspectedAction = default(Task);
			if (inspected is Token && !((Token)inspected).IsTemplate()){CameraPanner.Focus(inspected);}
		}
	}
	
	static Vector2 scrollPos = new Vector2(0,0);
	float internalW = 100;
	float iconSize = 30;
	float internalH = 0;
			
	static Task inspectedAction =  default(Task);
	public static ETip Tip {get; set;}

	public void Display(Panel p){
		GUI.DrawTexture(p.FullBox, ImageLoader.wood[1], ScaleMode.StretchToFill);

		Tip = ETip.NONE;
		panel = p;

		if (inspected is Token) {
			Token t = (Token)inspected;
			float boxH = (p.H-p.LineH) / p.H;
			//float scrollStart = p.y2;

			scrollPos = GUI.BeginScrollView(p.TallBox(boxH), scrollPos, new Rect(p.x2, p.y2, internalW, internalH));
				
			float tokenH = 0;
			Tokens(t, new Panel(p.TallBox(3), p.LineH, p.s), p, out tokenH);

			if (t.Notes() != "") {
				p.NudgeY();
				GUI.Label(p.TallBox(1.5f), t.Notes());
				p.NudgeY(false);
			}

			if (t.timers.Count>0) {
				foreach (Timer timer in t.timers) {
					timer.Display(new Panel(p.LineBox, p.LineH, p.s), iconSize);
				}
			}


			float unitH = 0;
			if (t is Unit) {Units (t, new Panel(p.TallBox(12), p.LineH, p.s), p, out unitH);}
			GUI.Label(p.LineBox, "");
			internalH = tokenH+unitH;

			if (ShiftKey() && Tip != ETip.NONE) {GUIToolTips.Tip(MousePos(), Tip);}
			GUI.EndScrollView();
		}

		else if (inspected is Cell) {
			Cell((Cell)inspected, new Panel(p.TallBox(3), p.LineH, p.s), p);



		}

	}

	void Tokens (Token t, Panel p, Panel super, out float h) {
		float yStart = p.y2;
		p.NudgeX();
		Name(t, new Panel (p.Box(200), p.LineH, p.s), super);
		if (t.OnDeath != EToken.NONE) {OnDeath(t, new Panel (p.Box(200), p.LineH, p.s), super);}
		p.NextLine();
		
		p.NudgeX();
		if (t.Owner != default(Player) && t.Owner!=Roster.Neutral) {Owner (t, new Panel (p.Box(100), p.LineH, p.s), super);}
		p.NextLine();
		
		p.NudgeX();	
		Plane(t, new Panel (p.Box(iconSize*3), p.LineH, p.s), super);
		p.NudgeX(); p.NudgeX();
		Special(t, new Panel (p.Box(iconSize*3), p.LineH, p.s), super);
		p.NudgeX(); p.NudgeX();
		if (!t.IsTemplate()) {Cell(t, new Panel (p.Box(iconSize*3), p.LineH, p.s), super);}


		p.NextLine();
		h=p.y2-yStart;
	}

	void Name (Token t, Panel p, Panel super) {
		FancyText.Highlight(p.FullBox, t.ToString(), p.s, t.Owner.Colors);
		if (GUI.Button (p.FullBox, "", p.s)) {t.Display.Effect(EEffect.SHOW);}
		if (ShiftMouseOver(p.FullBox)) {ToolTip("Name");}
	}
	void OnDeath (Token t, Panel p, Panel super) {
		GUI.Box(p.Box(20), Icons.ONDEATH(), p.s);
		p.NudgeX();
		GUI.Label(p.Box(1),TokenRef.CodeToString(t.OnDeath));
		if (ShiftMouseOver(p.FullBox)) {
			Tip = ETip.ONDEATH;
		}
	}

	void Owner (Token t, Panel p, Panel super) {
		GUI.Label (p.FullBox, t.Owner.ToString());
		if (ShiftMouseOver(p.FullBox)) {ToolTip("Owner");}
	}
	void Cell (Token t, Panel p, Panel super) {
		GUI.Box(p.FullBox, "");
		GUI.Box(p.Box(iconSize), Icons.Special(EType.CELL), p.s);
		p.NudgeY(); p.NudgeX();
		GUI.Label (p.Box(iconSize*2), t.Body.Cell.ToString(), p.s);
		p.NudgeY(false);
		if (ShiftMouseOver(p.FullBox)) {
			Tip = ETip.CELL;
		}
		if (GUI.Button(p.FullBox, "", p.s) && Input.GetMouseButtonUp(1)) {Inspected = t.Body.Cell;}
	}

	void Plane (Token t, Panel p, Panel super) {
		GUI.Box(p.FullBox, "");
		Rect box;
		string str = "";

		if (t.Plane.Is(EPlane.SUNK)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.Plane(EPlane.SUNK), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {str = "Sunken";}
		}	
		if (t.Plane.Is(EPlane.GND)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.Plane(EPlane.GND), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {str = "Ground";}
		}	
		if (t.Plane.Is(EPlane.AIR)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.Plane(EPlane.AIR), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {str = "Air";}
		}	
		if (t.Plane.Is(EPlane.ETH)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.Plane(EPlane.ETH), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {str = "Ethereal";}
		}	
		if (str != "" && ShiftMouseOver(p.FullBox)) {ToolTip(str);}
	}
	void Special (Token t, Panel p, Panel super) {
		GUI.Box(p.FullBox, "");
		Rect box;
		string str = "";

		if (t.Special.Is(EType.KING)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.Special(EType.KING), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {
				Tip = ETip.KING;
			}
		}	
		if (t.Special.Is(EType.HEART)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.Special(EType.HEART), p.s); p.NudgeX();
			if (ShiftMouseOver(box)){
				Tip = ETip.HEART;
			}
		}
		if (t.Special.Is(EType.DEST)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.Special(EType.DEST), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {
				Tip = ETip.DEST;
			}
		}
		if (t.Special.Is(EType.REM)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.Special(EType.REM), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {
				Tip = ETip.REM;
			}
		}
		if (t.Special.Is(EType.TRAM)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.Special(EType.TRAM), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {
				Tip = ETip.TRAM;
			}
		}

		if (str != "" && ShiftMouseOver(p.FullBox)) {ToolTip(str);}
	}

	void Units (Token t, Panel p, Panel Super, out float h) {
		Unit u = (Unit)t;
		float yStart = p.y2;
		p.NudgeX();
		u.Health.Display(new Panel(p.LineBox, p.LineH, p.s), iconSize);

		p.NudgeX();
		u.Watch.Display(new Panel(p.LineBox, p.LineH, p.s), iconSize);

		p.NudgeX();
		u.Wallet.Display(new Panel(p.LineBox, p.LineH, p.s), iconSize);

		Arsenal(u, new Panel(p.TallBox(9), p.LineH, p.s), p);
		h = p.y2-yStart;
	}

	void Arsenal (Unit u, Panel p, Panel super) {
		float btnW = 150;

		Rect box;
		if (u == TurnQueue.Top) {
			p.NudgeX();
			if (GUI.Button(p.Box(btnW), "End Turn")) {
				Targeter.Start(new AEnd(u));
				GUIMaster.PlaySound(EGUISound.CLICK);
			}
			p.NextLine();
		}

		foreach (Task a in u.Arsenal) {
			p.NudgeX();

			box = p.Box(btnW);

			if (a.Legal) {
				if (GUI.Button(box, a.Name)) {
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

			if (box.Contains(MousePos())) {inspectedAction = a;}
			p.NextLine();
			
		}


		Task(new Panel(new Rect (p.X+btnW+10, p.Y, p.W-btnW-25, p.LineH*9), p.LineH, p.s));

		p.NextLine();

		if (Targeter.Pending != default(Task)) {
			p.NudgeX(); p.NudgeY();
			GUI.Label(p.TallBox(3), "Pending: \n"+Targeter.PendingString());

			p.NudgeX();
			if (GUI.Button(p.Box(btnW), "Execute") || Input.GetKeyUp("space")) {
				Targeter.Execute();
				GUIMaster.PlaySound(EGUISound.CLICK);
			}

			if (GUI.Button(p.Box(btnW), "Cancel") || Input.GetKeyUp("escape")) {
				Targeter.Reset();
				GUIMaster.PlaySound(EGUISound.CLICK);
			}
		}
	}

	void Task (Panel p) {
		GUI.Box (p.FullBox, "");
		if (inspectedAction != default(Task)) {
			inspectedAction.Draw(p);
		}
	}

	void Cell (Cell c, Panel p, Panel super) {
		GUI.Box(p.Box(iconSize), Icons.Special(EType.CELL), p.s);
		p.NudgeX(); p.NudgeY();
		GUI.Label(p.Box(0.5f), c.ToString(), p.s);
		p.NextLine();

		Token t;
		Rect box;

		GUI.Box(p.Box(iconSize), Icons.Plane(EPlane.ETH), p.s);
		if (c.Contains(EPlane.ETH, out t)) {
			p.NudgeX();
			p.NudgeY();
			box = p.Box(0.5f);
			t.DisplayThumb(new Panel(box, p.LineH, p.s), iconSize);
			if (GUI.Button (box, "", p.s)) {
				if (Input.GetMouseButtonUp(0)) {t.Display.Effect(EEffect.SHOW);}
				if (Input.GetMouseButtonUp(1)) {Inspected = t;}
			}
			p.NudgeY(false);
		}
		p.NextLine();
		GUI.Box(p.Box(iconSize), Icons.Plane(EPlane.AIR), p.s);
		if (c.Contains(EPlane.AIR, out t)) {
			p.NudgeX();
			p.NudgeY();
			box = p.Box(0.5f);
			//FancyText.Highlight(box, t.ToString(), p.s, t.Owner.Colors);
			t.DisplayThumb(new Panel(box, p.LineH, p.s), iconSize);
			if (GUI.Button (box, "", p.s)) {
				if (Input.GetMouseButtonUp(0)) {t.Display.Effect(EEffect.SHOW);}
				if (Input.GetMouseButtonUp(1)) {Inspected = t;}
			}
			p.NudgeY(false);
		}
		p.NextLine();		
		GUI.Box(p.Box(iconSize), Icons.Plane(EPlane.GND), p.s);
		if (c.Contains(EPlane.GND, out t)) {
			p.NudgeX();
			p.NudgeY();
			box = p.Box(0.5f);
			t.DisplayThumb(new Panel(box, p.LineH, p.s), iconSize);
			if (GUI.Button (box, "", p.s)) {
				if (Input.GetMouseButtonUp(0)) {t.Display.Effect(EEffect.SHOW);}
				if (Input.GetMouseButtonUp(1)) {Inspected = t;}
			}
			p.NudgeY(false);
		}
		p.NextLine();		
		GUI.Box(p.Box(iconSize), Icons.Plane(EPlane.SUNK), p.s);
		if (c.Contains(EPlane.SUNK, out t)) {
			p.NudgeX();
			p.NudgeY();
			box = p.Box(0.5f);
			t.DisplayThumb(new Panel(box, p.LineH, p.s), iconSize);
			if (GUI.Button (box, "", p.s)) {
				if (Input.GetMouseButtonUp(0)) {t.Display.Effect(EEffect.SHOW);}
				if (Input.GetMouseButtonUp(1)) {Inspected = t;}
			}
			p.NudgeY(false);
		}
		p.NextLine();


		if (c.Sensors().Count > 0) {
			p.NextLine();
			GUI.Label(p.Box(0.5f), "Local effects:", p.s);
			p.NextLine();

			foreach (Sensor s in c.Sensors()) {
				p.NudgeX();
				FancyText.Highlight(p.Box(0.5f), s.ToString(), p.s, s.Parent.Owner.Colors);
				p.NextLine();
			}
		}

	}




	public static bool ShiftMouseOver(Rect box) {
		if (box.Contains(MousePos()) && ShiftKey()) {return true;}
		return false;

	}

	static Vector2 MousePos () {
		return new Vector2 (
			Input.mousePosition.x, 
			(-Input.mousePosition.y) + Screen.height + panel.H - panel.LineH + scrollPos.y);
	}

	public static void ToolTip (string text) {
		Rect box = new Rect(MousePos().x, MousePos().y, 100, 20);
		GUI.Box(box, ""); GUI.Box(box, ""); GUI.Box(box, ""); GUI.Box(box, "");
		GUI.Box(box, text);
	}

	static bool ShiftKey () {
		if (Input.GetKey("left shift") || Input.GetKey("right shift")) {return true;}
		return false;
	}
}
