#pragma strict

var mapPrefab: GameObject;
var cellMasterPrefab: GameObject;
var cellPrefab: GameObject;
var spawnZonePrefabs: GameObject[]=new GameObject[2];
var playManagerPrefab: GameObject;
var objectPrefab: GameObject;
var obstacleMasterPrefab: GameObject;
////
var gui_master: GUI_Master;
var gui_game: GUI_Game;
var gui_menu: GUI_Menu;
var gameindex: GameIndex;
var queue: QueueScript;
var obstaclegenerator: ObstacleGenerator;
////instances
var map: GameObject;
var cellMaster: GameObject;
var playManager: GameObject;
var spawnMaster: GameObject;
var obstacleMaster: GameObject;
var obstacle: GameObject;

var count: short;
var grid: byte;
var map_id: byte;

function Awake(){
	gui_master=GameObject.Find("GUIPrefab").GetComponent(GUI_Master);
	gui_game=GameObject.Find("GUIPrefab").GetComponent(GUI_Game);
	gui_menu=GameObject.Find("GUIPrefab").GetComponent(GUI_Menu);
	gameindex=gameObject.GetComponent(GameIndex);
	queue=gameObject.GetComponent(QueueScript);
}
///
function InitializePlay() {
	gui_master.loading=true;
	gui_menu.loadbox+="\n\tCreating map...";
	map=Instantiate(mapPrefab,transform.position,Quaternion.identity);

	yield MapSetup();
	
	gui_menu.loadbox+="\n\tGenerating cells...";
	yield CellGenerator();
	yield CellChecker();
	
	yield CreatePlayManager();

	gui_menu.loadbox+="\n\tPlacing obstacles...";
	yield CreateObstacleMaster();

	yield obstacleMaster.GetComponent(ObstacleGenerator).ObstacleGenerator();
	
	gui_menu.loadbox+="\n\tSpawning heroes...";
	yield SpawnZoneGenerator();
	
	var i: byte;
	for (i=1; i<=8; i++){yield HeroSpawner(i);}
	yield DestroySpawnZones();
	
	yield PrepareFirstUnit();

	gui_master.loading=false;
	gui_menu.loadbox="Loading...";
	gui_master.view="game";	
}
function InitializeDebug(){
	gui_master.loading=true;
	gui_menu.loadbox+="\n\tLoading debug mode...";
	map=Instantiate(mapPrefab,transform.position,Quaternion.identity);

	yield DebugMap();
	yield CellGenerator();
	yield CellChecker();
	yield CreatePlayManager();
	yield DebugHeroes();
	yield PrepareFirstUnit();
	yield CreateObstacleMaster();
	yield obstacleMaster.GetComponent(ObstacleGenerator).ObstacleGenerator(); //yields to DebugObstacles() if map_id==255

	gui_master.loading=false;
	gui_menu.loadbox="Loading...";
	gui_master.view="game";
}
///
function MapSetup(): IEnumerator{

	var terrain: Texture[] = new Texture[4]; 
	terrain[0]=Resources.Load("terrain/turf_grass") as Texture2D;
	terrain[1]=Resources.Load("terrain/turf_sand") as Texture2D;
	terrain[2]=Resources.Load("terrain/turf_ice") as Texture2D;
	terrain[3]=Resources.Load("terrain/turf_lava") as Texture2D;
	
	//declare map variables
	var map_title: String; //default no title
	var scaleFactor: float;

	//copy level selection from baton
	map_id=gameindex.levelSelection;
		
	//set grid spacing!!!
	grid=2;
	
	//random map
	if (map_id==0){
		map_id=Mathf.Ceil(Random.value*(gameindex.levels.length-1));
	}
		
	//assign map properties based on selection
	if (map_id==1){
		map_title=gameindex.levels[1];
		map.renderer.material.SetTexture("_MainTex",terrain[0]);
		scaleFactor=1.11;
		count=10;
		Camera.main.orthographicSize=5.55;}
	if (map_id==2){
		map_title=gameindex.levels[2];
		map.renderer.material.SetTexture("_MainTex",terrain[1]);
		scaleFactor=1.31;
		count=12;
		Camera.main.orthographicSize=5.55;}
	if (map_id==3){
		map_title=gameindex.levels[3];
		map.renderer.material.SetTexture("_MainTex",terrain[3]);
		scaleFactor=0.71;
		count=6;
		Camera.main.orthographicSize=5.55;}
	if (map_id==4){
		map_title=gameindex.levels[4];
		map.renderer.material.SetTexture("_MainTex",terrain[2]);
		scaleFactor=0.91;
		count=8;
		Camera.main.orthographicSize=5.55;}
	
	//scale map		
	map.transform.localScale=Vector3(scaleFactor,scaleFactor,scaleFactor);

/*
	//set camera size
	Camera.main.orthographicSize=scaleFactor*5;
	if (Camera.main.orthographicSize>5.5){
		Camera.main.orthographicSize=5.5;
	}
*/
	yield;
}
function DebugMap(): IEnumerator{
	map_id=255;
	grid=2;
	count=6;
	Camera.main.orthographicSize=5.55;
	var scaleFactor: float = 0.71;
	map.transform.localScale=Vector3(scaleFactor,scaleFactor,scaleFactor);

	var terrain: Texture2D = Resources.Load("terrain/turf_grass") as Texture2D;
	map.renderer.material.SetTexture("_MainTex",terrain);

	yield;
}
var cells_labeled: short;
function CellGenerator(): IEnumerator{
	cellMaster=Instantiate(cellMasterPrefab,transform.position,Quaternion.identity);
	
	var cell: GameObject;
	
	var startX:short = 0-(count/2)*grid; 
	var startY:short = 0-(count/2)*grid;
	var startZ:short = 0-(count/2)*grid;

	cells_labeled=0;

	var createX:short;
	var createY:short;
	var createZ:short;
	
	//front & back
	for (createZ=startZ; createZ<=startZ+count*grid; createZ+=count*grid){
		for (createY=startY; createY<=startY+count*grid; createY+=grid){//repeat if wall max height not reached
			for (createX=startX; createX<=startX+count*grid; createX+=grid){//repeat if wall max width not reached
				//Debug.Log(cellPrefab+""+createX+""+createY+""+createZ);
				cell = Instantiate(cellPrefab, Vector3(createX, createY, createZ), Quaternion.identity);
				cell.transform.parent=cellMaster.transform;
			}
		}
	}	
	
	//floor & ceiling
	for (createY=startY; createY<=startY+count*grid; createY+=count*grid){	
		for (createZ=startZ+grid; createZ<=startZ+((count-1)*grid); createZ+=grid){
			for (createX=startX; createX<=startX+count*grid; createX+=grid){
				cell=Instantiate (cellPrefab, Vector3(createX, createY, createZ), Quaternion.identity);
				cell.transform.parent=cellMaster.transform;
			}
		}
	}
	
	//left & right
	for (createX=startX; createX<=startX+count*grid; createX+=count*grid){	
		for (createY=startY+grid; createY<=startY+((count-1)*grid); createY+=grid){	
			for (createZ=startZ+grid; createZ<=startZ+((count-1)*grid); createZ+=grid){
				cell=Instantiate (cellPrefab, Vector3(createX, createY, createZ), Quaternion.identity);
				cell.transform.parent=cellMaster.transform;
			}
		}
	}
}
function CellChecker(): IEnumerator{
	while(true){
		var cell_count:short = GameObject.FindGameObjectsWithTag("cell").length;
		if (cell_count==cells_labeled){
			break;
		}
		Debug.Log("checking cells");
		yield;
	}
}
///
function SpawnZoneGenerator(): IEnumerator{
//spawn zone setup variables
	var spawnzoneLocation: Vector3;

	var spawntype:byte = gameindex.spawnselection;
	var cell_list=GameObject.FindGameObjectsWithTag("cell");

	//face spawnzones
	if (spawntype==0){
		var location: short;
		for (var i: byte=0; i<=5; i++){
			if (i<3) {location=count/2*grid;} else {location=0-count/2*grid;}
			if (i==0 || i==3){spawnzoneLocation=Vector3(0,0,location);}
			if (i==1 || i==4){spawnzoneLocation=Vector3(0,location,0);}
			if (i==2 || i==5){spawnzoneLocation=Vector3(location,0,0);}
			var spawnzone: GameObject = Instantiate(spawnZonePrefabs[0],spawnzoneLocation,Quaternion.identity);
			spawnzone.transform.localScale=Vector3(10,10,2);
			spawnzone.GetComponent(SpawnZoneSetup).spawntype=spawntype;
		}
	}
		
	//corner spawnzones
	if (spawntype==1){
		var spawnzoneX=0;
		var spawnzoneY=0;
		var spawnzoneZ=0;
		for (i=0; i<=7; i++){
			if (i<4) {spawnzoneY=count/2*grid-grid;} 
				else {spawnzoneY=0-(count/2*grid-grid);}
			if (i<2 || i>5) {spawnzoneX=count/2*grid-grid;} 
				else {spawnzoneX=0-(count/2*grid-grid);}
			if (i==0 || i==2 || i==4 || i==6) {spawnzoneZ=count/2*grid-grid;} 
				else {spawnzoneZ=0-(count/2*grid-grid);}
			spawnzoneLocation=Vector3(spawnzoneX,spawnzoneY,spawnzoneZ);
			spawnzone=Instantiate(spawnZonePrefabs[1],spawnzoneLocation,Quaternion.identity);
			//spawnzone.transform.parent=transform;
			spawnzone.transform.localScale=Vector3(6,6,6);
			spawnzone.GetComponent(SpawnZoneSetup).spawntype=spawntype;
		}
	}	
}
function CreatePlayManager(): IEnumerator{	
	playManager = Instantiate(playManagerPrefab,transform.position,Quaternion.identity);
	playManager.transform.parent=transform;
	playManager.name="PlayManager";
	yield;
}
function HeroSpawner(player: byte): IEnumerator{
	
	var hero_numbers: int[]=[0,1014,1024,1034,1044,1054,1064,1074,1084];
		
	if (gameindex.playerTeams[player]>0){
		//randomize random players
		if(gameindex.playerTeams[player]==9){
			gameindex.playerTeams[player]=Mathf.RoundToInt(Random.value*7)+1;
		}
		//subtract 1 from players' teams (teams now 1-8)
		//gameindex.playerTeams[player]-=1;
		
		var nodelist: GameObject[] = GameObject.FindGameObjectsWithTag("spawnnode");
		
		while(true){
			var randomnode: GameObject = nodelist[Mathf.Floor(Random.value*nodelist.length)];
			if (randomnode.GetComponent(SpawnNodeSetup).mycell){
				var randomcell: GameObject = randomnode.GetComponent(SpawnNodeSetup).mycell;
				var cellproperties: CellProperties = randomcell.GetComponent(CellProperties);
				if (cellproperties.occA==0 && cellproperties.occB==0){break;}
			}
		}
		
		Destroy(randomnode.GetComponent(SpawnNodeSetup).myzone);
			
		//create hero @ spawn_point	
		var hero: GameObject = Instantiate(objectPrefab,randomcell.transform.position,Quaternion.identity);
		//set hero objno
		hero.GetComponent(ObjectStats).objno=hero_numbers[gameindex.playerTeams[player]];
		//set hero owner
		hero.GetComponent(ObjectStats).owner=player;
		//add to queue
		queue.queuelist.Add(hero);						
	}
}
function DebugHeroes(): IEnumerator{

	var hero_numbers: int[]=[0,1013,1021,1034,1041,1054,1062,1074,1084];

	var spawnCells: Vector3[] = new Vector3[5];
	spawnCells[1]=Vector3(3,3,0);
	spawnCells[2]=Vector3(4,5,0);
	spawnCells[3]=Vector3(5,4,0);
	spawnCells[4]=Vector3(5,5,0);
	
	var spawnPoint: Vector3; //world point of spawning
	
	var i: byte;
	for (i=1; i<spawnCells.length; i++){
		spawnPoint=CellNumToWorldPoint(spawnCells[i]);
		
		//randomize random teams
		if(gameindex.playerTeams[i]==9){
			gameindex.playerTeams[i]=Mathf.RoundToInt(Random.value*7)+1;
		}
		//subtract 1 from players' teams (teams now 1-8)
		//gameindex.playerTeams[i]-=1;
		
		var hero: GameObject = Instantiate(objectPrefab,spawnPoint,Quaternion.identity);
		hero.GetComponent(ObjectStats).objno=hero_numbers[gameindex.playerTeams[i]];
		//hero.GetComponent(ObjectStats).objno=hero_numbers[i];
		hero.GetComponent(ObjectStats).owner=i;
		queue.queuelist.Add(hero);						
	}
	yield;
}
function DestroySpawnZones(): IEnumerator{
	var	zonelist: GameObject[] = GameObject.FindGameObjectsWithTag("spawnzone");
	for (var i: byte=0; i<zonelist.length; i++){
		Destroy(zonelist[i]);
	}
}
function PrepareFirstUnit(): IEnumerator{
	queue.currentunit=queue.queuelist[0] as GameObject;
	yield queue.ListShuffle(queue.queuelist as List.<GameObject>);
	yield queue.Advance();
	gui_game.View(queue.queuelist[0]);
	Camera.main.GetComponent(CamMain).CamFocus(queue.queuelist[0]);
	yield;
}
///
function CreateObstacleMaster(): IEnumerator{
	obstacleMaster=Instantiate(obstacleMasterPrefab,transform.position,Quaternion.identity);
	obstacleMaster.name="ObstacleMaster";
	var identifier = playManager.GetComponent(Identifier);
	identifier.obstacleMaster=obstacleMaster;
	
}
function DebugObstacles(): IEnumerator{

	var obstacles: int[] = new int[5];
	var spawnCells: Vector3[] = new Vector3[5];
	spawnCells[0]=Vector3(3,4,0);  obstacles[0]=3401;
	spawnCells[1]=Vector3(1,1,0);  obstacles[1]=3401;
	spawnCells[2]=Vector3(5,3,0);  obstacles[2]=3401;
	spawnCells[3]=Vector3(2,2,0);  obstacles[3]=3401;
	spawnCells[4]=Vector3(0,4,0);  obstacles[4]=3401;
	
	var spawnPoint: Vector3; //world point of spawning
	
	var i: byte;
	for (i=1; i<spawnCells.length; i++){
		spawnPoint=CellNumToWorldPoint(spawnCells[i]);
		var obstacle: GameObject = Instantiate(objectPrefab,spawnPoint,Quaternion.identity);
		obstacle.GetComponent(ObjectStats).objno=obstacles[i];
	}
	yield;
}
function CellNumToWorldPoint(gameCoord: Vector3): Vector3 {

	var positionX: int = (gameCoord.x*grid)-count;
	var positionY: int = (gameCoord.y*grid)-count;
	var positionZ: int = (gameCoord.z*grid)-count;
	
	var position: Vector3 =  Vector3(positionX,positionY,positionZ);
	return position;
}