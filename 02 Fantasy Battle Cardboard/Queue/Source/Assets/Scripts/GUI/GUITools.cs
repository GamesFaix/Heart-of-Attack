using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

enum TOOLVIEW {GAME, TOKEN, MODIFY, QUEUE, RANDOM, HIDE}
enum TOKENVIEW {CREATE, KILL, REPLACE}

public class GUITools : MonoBehaviour {
	static GUIStyle s = new GUIStyle();
	
	void Start(){
		s.normal.textColor = Color.white;
		s.fontSize = 20;
	}

	static float lineH=30;
	static float btnW = 70;
	static float x;
	static float y = 0;
	static float w;
	static float h;

	static int viewBtn = 1;
	static TOOLVIEW view;
	static string[] labels = new string[5] {"Game","Token","Modify","Queue","Random"};
	static bool minimize = false;

	void OnGUI(){
		w = Screen.width*0.30f;
		x = Screen.width*0.35f;

		GUI.Box(new Rect(x,y,w,h),"");
	//	GUI.Label(new Rect(x+5,y,btnW,lineH),"TOOLS");

		viewBtn = GUI.Toolbar(new Rect(x,y,w-btnW,lineH),viewBtn,labels);
		view = (TOOLVIEW)viewBtn;
		if (GUI.Button(new Rect(x+w-btnW,y,btnW,lineH),showHide)){
			if (minimize) {Show();}
			else {Hide();}
		}



		if (minimize){
			h = lineH;

		}
		else{
			h = Screen.height;
			switch(view){
				case TOOLVIEW.GAME:   Game();   break;
				case TOOLVIEW.TOKEN:  Token();  break;
				case TOOLVIEW.MODIFY: Modify(); break;
				case TOOLVIEW.QUEUE:  Queue();  break;
				case TOOLVIEW.RANDOM: Random(); break;
				default: break;
			}
		}
	}

	static string showHide = "Show";
	public static void Hide(){
		showHide = "Show";
		minimize = true;
	}
	public static void Show(){
		showHide = "Hide";
		minimize = false;
		GUIQueue.Shrink();
	}


	void Game(){
		Rect box = new Rect(x,y+lineH+5,btnW,lineH);

		if (GUI.Button(box,"New")){gameStart = !gameStart;};
		if (gameStart) {GameStart(x,box.y+lineH);}

		box.x+=btnW;
		if (GUI.Button(box,"Reset")){CMD.New("RESET");}
	}

	bool gameStart = false;
	void GameStart(float drawX, float drawY){
		GUI.Label(new Rect(drawX+100, drawY+5,w,lineH),"Select Attack Kings:",s);

		drawY+=lineH;
		float selectH;
		KingSelect(drawX, drawY, out selectH);

		drawY+=selectH+5;
		drawX+=5;
		for (int i=0; i<8; i++){
			if (kings[i]){

				GUI.Label(new Rect(drawX,drawY,w,lineH),UnitFactory.CodeNames[UnitFactory.kings[i]]);
				drawY+=lineH;

			}
		}
		drawX-=5;
		if (GUI.Button(new Rect(drawX,drawY,w,lineH), "Start Game")){
			string command = "start ";
			for (int i=0; i<8; i++){
				if (kings[i]) {
					command += UnitFactory.kings[i]+" ";
					kings[i] = false;
				}
			}
			CMD.New(command);
			view = TOOLVIEW.HIDE;
		}
	}

	static int tokenBtn = 0;
	static TOKENVIEW tokenView = TOKENVIEW.CREATE;
	static string[] tokenLabels = new string[3] {"Create", "Kill", "Replace"};
	static string printUnit = "";
	static string printInst = "";

