using UnityEngine;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Players;
using HOA.Actions;

public class GUIInspector : MonoBehaviour {

	
	static Token t = default(Token);
	public static void Inspect(Token token) {
		t = token;
		description = "";
	}

	public static Token Inspected {
		get {return t;}
		set {t = value;}
	}
	
	Vector2 scrollPos = new Vector2(0,0);
	float internalW = 100;
	float iconSize = 30;
	float internalH = 100;
			
	static string description =  "";
				
	public void Display(Panel p){

		GUI.Box(p.FullBox, "");
		
		p.x2 += 5;
		GUI.Label(p.Box(0.3f), "INSPECTOR");

		p.NextLine();

		if (t != null) {
			float boxH = (p.H-p.LineH) / p.H;
			
			scrollPos = GUI.BeginScrollView(p.TallBox(boxH), scrollPos, new Rect(p.x2, p.y2, internalW, internalH));
				//NAME
				p.x2 +=5;	
				StyledText.Highlight(p.Box(p.W*0.5f), t.FullName, p.s, t.Owner.Colors);
				p.x2 -= p.W*0.5f;
				if (GUI.Button (p.Box(p.W*0.5f), "", p.s)) {t.SpriteEffect(EFFECT.SHOW);}


				if (t.Owner != default(Player)) {
					StyledText.Highlight(p.Box(p.W*0.5f), t.Owner.ToString(), p.s, t.Owner.Colors);
				}
			
				string onDeathLabel;
				if (t.OnDeath != TTYPE.NONE) {onDeathLabel = TokenRef.CodeToString(t.OnDeath);}
				else {onDeathLabel = "None";}
					
				p.ResetX();
				p.y2 += 20;
				p.x2 += 25;
				GUI.Box(p.Box(20), Icons.REM(), p.s);
				p.x2 += 5;
				GUI.Label(p.Box(1), onDeathLabel);
				if (internalW < p.x2) {internalW = p.x2;}
				p.y2 += 20;
				p.ResetX();
			
				//PLANE & CLASS
				p.x2 += 5;
				if (t.IsPlane(PLANE.SUNK)) {GUI.Box(p.Box(iconSize), Icons.SUNK(), p.s); p.x2 += 5;}	
				if (t.IsPlane(PLANE.GND)) {GUI.Box(p.Box(iconSize), Icons.GND(), p.s); p.x2 += 5;}	
				if (t.IsPlane(PLANE.AIR)) {GUI.Box(p.Box(iconSize), Icons.AIR(), p.s); p.x2 += 5;}	
				if (t.IsPlane(PLANE.ETH)) {GUI.Box(p.Box(iconSize), Icons.ETH(), p.s); p.x2 += 5;}	
				if (t.IsSpecial(SPECIAL.KING)) {GUI.Box(p.Box(iconSize), Icons.KING(), p.s); p.x2 += 5;}	
				if (t.IsSpecial(SPECIAL.HOA)) {GUI.Box(p.Box(iconSize), Icons.HEART(), p.s); p.x2 += 5;}	
				if (t.IsSpecial(SPECIAL.DEST)) {GUI.Box(p.Box(iconSize), Icons.DEST(), p.s); p.x2 += 5;}	
				if (t.IsSpecial(SPECIAL.REM)) {GUI.Box(p.Box(iconSize), Icons.REM(), p.s); p.x2 += 5;}	
				if (t.IsSpecial(SPECIAL.TRAM)) {GUI.Box(p.Box(iconSize), Icons.TRAM(), p.s); p.x2 += 5;}
				
				p.x2 = p.X + p.W*0.5f;	

				GUI.Label (p.Box(p.W*0.5f), "Cell "+t.Cell.ToString(), p.s);
				
				p.NextLine();
			
				if (t.Notes() != "") {p.x2+=5; GUI.Label(p.LineBox, t.Notes(), p.s);}
				
				if (t is Unit){
					Unit u = (Unit)t;
					//HP, DEF, COR
					p.x2 += 5;
					GUI.Box(p.Box(iconSize), Icons.HP(), p.s);
					p.x2 += 5;
					GUI.Label(p.Box(90), u.HPString, p.s);	
				 
					if (u.DEF > 0) {
						GUI.Box(p.Box(iconSize), Icons.DEF(), p.s);
						p.x2 += 5;
						GUI.Label(p.Box(30), u.DEF+"", p.s);
						p.x2 += 5;
					}
					
					if (u.COR > 0 ) {
						GUI.Box(p.Box(iconSize), Icons.COR(), p.s);
						p.x2 += 5;
						GUI.Label(p.Box(30), u.COR+"", p.s);
						p.x2 += 5;
					}
					p.NextLine();
					
					//IN, STUN, SKIP
					p.x2 += 5;
					GUI.Box(p.Box(iconSize), Icons.IN(), p.s);
					p.x2 += 5;
					GUI.Label(p.Box(30), u.IN+"", p.s);
					
					if (u.IsStunned()) {
						p.x2 += 5;
						GUI.Box(p.Box(iconSize), Icons.STUN(), p.s);
						p.x2 += 5;
						GUI.Label(p.Box(30), u.STUN+"", p.s);
					}
					else if (u.IsSkipped()){
						p.x2 += 5;
						GUI.Label(p.Box(150), "Skipped!", p.s);
					}
					if (p.x2 > internalW) {internalW = p.x2;}
						
					//AP, FP
					p.NextLine();
					p.x2 += 5;
					GUI.Box(p.Box(iconSize), Icons.AP(), p.s);
					p.x2 += 5;	
					GUI.Label(p.Box(30), u.AP+"", p.s);
					
					p.x2 += 5;	
					GUI.Box(p.Box(iconSize), Icons.FP(), p.s);
					p.x2 += 5;
					GUI.Label(p.Box(100), u.FP+"", p.s);
					if (p.x2 > internalW) {internalW = p.x2;}
				
					p.NextLine();				
				
					float y3 = p.y2;
					float x3 = p.x2;
					//arsenal
					float btnW = 100;
					
					foreach (Action a in u.Arsenal()) {
						p.x2 += 5;
						
						Rect btnBox = p.Box(btnW);
						Vector2 mousePos = new Vector2(Input.mousePosition.x,Screen.height - Input.mousePosition.y + p.H - p.LineH); 
					
						if (GUI.Button(btnBox, a.Name)) {a.Perform();}
						if (btnBox.Contains(mousePos+scrollPos)) {description = a.ToString();}
						p.NextLine();
						
					}
					//p.y2 = y3;
					//p.x2 = x3;
					Rect rect = new Rect (x3+btnW+10, y3, p.W-btnW-25, p.LineH*5);
				
					GUI.Box (rect, "");
					rect.x+=5; rect.width-=5;
					GUI.Label(rect, description);			
					
				
				
				internalH = p.y2;
				
				}
			GUI.EndScrollView();
		}
	}
}
