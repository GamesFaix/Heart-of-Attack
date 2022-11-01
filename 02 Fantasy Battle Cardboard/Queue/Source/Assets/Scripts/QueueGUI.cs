using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QueueGUI : MonoBehaviour {
	public GUIStyle style;
	
	Unit inspectedUnit;
	string nameField = "(Enter name)";
	string initField = "(Init.)";

	void OnGUI(){
		int x = 0;
		int y = 0;
		int w = 250;
		int h = 60;
		
		nameField = GUI.TextField(new Rect(x,y,w,h), nameField, style);
		y+=h;
		initField = GUI.TextField(new Rect(x,y,w,h), initField, style);
		y+=h;
		
		
		if (GUI.Button(new Rect(x,y,w,h), "Add unit", style)){
			int i = int.Parse(initField);
			UnitFactory.Add(nameField);
		}
		y+=h;
		
		if (GUI.Button(new Rect(x,y,w,h), "Edit unit", style)){
			int i = int.Parse(initField);
			UnitFactory.Edit(inspectedUnit, nameField, i);
		}
		y+=h;
		
		
		if (GUI.Button(new Rect(x,y,w,h), "Delete unit", style)){
			if(inspectedUnit != default(Unit)) {UnitFactory.Delete(inspectedUnit.fullName);}
		}
		y+=h;
		
		
		if (GUI.Button(new Rect(x,y,w,h), "Move up", style)){
			TurnQueue.MoveUp(inspectedUnit, 1);	
		}
		y+=h;
		
		if (GUI.Button(new Rect(x,y,w,h), "Move down", style)){
			TurnQueue.MoveDown(inspectedUnit, 1);	
		}
		
		y+=2*h;
		if (GUI.Button(new Rect(x,y,w,h), "Advance turn", style)){
			TurnQueue.Advance();
		}

		y+=4*h;
		if (GUI.Button(new Rect(x,y,w,h), "New queue", style)){
			TurnQueue.Reset();
		}
		
		
		
		
		x+=w;
		y = 0;
		w = 500;
		
		byte j = 0;
		
		foreach (Unit unit in TurnQueue.units){
			if((j%12 == 0) && j != 0){
				y=0;
				x+=w;
			}
			string displayString = unit.fullName+" (IN:"+unit.init+")";
			if (unit.skipped == true){displayString += " Skipped!";}
			
			if (GUI.Button(new Rect(x,y,w,h), displayString, style)){
					inspectedUnit = unit;
					nameField = unit.fullName;
					initField = ""+unit.init;
			}	
			y+=h;	
			j++;
		}
		
		DisplayLog();
		
		
		
		
	}

	List<string> commandLog = new List<string>(); 
	public void AddToLog(string text){
		commandLog.Add(text);	
	}
	
	
	byte logOffset = 0; 		
	string input = "";
	
	void DisplayLog(){

		float xPos = 335;
		float width = Screen.width-xPos-90;
		Rect box = new Rect(xPos, Screen.height-85, width, 80);
		GUI.Box(box," ");
		Rect fieldBox = new Rect(xPos, Screen.height-25, width, 20);
			
		input = GUI.TextField(fieldBox,input,100);
		if(Event.current.type==EventType.KeyDown&&Event.current.character=='\n'){
			AddToLog(input);
			AddToLog(CMD.New(input));
			input="";
		}
		
		//offset buttons created - cannot not offset past 0 or mlog size
		int btnSize = 30;
		Rect upBox = new Rect(xPos+width-btnSize, Screen.height-85, btnSize, btnSize);
		Rect dnBox = new Rect(xPos+width-btnSize, Screen.height-55, btnSize, btnSize);
		if(GUI.Button(upBox,"Up") && logOffset<commandLog.Count-3){logOffset+=1;}
		if(GUI.Button(dnBox,"Dn") && logOffset>0)			{logOffset-=1;}

		string c1; string c2; string c3;
		//retrieve last 3 messages (factoring in offset)
		if (commandLog.Count>0){c1 = commandLog[commandLog.Count-1-(logOffset)];}	else {c1="";}
		if (commandLog.Count>1){c2 = commandLog[commandLog.Count-1-(logOffset+1)];} else {c2="";}
		if (commandLog.Count>2){c3 = commandLog[commandLog.Count-1-(logOffset+2)];} else {c3="";}

		//print 3 messages
		Rect c1Box = new Rect(xPos+5, Screen.height-85, width-10, 20);
		Rect c2Box = new Rect(xPos+5, Screen.height-65, width-10, 20);
		Rect c3Box = new Rect(xPos+5, Screen.height-45, width-10, 20);
		
		GUI.Label(c1Box,c3);
		GUI.Label(c2Box,c2);
		GUI.Label(c3Box,c1);
	}
	
	
	public void Reset(){
		inspectedUnit = default(Unit);
		nameField = "(Enter name)";
		initField = "(Init.)";
	}
}