	void Token(){
		Rect box = new Rect(x,y+lineH+5,w,lineH);
		tokenBtn = GUI.Toolbar(box,tokenBtn,tokenLabels);
		tokenView = (TOKENVIEW)tokenBtn;

		float selectH;
		switch(tokenView){
			case TOKENVIEW.CREATE: 	
				selectedInstance = "";
				printInst = "";
				box.y+=lineH+5;
				GUI.Label(new Rect(x+((w-btnW)/2), box.y, btnW, lineH),"Create:",s);
				UnitSelect(x,box.y+lineH, out selectH); 
				box.y += selectH+5;
				printUnit = UnitFactory.CodeNames[selectedUnit];
				if (GUI.Button(box,"Create "+printUnit)){
					CMD.New("CREATE "+selectedUnit);
					selectedUnit = "";
				}

				break;
			case TOKENVIEW.KILL:   
				selectedUnit = "";
				printUnit = "";
				box.y+=lineH+5;
				GUI.Label(new Rect(x+((w-btnW)/2), box.y, btnW, lineH),"Kill:",s);
				InstanceSelect(x,box.y+lineH, out selectH);
				box.y += selectH+5;
				if (selectedInstance != "") {printInst = TurnQueue.FindUnit(selectedInstance).fullName;}
				if (GUI.Button(box,"Kill "+printInst)){
					CMD.New("KILL "+selectedInstance);
					selectedInstance = "";
				}

				break;

			case TOKENVIEW.REPLACE: 
				box.y+=lineH+5;
				GUI.Label(new Rect(x+((w-btnW)/2), box.y, btnW, lineH),"Replace:",s);
				InstanceSelect(x,box.y+lineH, out selectH);  
				box.y += selectH+5;
				GUI.Label(new Rect(x+((w-btnW)/2), box.y, btnW, lineH),"with:",s);
				UnitSelect(x,box.y+lineH, out selectH); 
				box.y += selectH+5;
				printUnit = UnitFactory.CodeNames[selectedUnit];
				if (selectedInstance != "") {printInst = TurnQueue.FindUnit(selectedInstance).fullName;}
				if (GUI.Button(box,"Replace "+printInst+" with "+printUnit)){
					CMD.New("REPLACE "+selectedInstance+" "+selectedUnit);
					selectedUnit = "";
					selectedInstance = "";
				}

				break;
			default: 
				break;
		}


	}

