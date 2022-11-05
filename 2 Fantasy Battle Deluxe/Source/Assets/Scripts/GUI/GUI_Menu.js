#pragma strict
	
//external assets	
	var background: Texture2D;
	var gameindexprefab: GameObject;
	var gameIndex: GameIndex;
	var gui_master: GUI_Master;
	var gui_help: GUI_Help;
	var gui_styles: GUI_Styles;
	
/*dropdown menu buttons
	-each dropdown menu (team & color x8, +level select) has it's own on/off switch (teamMenu[x] & colorMenu[x])
	-active_button ensures that only one such switch can be on at a time
	-when a button is pushed, the switch for that dropdown turns on, 
		the dropdown displays, and active_button turns on
	-when a valid selection is made, the dropdown switch turns off, 
		the dropdown closes, and active_button turns off */
	var teamMenu=new List.<boolean>();
	var colorMenu=new List.<boolean>();
	for (var i: byte=0; i<=8; i++){
		teamMenu.Add(false);
		colorMenu.Add(false);
	}
	var levelMenu=false;
	var spawnMenu=false;
	var active_button: boolean = false;

//button spacing parameters
	var column1: short;
	var column2: short;
	var btnwidth: short;
	var btnheight: short;
	var labelheight: short;
	var startY: short;
	var spacingY1: short;

	var bg: byte;
	
function Awake(){
	gui_master=gameObject.GetComponent(GUI_Master);
	gui_help=gameObject.GetComponent(GUI_Help);
	gui_styles=gameObject.GetComponent(GUI_Styles);
	gameindexprefab=GameObject.Find("GameIndexPrefab");
	gameIndex=gameindexprefab.GetComponent(GameIndex);
	bg = Mathf.Floor(Random.value*gui_master.backgrounds.length);
		
}

