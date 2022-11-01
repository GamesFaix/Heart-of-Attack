using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIQueue : MonoBehaviour {
	static GUIStyle s = new GUIStyle();

	void Start(){
		s.normal.textColor = Color.white;
		s.fontSize = 20;
	}

	float lineH = 30;
	float x = 0;
	float y = 0;
	float w = Screen.width*0.35f;
	float h = Screen.height;

	void OnGUI(){
			
		Rect box = new Rect(x,y,w,h);
		GUI.Box(box,"");

		box.height = lineH;
		foreach (Unit u in TurnQueue.units){
			box.x = x;

			if(GUI.Button(box,u.fullName, s)){GUIStats.Inspect(u);}
			box.x += 200;

			GUI.Label(box, "IN ("+u.IN()+")", s);
			box.x += 60;

			if (u.STUN() > 0) {GUI.Label(box,"Stun ("+u.STUN()+")",s);}
			else if (u.skipped == true){GUI.Label(box,"Skipped!", s);}

			box.y += lineH;
		}
	}
}