	int modifyBtn = -1;
	string[] modifyLabels = new string[8] {"HP", "MHP", "DEF", "COR", "IN", "STUN", "AP", "FP"};
	int signBtn = -1;
	string[] signLabels = new string[3] {"=","+","-"};
	string magString="";
	int magnitude= -1;
	void Modify(){
		float drawY = y+lineH;
		GUI.Label(new Rect(x,drawY,w,lineH),"Select property:");

		drawY+=lineH;
		modifyBtn = GUI.SelectionGrid(new Rect(x,drawY,w,lineH), modifyBtn, modifyLabels, 8);

		drawY+=lineH+5;

		GUI.Label(new Rect(x,drawY,w,lineH),"Select change:");
		signBtn = GUI.Toolbar(new Rect(x+100,drawY,100,lineH),signBtn,signLabels);
		magString = GUI.TextField (new Rect(x+210, drawY, 30, lineH), magString, 3);
		if (magString != ""){Int32.TryParse(magString, out magnitude);}

		drawY+=lineH+5;

		GUI.Label(new Rect(x,drawY,w,lineH),"Select unit:");
		drawY+=lineH;
		float selectH;
		InstanceSelect(x,drawY, out selectH);

		drawY += selectH+5;
		if (selectedInstance != "") {printInst = TurnQueue.FindUnit(selectedInstance).fullName;}
		string command = printInst+" ";
		if (modifyBtn>=0){command+=modifyLabels[modifyBtn]+" ";}
		if (signBtn>=0)	{command+=signLabels[signBtn]+" ";}
		if (magnitude >=0) {command+=""+magnitude;}
		if (GUI.Button(new Rect(x,drawY,w,lineH),command)){
			CMD.New(command);
			selectedInstance = "";
			signBtn = -1;
			modifyBtn = -1;
			printInst = "";
			magnitude = -1;
			magString = "";
			command = "";
		}



	}
	bool viewShift = false;
	string[] shiftLabels = new string[2] {"Up", "Down"};
	int shiftBtn = 0;
	void Queue(){
		Rect box = new Rect(x,y+lineH+5,btnW,lineH);
		
		if (GUI.Button(box,"Advance")){CMD.New("+");};
		
		box.x+=btnW;
		if (GUI.Button(box,"Shuffle")){CMD.New("SHUFFLE");}
		
		box.x+=btnW;
		if (GUI.Button(box,"Shift")){viewShift = !viewShift;}
		if (viewShift){
			box.x = x;
			box.y+=lineH+5;
			box.width=box.width*2;
			shiftBtn = GUI.Toolbar(box,shiftBtn,shiftLabels);
			box.x+=box.width+5;
			box.width=30;
			magString = GUI.TextField (box, magString, 2);
			if (magString != ""){Int32.TryParse(magString, out magnitude);}
			box.y+=lineH+5;
			GUI.Label(new Rect(x,box.y,w,lineH),"Select unit:");
			box.y+=lineH;
			float selectH;
			InstanceSelect(x,box.y, out selectH);
			box.y+=selectH;
			box.x=x;
			box.width = w;
			if (selectedInstance != "") {printInst = TurnQueue.FindUnit(selectedInstance).fullName;}
			string command = "Shift "+printInst+" ";
			command+=shiftLabels[shiftBtn]+" ";
			if (magnitude >=0) {command+=""+magnitude;}
			if (GUI.Button(box,command)){
				CMD.New(command);
				selectedInstance="";


				magnitude = -1;
				magString = "";
				command = "";
			}



		}



	}
	void Random(){


	}

	string selectedUnit ="";
	void UnitSelect(float drawX, float drawY, out float height){
		height = 0;
		float buttonW = w/8;
		Rect box = new Rect(drawX,drawY,buttonW,lineH);
		for (int i=1; i<=8; i++){
			string[] faction = UnitFactory.Faction(i);
			for (int j=0; j<faction.Length; j++){
				if (GUI.Button(box,faction[j])){selectedUnit = faction[j];};
				box.y+=lineH;
				if (box.y-drawY > height){height = box.y-drawY;}

			}
			box.y = drawY;
			box.x += buttonW;
		}
		height+=lineH;
	}

	string selectedInstance ="";
	void InstanceSelect(float drawX, float drawY, out float height){
		height = 0;
		int pCount = Roster.Count();
		int[] players = Roster.Active();

		float buttonW = w/pCount;
		Rect box = new Rect(drawX,drawY,buttonW,lineH);

		for (int i=0; i<pCount; i++){
			List<Unit> team = Roster.OwnedUnits(players[i]);
			foreach (Unit u in team){
				if (GUI.Button(box,u.code+" "+u.instance)){selectedInstance = u.fullName;};
				box.y+=lineH;
				if (box.y-drawY > height){height = box.y-drawY;}
			}
			box.y = drawY;
			box.x += buttonW;

		}
		height += lineH;
	}

	bool[] kings = new bool[8];

	void KingSelect(float drawX, float drawY, out float height){
		height = 0;
		float buttonW = w/4;
		Rect box = new Rect(drawX,drawY,buttonW,lineH);

		for (int i=0; i<4; i++){
			if (GUI.Button(box,UnitFactory.kings[i])){kings[i] = !kings[i];};
			box.x+=buttonW;
		}
		box.x=drawX;
		box.y+=lineH;
		for (int i=4; i<8; i++){
			if (GUI.Button(box,UnitFactory.kings[i])){kings[i] = !kings[i];};
			box.x+=buttonW;
		}
		height+=lineH*2;
	}








}
