#pragma strict

//enums
var eActionName: byte=0;
var eFunc: byte=1;
var eDesc: byte=2;

var eAp: byte=0;
var eFp: byte=1;
var emRng: byte=2;
var eRng: byte=3;
var eMag: byte=4;
var eDec: byte=5;
var eRad: byte=6;
var eCrz: byte=7;
var eTar: byte=8;
var eDmgType: byte=9;
//

//icons
var iHp: Texture2D;
var iDef: Texture2D;
var iIn: Texture2D;

var iAct: Texture[]=new Texture[8];
var iMob: Texture[]=new Texture[5];
var iTar: Texture[]=new Texture[5];
var iDmgType: Texture[]=new Texture[7];

function LoadIcons():IEnumerator{
	//icons
	iHp = Resources.Load("icons/hp") as Texture2D;
	iDef = Resources.Load("icons/def") as Texture2D;
	iIn = Resources.Load("icons/in") as Texture2D;

	iAct[eAp] = Resources.Load("icons/ap") as Texture2D;
	iAct[eFp] = Resources.Load("icons/fp") as Texture2D;
	iAct[eRng] = Resources.Load("icons/rng") as Texture2D;
	iAct[eMag] = Resources.Load("icons/dmg") as Texture2D;
	iAct[eDec] = Resources.Load("icons/dec") as Texture2D;
	iAct[eRad] = Resources.Load("icons/rad") as Texture2D;
	iAct[eCrz] = Resources.Load("icons/crz") as Texture2D;

	iMob[1] = Resources.Load("icons/gnd") as Texture2D;
	iMob[2] = Resources.Load("icons/trm") as Texture2D;
	iMob[3] = Resources.Load("icons/fly") as Texture2D;
	iMob[4] = Resources.Load("icons/gas") as Texture2D;

	iTar[1] = Resources.Load("icons/serp") as Texture2D;
	iTar[2] = Resources.Load("icons/lin") as Texture2D;
	iTar[3] = Resources.Load("icons/arc") as Texture2D;
	// iAct[eRad]ial = Resources.Load("icons/radial") as Texture2D;
	
	iDmgType[2] = Resources.Load("icons/exp") as Texture2D;
	iDmgType[3] = Resources.Load("icons/fir") as Texture2D;
	iDmgType[4] = Resources.Load("icons/psn") as Texture2D;
	iDmgType[5] = Resources.Load("icons/elc") as Texture2D;
	iDmgType[6] = Resources.Load("icons/lsr") as Texture2D;
	//	

	yield;
}
//retrieve GUI background & map object
var gui_frame: Texture2D;
var leftbar: Texture2D;
	
var gameindexprefab: GameObject;
var gameindex: GameIndex;
var map: GameObject;
var queue: QueueScript;
var actionCoord: ActionCoordinator;
var gui_master: GUI_Master;
var gui_help: GUI_Help;
var targeting: Targeting;
		
var viewedobject: GameObject;
var currentunit: GameObject;
var viewedact1to9: byte;
	
//vars to make sure action isnt used twice
var actUsed: boolean[]=new boolean[10];
for (var i: byte=0; i<=9; i+=1){actUsed[i]=false;}
		
var legalunits=new List.<GameObject>();
var portalBtn: boolean=false;
var gate: PortalScript;

var camCenter: GameObject;

function Awake(){//finds other GameObjects and scripts
	//find important scripts
	gameindexprefab=GameObject.Find("GameIndexPrefab");
	gameindex=gameindexprefab.GetComponent(GameIndex);
	queue=gameindex.GetComponent(QueueScript);
	gui_master=gameObject.GetComponent(GUI_Master);
	gui_help=gameObject.GetComponent(GUI_Help);
	leftbar=Resources.Load("gui/panel_background") as Texture2D;
	camCenter=GameObject.Find("camCenter");
}	

function Start(){
	yield LoadIcons();
}

function MouseOnGUI(): boolean{

	if (Input.mousePosition.x<180 || Input.mousePosition.y<90){
		 return true;}
	else {return false;}

}

