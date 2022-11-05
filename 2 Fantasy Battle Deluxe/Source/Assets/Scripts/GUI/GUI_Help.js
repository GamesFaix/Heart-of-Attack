#pragma strict

var gui_master: GUI_Master;
var previousview: String;

var background: Texture2D;
var targeting: Texture[]= new Texture[4];

var column1: short;		
var column2: short;
var btnwidth: short;	
var btnheight: short;
var labelheight: short;
var startY: short;			
var startX: short;
var spacingY1: short;

function Awake(){
	gui_master=gameObject.GetComponent(GUI_Master);
	
	targeting[0]=Resources.Load("gui/help/targeting_serp") as Texture2D;
	targeting[1]=Resources.Load("gui/help/targeting_lin") as Texture2D;
	targeting[2]=Resources.Load("gui/help/targeting_lin_corner") as Texture2D;
	targeting[3]=Resources.Load("gui/help/targeting_arc") as Texture2D;	
}

function Update(){//scale to screen size
	btnwidth=90;//(Screen.width-(column1*2))/5;
	//if (btnwidth>150){btnwidth=150;}
	//if (btnwidth<75){btnwidth=75;}
	column1=Mathf.RoundToInt(Screen.width*0.05);
	column2=Mathf.RoundToInt(column1+btnwidth+30);
	
	btnheight=30;
	labelheight=20;
	startY=Mathf.RoundToInt((Screen.height)*0.2);
	spacingY1=Mathf.RoundToInt((Screen.height-(btnheight*8)-(startY*2))/7);
	if (spacingY1<0){spacingY1=0;}
}

	var displayInfo: float=1;
	var infoScrollview=Vector2.zero;
	var targetinfo: byte;
	var damageinfo: byte;
	var diagram: Texture2D;

