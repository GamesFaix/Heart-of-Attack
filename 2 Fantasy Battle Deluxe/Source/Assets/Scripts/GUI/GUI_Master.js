#pragma strict

var view: String;
var menuGUI: GUI_Menu;
var gameGUI: GUI_Game;
var helpGUI: GUI_Help;
var backgrounds: Texture[]=new Texture[5];
var lines: Texture[] = new Texture[3];
var loading: boolean=false;

function Awake(){
	view="menu";
	menuGUI=gameObject.GetComponent(GUI_Menu);
	gameGUI=gameObject.GetComponent(GUI_Game);
	helpGUI=gameObject.GetComponent(GUI_Help);
	
	backgrounds[0]= Resources.Load("gui/backgrounds/battle_apocalypse") as Texture2D;
	backgrounds[1]=Resources.Load("gui/backgrounds/battle_cats") as Texture2D;
	backgrounds[2]=Resources.Load("gui/backgrounds/battle_medieval") as Texture2D;
	backgrounds[3]=Resources.Load("gui/backgrounds/battle_robot") as Texture2D;
	backgrounds[4]=Resources.Load("gui/backgrounds/battle_steampunk") as Texture2D;	
	
	lines[0]=Resources.Load("gui/lines/gold_lateral") as Texture2D;
	lines[1]=Resources.Load("gui/lines/gold_Up") as Texture2D;
	lines[2]=Resources.Load("gui/lines/gold_Down") as Texture2D;
}