function OnGUI () {
if (gui_master.view=="game"){

	//scale GUI to screen size
	ScaleGUI();

	//draw left bar
	GUI.DrawTexture(Rect(0,0,180,Screen.height),leftbar,ScaleMode.StretchToFill,true,0);
	
	if (viewedobject){
		var viewedstats: ObjectStats = viewedobject.GetComponent(ObjectStats);
			
		//obstacles
		if (!viewedstats.owner){
			//draw name button
			if(GUI.Button(Rect(10,10,160,20),viewedstats.objname)){Focus(viewedobject);}
			//draw thumb
			GUI.DrawTexture(Rect((180-thumbscale)/2,35,thumbscale,thumbscale),viewedstats.thumb,ScaleMode.StretchToFill,true,0);
			//draw statbox
			GUI.Box(Rect(10,thumbscale+40,160,40),viewedstats.obtype);
		}
		//units
		if (viewedstats.owner){//if viewedobject is a unit
			//draw leftbar to match viewed unit ownership
			GUI.DrawTexture(Rect(5,5,170,Screen.height-10),gameindex.colors[gameindex.player_colors[viewedstats.owner]],ScaleMode.StretchToFill,true,0);
			//draw name button
			if(GUI.Button(Rect(10,10,160,20),viewedstats.objname)){
				Camera.main.GetComponent(CamOrbit).target=viewedobject.transform;
				camCenter.transform.position=viewedobject.transform.position;
			}
			//draw thumb
			GUI.DrawTexture(Rect((180-thumbscale)/2,35,thumbscale,thumbscale),viewedstats.thumb,ScaleMode.StretchToFill,true,0);
			//draw statbar
			GUI.Box(Rect(10,thumbscale+40,160,55),"");
			GUI.Label(Rect(15,thumbscale+40,100,20),viewedstats.composition);
			if (viewedstats.mob){
				GUI.DrawTexture(Rect(95,thumbscale+40,20,20),iMob[viewedstats.mob],ScaleMode.StretchToFill,true,0);
			}
			if (viewedstats.hp){
				GUI.DrawTexture(Rect(15,thumbscale+55,20,20),iHp,ScaleMode.StretchToFill,true,0);
				GUI.Label(Rect(35,thumbscale+55,100,20),viewedstats.hp+"/"+viewedstats.mhp);
			}
			if (viewedstats.def){
				GUI.DrawTexture(Rect(95,thumbscale+55,20,20),iDef,ScaleMode.StretchToFill,true,0);
				GUI.Label(Rect(115,thumbscale+55,25,20),(viewedstats.def).ToString());
			}
			if (viewedstats.init){
				GUI.DrawTexture(Rect(15,thumbscale+70,20,20),iIn,ScaleMode.StretchToFill,true,0);
				GUI.Label(Rect(35,thumbscale+70,30,20),(viewedstats.init).ToString());
			}
			
			GUI.DrawTexture(Rect(70,thumbscale+70,20,20),iAct[eAp],ScaleMode.StretchToFill,true,0);
			GUI.Label(Rect(90,thumbscale+70,20,20),(viewedstats.ap).ToString());
			
			GUI.DrawTexture(Rect(115,thumbscale+70,20,20),iAct[eFp],ScaleMode.StretchToFill,true,0);
			GUI.Label(Rect(135,thumbscale+70,20,20),(viewedstats.fp).ToString());
			//draw action buttons
			ActionButtons(viewedobject);
	
			GUI.Box(Rect(10,thumbscale+actbtn_height*9+115,160,Screen.height-(thumbscale+actbtn_height*9+160))," ");
			if (viewedact1to9){ActionInfo(viewedobject,viewedact1to9);}
		}
	}
	
	//error box
	Error();
	QuickKeys();
	DisplayWatchedUnits();
	//draw unit selection, if multiple legal units
	if (targeting.legalunits){TargetSelect();}
	//display queue
	if(queue.queuelist){QueueDisplay();}

	//if (portalBtn==true){Portal();}	
	
	//end turn button
	if(GUI.Button(Rect(10,Screen.height-40,160,30),"End Turn")){EndTurn();}
	//menu button
	if(GUI.Button(Rect(Screen.width-85,Screen.height-45,80,40),"Menu")){showMenu=!showMenu;}
	//show game menu
	if (showMenu==true){GameMenu();}
	//draw message log	
	if (showMlog==true){MessageLog();}
	
}
}
//commands

