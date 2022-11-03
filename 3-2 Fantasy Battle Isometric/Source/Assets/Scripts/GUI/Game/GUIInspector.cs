using UnityEngine;
using System.Collections.Generic;
using HOA;

public static class GUIInspector {

	static Panel panel;
	
	static Target inspected = default(Target);

	public static Target Inspected {
		get {return inspected;}
		set {
			inspected = value;
			GUIInspectorTask.inspectedAction = default(Task);
		}
	}
	
	static Vector2 scrollPos = new Vector2(0,0);
	static float internalW = 100;
	static float iconSize = 30;
	static float internalH = 0;
			
	public static void Display(Panel p){
		GUI.DrawTexture(p.FullBox, ImageLoader.wood[1], ScaleMode.StretchToFill);

		panel = p;

		if (inspected is Token) {
			Token t = (Token)inspected;
			float boxH = (p.H-p.LineH) / p.H;

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
			p.NudgeY();

			float unitH = 0;
			if (t is Unit) {Units (t, new Panel(p.TallBox(12), p.LineH, p.s), p, out unitH);}
			GUI.Label(p.LineBox, "");
			internalH = tokenH+unitH;

			GUI.EndScrollView();
		}

		else if (inspected is Cell) {
			GUIInspectorCell.Display((Cell)inspected, new Panel(p.TallBox(3), p.LineH, p.s), p);
		}

	}

	static void Tokens (Token t, Panel p, Panel super, out float h) {
		p.NudgeY();
		float yStart = p.y2;

		p.NudgeX();
		t.DisplayThumbName(new Panel (p.Box(250), p.LineH, p.s));

		p.NudgeX();
		if (t.Owner != default(Player) && t.Owner!=Roster.Neutral) {Owner (t, new Panel (p.Box(100), p.LineH, p.s), super);}

		p.NextLine();
		p.NudgeX();p.NudgeX();p.NudgeX();

		t.DisplayOnDeath(new Panel (p.Box(200), p.LineH, p.s));
		p.NextLine();
		
		p.NudgeX();	
		t.Plane.Display(new Panel (p.Box(iconSize*3), p.LineH, p.s));

		p.NudgeX(); p.NudgeX();
		t.Special.Display(new Panel (p.Box(iconSize*3.5f), p.LineH, p.s));

		p.NudgeX(); p.NudgeX();
		if (!t.IsTemplate()) {Cell(t, new Panel (p.Box(iconSize*3), p.LineH, p.s), super);}


		p.NextLine();
		h=p.y2-yStart;
	}

	static void Owner (Token t, Panel p, Panel super) {
		if (GUI.Button (p.FullBox, t.Owner.ToString())) {
//			if (RightClick) {ToolTip("Owner");}
		}
	}

	static void Cell (Token t, Panel p, Panel super) {
		if (GUI.Button(p.FullBox, "")) {
			if (LeftClick) {Inspected = t.Body.Cell;}
			if (RightClick) {TipInspector.Inspect(ETip.CELL);}
		}
		GUI.Box(p.Box(iconSize), Icons.Special(EType.CELL), p.s);
		p.NudgeY(); p.NudgeX();
		GUI.Label (p.Box(iconSize*2), t.Body.Cell.ToString(), p.s);
	}

	static void Units (Token t, Panel p, Panel Super, out float h) {
		Unit u = (Unit)t;
		float yStart = p.y2;
		p.NudgeX();
		u.Health.Display(new Panel(p.LineBox, p.LineH, p.s), iconSize);

		p.NudgeX();
		u.Watch.Display(new Panel(p.LineBox, p.LineH, p.s), iconSize);

		p.NudgeX();
		u.Wallet.Display(new Panel(p.LineBox, p.LineH, p.s), iconSize);

		p.NudgeY();
		GUIInspectorTask.Arsenal(u, new Panel(p.TallBox(9), p.LineH, p.s), p);
		h = p.y2-yStart;
	}

	public static bool LeftClick {get {return (Input.GetMouseButtonUp(0) ? true : false);} }
	public static bool RightClick {get {return (Input.GetMouseButtonUp(1) ? true : false);} }

	public static Vector2 MousePos () {
		return new Vector2 (
			Input.mousePosition.x, 
			(-Input.mousePosition.y) + Screen.height + panel.H - panel.LineH + scrollPos.y);
	}
}
