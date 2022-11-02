using UnityEngine;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Players;
using HOA.Actions;

public class GUIInspector : MonoBehaviour {

	Panel panel;
	
	static Token t = default(Token);

	public static Token Inspected {
		get {return t;}
		set {
			t = value;
			description = "";
		}
	}
	
	Vector2 scrollPos = new Vector2(0,0);
	float internalW = 100;
	float iconSize = 30;
	float internalH = 0;
			
	static string description =  "";
				
	public void Display(Panel p){
		panel = p;

		GUI.Box(p.FullBox, "");
		
		p.NudgeX();
		GUI.Label(p.Box(0.3f), "INSPECTOR");

		p.NextLine();

		if (t != null) {
			float boxH = (p.H-p.LineH) / p.H;
			//float scrollStart = p.y2;

			scrollPos = GUI.BeginScrollView(p.TallBox(boxH), scrollPos, new Rect(p.x2, p.y2, internalW, internalH));
				
			float tokenH = 0;
			Tokens(new Panel(p.TallBox(3), p.LineH, p.s), p, out tokenH);

			if (t.Notes() != "") {
				p.NudgeX();
				p.NudgeY();
				GUI.Label(p.Box(0.9f), t.Notes());
				p.NudgeY(false);
				p.NextLine();
			}
			float unitH = 0;
			if (t is Unit) {Units (new Panel(p.TallBox(12), p.LineH, p.s), p, out unitH);}
			GUI.Label(p.LineBox, "");
			internalH = tokenH+unitH;
			GUI.EndScrollView();
		}
	}

	void Tokens (Panel p, Panel super, out float h) {
		float yStart = p.y2;
		p.NudgeX();
		Name(new Panel (p.Box(200), p.LineH, p.s), super);
		if (t.OnDeath != TTYPE.NONE) {OnDeath(new Panel (p.Box(200), p.LineH, p.s), super);}
		p.NextLine();
		
		p.NudgeX();
		if (t.Owner != default(Player)) {Owner (new Panel (p.Box(100), p.LineH, p.s), super);}
		p.x2 = p.X + p.W*0.5f;
		Cell(new Panel (p.Box(100), p.LineH, p.s), super);
		p.NextLine();
		
		p.NudgeX();	
		Plane(new Panel (p.Box(iconSize*3), p.LineH, p.s), super);
		p.NudgeX(); p.NudgeX();
		Special(new Panel (p.Box(iconSize*3), p.LineH, p.s), super);
		p.NextLine();
		h=p.y2-yStart;
	}

	void Name (Panel p, Panel super) {
		FancyText.Highlight(p.FullBox, t.FullName, p.s, t.Owner.Colors);
		if (GUI.Button (p.FullBox, "", p.s)) {t.SpriteEffect(EFFECT.SHOW);}
		if (ShiftMouseOver(p.FullBox)) {ToolTip("Name");}
	}
	void OnDeath (Panel p, Panel super) {
		GUI.Box(p.Box(20), Icons.REM(), p.s);
		p.NudgeX();
		GUI.Label(p.Box(1),TokenRef.CodeToString(t.OnDeath));
		if (ShiftMouseOver(p.FullBox)) {ToolTip("Remains left on token death");}
	}

	void Owner (Panel p, Panel super) {
		GUI.Label (p.FullBox, t.Owner.ToString());
		if (ShiftMouseOver(p.FullBox)) {ToolTip("Owner");}
	}
	void Cell (Panel p, Panel super) {
		GUI.Label (p.FullBox, t.Cell.ToString(), p.s);
		if (ShiftMouseOver(p.FullBox)) {ToolTip("Cell");}
	}

	void Plane (Panel p, Panel super) {
		GUI.Box(p.FullBox, "");
		Rect box;
		string str = "";

		if (t.IsPlane(PLANE.SUNK)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.SUNK(), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {str = "Sunken";}
		}	
		if (t.IsPlane(PLANE.GND)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.GND(), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {str = "Ground";}
		}	
		if (t.IsPlane(PLANE.AIR)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.AIR(), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {str = "Air";}
		}	
		if (t.IsPlane(PLANE.ETH)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.ETH(), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {str = "Ethereal";}
		}	
		if (str != "" && ShiftMouseOver(p.FullBox)) {ToolTip(str);}
	}
	void Special (Panel p, Panel super) {
		GUI.Box(p.FullBox, "");
		Rect box;
		string str = "";

		if (t.IsSpecial(SPECIAL.KING)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.KING(), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {str = "Attack King";}
		}	
		if (t.IsSpecial(SPECIAL.HOA)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.HEART(), p.s); p.NudgeX();
			if (ShiftMouseOver(box)){str = "Heart of Attack";}
		}
		if (t.IsSpecial(SPECIAL.DEST)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.DEST(), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {str = "Destructible";}
		}
		if (t.IsSpecial(SPECIAL.REM)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.REM(), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {str = "Remains";}
		}
		if (t.IsSpecial(SPECIAL.TRAM)) {
			box = p.Box(iconSize);
			GUI.Box(box, Icons.TRAM(), p.s); p.NudgeX();
			if (ShiftMouseOver(box)) {str = "Trample";}
		}