function ActionButtons(object: GameObject){//displays action buttons/activates ActionPerform	
	var stats: ObjectStats = object.GetComponent(ObjectStats);
	//Debug.Log(stats);
	//Debug.Log(stats.actText[9,0]);
	var act: byte;
	for (act=1; act<=9; act++){
		var func: String = stats.actText[act,eFunc];
		var actionName: String = stats.actText[act,eActionName];
		var ap: byte = stats.actNums[act,eAp];
		var fp: byte = stats.actNums[act,eFp];
		var used: boolean = actUsed[act];
			
		if (func!=null){
			//Debug.Log(actionName);
			if (GUI.Button(Rect(10,thumbscale+110+((act-1)*actbtn_height),160,actbtn_height),actionName)){
				if (Event.current.button==0){
					actionCoord.PerformAction(object,act);
				}
				if (Event.current.button==1){
					if (viewedact1to9==act){viewedact1to9=0;}
					else {viewedact1to9=act;}
				}
			}
		}
	}
}

function EndTurn(){
	if (viewedobject==currentunit){
		yield queue.Advance();
		for(var i: byte=0; i<10; i+=1){
			actUsed[i]=false;
		}
		View(currentunit);
	}
	else {error="It's not your turn.";}
}
function QuickKeys(){
	var e: Event = Event.current;
	if (e.isKey && e.type.KeyUp){
		if(showMenu==false){
			if (e.character=="\t"){msgEnable=!msgEnable;} //enable/disable player msg
			if (msgEnable==false){
				var act: byte;
				var stats: ObjectStats = viewedobject.GetComponent(ObjectStats);
				for (act=1; act<=9; act+=1){
					if (e.character==act){
						if (stats.actText[act,eFunc]!=null){
							actionCoord.PerformAction(viewedobject,act);
						}
						else {error="No action assigned.";}
					}
				}
				act=1;
				
				if (e.character=="e"){Focus(currentunit);}
				if (e.character=="d"){View(currentunit);}
				if (e.character=="r"){Focus(viewedobject);}
				if (e.character=="q"){showQ=!showQ;}
				if (e.character=="w"){//queue up
					if (qOffset>0){qOffset-=1;}
				}
				if (e.character=="s"){//queue down
					if (qOffset<queue.queuelist.Count-qIndicies){qOffset+=1;}
				}
				if (e.character=="\n"){EndTurn();}
				if (e.character=="m"){showMlog=!showMlog;}

				if (Input.GetKeyDown ("z") && Input.GetKey ("left shift")){//set watched1
					watched[0]=viewedobject;
				}
				if (Input.GetKeyDown ("z") && !Input.GetKey("left shift")){//view watched1
					if (watched[0]){
						viewedobject=watched[0];
						Camera.main.GetComponent(CamOrbit).target=watched[0].transform;
						camCenter.transform.position=watched[0].transform.position;
					}
				}
			
				if (Input.GetKeyDown ("x") && Input.GetKey ("left shift")){//set watched2
					watched[1]=viewedobject;
				}
				if (Input.GetKeyDown ("x") && !Input.GetKey("left shift")){//view watched2
					if (watched[1]){
						viewedobject=watched[1];
						Camera.main.GetComponent(CamOrbit).target=watched[1].transform;
						camCenter.transform.position=watched[1].transform.position;
					}
				}
			
				if (Input.GetKeyDown ("c") && Input.GetKey ("left shift")){//set watched3
					watched[2]=viewedobject;
				}
				if (Input.GetKeyDown ("c") && !Input.GetKey("left shift")){//view watched3
					if (watched[2]){
						viewedobject=watched[2];
						Camera.main.GetComponent(CamOrbit).target=watched[2].transform;
						camCenter.transform.position=watched[2].transform.position;
					}
				}
			}
		}
		if (Input.GetKeyUp("escape")){
			showMenu=!showMenu; 
			showControls=false;
		}
	}	
}

