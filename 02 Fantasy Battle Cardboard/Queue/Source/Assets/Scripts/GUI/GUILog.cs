using UnityEngine;
using System.Collections;

public class GUILog : MonoBehaviour {
	static GUIStyle s = new GUIStyle();
	void Start(){
		s.normal.textColor = Color.white;
		s.fontSize = 16;
	}
	static bool hide = false;
	public static void Hide(){hide = true;}
	public static void Show(){hide = false;}

	static float w;
	static float x;
	static float y;
	static float h;
	static int lineH = 25;
	static int lines;
	static bool minimize = false;
	static string label = "Expand";

	void OnGUI(){
		w = Screen.width*0.35f;
		x = Screen.width-w;
		if (!minimize){
			y = Screen.height/2;
			h = Screen.height/2;
		}
		else {
			h = 5*lineH;
			y = Screen.height-h;
		}
		lines = (int)Mathf.Floor((h-lineH)/lineH)-1;

		if (hide==false){GUI.Box(new Rect(x,y,w,h)," ");}

		CommandField();
		if (GameLog.Count(ShowLog())>lines){DrawScroll();}
		if (hide==false){CommandHistory();}
		ControlButtons();

	}

	int view = 2;
	void ControlButtons(){
		float btnW = (w-scrollW)/5;
		GUI.Label(new Rect(x+5,y,btnW,lineH),"LOG");
		string[] views = new string[3]{"Input","Output","Debug"};
		view = GUI.Toolbar(new Rect(x+btnW,y,btnW*3,lineH), view, views);
		switch (view){
		case 0:
			debugLog=false;
			inLog=true;
			outLog=false;
			break;
		case 1:
			debugLog=false;
			inLog=false;
			outLog=true;
			break;
		case 2:
			debugLog=true;
			break;
		default:
			break;
		}
			
		if (GUI.Button(new Rect(x+w-btnW-scrollW/2,y,btnW,lineH),label)){
			minimize = !minimize;
			if (minimize){label = "Expand";}
			else {label = "Shrink";}
		}


	}

	string input = "";
	void CommandField(){

		Rect fieldBox = new Rect(x, y+h-lineH, w, lineH);
		input = GUI.TextField(fieldBox,input,100);
		if(EnterKeyPressed()){
			CMD.New(input);
			input="";
			ScrollToBottom();
		}
	}

	public static void ScrollToBottom(){
		scrollPos = scrollBtm;
	}

	bool EnterKeyPressed(){
		if(Event.current.type==EventType.KeyDown 
		   && Event.current.character=='\n'){return true;}
		return false;
	}

	static int offset = 0;
	int First(){return offset;}
	int Last(){
		if(GameLog.Count(ShowLog())<=lines) {return GameLog.Count(ShowLog())-1;}
		else {return First()+lines-1;}
	}

	static float scrollW = 30;
	static float scrollPos = 0;
	static float scrollBtm;
	void DrawScroll(){
		float size = lines;
		scrollBtm = GameLog.Count(ShowLog());
		float top = 0;
		Rect scrollBox = new Rect(x+w-(scrollW/2),y,scrollW,h-lineH);

		scrollPos = GUI.VerticalScrollbar(scrollBox, scrollPos, size, top, scrollBtm);
		int round = (int)Mathf.RoundToInt(scrollPos);
		offset = round;
	}
	
	public static bool inLog = false;
	public static bool outLog = true;
	public static bool debugLog = true;
	
	LogIO ShowLog(){
		if (debugLog == true){return LogIO.DEBUG;}
		else{
			if ((inLog == true) && (outLog == true)){return LogIO.IO;}
			else {
				if (inLog == true) {return LogIO.IN;}
			}
		}
		return LogIO.OUT;
	}
	
	void CommandHistory(){
		float printY = Screen.height-lineH*2;
		for (int i=Last(); i>=First(); i--){
			string c = GameLog.Index(i,ShowLog());
			if (c.Length>50){
				c = c.Remove(50);
				c +="...";
			}
			GUI.Label(new Rect(x+5,printY,w-10,lineH),c,s);
			printY-=lineH;
		}
	}	
}