		if (str != "" && ShiftMouseOver(p.FullBox)) {ToolTip(str);}
	}

	void Units (Panel p, Panel Super, out float h) {
		float yStart = p.y2;
		p.NudgeX();
		Health(new Panel(p.LineBox, p.LineH, p.s), p);

		p.NudgeX();
		Watch(new Panel(p.LineBox, p.LineH, p.s), p);

		p.NudgeX();
		Wallet(new Panel(p.LineBox, p.LineH, p.s), p);
		Arsenal(new Panel(p.TallBox(9), p.LineH, p.s), p);
		h = p.y2-yStart;

	}

	void Health (Panel p, Panel super) {
		Unit u = (Unit)t;
		string str = "";
	
		Rect hpBox = p.Box(iconSize + 90 +5); 
		if (ShiftMouseOver(hpBox)) {str = "Hit Points";}
		p.ResetX();
		Rect box = p.Box(iconSize);
		GUI.Box(box, Icons.HP(), p.s);
		p.NudgeX();
		p.NudgeY();
		box = p.Box(90);
		GUI.Label(box, u.HPString, p.s);	

		p.NudgeY(false);
		p.NudgeX();
		if (u.DEF > 0) {
			float x3 = p.x2;
			Rect defBox = p.Box(iconSize*2 +5);
			if (ShiftMouseOver(defBox)) {str = "Defense";}
			p.x2 = x3;
			box = p.Box(iconSize);
			GUI.Box(box, Icons.DEF(), p.s);
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
			GUI.Box(box, Icons.COR(), p.s);
			p.NudgeX();
			p.NudgeY();
			box = p.Box(iconSize);
			GUI.Label(box, u.COR+"", p.s);
		
		}

		if (str != "" && ShiftMouseOver(p.FullBox)) {ToolTip(str);}
	}

	void Watch (Panel p, Panel super) {
		Unit u = (Unit)t;
		string str = "";

		float x3 = p.x2;
		Rect inBox = p.Box(iconSize*2 + 5);
		if (ShiftMouseOver(inBox)) {str = "Initiative";}
		p.x2 = x3;
		Rect box = p.Box(iconSize);
		GUI.Box(box, Icons.IN(), p.s);
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
			GUI.Box(box, Icons.STUN(), p.s);
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

	void Wallet (Panel p, Panel super) {
		Unit u = (Unit)t;
		string str = "";

		float x3 = p.x2;
		Rect apBox = p.Box(iconSize*2 +5);
		if (ShiftMouseOver(apBox)) {str = "Action points";}
		p.x2 = x3;
		Rect box = p.Box(iconSize);
		GUI.Box(box, Icons.AP(), p.s);
		p.NudgeX();
		p.NudgeY();
		box = p.Box(iconSize);
		GUI.Label(box, u.AP+"", p.s);
		p.NudgeY(false);

		x3 = p.x2;
		Rect fpBox = p.Box(iconSize*2 + 5);
		if (ShiftMouseOver(fpBox)) {str = "Focus points";}
		p.x2 = x3;
		p.NudgeX();	
		box = p.Box(iconSize);
		GUI.Box(box, Icons.FP(), p.s);
		p.NudgeX();
		p.NudgeY();
		box = p.Box(iconSize);
		GUI.Label(box, u.FP+"", p.s);

		if (str != "" && ShiftMouseOver(p.FullBox)) {ToolTip(str);}
	}

	void Arsenal (Panel p, Panel super) {
		Unit u = (Unit)t;

		float btnW = 100;

		Rect box;
		foreach (Action a in u.Arsenal()) {
			p.NudgeX();

			box = p.Box(btnW);
			
			if (GUI.Button(box, a.Name)) {a.Perform();}
			if (box.Contains(MousePos())) {description = a.ToString();}
			p.NextLine();
			
		}
		box = new Rect (p.X+btnW+10, p.Y, p.W-btnW-25, p.LineH*5);
		
		GUI.Box (box, "");
		box.x+=5; box.width-=5;
		GUI.Label(box, description);			
	}




	bool ShiftMouseOver(Rect box) {
		if (box.Contains(MousePos()) && ShiftKey()) {return true;}
		return false;

	}

	Vector2 MousePos () {
		return new Vector2 (
			Input.mousePosition.x, 
			(-Input.mousePosition.y) + Screen.height + panel.H - panel.LineH + scrollPos.y);
	}

	void ToolTip (string text) {
		Rect box = new Rect(MousePos().x, MousePos().y, 100, 20);
		GUI.Box(box, ""); GUI.Box(box, ""); GUI.Box(box, ""); GUI.Box(box, "");
		GUI.Box(box, text);
	}

	bool ShiftKey () {
		if (Input.GetKey("left shift") || Input.GetKey("right shift")) {return true;}
		return false;
	}
}