var watched: GameObject[] = new GameObject[3];
function DisplayWatchedUnits(){
	var height: short = 60;
	var width: short = 3*height;
	var yPos: short = Mathf.RoundToInt(Screen.height-90-height);
	var xPos: short = Mathf.RoundToInt(Screen.width-width);
	
	if (watched[0] || watched[1] || watched[2]){GUI.Box(Rect(xPos,yPos,width,height)," ");}
	
	var i: byte;
	for (i=0; i<3; i++){
		if (watched[i]){
			var stats: ObjectStats = watched[i].GetComponent(ObjectStats);
			if (stats.owner){
				GUI.DrawTexture(Rect(xPos+3+(height*i),yPos+3,height-6,height-6),gameindex.colors[gameindex.player_colors[stats.owner]],ScaleMode.StretchToFill,true,0);
			}
			if (GUI.Button(Rect(xPos+2+(height*i),yPos+2,height-4,height-4),stats.thumb)){
				if (Input.GetKey("left shift")) {watched[i]=null;}
				else {
					View(watched[i]);
					Focus(watched[i]);
				}
			}
		}	
	}
}

var showMenu: boolean = false;

var showControls: boolean = false;
var controlsScrollview: Vector2 = Vector2.zero;
function GameMenu(){
	//position
	var xPos: short = Mathf.RoundToInt(Screen.width/2-250);
	var yPos: short = Mathf.RoundToInt(Screen.height/2-150);
	var width: short =500; 
	var height: short =300;
	//background box
	GUI.DrawTexture(Rect(xPos+3,yPos+3,width-6,height-6),leftbar,ScaleMode.StretchToFill,true,0);
	GUI.Box(Rect(xPos,yPos,width,height)," ");
	//
	var buttons: byte = 6;
	var btnHeight: byte=Mathf.RoundToInt((height-20)/buttons);
	if (showControls==false){
		if(GUI.Button(Rect(xPos+10,yPos+10,width-20,btnHeight),"FIGHTING RESUME")){showMenu=false;}
		if(GUI.Button(Rect(xPos+10,yPos+10+btnHeight,width-20,btnHeight),"Screen Fills")){
			Screen.fullScreen=!Screen.fullScreen;
		}
		if(GUI.Button(Rect(xPos+10,yPos+10+btnHeight*2,width-20,btnHeight),"How does I buttons?")){showControls=true;}	
		if(GUI.Button(Rect(xPos+10,yPos+10+btnHeight*3,width-20,btnHeight),"How does fighting?")){
			gui_master.view="help";
			gui_help.previousview="game";
		}	
		if(GUI.Button(Rect(xPos+10,yPos+10+btnHeight*4,width-20,btnHeight),"Fighting End")){Application.LoadLevel(0);}
		if(GUI.Button(Rect(xPos+10,yPos+10+btnHeight*5,width-20,btnHeight),"I Am Quit!")){Application.Quit();}
	}
	if (showControls==true){//controls list
		controlsScrollview = GUI.BeginScrollView(Rect(xPos+10,yPos+10,width-20,height-80), //screen position
																controlsScrollview, 
																Rect(xPos+10,yPos+10,width-40,height),false,true); //content area
	
			GUI.Label(Rect(xPos+10,yPos+10,100,height),
				"Left click \nRight click \nRight drag \n\n1-9 \nEnter \n\nQ \nW/S \n\nE \nD \nR \n\nShift+Z/X/C \nZ/X/C \n\nM\nTab\nEscape");
			GUI.Label(Rect(xPos+150,yPos+10,300,height),
				"Select \nView Object \nPan Camera \n\nPerform Action (Current unit only) \nEnd turn (Current unit only) \n\nShow/Hide Queue \nQueue Scroll Up/Down \n\nFocus on Current Unit \nView Current Unit \nFocus on Viewed Object \n\nAssign Watched Object 1/2/3 \nView & Focus on Watched Object 1/2/3 \n\nShow/hide message log\nEnter message\nShow/Hide Game Menu");
		GUI.EndScrollView();
	
		if (GUI.Button(Rect(xPos+10,yPos+height-60,width-20,50),"Back")){
			showControls=false;
		}
	}
}