function OnGUI () {
	
if (gui_master.view=="help"){	
	background = gui_master.backgrounds[0] as Texture2D;
	//draw background and heading
	GUI.DrawTexture(Rect(0,0,Screen.width,Screen.height),background,ScaleMode.StretchToFill,true,0);
	GUI.Box(Rect(Screen.width*.3,30,Screen.width*0.4,btnheight*2),"Welcoming to\nFantasy Battle: Deluxe");
	//leave help button
	if (GUI.Button(Rect(Screen.width*0.8,30,100,btnheight*2),"Information\nAcknowledged")){
		if (previousview=="menu"){gui_master.view="menu";}
		if (previousview=="game"){gui_master.view="game";}
	}
	
	if (GUI.Button(Rect(column1,startY,btnwidth,btnheight),"Objective")){
		if (displayInfo!=1){displayInfo=1;}
		else {displayInfo=0;}
	}
	if (GUI.Button(Rect(column1,startY+btnheight,btnwidth,btnheight),"Turns")){
		if (displayInfo!=2){displayInfo=2;}
		else {displayInfo=0;}
	}		
	if (GUI.Button(Rect(column1,startY+btnheight*2,btnwidth,btnheight),"Actions")){
		if (displayInfo<3 || displayInfo>=4){displayInfo=3.1;}
		else {displayInfo=0;}
	}
	if (GUI.Button(Rect(column1,startY+btnheight*3,btnwidth,btnheight),"Units")){
		if (displayInfo<4 || displayInfo>=5){displayInfo=4;}
		else {displayInfo=0;}
	}
	if (GUI.Button(Rect(column1,startY+btnheight*4,btnwidth,btnheight),"Obstacles")){
		if (displayInfo<5 || displayInfo>=6){displayInfo=5;}
		else {displayInfo=0;}
	}
	if (displayInfo>=3 && displayInfo<4){//action subcategories
		GUI.DrawTexture(Rect(column1+btnwidth,startY+btnheight*2,15,30),gui_master.lines[0],ScaleMode.StretchToFill,true,0);
		//general
		GUI.DrawTexture(Rect(column1+btnwidth+15,startY,15,60),gui_master.lines[1],ScaleMode.StretchToFill,true,0);
		if (GUI.Button(Rect(column2,startY,btnwidth,btnheight),"General")){
			displayInfo=3.1;
		}
		//targeting
		GUI.DrawTexture(Rect(column1+btnwidth+15,startY+btnheight,15,60),gui_master.lines[1],ScaleMode.StretchToFill,true,0);
		if (GUI.Button(Rect(column2,startY+btnheight,btnwidth,btnheight),"Targeting")){
			if (displayInfo!=3.2){displayInfo=3.2;}
			else {displayInfo=3;}
		}
		//damage
		GUI.DrawTexture(Rect(column1+btnwidth+15,startY+btnheight,15,60),gui_master.lines[2],ScaleMode.StretchToFill,true,0);
		if (GUI.Button(Rect(column2,startY+btnheight*2,btnwidth,btnheight),"Damage")){
			if (displayInfo!=3.3){displayInfo=3.3;}
			else {displayInfo=3;}
		}
		//keywords
		GUI.DrawTexture(Rect(column1+btnwidth+15,startY+btnheight*2,15,60),gui_master.lines[2],ScaleMode.StretchToFill,true,0);
		if (GUI.Button(Rect(column2,startY+btnheight*3,btnwidth,btnheight),"Keywords")){
			if (displayInfo!=3.4){displayInfo=3.4;}
			else {displayInfo=3;}
		}
	}
	if (displayInfo>=4 && displayInfo<5){//unit subcategories
		GUI.DrawTexture(Rect(column1+btnwidth,startY+btnheight*3,15,30),gui_master.lines[0],ScaleMode.StretchToFill,true,0);
		//movement
		GUI.DrawTexture(Rect(column1+btnwidth+15,startY,15,60),gui_master.lines[1],ScaleMode.StretchToFill,true,0);
		if (GUI.Button(Rect(column2,startY,btnwidth,btnheight),"Movement")){
			if (displayInfo!=4.1){displayInfo=4.1;}
			else {displayInfo=4;}
		}
		//composition
		GUI.DrawTexture(Rect(column1+btnwidth+15,startY+btnheight,15,60),gui_master.lines[1],ScaleMode.StretchToFill,true,0);
		if (GUI.Button(Rect(column2,startY+btnheight,btnwidth,btnheight),"Composition")){
			if (displayInfo!=4.2){displayInfo=4.2;}
			else {displayInfo=4;}
		}
		//combat
		GUI.DrawTexture(Rect(column1+btnwidth+15,startY+btnheight*2,15,60),gui_master.lines[1],ScaleMode.StretchToFill,true,0);
		if (GUI.Button(Rect(column2,startY+btnheight*2,btnwidth,btnheight),"Combat")){
			if (displayInfo!=4.3){displayInfo=4.3;}
			else {displayInfo=4;}
		}
	}
	//info box parameters
	var infoX: short = Mathf.RoundToInt(Screen.width*0.3);
	var infoY: short = startY;
	var infoWidth: short = Mathf.RoundToInt(Screen.width*0.6);
	var infoHeight: short = Mathf.RoundToInt(Screen.height-startY-100);
	//info box background
	//if (displayInfo!=0){
		GUI.Box(Rect(infoX,infoY,infoWidth,infoHeight)," ");
	//}
	infoScrollview = GUI.BeginScrollView(Rect(infoX,infoY,infoWidth,infoHeight), infoScrollview, Rect(infoX,infoY,infoWidth-20,infoHeight*4),false,true); //content area
		if (displayInfo==1){//objective
			GUI.Label(Rect(infoX+10,infoY+10,infoWidth-40,infoHeight-20),
				"Fantasy Battle: Deluxe is ultimate battle championship, do not worry if are confuse. \n\nEach player chooses a team for having battle.  Each team is the best! \n\nPlayers start with team Hero Unit and may use Hero to create other team Units. \n\t- Being careful with Hero!!  If Hero is killed, team has losing. \n\t- If all other teams has lose, you have win.");
		}
		if (displayInfo==2){//turns
			GUI.Label(Rect(infoX+10,infoY+10,infoWidth-40,infoHeight-20),
				"Fantasy Battle: Deluxe is a game of turn-based. \n\nPlayers take turn for each Unit they having.  The order of turns is listed in the Queue. \n\t- The Current Unit is Unit at the top of the Queue, whose turn it is. \n\t- The Last Unit is the Unit at the bottom of the Queue. \n\nEach Unit has Initiative (in).  Units with higher in take another turn sooner. \nWhen the Current Unit's turn is over, if its in is higher than the Last Unit and the Last Unit has not already been skipped, the Current Unit skips ahead of the Last Unit in the Queue. \n\nAt the beginning of the game, all Heroes are randomly placed in the Queue. \n\nDuring a Unit's turn, it may spend resources to perform different Actions. \n\t- At the beginning of each Unit's turn, it recieves 2 Action Points (ap).\n\t- All ap are lost at the end of the turn.\n\nWhen a new Unit is created, it is added to the bottom of the Queue.\n\t- New Units do not skip ahead, regardless of their in.");
		}
		if (displayInfo==3.1){//actions general
			GUI.Label(Rect(infoX+10,infoY+10,infoWidth-40,infoHeight-20),
				"Units may perform up to 9 different Actions during their turn. \n\t- No action may be performed twice in a turn. \n\nActions typically have a cost in Action Points (ap) and/or Focus Points (fp). \n\t- ap are lost at the end of the turn.  \n\t- fp are not lost and can accumulate. \n\nCommon Actions:\n\t- Most Units may Move for 1AP.\n\t- All Units may Focus for 1AP to gain 1FP. \n\t- Most Units may Attack for 1AP. \n\t- Heroes typically have 3 Unit creation Actions.\n\t- Units may have 1-3 additional special Actions. \n\nActions cannot be performed if:\n\t- There are no legal targets.\n\t- The Unit does not have the required resources.\n\t- The Action has already been performed this turn.");
		}
		if (displayInfo==3.2){//actions targeting
			GUI.Label(Rect(infoX+10,infoY+10,infoWidth-40,infoHeight-20),
				"Many Actions will require a player to select a target.  Targeting Actions will have a Range (RNG) parameter that will limit how many Cells from the performing Unit the target may be.  Targeting Actions determine legal targets in one of several ways.");
			var diagramPos: Rect=new Rect(infoX+300,infoY+70,250,250);

			if (GUI.Button(Rect(infoX+10,infoY+70,90,20),"Serpentine")){
				if(targetinfo!=1){
					targetinfo=1;
					diagram=targeting[0] as Texture2D;
				}
				else {
					diagram=null;
					targetinfo=0;
				}
			}
			if (GUI.Button(Rect(infoX+10,infoY+90,90,20),"Linear")){
				if(targetinfo!=2){
					diagram=targeting[1] as Texture2D;
					targetinfo=2;}
				else {
					diagram=null;
					targetinfo=0;}
			}
			if (GUI.Button(Rect(infoX+10,infoY+110,90,20),"Corners")){
				if(targetinfo!=3){
					diagram=targeting[2] as Texture2D;
					targetinfo=3;}
				else {
					diagram=null;
					targetinfo=0;}
			}
			if (GUI.Button(Rect(infoX+10,infoY+130,90,20),"Arc")){
				if(targetinfo!=4){
					diagram=targeting[3] as Texture2D;
					targetinfo=4;
				}
				else {
					diagram=null;
					targetinfo=0;
				}
			}
			if (GUI.Button(Rect(infoX+10,infoY+150,90,20),"Targetless")){
				diagram=null;
				if(targetinfo!=5){targetinfo=5;}
				else{targetinfo=0;}
			}
			if (GUI.Button(Rect(infoX+10,infoY+170,90,20),"Other")){
				diagram=null;
				if(targetinfo!=6){targetinfo=6;}
				else{targetinfo=0;}
			}
			if (diagram!=null){GUI.DrawTexture(diagramPos,diagram,ScaleMode.StretchToFill,true,0);}
			var blahblah: String;
			switch(targetinfo){
			
				case 1://serpentine
					blahblah="The RNG determines how many Cells may be crossed to get to the target.  Cells may be crossed horizontally, vertically, or diagonally.  Direction can change at any Cell. \n\nExcept for Move Actions, Serpentine target paths cannot cross Units or Obstacles. \n\nAll moves shown are within a RNG of 4.";
					break;
				case 2://linear
					blahblah="The RNG determines how many Cells may be crossed to get to the target.  Cells may be crossed horizontally, vertically, or diagonally.  Direction cannot change. \n\nExcept for Move Actions, Linear target paths cannot cross Units or Obstacles. \n\nAll moves shown are within a RNG of 3.";
					break;
				case 3://linear corners
					blahblah="When a Linear target path reaches a corner of the Map...\n\n...If it enters the corner diagonally, it exits vertically or horizontally.\n\n...If it enters vertically or horizontally, it exits diagonally.";
					break;
				case 4://arc
					blahblah="The RNG determines the size of a box around the Unit in which Cells can be targeted. \n\nArc target paths can cross Units or Passible Obstacles. \n\nAll moves shown are within a RNG of 3.";
					break;
				case 5://targetless
					blahblah="Some Actions do not require a target.  For example, an Action that allows a Unit to restore it's own hp would automatically select that Unit to heal, and not need a target to be selected.";
					break;
				case 6://other
					blahblah="Some Actions will allow you to target Units anywhere on the Map, or will have special target selection.  These are explained on a case by case basis.";
			}
			GUI.Label(Rect(infoX+120,infoY+70,170,400),blahblah);
		}
		if (displayInfo==3.3){//actions damage
			GUI.Label(Rect(infoX+10,infoY+10,infoWidth-40,infoHeight*3),
				"Actions may deal damage to Units in several different ways. Different damage types have different parameters.  Common parameters are:\n\tDamage (DMG) - Decay (DEC) - Radius (RAD)");

			if (GUI.Button(Rect(infoX+10,infoY+70,150,20),"Normal")){
				if(damageinfo!=1){damageinfo=1;}
				else {damageinfo=0;}
			}
			if (GUI.Button(Rect(infoX+10,infoY+90,150,20),"Fire (FIR)")){
				if(damageinfo!=2){damageinfo=2;}
				else {damageinfo=0;}
			}
			if (GUI.Button(Rect(infoX+10,infoY+110,150,20),"Laser (LSR)")){
				if(damageinfo!=3){damageinfo=3;}
				else {damageinfo=0;}
			}
			if (GUI.Button(Rect(infoX+10,infoY+130,150,20),"Explosive (EXP)")){
				if(damageinfo!=4){damageinfo=4;}
				else {damageinfo=0;}
			}
			if (GUI.Button(Rect(infoX+10,infoY+150,150,20),"Poison (PSN)")){
				if(damageinfo!=5){damageinfo=5;}
				else{damageinfo=0;}
			}
			if (GUI.Button(Rect(infoX+10,infoY+170,150,20),"Electrical (ELC)")){
				if(damageinfo!=6){damageinfo=6;}
				else{damageinfo=0;}
			}
			switch(damageinfo){
				case 1://normal
					blahblah="Normal Damage \n\t- Target receives damage.\n\t- DMG: hp reduction to target, before target's def is subtracted. ";
					break;
				case 2://fire
					blahblah="Fire Damage (FIR)\n\t- Target receives damage.\n\t- Units touching damaged Units also receive some damage.\n\t- DMG: hp reduction to target, before target's def is subtracted.\n\t- DEC: % of DMG, after def subtracted, done to neighboring \n\tUnits. \n\t\t(DEC is compounded each time DMG is spread.)\n\t- RAD: Maximum distance DMG is spread.";
					break;
				case 3://laser
					blahblah="Laser Damage (LSR)\n\t- Target receives damage.\n\t- Units behind target also receive some damage.\n\t- DMG: hp reduction to target, before target's def is subtracted.\n\t- DEC: % of DMG, after def subtracted, done to next Unit.\n\t\t(DEC is compounded each time DMG is passed.) \n\t- RAD: Maximum distance DMG can be dealt.\n\t- Laser Damage cannot move past Obstacles.";
					break;
				case 4://explosive
					blahblah="Explosive Damage (EXP)\n\t- Target and all Units within the Critical Zone receive damage.\n\t- Units outside of Critical Zone but within Radius also receive \n\tsome damage.\n\t- DMG: hp reduction to all Units in Critical Zone, before def \n\tsubtracted. \n\t- Critical Zone (CRZ): Size of box around target Cell where initial \n\tDMG is dealt.  \n\t\t(CRZ of 0 contains 1 Cell, CRZ of 1 contains 9 Cells, CRZ of 2 \n\t\tcontains 25 Cells.)\n\t- DEC: % of DMG done in each Radius outside of Critical Zone.\n\t\t(DEC is compounded in each successive Radius.)\n\t- RAD: Maximum distance damage is dealt.\n\t- Destructible Obstacles within the RAD of an explosion are\n\tdestroyed.";
					break;
				case 5://poison
					blahblah="Poison Damage (PSN)\n\t- Damage is dealt to target.\n\t- If target is Biological, additional damage is dealt later.\n\t- DMG: Initial hp reduction to target, before target's def is \n\tsubtracted.\n\t- DEC: % of DMG dealt to target at the beginning of its next turn \n\t(if Biological).\n\t\t(DEC is compounded each turn.)\n\t- RAD: Maximum number of turns damage can be dealt.\n\t- DMG dealt after initial DMG does not subtract target's def. \n\n(See also Units/Composition)";
					break;
				case 6://electrical
					blahblah="Electrical Damage (ELC)\n\t- Target is dealt damage.  \n\t- Target's Speed is temporarily reduced by %50.\n\t- Target's Initiative is temporarily reduced by 1.\n\t- DMG: hp reduction to target, before target's def is subtracted.\n\t\t(Mechanical Units take +50% DMG, before def.)\n\t- RAD: Number of turns before target's sp and in are restored.\n\t\t(sp and in are restored at the end of the target's turn.)\n\n(See also Units/Composition)";
			}
			GUI.Label(Rect(infoX+170,infoY+70,400,400),blahblah);
			
			GUI.Label(Rect(infoX+10,infoY+infoHeight-50,infoWidth-40,infoHeight*3),
				"(See also Units/Combat)");
		}
		if (displayInfo==3.4){//actions keywords
			GUI.Label(Rect(infoX+10,infoY+10,infoWidth-40,infoHeight*3),
				"Knockback (KB)\n\t- Moves neighboring Unit away from Action performer in Linear path.\n\t- RAD - Maximum distance Unit can be moved.\n\t- Knockback cannot move Units past any Obstacles they could not normally move past.\n\n\t(See also Units/Mobility, Obstacles)\n\nCounter-Attack (CNT)\n\t- When a Unit with CNT is damaged, it automatically deals damage back to the damager.\n\t- DEC: % of received DMG, after def subtracted, dealt back to damager.\n\n\t(See also Units/Combat, Actions/Damage)");
		}
		if (displayInfo==4.1){//unit movement
			GUI.Label(Rect(infoX+10,infoY+10,infoWidth-40,infoHeight-20),
				"Speed (sp)\n\t- Number of Cells the Unit can move with its Move Action. \n\nMobility\n\t- Defines where a Unit can move. \n\t\tGround\n\t\t\t- Unit cannot move past any Obstacles. \n\t\t\t- Unit may occupy a Cell with a Flying Unit.\n\t\tTrample\n\t\t\t- Unit can move past Destructible Obstacles, destroying them.\n\t\t\t- Unit may occupy a Cell with a Flying Unit. \n\t\tFlying\n\t\t\t- Unit can move past Passible Obstacles.\n\t\t\t- Unit may occupy a Cell with a Ground Unit, Trample Unit, or Passible Obstacle. \n\t\tGaseous\n\t\t\t- Unit can move past any Obstacle.\n\t\t\t- Unit can occupy a Cell with any Units, Obstacles, or combination.\n\n(See also Actions/Targeting, Obstacles)");
		}
		if (displayInfo==4.2){//unit composition
			GUI.Label(Rect(infoX+10,infoY+10,infoWidth-40,infoHeight-20),
				"Composition\n\t- What the Unit's made of (affected by some Actions).\n\t\tBiological \n\t\tMechanical \n\t\tCybernetic - Unit counts as Biological and Mechanical.\n\t\tEthereal - Unit counts as neither Biological nor Mechanical.");
		}
		if (displayInfo==4.3){//unit combat stats
			GUI.Label(Rect(infoX+10,infoY+10,infoWidth-40,infoHeight-20),
				"Hit Points (hp)\n\t- Unit's current health. \nMax Hit Points (mhp)\n\t- Unit's maximum health. \n\nIf a Unit's hp is 0, it dies.  \n\t- In most cases, dead Units become Corpse Obstacles.\n\nDefense (def)\n\t- Amount reduced from any damage the Unit recieves.\n\t- Actions that give a direct hp reduction do not factor in the Unit's def.\n\n(See also Actions/Damage)");
		}
		if (displayInfo==5){//obstacles
			GUI.Label(Rect(infoX+10,infoY+10,infoWidth-40,infoHeight),
		"Obstacles are non-Unit objects that can occupy Cells. They fall into several categories.\n\nImpassible Obstacles\n\t- Only Units with Gaseous mobility may move across Impassible Obstacles.\n\t- Target paths may not cross Impassible Obstacles.\n\t- All Impassible Obstacles are Indestructible.\n\nPassible Obstacles\n\t- Flying and Gaseous Units may move across Passible Obstacles.\n\t- Arc target paths may cross any Passible Obstacle.\n\t- Some Passible Obstacles are Destructible. \n\nDestructible Obstacles\n\t- Units with Trample mobility may move across Destructible Obstacles, destroying them.\n\t- Destructible Obstacles within the Radius of Explosive damage will be destroyed.\n\nCorpses\n\t- Corpses are a special kind of Destructible Obstacle.\n\t- Most Units will leave a Corpse in their Cell when they die.\n\t- Flying Units above other Units or Obstacles will not leave a Corpse.\n\n(See also Actions/Targeting, Units/Mobility)");}
	GUI.EndScrollView();
	}
}