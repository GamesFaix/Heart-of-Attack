using UnityEngine;
using System.Collections.Generic;
using HOA;

public class GUIInspector : MonoBehaviour {

	static Panel panel;
	
	static ITargetable inspected = default(ITargetable);

	public static ITargetable Inspected {
		get {return inspected;}
		set {
			inspected = value;
			inspectedAction = default(Action);
		}
	}
	
	static Vector2 scrollPos = new Vector2(0,0);
	float internalW = 100;
	float iconSize = 30;
	float internalH = 0;
			
	static Action inspectedAction =  default(Action);
	static ETip tip;			
	public static ETip Tip {set {tip = value;}}

	public void Display(Panel p){
		tip = ETip.NONE;
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
					GUI.Label(p.Box(iconSize), Icons.TIMER());
					p.NudgeY();
					GUI.Label(p.Box(100), timer.Name);
					p.NudgeY(false);
					p.NextLine();
				}
			}


			float unitH = 0;
			if (t is Unit) {Units (t, new Panel(p.TallBox(12), p.LineH, p.s), p, out unitH);}
			GUI.Label(p.LineBox, "");
			internalH = tokenH+unitH;

			if (ShiftKey() && tip != ETip.NONE) {GUIToolTips.Tip(MousePos(), tip);}
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
		if (t.Owner != default(Player)) {Owner (t, new Panel (p.Box(100), p.LineH, p.s), super);}
		p.NextLine();
		
		p.NudgeX();	
		Plane(t, new Panel (p.Box(iconSize*3), p.LineH, p.s), super);
		p.NudgeX(); p.NudgeX();
		Special(t, new Panel (p.Box(iconSize*3), p.LineH, p.s), super);
		p.NudgeX(); p.NudgeX();
		Cell(t, new Panel (p.Box(iconSize*3), p.LineH, p.s), super);


		p.NextLine();
		h=p.y2-yStart;
	}

	void Name (Token t, Panel p, Panel super) {
		FancyText.Highlight(p.FullBox, t.FullName, p.s, t.Owner.Colors);
		if (GUI.Button (p.FullBox, "", p.s)) {t.SpriteEffect(EEffect.SHOW);}
		if (ShiftMouseOver(p.FullBox)) {ToolTip("Name");}
	}
	void OnDeath (Token t, Panel p, Panel super) {
		GUI.Box(p.Box(20), Icons.ONDEATH(), p.s);
		p.NudgeX();
		GUI.Label(p.Box(1),TokenRef.CodeToString(t.OnDeath));
		if (ShiftMouseOver(p.FullBox)) {
			tip = ETip.ONDEATH;
		}
	}

	void Owner (Token t, Panel p, Panel super) {
		GUI.Label (p.FullBox, t.Owner.ToString());
		if (ShiftMouseOver(p.FullBox)) {ToolTip("Owner");}
	}
	void Cell (Token t, Panel p, Panel super) {
		GUI.Box(p.FullBox, "");
		GUI.Box(p.Box(iconSize), Icons.Class(EClass.CELL), p.s);
		p.NudgeY(); p.NudgeX();
		GUI.Label (p.Box(iconSize*2), t.Cell.ToString(), p.s);
		p.NudgeY(false);
		if (ShiftMouseOver(p.FullBox)) {
			tip = ETip.CELL;
		}
		if (GUI.Button(p.FullBox, "", p.s) && Input.GetMouseButtonUp(1)) {Inspected = t.Cell;}
	}

	void Plane (Token t, Panel p, Panel super) {
		GUI.Box(p.FullBox, "");
		Rect box;
		string str = "";

		if (t.IsPlane(EPlane.SUNK)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.Plane(EPlane.SUNK), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {str = "Sunken";}
		}	
		if (t.IsPlane(EPlane.GND)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.Plane(EPlane.GND), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {str = "Ground";}
		}	
		if (t.IsPlane(EPlane.AIR)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.Plane(EPlane.AIR), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {str = "Air";}
		}	
		if (t.IsPlane(EPlane.ETH)) {
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

		if (t.IsClass(EClass.KING)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.Class(EClass.KING), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {
				tip = ETip.KING;
			}
		}	
		if (t.IsClass(EClass.HEART)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.Class(EClass.HEART), p.s); p.NudgeX();
			if (ShiftMouseOver(box)){
				tip = ETip.HEART;
			}
		}
		if (t.IsClass(EClass.DEST)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.Class(EClass.DEST), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {
				tip = ETip.DEST;
			}
		}
		if (t.IsClass(EClass.REM)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.Class(EClass.REM), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {
				tip = ETip.REM;
			}
		}
		if (t.IsClass(EClass.TRAM)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.Class(EClass.TRAM), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {
				tip = ETip.TRAM;
			}
		}

		if (str != "" && ShiftMouseOver(p.FullBox)) {ToolTip(str);}
	}

	void Units (Token t, Panel p, Panel Super, out float h) {
		Unit u = (Unit)t;
		float yStart = p.y2;
		p.NudgeX();
		Health(u, new Panel(p.LineBox, p.LineH, p.s), p);

		p.NudgeX();
		Watch(u, new Panel(p.LineBox, p.LineH, p.s), p);

		p.NudgeX();
		Wallet(u, new Panel(p.LineBox, p.LineH, p.s), p);
		Arsenal(u, new Panel(p.TallBox(9), p.LineH, p.s), p);
		h = p.y2-yStart;

	}

	void Health (Unit u, Panel p, Panel super) {
		string str = "";
	
		Rect hpBox = p.Box(iconSize + 90 +5); 
		if (ShiftMouseOver(hpBox)) {
			tip = ETip.HP;
		}
		p.ResetX();
		Rect box = p.Box(iconSize);
		GUI.Box(box, Icons.Stat(EStat.HP), p.s);
		p.NudgeX();
		p.NudgeY();
		box = p.Box(90);
		GUI.Label(box, u.HPString, p.s);	

		p.NudgeY(false);
		p.NudgeX();
		if (u.DEF > 0) {
			float x3 = p.x2;
			Rect defBox = p.Box(iconSize*2 +5);
			if (ShiftMouseOver(defBox)) {
				tip = ETip.DEF;
			}
			p.x2 = x3;
			box = p.Box(iconSize);
			GUI.Box(box, Icons.Stat(EStat.DEF), p.s);
			p.NudgeX();
			p.NudgeY();
			box = p.Box(iconSize);
			GUI.Label(box, u.DEF+"", p.s);
			p.NudgeY(false);
		}

		p.NudgeX();
		if (u.COR > 0 ) {
			float x3 = p.x2;
			Rect corBox = p.Box(iconSize*2 + 5);
			if (ShiftMouseOver(corBox)) {str = "Corrosion";}
			p.x2 = x3;
			box = p.Box(iconSize);
			GUI.Box(box, Icons.Stat(EStat.COR), p.s);
			p.NudgeX();
			p.NudgeY();
			box = p.Box(iconSize);
			GUI.Label(box, u.COR+"", p.s);
		
		}

		if (str != "" && ShiftMouseOver(p.FullBox)) {ToolTip(str);}
	}

	void Watch (Unit u, Panel p, Panel super) {
		string str = "";

		float x3 = p.x2;
		Rect inBox = p.Box(iconSize*2 + 5);
		if (ShiftMouseOver(inBox)) {
			tip = ETip.IN;
		}
		p.x2 = x3;
		Rect box = p.Box(iconSize);
		GUI.Box(box, Icons.Stat(EStat.IN), p.s);
		p.NudgeX();
		p.NudgeY();
		box = p.Box(iconSize);
		GUI.Label(box, u.IN+"", p.s);
		p.NudgeY(false);

		if (u.IsStunned()) {
			x3 = p.x2;
			Rect stunBox = p.Box(iconSize*2 + 5);
			if (ShiftMouseOver(stunBox)) {str = "Stunned";}
			p.x2 = x3;
			p.NudgeX();
			box = p.Box(iconSize);
			GUI.Box(box, Icons.Stat(EStat.STUN), p.s);
			p.NudgeX();
			p.NudgeY();
			box = p.Box(iconSize);
			GUI.Label(box, u.STUN+"", p.s);
			p.NudgeY(false);
		}
		else if (u.IsSkipped()){
			p.NudgeX();
			box = p.Box(iconSize);
			GUI.Box(box, Icons.SKIP(), p.s);
			if (ShiftMouseOver(box)) {str = "Skipped";}
		}
		if (str != "" && ShiftMouseOver(p.FullBox)) {ToolTip(str);}
	}

	void Wallet (Unit u, Panel p, Panel super) {
		string str = "";

		float x3 = p.x2;
		Rect apBox = p.Box(iconSize*2 +5);
		if (ShiftMouseOver(apBox)) {
			tip = ETip.AP;
		}
		p.x2 = x3;
		Rect box = p.Box(iconSize);
		GUI.Box(box, Icons.Stat(EStat.AP), p.s);
		p.NudgeX();
		p.NudgeY();
		box = p.Box(iconSize);
		GUI.Label(box, u.AP+"", p.s);
		p.NudgeY(false);

		x3 = p.x2;
		Rect fpBox = p.Box(iconSize*2 + 5);
		if (ShiftMouseOver(fpBox)) {
			tip = ETip.FP;
		}
		p.x2 = x3;
		p.NudgeX();	
		box = p.Box(iconSize);
		GUI.Box(box, Icons.Stat(EStat.FP), p.s);
		p.NudgeX();
		p.NudgeY();
		box = p.Box(iconSize);
		GUI.Label(box, u.FP+"", p.s);

		if (str != "" && ShiftMouseOver(p.FullBox)) {ToolTip(str);}
	}

	void Arsenal (Unit u, Panel p, Panel super) {
		float btnW = 150;

		Rect box;
		if (u == TurnQueue.Top) {
			p.NudgeX();
			if (GUI.Button(p.Box(btnW), "End Turn")) {
				Targeter.Find(new AEnd(u));
				GUIMaster.PlaySound(EGUISound.CLICK);
			}
			p.NextLine();
		}

		foreach (Action a in u.Arsenal()) {
			p.NudgeX();

			box = p.Box(btnW);
			
			if (GUI.Button(box, a.Name)) {
				GUIMaster.PlaySound(EGUISound.CLICK);
				Targeter.Find(a);
			}
			if (box.Contains(MousePos())) {inspectedAction = a;}
			p.NextLine();
			
		}


		Action(new Panel(new Rect (p.X+btnW+10, p.Y, p.W-btnW-25, p.LineH*9), p.LineH, p.s));

		p.NextLine();

		if (Targeter.Pending() != default(Action)) {
			p.NudgeX(); p.NudgeY();
			GUI.Label(p.TallBox(3), "Pending: \n"+Targeter.PendingString());

			p.NudgeX();
			if (GUI.Button(p.Box(btnW), "Execute") || Input.GetKeyUp("space")) {

				if (Targeter.Ready) {
					Targeter.Execute();
					GUIMaster.PlaySound(EGUISound.CLICK);
				}
				else {GameLog.Out("Please finish selecting targets.");}
			}

			if (GUI.Button(p.Box(btnW), "Cancel") || Input.GetKeyUp("escape")) {
				Targeter.Reset();
				GUIMaster.PlaySound(EGUISound.CLICK);
			}
		}
	}

	void Action (Panel p) {
		GUI.Box (p.FullBox, "");
		if (inspectedAction != default(Action)) {
			Action a = inspectedAction;
			GUI.Label(p.LineBox, a.Name, p.s);

			a.DrawPrice(new Panel(p.LineBox, p.LineH, p.s));

			a.DrawAim(0, new Panel(p.LineBox, p.LineH, p.s));



			float descH = (p.H-(p.LineH*2))/p.H;
			//Rect descBox = new Rect(p.x2, p.y2, p.W, descH);

			GUI.Label(p.TallBox(descH), a.Desc());			
		}
	}

	void Cell (Cell c, Panel p, Panel super) {
		GUI.Box(p.Box(iconSize), Icons.Class(EClass.CELL), p.s);
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
			FancyText.Highlight(box, t.FullName, p.s, t.Owner.Colors);
			if (GUI.Button (box, "", p.s)) {
				if (Input.GetMouseButtonUp(0)) {t.SpriteEffect(EEffect.SHOW);}
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
			FancyText.Highlight(box, t.FullName, p.s, t.Owner.Colors);
			if (GUI.Button (box, "", p.s)) {
				if (Input.GetMouseButtonUp(0)) {t.SpriteEffect(EEffect.SHOW);}
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
			FancyText.Highlight(box, t.FullName, p.s, t.Owner.Colors);
			if (GUI.Button (box, "", p.s)) {
				if (Input.GetMouseButtonUp(0)) {t.SpriteEffect(EEffect.SHOW);}
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
			FancyText.Highlight(box, t.FullName, p.s, t.Owner.Colors);
			if (GUI.Button (box, "", p.s)) {
				if (Input.GetMouseButtonUp(0)) {t.SpriteEffect(EEffect.SHOW);}
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

	void ToolTip (string text) {
		Rect box = new Rect(MousePos().x, MousePos().y, 100, 20);
		GUI.Box(box, ""); GUI.Box(box, ""); GUI.Box(box, ""); GUI.Box(box, "");
		GUI.Box(box, text);
	}

	static bool ShiftKey () {
		if (Input.GetKey("left shift") || Input.GetKey("right shift")) {return true;}
		return false;
	}
}