var actionScrollView: Vector2 = Vector2.zero;

function ActionInfo(object: GameObject, act1to9: byte){//displays action info
	var stats: ObjectStats = object.GetComponent(ObjectStats);
	
	var name: String = stats.actText[act1to9,eActionName];
	var desc: String = stats.actText[act1to9,eDesc];
	
	var ap: byte = stats.actNums[act1to9,eAp];
	var fp: byte = stats.actNums[act1to9,eFp];
	var used: boolean = actUsed[act1to9];
	
	var mag: float = stats.actNums[act1to9,eMag];
	var rng: float = stats.actNums[act1to9,eRng];
	var dec: float = stats.actNums[act1to9,eDec];
	var rad: float = stats.actNums[act1to9,eRad];
	var crz: float = stats.actNums[act1to9,eCrz];		
	var tar: float = stats.actNums[act1to9,eTar];
	var dmgtype: float = stats.actNums[act1to9,eDmgType];
		
	var yPos=thumbscale+actbtn_height*9+115;
	
	actionScrollView = GUI.BeginScrollView(Rect(15,yPos,155,140), actionScrollView, Rect(15,yPos,138,200),false,true);
		//name
		GUI.Label(Rect(15,yPos,140,20),name);
		yPos+=20;
		//cost
		GUI.DrawTexture(Rect(15,yPos,20,20),iAct[eAp],ScaleMode.StretchToFill,true,0);
		GUI.Label(Rect(35,yPos,20,20),ap.ToString());
		GUI.DrawTexture(Rect(60,yPos,20,20),iAct[eFp],ScaleMode.StretchToFill,true,0);
		GUI.Label(Rect(80,yPos,20,20),fp.ToString());
		if (used==true && viewedobject==currentunit){GUI.Label(Rect(105,yPos,40,20),"USED");}
		yPos+=20;
		//targeting
		if (tar || rng){
			if (tar){GUI.DrawTexture(Rect(15,yPos,20,20),iTar[tar],ScaleMode.StretchToFill,true,0);}
			if (rng){
				GUI.DrawTexture(Rect(40,yPos,20,20),iAct[eRng],ScaleMode.StretchToFill,true,0);
				GUI.Label(Rect(60,yPos,20,20),rng.ToString());
			}
			yPos+=20;
		}
		if (dmgtype){
			GUI.DrawTexture(Rect(15,yPos,20,20),iAct[eMag],ScaleMode.StretchToFill,true,0);
			GUI.Label(Rect(35,yPos,20,20),mag.ToString());
			
			if (dmgtype>1){
				GUI.DrawTexture(Rect(55,yPos,20,20),iDmgType[dmgtype],ScaleMode.StretchToFill,true,0);
				yPos+=20;
				var xPos: short = 15;
				if (dec){
					GUI.DrawTexture(Rect(xPos,yPos,20,20),iAct[eDec],ScaleMode.StretchToFill,true,0);
					GUI.Label(Rect(xPos+20,yPos,20,20),dec.ToString());
					xPos+=45;
				}
				if (rad){
					GUI.DrawTexture(Rect(xPos,yPos,20,20),iAct[eRad],ScaleMode.StretchToFill,true,0);
					GUI.Label(Rect(xPos+20,yPos,20,20),rad.ToString());
					xPos+=45;
				}
				if (dmgtype==2){
					GUI.DrawTexture(Rect(xPos,yPos,20,20),iAct[eCrz],ScaleMode.StretchToFill,true,0);
					GUI.Label(Rect(xPos+20,yPos,20,20),crz.ToString());
					xPos+=45;
				}
			}
			yPos+=20;	
		}
		
		if(desc){GUI.Label(Rect(15,yPos,140,100),desc);}
	GUI.EndScrollView();
}

