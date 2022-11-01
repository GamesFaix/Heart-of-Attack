using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QueueGUI : MonoBehaviour {
	GUIStyle s;
	public GUIStyle l;
	
	void Start(){
		s = new GUIStyle();
		s.normal.textColor = Color.white;
		s.font = Resources.Load("Fonts/QueueFont1") as Font;
		l = new GUIStyle();
		l.normal.textColor = Color.white;
		l.font = s.font;
		l.fontSize = 16;
	}
	
	
	void OnGUI(){
		DisplayQueue();
		DisplayLog();
	}
	
	void DisplayQueue(){
		int x = 0;
		int y = 0;
		int w = 1000;
		int h = 30;
					
		foreach (Unit u in TurnQueue.units){
			Rect textBox = new Rect(x,y,w,h);
			if (u.STUN() > 0) {GUI.Label(textBox,"Stun ("+u.STUN()+")",s);}
			else if (u.skipped == true){GUI.Label(textBox,"Skipped!", s);}
			x+=150;
			
			textBox = new Rect(x,y,w,h);
			GUI.Label(textBox,u.fullName, s);
			x += 300;
			
			textBox = new Rect(x,y,w,h);
			string info = "IN:"+u.IN();
			GUI.Label(textBox, info, s);
			x += 60;
			
			textBox = new Rect(x,y,w,h);
			info = "HP:"+u.HPFraction();
			if (u.DEF()>0){info += ", DEF:"+u.DEF();}
			if (u.COR()>0){info += ", Corrosion:"+u.COR();}
			GUI.Label(textBox, info, s);
			
			y+=h;
			x=0;
		}
	}
	
	void DisplayLog(){
		float w = 400;
		float x = Screen.width-w;
		float y = 0;
		float h = Screen.height;
		
		Rect box = new Rect(x, y, w, h);
		GUI.Box(box," ");
		
		CommandField(x,y,w,h);
		//ScrollButtons(x,y,w,h);
		CommandHistory(x,y,w,h);
	}
	
	string input = "";
	void CommandField(float x, float y, float w, float h){
		Rect fieldLabel = new Rect(x-100,h-25,100,25);
		GUI.Label(fieldLabel,"Type here ==>",l);
		
		Rect fieldBox = new Rect(x, h-25, w, 25);
		input = GUI.TextField(fieldBox,input,100);
		if(EnterKeyPressed()){
			//AddToLog(input);
			AddToLog(CMD.New(input));
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
		if(GUI.Button(dnBox,"Dn") && offset<(commandLog.Count-1)) {offset+=1;}
	}
	
	static List<string> commandLog = new List<string>(); 
	public static void AddToLog(string text){
		commandLog.Add(text);	
	}
	int lineH = 30;
	void CommandHistory(float x,float y,float w,float h){
		float printY = Screen.height-50;
		
		for (int i=(commandLog.Count-1); i>=0; i--){ 
			int j = i+offset;
			//Debug.Log(j);
			if (commandLog.Count > j){
				string c = commandLog[j];
				
				if (printY >= 0){
					Rect cBox = new Rect(x+5,printY,w-10,lineH);
					GUI.Label(cBox,c,l);
					printY-=lineH;
			
				}
			}
		}
	}	
}
