#pragma strict
static var gameindexprefab: GameObject;

static var cell_tex: Texture[] = new Texture[6]; //all values set in inspector
	
var gameX: byte; 
var gameY: byte; 
var gameZ: byte;
	
static var count: short;
static var grid: byte;
var cellPosition: Vector3;
var shellprefabs: GameObject[] = new GameObject[3];
var shell: GameObject;

//enums
	//core
static var eMOB: byte = 6;
static var eObclass: byte = 8;
	
	//mob
static var eGND: byte=1;
static var eTRM: byte=2;
static var eFLY: byte=3;
static var eGAS: byte=4;
	
	
function Awake() {//give cell game coordinates & rotation
	//retrieve count & grid from map_properties
	gameindexprefab=GameObject.Find("GameIndexPrefab");
	var fightingstart: FightingStart = gameindexprefab.GetComponent(FightingStart);
	count=fightingstart.count;
	grid=fightingstart.grid;
	//find physical space position of cell
	cellPosition = transform.position;
	GameCoord(cellPosition);
	
	name="Cell - ("+gameX+","+gameY+","+gameZ+")";
	CellRotate(gameX,gameY,gameZ);
	fightingstart.cells_labeled+=1;	
}

function GameCoord(position:Vector3){//gameXYZ based on transform.position
	gameX=(position.x+count)/grid;
	gameY=(position.y+count)/grid;
	gameZ=(position.z+count)/grid;
}