var error: String; //error messages to display
private var errortime: float=0;
function Error(){//displays error messages
	//check time error happened
	if (error && errortime==0){
		errortime=Time.time;
	}
	//after 2sec, reset
	if (errortime!=0 && Time.time-errortime>2){
		errortime=0;
		error=null;
	}
	if (errortime>0){
		GUI.Box(Rect(180,0,250,25),error);
	}
}

var showMlog: boolean=false;
var mlog = new List.<String>(); 
var mlog_offset: byte = 0; 		
var msgEnable: boolean = false;
var player_msg: String;
function MessageLog(){//displays message log

	var xPos: short = 335;
	var width: short = Mathf.RoundToInt(Screen.width-xPos-90);
	
	//draw message box
	GUI.Box(Rect(xPos,Screen.height-85,width,80)," ");

	if (msgEnable==true){
		//player message input via TextField
		player_msg = GUI.TextField(Rect(xPos,Screen.height-25,width,20),player_msg,100);
		//enter key submits message, clears player_msg String
		if(Event.current.type==EventType.KeyDown&&Event.current.character=='\n'){
			mlog.Add(player_msg);
			player_msg="";
		}
	}
	if (msgEnable==false){
		GUI.Label(Rect(xPos+5,Screen.height-25,width,20),"--- (Press TAB to enter message.) ---");
	}
	
	//offset buttons created - cannot not offset past 0 or mlog size
	if(GUI.Button(Rect(xPos+width-30,Screen.height-85,30,30),"Up") && mlog_offset<mlog.Count-3){mlog_offset+=1;}
	if(GUI.Button(Rect(xPos+width-30,Screen.height-55,30,30),"Dn") && mlog_offset>0)			{mlog_offset-=1;}

	var m1: String; var m2: String; var m3: String;
	//retrieve last 3 messages (factoring in offset)
	if (mlog.Count>0){m1 = mlog[mlog_offset];}	else {m1="";}
	if (mlog.Count>1){m2 = mlog[mlog_offset+1];} else {m2="";}
	if (mlog.Count>2){m3 = mlog[mlog_offset+2];} else {m3="";}

	//print 3 messages
	GUI.Label(Rect(xPos+5,Screen.height-85,width-10,20),m3);
	GUI.Label(Rect(xPos+5,Screen.height-65,width-10,20),m2);
	GUI.Label(Rect(xPos+5,Screen.height-45,width-10,20),m1);
}

