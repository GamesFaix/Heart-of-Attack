using UnityEngine;
using HOA;

public class GUIGame : MonoBehaviour {
	static GUIStyle s = new GUIStyle();

	public static GUIBoard board;
	public static GUIInspector inspector;
	public static GUILog log;
	public static GUIQueue queue;
	public static GUITools tools;

	void Awake() {
		board 	  = gameObject.AddComponent("GUIBoard") 	as GUIBoard;
		inspector = gameObject.AddComponent("GUIInspector") as GUIInspector;
		log 	  = gameObject.AddComponent("GUILog") 		as GUILog;
		queue 	  = gameObject.AddComponent("GUIQueue") 	as GUIQueue;
		tools 	  = gameObject.AddComponent("GUITools") 	as GUITools;

		s.normal.textColor = Color.white;
		s.fontSize = 20;
	}

	bool showTools = false;

	public void Display() {
		float sW = Screen.width;
		float sH = Screen.height;


		float barW = 400;
		float lineH = 30;
		GUI.DrawTexture(new Rect(sW-barW, 0, barW, lineH), ImageLoader.wood[1], ScaleMode.StretchToFill);

		if (GUI.Button(new Rect(sW-barW, 0, barW/4, lineH), "Game")) {
			showTools = false;	
		}
		if (GUI.Button(new Rect(sW-(barW*3/4), 0, barW/4, lineH), "Manual")) {
			showTools = true;	
		}
		if (GUI.Button(new Rect(sW-(barW*1/4), 0, barW/4, lineH), "Quit")) {
			Game.Quit();	
		}
		
		float inspH = sH-lineH;
		float qH = 150;
		float qW = (sW - barW)/2;

		if (showTools) {
			Panel toolsPanel = new Panel(new Rect(sW-barW, lineH, barW, sH-lineH), 30, s);
			tools.Display (toolsPanel);
		}
		else {
			Panel inspectorPanel = new Panel(new Rect(sW-barW, lineH, barW, inspH), lineH, s);
			inspector.Display(inspectorPanel);

			Panel queuePanel = new Panel(new Rect(0, sH-qH, qW, qH), 20, s);
			queue.Display(queuePanel);

			Panel logPanel = new Panel(new Rect(qW, sH-qH, qW, qH), 20, s);
			log.Display(logPanel);
		}

		Rect boardRect = new Rect(0, 0, sW-barW, sH-qH);
		/*
		Panel boardPanel = new Panel(boardRect, 30, s);
		board.Display(boardPanel);

		*/
		Camera.main.rect = InvNormalRect(boardRect);
	}


	Rect InvNormalRect (Rect rect) {
		float sH = Screen.height;
		float sW = Screen.width;

		float x = rect.x/sW;

		float y = (float)(sH-rect.height)/sH;

		float w = rect.width/sW;
		float h = rect.height/sH;

		return new Rect (x,y,w,h);
	}


}
