using UnityEngine;

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
		
		Panel boardPanel = new Panel(new Rect(0, 0, sW-barW, sH), 30, s);
		board.Display(boardPanel);
		
		
		if (GUI.Button(new Rect(sW-barW, 0, barW/4, lineH), "Game")) {
			showTools = false;	
		}
		if (GUI.Button(new Rect(sW-(barW*3/4), 0, barW/4, lineH), "Manual")) {
			showTools = true;	
		}
		if (GUI.Button(new Rect(sW-(barW*1/4), 0, barW/4, lineH), "Quit")) {
			InputBuffer.Submit(new RQuit(Source.ActivePlayer));	
		}
		
		
		
		if (showTools) {
			Panel toolsPanel = new Panel(new Rect(sW-barW, lineH, barW, sH-lineH), 30, s);
			tools.Display (toolsPanel);
		}
		else {

			float logH = 150;
			float qH = lineH * 5;
			float inspH = sH - lineH - qH - logH;


			Panel inspectorPanel = new Panel(new Rect(sW-barW, lineH, barW, inspH), lineH, s);
			inspector.Display(inspectorPanel);

			Panel queuePanel = new Panel(new Rect(sW-barW, inspH + lineH, barW, qH), 20, s);
			queue.Display(queuePanel);

			Panel logPanel = new Panel(new Rect(sW-barW, sH-logH, barW, logH), 20, s);
			log.Display(logPanel);
		}
	}
}
