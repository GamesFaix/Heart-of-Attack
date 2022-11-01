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

	public static void Inspect(Unit unit){
		u = unit;
		if (u!=default(Unit)){GameLog.Add("Inspected "+unit.fullName+".", LogIO.DEBUG);}
		else {GameLog.Add("Inspector cleared.", LogIO.DEBUG);}

	}

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
				GUI.Label(box,u.fullName,s);

				box.x+=250;
				GUI.Label(box,Roster.Name(u.Owner()),s);

				box.x=x;

				//PLANE & CLASS
				box.y += lineH;
				GUI.Label(box,PlaneToString(u.Plane()),s);
				if (TClassToString(u.TClass())!=""){
					box.x += 150;
					GUI.Label(box,TClassToString(u.TClass()),s);
					box.x = x;
				}

				//HP, DEF, COR
				box.y += lineH;
				GUI.Label(box,"HP "+u.HPFraction(),s);
				if (u.DEF() > 0){
					box.x += 150;
					GUI.Label(box,"DEF ("+u.DEF()+")",s);
				}
				if (u.COR()>0){
					box.x += 150;
					GUI.Box(box,"Corrosion ("+u.COR()+")",s);
				}
				box.x = x;

				//IN, STUN, SKIP
				box.y += lineH;
				GUI.Label(box,"IN ("+u.IN()+")",s);

				if (u.STUN() > 0) {
					box.x += 150;
					GUI.Label(box,"Stunned ("+u.STUN()+")",s);
					box.x = x;
				}
				else if (u.skipped == true){
					box.x += 150;
					GUI.Label(box,"Skipped!", s);
					box.x = x;
				}
			
				//AP, FP
				box.y += lineH;
				GUI.Label(box,"AP ("+u.AP()+")",s);
				box.x += 150;
				GUI.Label(box,"FP ("+u.FP()+")",s);
			}
		}
		else{
			Rect box = new Rect(x,y,w,lineH);
			GUI.Box(box,"");
			GUI.Label(new Rect(x+5,y,btnW,lineH),"INSPECTOR");
			if (GUI.Button(new Rect(x+w-btnW,y,btnW,lineH),"Show")) {hide = !hide;}
		}



	}

	string PlaneToString(List<PLANE> planes){
		string s = "";
		foreach (PLANE p in planes){
			if (p == PLANE.SUNK){s += "Sunken, ";}
			if (p == PLANE.GND) {s += "Ground, ";}
			if (p == PLANE.AIR) {s += "Air, ";}
			if (p == PLANE.ETH) {s += "Ethereal, ";}
		}
		char[] trim = new char[2]{' ',','};
		s = s.Trim(trim);
		return s;
	}
	string TClassToString(List<TCLASS> tClasses){
		string s = "";
		foreach (TCLASS tc in tClasses){
			if (tc == TCLASS.KING){s += "Attack King, ";}
			if (tc == TCLASS.TRAM){s += "Trample, ";}
			if (tc == TCLASS.DEST){s += "Destructible, ";}
			if (tc == TCLASS.REM) {s += "Remains, ";}
		}
		char[] trim = new char[2]{' ',','};
		s = s.Trim(trim);
		return s;
	}


}
