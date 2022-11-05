#pragma strict
var gameindexprefab: GameObject;

var cell_tex: Texture[] = new Texture[6]; //all values set in inspector

var gameCoord: Vector3;
	
var count: short;
var grid: byte;
var cellPosition: Vector3;
var shellprefabs: GameObject[] = new GameObject[3];
var shell: GameObject;
	
function Awake() {//give cell game coordinates & rotation
	//retrieve count & grid from map_properties
	gameindexprefab=GameObject.Find("GameIndexPrefab");
	var createWorld: CreateWorld = gameindexprefab.GetComponent(CreateWorld);
	count=createWorld.count;
	grid=createWorld.grid;
	//find physical space position of cell
	cellPosition = transform.position;
	GameCoord(cellPosition);
	
	name="Cell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
	CellRotate(gameCoord);
	createWorld.cells_labeled+=1;	
}
function GameCoord(position:Vector3){//gameCoord.xYZ based on transform.position
	gameCoord.x=(position.x+count)/grid;
	gameCoord.y=(position.y+count)/grid;
	gameCoord.z=(position.z+count)/grid;
}
function CellRotate(gameCoord: Vector3){//shell instantiation + cell rotation
	//assign cell orientations (eulerAngles rotates Z, then X, then Y)
	//back face
	if (gameCoord.z==count && (gameCoord.y!=count && gameCoord.y!=0) && (gameCoord.x!=count && gameCoord.x!=0)){
		shell=Instantiate(shellprefabs[0],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(0,0,0);}
	//front face
	if (gameCoord.z==0 && (gameCoord.y!=count && gameCoord.y!=0) && (gameCoord.x!=count && gameCoord.x!=0))	{
		shell=Instantiate(shellprefabs[0],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(180,0,0);}
	//bottom face
	if (gameCoord.y==0 && (gameCoord.x!=count && gameCoord.x!=0) && (gameCoord.z!=count && gameCoord.z!=0))	{
		shell=Instantiate(shellprefabs[0],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(90,0,0);}
	//top face
	if (gameCoord.y==count && (gameCoord.x!=count && gameCoord.x!=0) && (gameCoord.z!=count && gameCoord.z!=0)){
		shell=Instantiate(shellprefabs[0],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(270,0,0);}
	//left face
	if (gameCoord.x==0 && (gameCoord.y!=count && gameCoord.y!=0) && (gameCoord.z!=count && gameCoord.z!=0))	{
		shell=Instantiate(shellprefabs[0],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(0,270,0);}
	//right face
	if (gameCoord.x==count && (gameCoord.y!=count && gameCoord.y!=0) && (gameCoord.z!=count && gameCoord.z!=0)){
		shell=Instantiate(shellprefabs[0],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(0,90,0);}

	//top-back edge
	if ((gameCoord.y==count)&&(gameCoord.z==count)&&((gameCoord.x!=0)||(gameCoord.x!=count))){
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(0,0,0);}
	//bottom-back edge
	if ((gameCoord.y==0)&&(gameCoord.z==count)&&((gameCoord.x!=0)||(gameCoord.x!=count)))	{
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(90,0,0);}
	//bottom-front edge
	if ((gameCoord.y==0)&&(gameCoord.z==0)&&((gameCoord.x!=0)||(gameCoord.x!=count)))		{
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(180,0,0);}
	//top-front edge
	if ((gameCoord.y==count)&&(gameCoord.z==0)&&((gameCoord.x!=0)||(gameCoord.x!=count)))	{
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(270,0,0);}
	//top-right edge
	if ((gameCoord.x==count)&&(gameCoord.y==count)&&((gameCoord.z!=0)||(gameCoord.z!=count))){
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(0,90,0);}
	//top-left edge
	if ((gameCoord.x==0)&&(gameCoord.y==count)&&((gameCoord.z!=0)||(gameCoord.z!=count)))	{
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(0,270,0);}
	//bottom-right edge
	if ((gameCoord.y==0)&&(gameCoord.x==count)&&((gameCoord.z!=0)||(gameCoord.z!=count)))	{
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(90,0,270);}
	//bottom-left edge	
	if ((gameCoord.y==0)&&(gameCoord.x==0)&&((gameCoord.z!=0)||(gameCoord.z!=count)))		{
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(90,180,270);}
	//front-left edge
	if ((gameCoord.x==0)&&(gameCoord.z==0)&&((gameCoord.y!=0)||(gameCoord.y!=count)))		{
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(0,270,90);}
	//front-right edge
	if ((gameCoord.x==count)&&(gameCoord.z==0)&&((gameCoord.y!=0)||(gameCoord.y!=count)))	{
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(0,90,270);}
	//back-right edge
	if ((gameCoord.x==count)&&(gameCoord.z==count)&&((gameCoord.y!=0)||(gameCoord.y!=count))){
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(0,0,270);}
	//back-left edge
	if ((gameCoord.x==0)&&(gameCoord.z==count)&&((gameCoord.y!=0)||(gameCoord.y!=count)))	{
		shell=Instantiate(shellprefabs[1],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(0,0,90);}
	
	//back-top-left corner
	if((gameCoord.x==0)&&(gameCoord.y==count)&&(gameCoord.z==count)){
		shell=Instantiate(shellprefabs[2],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(0,0,0);}
	//front-bottom-left corner
	if((gameCoord.x==0)&&(gameCoord.y==0)&&(gameCoord.z==0))		{
		shell=Instantiate(shellprefabs[2],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(180,0,0);}
	//front-bottom-right corner
	if((gameCoord.x==count)&&(gameCoord.y==0)&&(gameCoord.z==0))	{
		shell=Instantiate(shellprefabs[2],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(0,90,180);}
	//front-top-left corner
	if((gameCoord.x==0)&&(gameCoord.y==count)&&(gameCoord.z==0))	{
		shell=Instantiate(shellprefabs[2],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(0,270,0);}
	//front-top-right corner
	if((gameCoord.x==count)&&(gameCoord.y==count)&&(gameCoord.z==0)){
		shell=Instantiate(shellprefabs[2],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(0,180,0);}
	//back-bottom-left corner
	if((gameCoord.x==0)&&(gameCoord.y==0)&&(gameCoord.z==count))	{
		shell=Instantiate(shellprefabs[2],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(90,0,0);}
	//back-bottom-right corner
	if((gameCoord.x==count)&&(gameCoord.y==0)&&(gameCoord.z==count)){
		shell=Instantiate(shellprefabs[2],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(0,0,180);}
	//back-top-right corner
	if((gameCoord.x==count)&&(gameCoord.y==count)&&(gameCoord.z==count)){
		shell=Instantiate(shellprefabs[2],cellPosition,Quaternion.identity);
		shell.transform.parent=transform;
		shell.name="Shell - ("+gameCoord.x+","+gameCoord.y+","+gameCoord.z+")";
		transform.eulerAngles = Vector3(0,90,0);}
}

var legal: boolean = false;
var selected=false;
	
var hit : RaycastHit;
var celllist: GameObject[];

function Update () {//update texture if legal / check if cell selected
	//face
	if (((gameCoord.x==0||gameCoord.x==count) && (gameCoord.y!=0&&gameCoord.y!=count) && (gameCoord.z!=0&&gameCoord.z!=count))||
		((gameCoord.y==0||gameCoord.y==count) && (gameCoord.x!=0&&gameCoord.x!=count) && (gameCoord.z!=0&&gameCoord.z!=count))||
		((gameCoord.z==0||gameCoord.z==count) && (gameCoord.y!=0&&gameCoord.y!=count) && (gameCoord.x!=0&&gameCoord.x!=count))){
			if (legal==false){renderer.material.SetTexture("_MainTex",cell_tex[0]);}
			if (legal==true){renderer.material.SetTexture("_MainTex",cell_tex[1]);}
	}
	
	//edge
	if (((gameCoord.x==0||gameCoord.x==count) && (gameCoord.y==0||gameCoord.y==count) && (gameCoord.z!=0&&gameCoord.z!=count))||
		((gameCoord.x==0||gameCoord.x==count) && (gameCoord.z==0||gameCoord.z==count) && (gameCoord.y!=0&&gameCoord.y!=count))||
		((gameCoord.z==0||gameCoord.z==count) && (gameCoord.y==0||gameCoord.y==count) && (gameCoord.x!=0&&gameCoord.x!=count))){
			if (legal==false){renderer.material.SetTexture("_MainTex",cell_tex[2]);}
			if (legal==true){renderer.material.SetTexture("_MainTex",cell_tex[3]);}
	}

	//corner
	if ((gameCoord.x==0||gameCoord.x==count)&&(gameCoord.y==0||gameCoord.y==count)&&(gameCoord.z==0||gameCoord.z==count)){
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
		objectstats.gameCoord.x=gameCoord.x;
		objectstats.gameCoord.y=gameCoord.y;
		objectstats.gameCoord.z=gameCoord.z;

		//unit
		if (objectstats.mob){
			//set cell occupancy
			var mob: byte = objectstats.mob;
			if (mob==1 || mob==2){occA=1;}
			if (mob==3){occB=1;}
		}
			
		//obstacle
		if (objectstats.obclass){
			if (objectstats.obclass==4){occA=2;}
			if (objectstats.obclass==3){occA=3;}
			if (objectstats.obclass==2){occA=4;}
			if (objectstats.obclass==1){occA=5; occB=1;}
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
		if (gameCoord.x==0){object.transform.position.x-=1;}
		if (gameCoord.x==count){object.transform.position.x+=1;}
		if (gameCoord.y==0){object.transform.position.y-=1;}
		if (gameCoord.y==count){object.transform.position.y+=1;}
		if (gameCoord.z==0){object.transform.position.z-=1;}
		if (gameCoord.z==count){object.transform.position.z+=1;}
	}
}


function OnTriggerExit(object:Collider){//when unit or obstacle leaves
	if (object.CompareTag("unit") || object.CompareTag("obstacle")){
		objectstats=object.GetComponent(ObjectStats);
		occupied=true;
		if (objectstats.mob && objectstats.mob==3){
			occB=0;
		}
		else{
			occA=0;
		}
	}
}



