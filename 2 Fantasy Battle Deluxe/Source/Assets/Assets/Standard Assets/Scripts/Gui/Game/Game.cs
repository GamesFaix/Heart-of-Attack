/* In-game GUI screen. */

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FBI.Gui;

namespace FBI.Gui.Game{
	public static class Game {
		
		static GameObject map;
		static Texture2D bg;
		
		//array for when multiple targets available (drop down buttons)
		//List<GameObject> legalUnits = new List<GameObject>();
		
		static Game() {
			bg = Resources.Load("gui/inspector_bg") as Texture2D;
		}
		public static void Draw () {
			/*
			ScaleGUI();
			Inspector.Draw();
	
			//end turn button
			if(GUI.Button(new Rect(10,Screen.height-40,180,30),"End Turn")){EndTurn();}
			//menu
			if(GUI.Button(new Rect(Screen.width-85,Screen.height-45,80,40),"Menu")){showMenu=!showMenu;}
			//
			GameMenu();
			MessageLog();	
			Info.PlayerDisplay();
			Info.QueueDisplay();
			Info.ClockDisplay();
			Error();
			QuickKeys.Keys();
			Tracking.Draw();
			//TargetSelect();
			//Portal();	
			*/
		}
		/*
		//display
		public static short thumbScale;
		static void ScaleGUI(){//scales gui to screensize
			//gui scaling
			thumbScale=180;
			if (Screen.height<600){thumbScale=(short)Mathf.Round((float)(Screen.height*0.25));}
			if (thumbScale<80){thumbScale=80;}
		
			Inspector.actBtnHeight = (byte)Mathf.RoundToInt((float)(Screen.height*0.04));
			if (Inspector.actBtnHeight<20){Inspector.actBtnHeight=20;}
			if (Inspector.actBtnHeight>30){Inspector.actBtnHeight=30;}
				
			//viewport scaling	
			float xBord = 180; 
		
			float camX = xBord/Screen.width;
		
			Camera.main.rect = new Rect(camX,0,1-camX,1);
		}
				//
		//buttons
		public static IEnumerator EndTurn(){
			if (Inspector.iToken==GameQueue.turnQueue.First()){
				yield return GameQueue.EndTurn();
				Inspector.Inspect(GameQueue.turnQueue.First());
				CameraRig.CamFocus(GameQueue.turnQueue.First());
			}
			else {error="It's not your turn.";}
		}
	
		
				//
				//
		//user input
		public static bool MouseOnGUI(){//returns true if mouse is over GUI, false if mouse over map
			if (Input.mousePosition.x<180 || Input.mousePosition.y<90){
				 return true;}
			else {return false;}
		}
	
		
				//
				//
		//info display
		public static string error; //error messages to display
		static float errorTime = 0;
		public static void Error(){//displays error messages
			//check time error happened
			if (error!=null && errorTime==0){
				errorTime=Time.time;
			}
			//after 2sec, reset
			if (errorTime!=0 && Time.time-errorTime>2){
				errorTime=0;
				error=null;
			}
			if (errorTime>0){
				GUI.Box(new Rect(200,0,250,25),error);
			}
		}
				//
		public static bool showMlog = true;
		public static List<string> mlog = new List<string>(); 
		static byte mlogOffset = 0; 		
		public static bool msgEnable = false;
		static string playerMsg;
		
		static void MessageLog(){//displays message log
		
			short xPos = 335;
			short width = (short)Mathf.RoundToInt((float)(Screen.width-xPos-90));
			if (showMlog==true){
				//draw message box
				GUI.Box(new Rect(xPos,Screen.height-85,width,80)," ");
		
				if (msgEnable==true){
					//player message input via TextField
					playerMsg = GUI.TextField(new Rect(xPos,Screen.height-25,width,20),playerMsg,100);
					//enter key submits message, clears playerMsg String
					if(Event.current.type==EventType.KeyDown&&Event.current.character=='\n'){
						mlog.Add(playerMsg);
						playerMsg="";
					}
				}
				if (msgEnable==false){
					GUI.Label(new Rect(xPos+5,Screen.height-25,width,20),"--- (Press TAB to enter message.) ---");
				}
			
				//offset buttons created - cannot not offset past 0 or mlog size
				if(GUI.Button(new Rect(xPos+width-30,Screen.height-85,30,30),"Up") && mlogOffset<mlog.Count-3){mlogOffset+=1;}
				if(GUI.Button(new Rect(xPos+width-30,Screen.height-55,30,30),"Dn") && mlogOffset>0)			{mlogOffset-=1;}
		
				string m1; 
				string m2; 
				string m3;
				//retrieve last 3 messages (factoring in offset)
				if (mlog.Count>0){m1 = mlog[mlog.Count-1-(mlogOffset)];}	else {m1="";}
				if (mlog.Count>1){m2 = mlog[mlog.Count-1-(mlogOffset+1)];} else {m2="";}
				if (mlog.Count>2){m3 = mlog[mlog.Count-1-(mlogOffset+2)];} else {m3="";}
		
				//print 3 messages
				GUI.Label(new Rect(xPos+5,Screen.height-85,width-10,20),m3);
				GUI.Label(new Rect(xPos+5,Screen.height-65,width-10,20),m2);
				GUI.Label(new Rect(xPos+5,Screen.height-45,width-10,20),m1);
			}
		}
		
				//
		//help
		public static bool showMenu = false;
		public static bool showControls = false;
		static Vector2 controlsScrollview = Vector2.zero;
		static void GameMenu(){
			//position
			short xPos = (short)Mathf.RoundToInt(Screen.width/2-250);
			short yPos = (short)Mathf.RoundToInt(Screen.height/2-150);
			short width =500; 
			short height =300;
			//background box
			if (showMenu==true){
				GUI.DrawTexture(new Rect(xPos+3,yPos+3,width-6,height-6),bg,ScaleMode.StretchToFill,true,0);
				GUI.Box(new Rect(xPos,yPos,width,height)," ");
				//
				byte buttons = 6;
				byte btnHeight = (byte)Mathf.RoundToInt((height-20)/buttons);
				if (showControls==false){
					if(GUI.Button(new Rect(xPos+10,yPos+10,width-20,btnHeight),"FIGHTING RESUME")){showMenu=false;}
					if(GUI.Button(new Rect(xPos+10,yPos+10+btnHeight,width-20,btnHeight),"Fullscreen On/Off")){Screen.fullScreen=!Screen.fullScreen;}
					if(GUI.Button(new Rect(xPos+10,yPos+10+btnHeight*2,width-20,btnHeight),"Controls")){showControls=true;}	
					//if(GUI.Button(new Rect(xPos+10,yPos+10+btnHeight*3,width-20,btnHeight),"Help")){
					//	G.view="help";
						//G.help.previousview="game";
					//}		
					if(GUI.Button(new Rect(xPos+10,yPos+10+btnHeight*4,width-20,btnHeight),"Return to Main Menu")){GameReset.Reset();}
					if(GUI.Button(new Rect(xPos+10,yPos+10+btnHeight*5,width-20,btnHeight),"Exit Program")){Application.Quit();}
				}
				if (showControls==true){//controls list
					controlsScrollview = GUI.BeginScrollView(new Rect(xPos+10,yPos+10,width-20,height-80), //screen position
																			controlsScrollview, 
																			new Rect(xPos+10,yPos+10,width-40,height),false,true); //content area
			
					GUI.Label(new Rect(xPos+10,yPos+10,100,height),
						"Left click  \nRight click  \nRight drag  \nMiddle click  \n\n1-9  \nEnter  \n\nP  \nQ  \nW/S  \n\nE  \nD  \nR  \n\nShift+Z/X/C  \nZ/X/C  \n\nM  \nTab  \nEscape");
					GUI.Label(new Rect(xPos+150,yPos+10,300,height),
						"Select target  \nInspect token  \nPan camera  \nRotate camera  \n\nPerform action (If your turn)  \nEnd turn (If your turn)  \n\nShow/hide player thumbnails  \nShow/hide Queue  \nQueue scroll up/down  \n\nFocus camera on current unit  \nInspect current unit  \nFocus camera on inspected token  \n\nWatch token 1/2/3  \nInspect & focus on watched token 1/2/3  \n\nShow/hide message log  \nEnter message  \nShow/hide menu");
					GUI.EndScrollView();
			
					if (GUI.Button(new Rect(xPos+10,yPos+height-60,width-20,50),"Back")){
						showControls=false;
					}
				}
			}
	
		}
	*/
	}
	
}