#pragma strict

//retrieve GUI menu to reference
var gui_menu: GameObject;
var colors: Texture[]=new Texture[16];

var team_names: String[] = ["[disabled]", "[random]",
	"G.E.A.R.P.", "New Republic", "Torridale", "Forgotten Grove", 
	"Chrononistas", "Psycho Tropics", "Psilent Aureators", "Voidoids"];
	
var levels: String[]= ["[random]","Grassafras","Just Deserts","Magmountain","Ice Mountain"];
var levelSelection: byte = 0;//default [random]

var spawntypes: String[]=["Face (2-6 players)","Corner (2-8 players)"];
var spawnselection: byte = 0; //default Face

//players default to [random]
var player_team_numbers: byte[]= new byte[9];
var i: byte;
for(i=1; i<=8; i++){player_team_numbers[i]=1;}

//players given default colors
var player_colors: byte[] = new byte[9]; //player color selections override defaults when selected
	player_colors[1]=1; player_colors[2]=10; player_colors[3]=8; player_colors[4]=0;
	player_colors[5]=5; player_colors[6]=13; player_colors[7]=2; player_colors[8]=11;

/*player counting
	-player_slots will be used to check if each player is left disabled, all default to disabled
	-player_count will sum the number of enabled slots */
var player_slots: int[] = new int[9];
for (i=1; i<=8; i++){player_slots[i]=0;}
var player_count: byte;
	
var mobilities: String[] = ["","Ground","Trample","Flying","Gaseous"];
	
var density: byte = 10;

function Update(){
	//each active player slot is counted, and player_count summed
	var i: byte;
	for (i=1; i<=8; i++){
		if(player_team_numbers[i]>0){
			player_slots[i]=1;
		}
	}
	player_count=player_slots[1]+player_slots[2]+player_slots[3]+player_slots[4]+
			player_slots[5]+player_slots[6]+player_slots[7]+player_slots[8];
	i=1;
}