function OnGUI () {
	if (gui_master.view=="menu"){
		ScaleGUI();
		
		Background();
		Heading();
		
		HelpButton();
	
		TeamSelect();
		ColorSelect();
		MapSelect();
		SpawnTypeSelect();
		ObstacleSlider();
	
		StartGameButtons();
		LoadScreen();	
	}
}
function ScaleGUI(){
	column1=Mathf.RoundToInt(Screen.width*0.1);
	column2=Mathf.RoundToInt(Screen.width*0.6);
	btnwidth=Mathf.RoundToInt((Screen.width-(column1*2))/5);
	if (btnwidth>150){btnwidth=150;}
	if (btnwidth<75){btnwidth=75;}
	
	btnheight=30;
	labelheight=20;
	startY=Mathf.RoundToInt((Screen.height)*0.2);
	spacingY1=Mathf.RoundToInt((Screen.height-(btnheight*8)-(startY*2))/7);
	if (spacingY1<0){spacingY1=0;}
}
function Background(){
	background = gui_master.backgrounds[bg] as Texture2D;
	GUI.DrawTexture(Rect(0,0,Screen.width,Screen.height),background,ScaleMode.StretchToFill,true,Screen.width/Screen.height);
}
function Heading(){
	GUI.Box(Rect(Screen.width*.35,30,Screen.width*0.3,btnheight*2)," Fantasy Battle:", gui_styles.heading[0]);
	GUI.Box(Rect(Screen.width*.35,65,Screen.width*0.3,btnheight*2),"DELUXE", gui_styles.heading[1]);
}
function HelpButton(){
	//show help button
	if(GUI.Button(Rect(Screen.width*0.8,30,100,btnheight*2), "Help", gui_styles.sButton[0])){
		gui_master.view="help";
		gui_help.previousview="menu";
	}
}
//
function TeamSelect(){
	for (var i: byte=1; i<=8; i++){
		if (GUI.Button(Rect(column1,startY+(i-1)*(btnheight+spacingY1),btnwidth,btnheight),gameIndex.teamNames[gameIndex.playerTeams[i]], gui_styles.sButton[gameIndex.playerTeams[i]])){
			if (active_button==false){
				teamMenu[i] = true;
			}
		}
		if(teamMenu[i]==true){
			active_button=true;
			GUI.Box(Rect(column1+btnwidth,startY,btnwidth,labelheight),"Player "+i+":");
			for (var j: byte=0; j<gameIndex.teamNames.length; j++){
				if (GUI.Button(Rect(column1+btnwidth, startY+labelheight+btnheight*(j), btnwidth, btnheight), gameIndex.teamNames[j], gui_styles.sButton[j])){
					gameIndex.playerTeams[i]=j;
					active_button=false;
					teamMenu[i]=false;
				}
			}
		}
	}
}
var taken: String ="";
function ColorSelect(){
	for (var i: byte=1; i<=8; i++){
		if (GUI.Button(Rect(column1+btnwidth*2,startY+(i-1)*(btnheight+spacingY1),btnheight,btnheight),gameIndex.colors[gameIndex.playerColors[i]])){
			if (active_button==false){
				colorMenu[i] = true;
				taken="";
			}
		}
		if(colorMenu[i]==true){
			active_button=true;
			GUI.Box(Rect(column1+btnwidth*2+btnheight,startY,btnheight*2*1.125,labelheight),"Player"+i+":");

			var color: byte=0;
			for (var k: byte=0; k<=3; k++){
				for (var l: byte=0; l<=3; l++){
					if (GUI.Button(Rect(column1+btnwidth*2+btnheight*(k+1), startY+labelheight+btnheight*l, btnheight, btnheight), gameIndex.colors[color])){
						var playerColor=color;
						var colorTaken: boolean=false;
						for (var j: byte=1; j<=8; j++){
							if (i!=j && playerColor==gameIndex.playerColors[j]){
								colorTaken=true;
								taken="Color taken";
							}	
						}
						if (colorTaken==false){
							gameIndex.playerColors[i]=playerColor;
							active_button=false;
							colorMenu[i]=false;
						}
					}
					color++;
				}
			}
			if (taken=="Color taken"){
				GUI.Label(Rect(column1+btnwidth*2,startY-btnheight,btnwidth, btnheight),taken, gui_styles.label);
			}
		}	
	}
}
function MapSelect(){
	//map label
	GUI.Box(Rect(column2,startY,btnwidth,labelheight),"Map Selection:");
	//map selector - create button
	if (GUI.Button(Rect(column2,startY+labelheight,btnwidth,btnheight),gameIndex.levels[gameIndex.levelSelection], gui_styles.sButton[0])){
		if (active_button==false){
			levelMenu = true;}}
	//when button pushed, create selection grid
	if(levelMenu==true){
		active_button=true;
		gameIndex.levelSelection=GUI.SelectionGrid(Rect(column2+btnwidth,startY+labelheight,btnwidth,gameIndex.levels.length*btnheight),gameIndex.levelSelection,gameIndex.levels,1);
		if(GUI.changed) {
			active_button=false;
			levelMenu=false;
		}
	}
}
function SpawnTypeSelect(){
	//spawntype label
	GUI.Box(Rect(column2,startY+labelheight*2+btnheight,btnwidth,labelheight),"Spawn location:");
	//spawntype selector - create button
	if (GUI.Button(Rect(column2,startY+labelheight*3+btnheight,btnwidth,btnheight),gameIndex.spawntypes[gameIndex.spawnselection], gui_styles.sButton[0])){
		if (active_button==false){
			spawnMenu = true;}}
	//when button pushed, create selection grid
	if(spawnMenu==true){
		active_button=true;
		gameIndex.spawnselection=GUI.SelectionGrid(Rect(column2+btnwidth,startY+labelheight*3+btnheight,btnwidth,gameIndex.spawntypes.length*btnheight),gameIndex.spawnselection,gameIndex.spawntypes,1);
		if(GUI.changed) {
			active_button=false;
			spawnMenu=false;
		}
	}
	
	//disable player 7 & 8 on face spawning
	if (gameIndex.spawnselection==0){
		gameIndex.playerTeams[7]=0;
		gameIndex.playerTeams[8]=0;
	}
}
function ObstacleSlider(){
	//obstacle density slider
	GUI.Box(Rect(column2,startY+labelheight*4+btnheight*2,btnwidth,labelheight),"Obstacle density");
	gameIndex.density=GUI.HorizontalSlider (Rect(column2,startY+labelheight*5.5+btnheight*2,btnwidth,labelheight),gameIndex.density,0,95);
}
//
function StartGameButtons(){
	//battle button
	if(GUI.Button(Rect(column2,startY+labelheight*7+btnheight*2,btnwidth,btnheight*2),"FIGHTING\nSTART", gui_styles.sButton[0])){
		gameindexprefab.GetComponent(CreateWorld).InitializePlay();
	}
	//debug button
	if(GUI.Button(Rect(column2,startY+labelheight*7+btnheight*2+btnheight*2,btnwidth,btnheight*2),"DEBUG\nSTART", gui_styles.sButton[0])){
		gameindexprefab.GetComponent(CreateWorld).InitializeDebug();
	}
	//8teams button
	if(GUI.Button(Rect(column2,startY+labelheight*7+btnheight*2+btnheight*4,btnwidth,btnheight*2),"8 players\nAll teams", gui_styles.sButton[0])){
		gameIndex.spawnselection=1;
		for (var i: byte=1; i<=8; i++){
			gameIndex.playerTeams[i]=i;
		}
		gameindexprefab.GetComponent(CreateWorld).InitializePlay();
	}
}

var loadbox: String= "Loading...";
function LoadScreen(){
	if(gui_master.loading==true){		
		GUI.Box(Rect(Screen.width*0.4,Screen.height*0.4,Screen.width*0.2,Screen.height*0.2)," ");
		GUI.Label(Rect(Screen.width*0.4+5,Screen.height*0.4+5,Screen.width*0.2-10,Screen.height*0.2-10),loadbox);	
	}
}