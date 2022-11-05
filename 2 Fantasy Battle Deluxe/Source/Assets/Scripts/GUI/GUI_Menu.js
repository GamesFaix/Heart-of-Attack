#pragma strict
	
	var background: Texture2D;
	var gameindexprefab: GameObject;
	var gameindex: GameIndex;
	var gui_master: GUI_Master;
	var gui_help: GUI_Help;
	
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

	var loadbox: String= "Loading...";

function Awake(){
	gui_master=gameObject.GetComponent(GUI_Master);
	gui_help=gameObject.GetComponent(GUI_Help);
	gameindexprefab=GameObject.Find("GameIndexPrefab");
	gameindex=gameindexprefab.GetComponent(GameIndex);
	
}

function OnGUI () {
	if (gui_master.view=="menu"){
		ScaleGUI();

		background = gui_master.backgrounds[0] as Texture2D;
	//draw background & title
		GUI.DrawTexture(Rect(0,0,Screen.width,Screen.height),background,ScaleMode.StretchToFill,true,Screen.width/Screen.height);
		GUI.Box(Rect(Screen.width*.3,30,Screen.width*0.4,btnheight*2),"\nFantasy Battle: Deluxe");
		//show help button
		if(GUI.Button(Rect(Screen.width*0.8,30,100,btnheight*2),"How does\nfighting?")){
			gui_master.view="help";
			gui_help.previousview="menu";
		}
	for (var i: byte=1; i<=8; i++){
		//team selector - create button
		if (GUI.Button(Rect(column1,startY+(i-1)*(btnheight+spacingY1),btnwidth,btnheight),gameindex.team_names[gameindex.player_team_numbers[i]])){
			if (active_button==false){
				teamMenu[i] = true;}}
		//when button pushed, create selection grid
		if(teamMenu[i]==true){
			active_button=true;
			GUI.Box(Rect(column1+btnwidth,startY,btnwidth,labelheight),"Player "+i+":");
			gameindex.player_team_numbers[i]=GUI.SelectionGrid(Rect(column1+btnwidth,startY+labelheight,btnwidth,btnheight*gameindex.team_names.length),gameindex.player_team_numbers[i],gameindex.team_names,1);
			if(GUI.changed) {
				active_button=false;
				teamMenu[i]=false;}
		}
		// color selector - create button
		if (GUI.Button(Rect(column1+btnwidth*2,startY+(i-1)*(btnheight+spacingY1),btnheight,btnheight),gameindex.colors[gameindex.player_colors[i]])){
			if (active_button==false){
				colorMenu[i] = true;}}
		//when pushed, create selection grid
		if(colorMenu[i]==true){
			active_button=true;
			GUI.Box(Rect(column1+btnwidth*2+btnheight,startY,btnheight*2*1.125,labelheight),"Player"+i+":");
			gameindex.player_colors[i]=GUI.SelectionGrid(Rect(column1+btnwidth*2+btnheight,startY+labelheight,btnheight*2*1.125,btnheight*gameindex.colors.length/2*1.125),gameindex.player_colors[i],gameindex.colors,2);
			if(GUI.changed){
				//when color selected, make sure color is not in use
				if((i==1 && gameindex.player_colors[i]!=gameindex.player_colors[2] && gameindex.player_colors[i]!=gameindex.player_colors[3] && 
							gameindex.player_colors[i]!=gameindex.player_colors[4] && gameindex.player_colors[i]!=gameindex.player_colors[5] && 
							gameindex.player_colors[i]!=gameindex.player_colors[6] && gameindex.player_colors[i]!=gameindex.player_colors[7] && 
							gameindex.player_colors[i]!=gameindex.player_colors[8])||
					(i==2 && gameindex.player_colors[i]!=gameindex.player_colors[1] && gameindex.player_colors[i]!=gameindex.player_colors[3] && 
							gameindex.player_colors[i]!=gameindex.player_colors[4] && gameindex.player_colors[i]!=gameindex.player_colors[5] && 
							gameindex.player_colors[i]!=gameindex.player_colors[6] && gameindex.player_colors[i]!=gameindex.player_colors[7] && 
							gameindex.player_colors[i]!=gameindex.player_colors[8])||
					(i==3 && gameindex.player_colors[i]!=gameindex.player_colors[1] && gameindex.player_colors[i]!=gameindex.player_colors[2] && 
							gameindex.player_colors[i]!=gameindex.player_colors[4] && gameindex.player_colors[i]!=gameindex.player_colors[5] && 
							gameindex.player_colors[i]!=gameindex.player_colors[6] && gameindex.player_colors[i]!=gameindex.player_colors[7] && 
							gameindex.player_colors[i]!=gameindex.player_colors[8])||
					(i==4 && gameindex.player_colors[i]!=gameindex.player_colors[1] && gameindex.player_colors[i]!=gameindex.player_colors[2] && 
							gameindex.player_colors[i]!=gameindex.player_colors[3] && gameindex.player_colors[i]!=gameindex.player_colors[5] && 
							gameindex.player_colors[i]!=gameindex.player_colors[6] && gameindex.player_colors[i]!=gameindex.player_colors[7] && 
							gameindex.player_colors[i]!=gameindex.player_colors[8])||
					(i==5 && gameindex.player_colors[i]!=gameindex.player_colors[1] && gameindex.player_colors[i]!=gameindex.player_colors[2] && 
							gameindex.player_colors[i]!=gameindex.player_colors[3] && gameindex.player_colors[i]!=gameindex.player_colors[4] && 
							gameindex.player_colors[i]!=gameindex.player_colors[6] && gameindex.player_colors[i]!=gameindex.player_colors[7] && 
							gameindex.player_colors[i]!=gameindex.player_colors[8])||
					(i==6 && gameindex.player_colors[i]!=gameindex.player_colors[1] && gameindex.player_colors[i]!=gameindex.player_colors[2] && 
							gameindex.player_colors[i]!=gameindex.player_colors[3] && gameindex.player_colors[i]!=gameindex.player_colors[4] && 
							gameindex.player_colors[i]!=gameindex.player_colors[5] && gameindex.player_colors[i]!=gameindex.player_colors[7] && 
							gameindex.player_colors[i]!=gameindex.player_colors[8])||
					(i==7 && gameindex.player_colors[i]!=gameindex.player_colors[1] && gameindex.player_colors[i]!=gameindex.player_colors[2] && 
							gameindex.player_colors[i]!=gameindex.player_colors[3] && gameindex.player_colors[i]!=gameindex.player_colors[4] && 
							gameindex.player_colors[i]!=gameindex.player_colors[5] && gameindex.player_colors[i]!=gameindex.player_colors[6] && 
							gameindex.player_colors[i]!=gameindex.player_colors[8])||
					(i==8 && gameindex.player_colors[i]!=gameindex.player_colors[1] && gameindex.player_colors[i]!=gameindex.player_colors[2] && 
							gameindex.player_colors[i]!=gameindex.player_colors[3] && gameindex.player_colors[i]!=gameindex.player_colors[4] && 
							gameindex.player_colors[i]!=gameindex.player_colors[5] && gameindex.player_colors[i]!=gameindex.player_colors[6] && 
							gameindex.player_colors[i]!=gameindex.player_colors[6])){
						
						active_button=false;
						colorMenu[i]=false;}	
			}	
		}
	}
	i=1; //reset
	
	//map label
	GUI.Box(Rect(column2,startY,btnwidth,labelheight),"Map Selection:");
	//map selector - create button
	if (GUI.Button(Rect(column2,startY+labelheight,btnwidth,btnheight),gameindex.levels[gameindex.levelSelection])){
		if (active_button==false){
			levelMenu = true;}}
	//when button pushed, create selection grid
	if(levelMenu==true){
		active_button=true;
		gameindex.levelSelection=GUI.SelectionGrid(Rect(column2+btnwidth,startY+labelheight,btnwidth,gameindex.levels.length*btnheight),gameindex.levelSelection,gameindex.levels,1);
		if(GUI.changed) {
			active_button=false;
			levelMenu=false;
		}
	}
	
	//spawntype label
	GUI.Box(Rect(column2,startY+labelheight*2+btnheight,btnwidth,labelheight),"Spawn location:");
	//spawntype selector - create button
	if (GUI.Button(Rect(column2,startY+labelheight*3+btnheight,btnwidth,btnheight),gameindex.spawntypes[gameindex.spawnselection])){
		if (active_button==false){
			spawnMenu = true;}}
	//when button pushed, create selection grid
	if(spawnMenu==true){
		active_button=true;
		gameindex.spawnselection=GUI.SelectionGrid(Rect(column2+btnwidth,startY+labelheight*3+btnheight,btnwidth,gameindex.spawntypes.length*btnheight),gameindex.spawnselection,gameindex.spawntypes,1);
		if(GUI.changed) {
			active_button=false;
			spawnMenu=false;
		}
	}
	
	//disable player 7 & 8 on face spawning
	if (gameindex.spawnselection==0){
		gameindex.player_team_numbers[7]=0;
		gameindex.player_team_numbers[8]=0;
		}
	
	
	//obstacle density slider
	GUI.Box(Rect(column2,startY+labelheight*4+btnheight*2,btnwidth,labelheight),"Obstacle density");
	gameindex.density=GUI.HorizontalSlider (Rect(column2,startY+labelheight*5.5+btnheight*2,btnwidth,labelheight),gameindex.density,0,95);
	
	//battle button
	if(GUI.Button(Rect(column2,startY+labelheight*7+btnheight*2,btnwidth,btnheight*2),"FIGHTING\nSTART")){
		gameindexprefab.GetComponent(FightingStart).FightingStart();
	}
	
	if(gui_master.loading==true){
		
		GUI.Box(Rect(Screen.width*0.4,Screen.height*0.4,Screen.width*0.2,Screen.height*0.2)," ");
		GUI.Label(Rect(Screen.width*0.4+5,Screen.height*0.4+5,Screen.width*0.2-10,Screen.height*0.2-10),loadbox);
	
	}
	
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