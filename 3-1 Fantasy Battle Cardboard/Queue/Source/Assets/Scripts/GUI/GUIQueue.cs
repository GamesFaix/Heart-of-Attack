using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIQueue : MonoBehaviour {
	static GUIStyle s = new GUIStyle();

	void Start(){
		s.normal.textColor = Color.white;
		s.fontSize = 20;
	}

	static float lineH = 30;
	static int lines;
	static float x = 0;
	static float y = 0;
	static float w;
	static float h;
	static float btnW = 70;

	void OnGUI(){
		h = Screen.height;
		if (minimize==true){w = Screen.width*0.35f;}
		else {w = Screen.width*0.65f;}
		lines = (int)Mathf.Ceil(Screen.height/lineH)-1;

		DrawBox();

		if (TurnQueue.units.Count>0){DrawList();}
		if (TurnQueue.units.Count>lines){DrawScroll();}

		ExpandButton();
	}

	static int offset = 0;
	int First(){return offset;}
	int Last(){
		if(TurnQueue.units.Count<=lines) {return TurnQueue.units.Count-1;}
		else {return First()+lines-1;}
	}


	static bool minimize = true;
	public static void Shrink(){
		minimize=true;
		label = "Expand";
	}
	public static void Expand(){
		minimize = false;
		label = "Shrink";
		GUITools.Hide();
	}

	static string label = "Expand";
	void ExpandButton(){
		if (GUI.Button(new Rect(x+Screen.width*0.35f-btnW-scrollW/2,y,btnW,lineH),label)){
			if (minimize){Expand();}
			else {Shrink();}
		}
	}

	void DrawBox(){
		GUI.Box(new Rect(x,y,w,Screen.height),"");
		GUI.Label(new Rect(x+5,y,btnW,lineH),"QUEUE");
	}

	void DrawList(){
		float drawX = x+5;
		float drawY = y+lineH;
		for (int i=First(); i<=Last(); i++){
			Unit u = TurnQueue.units[i];
			float fieldW = 250;
			if(GUI.Button(new Rect(drawX,drawY,fieldW,lineH),u.FullName(), s)){
				GUIInspector.Inspect(u);
			}
			drawX+=fieldW;
			fieldW=70;
			GUI.Label(new Rect(drawX,drawY,fieldW,lineH), "IN ("+u.IN()+")", s);

			drawX+=fieldW;
			fieldW = 90;
			if (u.IsStunned()) {GUI.Label(new Rect(drawX,drawY,fieldW,lineH),"Stun ("+u.Stunned()+")",s);}
			else if (u.IsSkipped()){GUI.Label(new Rect(drawX,drawY,fieldW,lineH),"Skipped!", s);}

			if (!minimize){
				drawX+=fieldW;
				fieldW=70;
				if(u.FP()>0){GUI.Label(new Rect(drawX,drawY,fieldW,lineH), "FP "+u.FPString(), s);}

				drawX+=fieldW;
				fieldW=120;
				GUI.Label(new Rect(drawX,drawY,fieldW,lineH), "HP "+u.HPString(), s);

				drawX+=fieldW;
				fieldW=80;
				if(u.DEF()>0){GUI.Label(new Rect(drawX,drawY,fieldW,lineH), "DEF "+u.DEFString(), s);}

				drawX+=fieldW;
				fieldW=70;
				if(u.COR()>0){GUI.Label(new Rect(drawX,drawY,fieldW,lineH), "COR ("+u.COR()+")", s);}

				
				
			}
			drawY+=lineH;
			drawX = x+5;
		}
	}

	static float scrollW = 30;
	float scrollPos = 0;

	void DrawScroll(){
		float size = lines;
		float bottom = TurnQueue.units.Count;
		float top = 0;
		Rect scrollBox = new Rect(w-(scrollW/2),y,scrollW,h);

		scrollPos = GUI.VerticalScrollbar(scrollBox, scrollPos, size, top, bottom);
		int round = (int)Mathf.RoundToInt(scrollPos);
		offset = round;

	}

}