function CellRotate(gameX:int,gameY:int,gameZ:int){//shell instantiation + cell rotation
	//assign cell orientations (eulerAngles rotates Z, then X, then Y)
	//back face
	if (gameZ==count && (gameY!=count && gameY!=0) && (gameX!=count && gameX!=0)){
		shell=Instantiate(shellprefabs[0],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(0,0,0);}
	//front face
	if (gameZ==0 && (gameY!=count && gameY!=0) && (gameX!=count && gameX!=0))	{
		shell=Instantiate(shellprefabs[0],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(180,0,0);}
	//bottom face
	if (gameY==0 && (gameX!=count && gameX!=0) && (gameZ!=count && gameZ!=0))	{
		shell=Instantiate(shellprefabs[0],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(90,0,0);}
	//top face
	if (gameY==count && (gameX!=count && gameX!=0) && (gameZ!=count && gameZ!=0)){
		shell=Instantiate(shellprefabs[0],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(270,0,0);}
	//left face
	if (gameX==0 && (gameY!=count && gameY!=0) && (gameZ!=count && gameZ!=0))	{
		shell=Instantiate(shellprefabs[0],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(0,270,0);}
	//right face
	if (gameX==count && (gameY!=count && gameY!=0) && (gameZ!=count && gameZ!=0)){
		shell=Instantiate(shellprefabs[0],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(0,90,0);}

	//top-back edge
	if ((gameY==count)&&(gameZ==count)&&((gameX!=0)||(gameX!=count))){
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(0,0,0);}
	//bottom-back edge
	if ((gameY==0)&&(gameZ==count)&&((gameX!=0)||(gameX!=count)))	{
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(90,0,0);}
	//bottom-front edge
	if ((gameY==0)&&(gameZ==0)&&((gameX!=0)||(gameX!=count)))		{
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(180,0,0);}
	//top-front edge
	if ((gameY==count)&&(gameZ==0)&&((gameX!=0)||(gameX!=count)))	{
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(270,0,0);}
	//top-right edge
	if ((gameX==count)&&(gameY==count)&&((gameZ!=0)||(gameZ!=count))){
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(0,90,0);}
	//top-left edge
	if ((gameX==0)&&(gameY==count)&&((gameZ!=0)||(gameZ!=count)))	{
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(0,270,0);}
	//bottom-right edge
	if ((gameY==0)&&(gameX==count)&&((gameZ!=0)||(gameZ!=count)))	{
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(90,0,270);}
	//bottom-left edge	
	if ((gameY==0)&&(gameX==0)&&((gameZ!=0)||(gameZ!=count)))		{
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(90,180,270);}
	//front-left edge
	if ((gameX==0)&&(gameZ==0)&&((gameY!=0)||(gameY!=count)))		{
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(0,270,90);}
	//front-right edge
	if ((gameX==count)&&(gameZ==0)&&((gameY!=0)||(gameY!=count)))	{
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(0,90,270);}
	//back-right edge
	if ((gameX==count)&&(gameZ==count)&&((gameY!=0)||(gameY!=count))){
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(0,0,270);}
	//back-left edge
	if ((gameX==0)&&(gameZ==count)&&((gameY!=0)||(gameY!=count)))	{
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(0,0,90);}
	
	//back-top-left corner
	if((gameX==0)&&(gameY==count)&&(gameZ==count)){
		shell=Instantiate(shellprefabs[2],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(0,0,0);}
	//front-bottom-left corner
	if((gameX==0)&&(gameY==0)&&(gameZ==0))		{
		shell=Instantiate(shellprefabs[2],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(180,0,0);}
	//front-bottom-right corner
	if((gameX==count)&&(gameY==0)&&(gameZ==0))	{
		shell=Instantiate(shellprefabs[2],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(0,90,180);}
	//front-top-left corner
	if((gameX==0)&&(gameY==count)&&(gameZ==0))	{
		shell=Instantiate(shellprefabs[2],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(0,270,0);}
	//front-top-right corner
	if((gameX==count)&&(gameY==count)&&(gameZ==0)){
		shell=Instantiate(shellprefabs[2],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(0,180,0);}
	//back-bottom-left corner
	if((gameX==0)&&(gameY==0)&&(gameZ==count))	{
		shell=Instantiate(shellprefabs[2],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(90,0,0);}
	//back-bottom-right corner
	if((gameX==count)&&(gameY==0)&&(gameZ==count)){
		shell=Instantiate(shellprefabs[2],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(0,0,180);}
	//back-top-right corner
	if((gameX==count)&&(gameY==count)&&(gameZ==count)){
		shell=Instantiate(shellprefabs[2],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameX+","+gameY+","+gameZ+")";
		transform.eulerAngles = Vector3(0,90,0);}
}

var legal: boolean = false;
var selected=false;
	
var hit : RaycastHit;
static var celllist: GameObject[];

function Update () {//update texture if legal / check if cell selected
	//face
	if (((gameX==0||gameX==count) && (gameY!=0&&gameY!=count) && (gameZ!=0&&gameZ!=count))||
		((gameY==0||gameY==count) && (gameX!=0&&gameX!=count) && (gameZ!=0&&gameZ!=count))||
		((gameZ==0||gameZ==count) && (gameY!=0&&gameY!=count) && (gameX!=0&&gameX!=count))){
			if (legal==false){renderer.material.SetTexture("_MainTex",cell_tex[0]);}
			if (legal==true){renderer.material.SetTexture("_MainTex",cell_tex[1]);}
	}
	
	//edge
	if (((gameX==0||gameX==count) && (gameY==0||gameY==count) && (gameZ!=0&&gameZ!=count))||
		((gameX==0||gameX==count) && (gameZ==0||gameZ==count) && (gameY!=0&&gameY!=count))||
		((gameZ==0||gameZ==count) && (gameY==0||gameY==count) && (gameX!=0&&gameX!=count))){
			if (legal==false){renderer.material.SetTexture("_MainTex",cell_tex[2]);}
			if (legal==true){renderer.material.SetTexture("_MainTex",cell_tex[3]);}
	}

	//corner
	if ((gameX==0||gameX==count)&&(gameY==0||gameY==count)&&(gameZ==0||gameZ==count)){
		if (legal==false){renderer.material.SetTexture("_MainTex",cell_tex[4]);}
		if (legal==true){renderer.material.SetTexture("_MainTex",cell_tex[5]);}
	}
		
}

var spawn_number: byte;
var noded: boolean;
var occupied: boolean = false;
var occA: byte = 0;
var occB: byte = 0;

var objectstats: ObjectStats;

function OnTriggerStay(object:Collider){//if unit, obstacle or spawnnode is in cell
		
	if (object.GetComponent(ObjectStats)){

		objectstats=object.GetComponent(ObjectStats);
		
		//set unit cell data
		objectstats.mycell=gameObject;
		objectstats.gameX=gameX;
		objectstats.gameY=gameY;
		objectstats.gameZ=gameZ;

		//unit
		if (objectstats.coreStats[eMOB]){
			//set cell occupancy
			var mob: byte = objectstats.coreStats[eMOB];
			if (mob==eGND || mob==eTRM){occA=1;}
			if (mob==eFLY){occB=1;}
		}
			
		//obstacle
		if (objectstats.coreStats[eObclass]){
			switch (objectstats.coreStats[eObclass]){
				case 4: occA=2; break;
				case 3: occA=3; break;
				case 2: occA=4; break;
				case 1: occA=5;
			}
		}	
		occupied=true;
	}	
	//spawnnode
	if (object.CompareTag("spawnnode")){
		noded=true;
		spawn_number=object.GetComponent(SpawnNodeSetup).spawn_number;
	}
}



function OnTriggerEnter(object:Collider){//embed units & obstacles in map
	if (object.GetComponent(ObjectStats)){
		if (gameX==0){object.transform.position.x-=1;}
		if (gameX==count){object.transform.position.x+=1;}
		if (gameY==0){object.transform.position.y-=1;}
		if (gameY==count){object.transform.position.y+=1;}
		if (gameZ==0){object.transform.position.z-=1;}
		if (gameZ==count){object.transform.position.z+=1;}
	}
}


function OnTriggerExit(object:Collider){//when unit or obstacle leaves
	if (object.CompareTag("unit") || object.CompareTag("obstacle")){
		objectstats=object.GetComponent(ObjectStats);
		occupied=true;
		if (objectstats.coreStats[eMOB] && objectstats.coreStats[eMOB]==eFLY){
			occB=0;
		}
		else{
			occA=0;
		}
	}
}



