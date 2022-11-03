using UnityEngine;
using HOA;

public class GUIGame : MonoBehaviour {
	static GUIStyle s = new GUIStyle();

	void Awake() {
		s.normal.textColor = Color.white;
		s.fontSize = 20;
	}

	bool cheat = false;

	public void Display() {
		float sW = Screen.width;
		float sH = Screen.height;
		float barW = 400;
		float lineH = 30;

		GUI.DrawTexture(new Rect(sW-barW, 0, barW, lineH), ImageLoader.wood[1], ScaleMode.StretchToFill);

		string cheatLabel = (cheat ? "Resume" : "Cheat");

		if (GUI.Button(new Rect(sW-barW, 0, barW/4, lineH), cheatLabel)) {
			GUIMaster.PlaySound(EGUISound.CLICK);
			Targeter.Reset();
			cheat = !cheat;
		}
		if (GUI.Button(new Rect(sW-(barW*1/4), 0, barW/4, lineH), "Quit")) {
			GUIMaster.PlaySound(EGUISound.CLICK);
			Game.Quit();
		}
		
		float inspH = sH-lineH;
		float qH = 150;
		float qW = (sW - barW)/2;

		if (cheat) {GUICheats.Display (new Panel(new Rect(sW-barW, lineH, barW, sH-lineH), 30, s));}
		else {GUIInspector.Display(new Panel(new Rect(sW-barW, lineH, barW, inspH), lineH, s));}

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