var showQ: boolean = false;
var qOffset: short;
var qIndicies: byte = 6;
function QueueDisplay(){//draws currentunit button
	var xPos: short = Screen.width-150;
	var width: short = 150;
	var height: short = 30;
	var yPos: short = 0;
	//current unit
	var currentunitstats: ObjectStats = currentunit.GetComponent(ObjectStats);
	GUI.Box(Rect(xPos,yPos,width,20),"Current turn:");		
	GUI.DrawTexture(Rect(xPos+3,yPos+23,width-6,24),gameindex.colors[gameindex.player_colors[currentunitstats.owner]],ScaleMode.StretchToFill,true,0);
	if(GUI.Button(Rect(xPos,yPos+20,width,height),currentunitstats.objname)){
		View(currentunit);
		Focus(currentunit);
	}
		
	//queue show/hide
	var showHide: String;
	if (showQ==false){showHide="Show Queue";}
	else {showHide="Hide Queue";}
	if (GUI.Button(Rect(xPos,yPos+50,width,20),showHide)){showQ=!showQ;}
	
	//queue list
	if (showQ==true){
		var qHeight: byte=20;
		
		//draw background box
		GUI.Box(Rect(xPos-height,yPos+70,width+height,qHeight*qIndicies)," ");
		
		//offset buttons
		if(GUI.Button(Rect(xPos-height,yPos+70,height,qHeight),"^") && qOffset>0){
			qOffset-=1;}
		if(GUI.Button(Rect(xPos-height,yPos+70+((qIndicies-1)*qHeight),height,qHeight),"v") && qOffset<queue.queuelist.Count-qIndicies){
			qOffset+=1;}
	
		var i: byte;
		var unit: GameObject;
		var unitstats: ObjectStats;
		for (i=0; i<qIndicies; i++){
			if (queue.queuelist.Count>i){
				unit=queue.queuelist[qOffset+i];
				unitstats=unit.GetComponent(ObjectStats);
				
				GUI.DrawTexture(Rect(xPos+2,yPos+70+(qHeight*i)+2,width-4,qHeight-4),gameindex.colors[gameindex.player_colors[unitstats.owner]],ScaleMode.StretchToFill,true,0);
				if (GUI.Button(Rect(xPos,yPos+70+(qHeight*i),width,qHeight),unitstats.objname)){
					View(unit);
					Focus(unit);
				}
			}
		}
	}
}
var thumbscale: short;
var actbtn_height: byte;
function ScaleGUI(){//scales gui to screensize
	//gui scaling
	thumbscale=160;
	if (Screen.height<600){thumbscale=Mathf.RoundToInt(Screen.height*0.25);}
	if (thumbscale<80){thumbscale=80;}

	actbtn_height=Mathf.RoundToInt(Screen.height*0.04);
	if (actbtn_height<20){actbtn_height=20;}
	if (actbtn_height>30){actbtn_height=30;}
		
	//viewport scaling	
	var widthF: float = Screen.width; 
	var xBord: float = 180; 

	var camX = xBord/widthF;

	Camera.main.rect = new Rect(camX,0,1-camX,1);
}

var targetunit: GameObject;
function TargetSelect(){//displays target selection buttons if 2 targets in cell
	if (targeting.legalunits.Count>1){
		GUI.Box(Rect(190,0,100,actbtn_height),"Choose Target");
		if (targeting.legalunits.Count>2){
			if(GUI.Button(Rect(190,actbtn_height+200,100,100),targeting.legalunits[2].GetComponent(ObjectStats).thumb)){
				targeting.targetobject=targeting.legalunits[2];
				targeting.legalunits.Clear();
			}	
		}
		if(GUI.Button(Rect(190,actbtn_height+100,100,100),targeting.legalunits[1].GetComponent(ObjectStats).thumb)){
			targeting.targetobject=targeting.legalunits[1];
			targeting.legalunits.Clear();
		}
		if(GUI.Button(Rect(190,actbtn_height,100,100),targeting.legalunits[0].GetComponent(ObjectStats).thumb)){
			targeting.targetobject=targeting.legalunits[0];
			targeting.legalunits.Clear();
		}

	}
}

/*
function Portal(){//shows button to send unit through portal
	var xPos: short = 400;
	var yPos: short = Mathf.RoundToInt(Screen.height-170);
	var size: short = 80;
	
	var blue: Texture2D = Resources.Load("sprites/sprite2051A") as Texture2D;
	var orange: Texture2D = Resources.Load("sprites/sprite2051B") as Texture2D;
		
	GUI.DrawTexture(Rect(xPos+3,yPos+3,size-6,size-6),blue);
	if (GUI.Button(Rect(xPos,yPos,size,size),"Use\nPortal")){
		//Debug.Log(gate);
		gate.pass=true;
		portalBtn=false;
		gate=null;
	}
	GUI.DrawTexture(Rect(xPos+size+3,yPos+3,size-6,size-6),orange);
	if (GUI.Button(Rect(xPos+size,yPos,size,size),"Don't Use\nPortal")){
		portalBtn=false;
		gate=null;
	}
}
*/
function View(object: GameObject){
	viewedobject=object;
}
function Focus(object: GameObject){
	Camera.main.GetComponent(CamOrbit).target=object.transform;
	camCenter.transform.position=object.transform.position;
}

