using UnityEngine;
using HOA;

public class Core : MonoBehaviour {
	//public static NetworkConsole nc;
	
	public static GUISelectors guiSelectors;
	
	void Start () {
		Setup();
		//DebugShortcut();
	
		GUILog.ScrollToBottom();
	}

	void Setup () {
		//nc = gameObject.AddComponent("NetworkConsole") as NetworkConsole;
		gameObject.AddComponent("GUIMaster");
		ImageLoader.Load();
		TemplateFactory.MakeTemplates();
		guiSelectors = gameObject.AddComponent("GUISelectors") as GUISelectors;
	}
	
	void DebugShortcut () {
		for (int i=0; i<8; i++) {
			InputBuffer.Submit(new RRosterAdd(Source.ActivePlayer, new Player(i)));
		}
		InputBuffer.Submit(new RRosterRandom(Source.ActivePlayer));
		InputBuffer.Submit(new RStart(Source.ActivePlayer, 6));
		
		
	}
}
