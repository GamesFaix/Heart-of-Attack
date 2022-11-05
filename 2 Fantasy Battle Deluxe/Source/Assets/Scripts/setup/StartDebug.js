#pragma strict

var mapPrefab: GameObject;
var cellMasterPrefab: GameObject;
var cellPrefab: GameObject;
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
function CreateWorld(){
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
function CreatePlayManager(): IEnumerator{	
	playManager = Instantiate(playManagerPrefab,transform.position,Quaternion.identity);
	playManager.transform.parent=transform;
	playManager.name="PlayManager";
	yield;
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
		if(gameindex.player_team_numbers[i]==1){
			gameindex.player_team_numbers[i]=Mathf.RoundToInt(Random.value*7)+2;
		}
		//subtract 1 from players' teams (teams now 1-8)
		gameindex.player_team_numbers[i]-=1;
		
		var hero: GameObject = Instantiate(objectPrefab,spawnPoint,Quaternion.identity);
		hero.GetComponent(ObjectStats).objno=hero_numbers[gameindex.player_team_numbers[i]];
		//hero.GetComponent(ObjectStats).objno=hero_numbers[i];
		hero.GetComponent(ObjectStats).owner=i;
		queue.queuelist.Add(hero);						
	}
	yield;
}
function PrepareFirstUnit(): IEnumerator{
	queue.currentunit=queue.queuelist[0] as GameObject;
	yield queue.ListShuffle(queue.queuelist as List.<GameObject>);
	yield queue.Advance();
	gui_game.View(queue.queuelist[0]);
	yield;
}
///
function CreateObstacleMaster(): IEnumerator{
	obstacleMaster=Instantiate(obstacleMasterPrefab,transform.position,Quaternion.identity);
	obstacleMaster.name="ObstacleMaster";
	var identifier = playManager.GetComponent(Identifier1);
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