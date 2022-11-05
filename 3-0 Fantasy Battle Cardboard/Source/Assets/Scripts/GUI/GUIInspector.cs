using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIInspector : MonoBehaviour {
	static Unit u = default(Unit);
	static GUIStyle s = new GUIStyle();
	
	void Start(){
		s.normal.textColor = Color.white;
		s.fontSize = 20;
	}

	public static void Inspect(Unit unit) {u = unit;}

	bool hide = false;

	float lineH=30;
	void OnGUI(){
		float y = 0;
		float w = Screen.width*0.35f;
		float x = Screen.width-w;
		float h = Screen.height/2;
		float btnW = 80;

		if (!hide){
			Rect box = new Rect(x,y,w,h);
			GUI.Box(box,"");
			GUI.Label(new Rect(x+5,y,btnW,lineH),"INSPECTOR");
			if (GUI.Button(new Rect(x+w-btnW,y,btnW,lineH),"Hide")) {hide = !hide;}
			y+=lineH;

			if (u!=null){
				
				//NAME
				box = new Rect(x,y,w,lineH);
				GUI.Label(box,u.FullName(),s);

				box.x+=250;
				GUI.Label(box,Roster.Name(u.Owner()),s);

				box.x=x;

				//PLANE & CLASS
				box.y += lineH;
				GUI.Label(box,u.PlaneString(),s);
				if (u.SpecialString()!=""){
					box.x += 150;
					GUI.Label(box,u.SpecialString(),s);
					box.x = x;
				}

				//HP, DEF, COR
				box.y += lineH;
				GUI.Label(box,"HP "+u.HPString(),s);
				if (u.DEF() > 0){
					box.x += 150;
					GUI.Label(box,"DEF "+u.DEFString(),s);
				}
				if (u.COR()>0){
					box.x += 150;
					GUI.Box(box,"Corrosion ("+u.COR()+")",s);
				}
				box.x = x;

				//IN, STUN, SKIP
				box.y += lineH;
				GUI.Label(box,"IN ("+u.IN()+")",s);

				if (u.IsStunned()) {
					box.x += 150;
					GUI.Label(box,"Stunned ("+u.Stunned()+")",s);
					box.x = x;
				}
				else if (u.IsSkipped()){
					box.x += 150;
					GUI.Label(box,"Skipped!", s);
					box.x = x;
				}
			
				//AP, FP
				box.y += lineH;
				GUI.Label(box,"AP: "+u.APString(),s);
				box.x += 150;
				GUI.Label(box,"FP "+u.FPString(),s);
			}
		}
		else{
			Rect box = new Rect(x,y,w,lineH);
			GUI.Box(box,"");
			GUI.Label(new Rect(x+5,y,btnW,lineH),"INSPECTOR");
			if (GUI.Button(new Rect(x+w-btnW,y,btnW,lineH),"Show")) {hide = !hide;}
		}



	}




}
