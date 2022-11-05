#pragma strict

//retrieve GUI menu to reference
var gui_menu: GameObject;
var colors: Texture[]=new Texture[16];

var teamNames: String[] = [
	"[disabled]",
	"G.E.A.R.P.", 
	"New Republic", 
	"Torridale", 
	"Forgotten Grove", 
	"Chrononistas", 
	"Psycho Tropics", 
	"Psilent Aureators", 
	"Voidoids", 
	"[random]"];
	
var levels: String[]= ["[random]","Grassafras","Just Deserts","Magmountain","Ice Mountain"];
var levelSelection: byte = 0;//default [random]

var spawntypes: String[]=["Face (2-6 players)","Corner (2-8 players)"];
var spawnselection: byte = 0; //default Face

//players default to [random]
var playerTeams: byte[]= new byte[9];
var playerColors: byte[] = new byte[9];
	
/*player counting
	-player_slots will be used to check if each player is left disabled, all default to disabled
	-player_count will sum the number of enabled slots */
var player_slots: int[] = new int[9];
var player_count: byte;
	
var mobilities: String[] = ["","Ground","Trample","Flying","Gaseous"];
	
var density: byte = 10;

function Awake(){
	colors[0]=Resources.Load("Colors/black_thumb") as Texture2D;
	colors[1]=Resources.Load("Colors/grey_thumb") as Texture2D;
	colors[2]=Resources.Load("Colors/white_thumb") as Texture2D;
	colors[3]=Resources.Load("Colors/pink_thumb") as Texture2D;
	
	colors[4]=Resources.Load("Colors/blue_thumb") as Texture2D;
	colors[5]=Resources.Load("Colors/l_blue_thumb") as Texture2D;
	colors[6]=Resources.Load("Colors/purple_thumb") as Texture2D;
	colors[7]=Resources.Load("Colors/magenta_thumb") as Texture2D;
	
	colors[8]=Resources.Load("Colors/dark_green_thumb") as Texture2D;
	colors[9]=Resources.Load("Colors/green_thumb") as Texture2D;
	colors[10]=Resources.Load("Colors/teal_thumb") as Texture2D;
	colors[11]=Resources.Load("Colors/red_thumb") as Texture2D;
	
	colors[12]=Resources.Load("Colors/brown_thumb") as Texture2D;
	colors[13]=Resources.Load("Colors/gold_thumb") as Texture2D;
	colors[14]=Resources.Load("Colors/yellow_thumb") as Texture2D;
	colors[15]=Resources.Load("Colors/orange_thumb") as Texture2D;

	playerColors[1]=4; 
	playerColors[2]=11; 
	playerColors[3]=14; 
	playerColors[4]=8;
	playerColors[5]=2; 
	playerColors[6]=15; 
	playerColors[7]=6; 
	playerColors[8]=12;

	var i: byte;
	for(i=1; i<=8; i++){playerTeams[i]=9;}
	for (i=1; i<=8; i++){player_slots[i]=0;}
}

function Update(){
	//each active player slot is counted, and player_count summed
	var i: byte;
	for (i=1; i<=8; i++){
		if(playerTeams[i]>0){
			player_slots[i]=1;
		}
	}
	player_count=player_slots[1]+player_slots[2]+player_slots[3]+player_slots[4]+
			player_slots[5]+player_slots[6]+player_slots[7]+player_slots[8];
	i=1;
}

