using UnityEngine;
using HOA;

public class GUILog : MonoBehaviour {
	
	public void Display (Panel p){
		p.NudgeY();

		float historySize = (p.H-2*p.LineH) / p.H;
		Panel historyPanel = new Panel(p.TallBox(historySize), p.LineH, p.s);
		CommandHistory(historyPanel);
		
		CommandField(p.LineBox);

	}

	//Control Buttons
	//int view = 1;
	ELog showLog = ELog.OUT;
	/*
	void ControlButtons(Panel p){
		string[] views = new string[3]{"Input","Output","Debug"};
		view = GUI.Toolbar(p.LineBox, view, views);
		switch (view){
		case 0:
			showLog = ELog.IN;
			break;
		case 1:
			showLog = ELog.OUT;
			break;
		case 2:
			showLog = ELog.DEBUG;
			break;
		default:
			break;
		}
	}*/


	static Vector2 historyScroll = new Vector2 (0,0);
	static float internalH;
	static float internalW = 1;
	float charWidth = 7f;
	string entry;
	
	//Command History
	void CommandHistory(Panel p){
		//GUI.Box(p.FullBox,"");

		internalH = p.LineH * GameLog.Count(showLog);
		historyScroll = GUI.BeginScrollView(p.FullBox, historyScroll, new Rect(p.X,p.Y,internalW,internalH));
		
		for (int i=0; i<GameLog.Count(showLog); i++){
			entry = GameLog.Index(i, showLog);
			p.x2 += 5;
			GUI.Label(p.Box(1), entry);
			if (entry.Length*charWidth > internalW) {internalW = entry.Length*charWidth;}
			p.NextLine();
		}
		
		GUI.EndScrollView();		
	}
	public static void ScrollToBottom () {historyScroll = new Vector2 (0, internalH);}

	//Command Field
	string input = "";
	void CommandField (Rect r) {

		input = GUI.TextField(r, input, 100);
		if(EnterKeyPressed()){
			input="";
			ScrollToBottom();
		}
	}
	bool EnterKeyPressed () {
		if(Event.current.type == EventType.KeyDown 
		   && Event.current.character == '\n') {
			return true;
		}
		return false;
	}
}
