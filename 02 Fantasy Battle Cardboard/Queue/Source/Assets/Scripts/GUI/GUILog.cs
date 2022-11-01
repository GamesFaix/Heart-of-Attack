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

	void OnGUI(){
		float w = Screen.width*0.3f;
		float x = Screen.width-w;
		float y = 0;
		float h = Screen.height;
		
		Rect box = new Rect(x, y, w, h);
		if (hide==false){GUI.Box(box," ");}
		
		CommandField(x,y,w,h);
		//ScrollButtons(x,y,w,h);
		if (hide==false){CommandHistory(x,y,w,h);}
	}
	
	string input = "";
	void CommandField(float x, float y, float w, float h){
		//Rect fieldLabel = new Rect(x-100,h-25,100,25);
		//GUI.Label(fieldLabel,"Type here ==>",l);
		
		Rect fieldBox = new Rect(x, h-25, w, 25);
		input = GUI.TextField(fieldBox,input,100);
		if(EnterKeyPressed()){
			CMD.New(input);
			input="";
		}
	}
	
	bool EnterKeyPressed(){
		if(Event.current.type==EventType.KeyDown 
		   && Event.current.character=='\n'){return true;}
		return false;
	}
	
	
	byte offset = 0; 
	void ScrollButtons (float x, float y, float w, float h){
		//offset buttons created - cannot not offset past 0 or mlog size
		int btnSize = 30;
		Rect upBox = new Rect(x+w-btnSize, 0, btnSize, btnSize);
		Rect dnBox = new Rect(x+w-btnSize, btnSize, btnSize, btnSize);
		if(GUI.Button(upBox,"Up") && offset>0) 				{offset-=1;}
		if(GUI.Button(dnBox,"Dn") && offset<(GameLog.LastIndex(ShowLog()))) {offset+=1;}
	}
	
	int lineH = 30;
	
	
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
	
	void CommandHistory(float x,float y,float w,float h){
		LogIO showLog = ShowLog();
		float printY = Screen.height-50;
		for (int i=(GameLog.LastIndex(showLog)); i>=0; i--){ 
			int j = i+offset;
			//Debug.Log(j);
			if (GameLog.Count(showLog) > j){
				string c = GameLog.Index(j,showLog);
				
				if (printY >= 0){
					Rect cBox = new Rect(x+5,printY,w-10,lineH);
					GUI.Label(cBox,c,s);
					printY-=lineH;
					
				}
			}
		}
	}	
}
