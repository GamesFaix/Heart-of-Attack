using UnityEngine;
using HOA;
using HOA.Textures;

public class GUIGame : MonoBehaviour {
	static GUIStyle s = new GUIStyle();

	void Awake() {
		s.normal.textColor = Color.white;
		s.fontSize = 20;
	}

	bool cheat = false;
	public static bool tip = false;

	public void Display() {
		float sW = Screen.width;
		float sH = Screen.height;
		float barW = 400;
		float lineH = 30;

		GUI.DrawTexture(new Rect(sW-barW, 0, barW, lineH), Backgrounds.WoodLarge, ScaleMode.StretchToFill);

		string cheatLabel = (cheat ? "Resume" : "Cheat");

		if (GUI.Button(new Rect(sW-barW, 0, barW/4, lineH), cheatLabel)) {
			GUIMaster.PlaySound(GUISounds.Click);
			Targeter.Reset();
			cheat = !cheat;
		}
		if (GUI.Button(new Rect(sW-(barW*1/4), 0, barW/4, lineH), "Quit")) {
			GUIMaster.PlaySound(GUISounds.Click);
			Game.Quit();
		}
		
//		float inspH = sH-lineH;
		float qH = 150;
		float qW = (sW - barW)/2;

		Panel inspectorPanel = new Panel(new Rect(sW-barW, lineH, barW, sH-lineH), lineH, s);

		if (cheat) {GUICheats.Display (inspectorPanel);}
		else if (tip) {TipInspector.Display (inspectorPanel);}
		else {GUIInspector.Display(inspectorPanel);}

		GUIQueue.Display(new Panel(new Rect(0, sH-qH, qW, qH), 20, s));
		GUILog.Display(new Panel(new Rect(qW, sH-qH, qW, qH), 20, s));

		Rect boardRect = new Rect(0, 0, sW-barW, sH-qH);

		GameWorldCursor.gameWindow = InvertY(boardRect);

		Camera.main.rect = Normalize(InvertY(boardRect));
	}

	Rect InvertY (Rect r) {return new Rect (r.x, Screen.height-r.height, r.width, r.height);}

	Rect Normalize (Rect r) {
		float sH = Screen.height;
		float sW = Screen.width;
		return new Rect (r.x/sW, r.y/sH, r.width/sW, r.height/sH);
	}
}
