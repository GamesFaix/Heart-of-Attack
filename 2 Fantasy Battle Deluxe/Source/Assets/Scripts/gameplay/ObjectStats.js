#pragma strict

var obprefab: GameObject;

//general
var objname: String;
var thumb: Texture2D;
var sprite: Texture2D;

var mycell: GameObject;	//current cell being occupied, set by cells ontrigger
var gameX: byte;
var gameY: byte; 
var gameZ: byte;

//enums
	//core
static var eMOB: byte = 6;
static var eObjno: byte = 7;
static var eOwner: byte = 9;
static var eCorpsetype: byte = 10;

//unit
var comp: boolean[] = new boolean[2];
var composition: String;

var coreStats: short[] = new short[11];
var actText : String[,] = new String[10,2];
var actNums : int[,] = new int[10,10];

var morph: byte; //for morphing units (tank/fortress) (castle/dragon)
var bombs: byte; //pterrordactyl
var armor: byte; //demolitia, panopticlops
var master: GameObject; //mines

var psnRAD: byte; 
var psnDEC: float; 
var psnDMG: byte;

var elcRAD: byte; 
var elcSPreduction: byte;

var thaw: byte;
var portalA: boolean;
var otherGate: GameObject;
var upkeepTimer: float = 0; 
var endTimer: float = 0;

var skipped: boolean; //skipped on queue

//obstacles
var obtype: String;

//other objects
static var gameindexprefab: GameObject;
static var gui_game: GUI_Game;
static var playmanager: GameObject;
static var actionCoord: ActionCoordinator;
static var identifier: Identifier1;
var plane: GameObject; //plane prefab

function Awake(){
	gameindexprefab=GameObject.Find("GameIndexPrefab");
	playmanager=GameObject.Find("PlayManager");
	actionCoord=playmanager.GetComponent(ActionCoordinator);
	identifier=playmanager.GetComponent(Identifier1);
	gui_game=GameObject.Find("GUIPrefab").GetComponent(GUI_Game);
}
function Start(){
	var me: GameObject = this.gameObject;
	yield ClearActData();
	yield identifier.Identity(me,coreStats[eObjno],coreStats[eOwner]);
	yield CreateSprite();
}
function Update(){	
	PivotSprite();
}
function CreateSprite(): IEnumerator{
	var mysprite: GameObject;
	mysprite = Instantiate(plane,Vector3(transform.position.x,transform.position.y+1,transform.position.z),Quaternion.identity);
	mysprite.transform.parent=transform;
	mysprite.name="Sprite - "+name;
	//Debug.Log("me: "+transform.position+"/sprite: "+mysprite.transform.position);
}
function PivotSprite(){
	var camRot: Vector3 = Camera.main.transform.eulerAngles;
	transform.rotation.eulerAngles=Vector3(camRot.x-90,camRot.y,camRot.z);
}
function Die(){
	var queue: QueueScript = gameindexprefab.GetComponent(QueueScript);
	var mycellstats: CellProperties = mycell.GetComponent(CellProperties);
	
	if(coreStats[eOwner]){
		var i: short;
		for (i=0; i<queue.queuelist.Count; i++){
			if (queue.queuelist[i]==gameObject){
				var spot: short = i;
				break;
			}
		}
		//remove from queue
		queue.queuelist.RemoveAt(spot);
	
		//report death in mLog
		var gui_game: GUI_Game = GameObject.Find("GUIPrefab").GetComponent(GUI_Game);
		gui_game.mlog.Add("Player"+coreStats[eOwner]+"'s "+objname+" was destroyed.");
	}
	//create corpse 
	if (coreStats[eCorpsetype]==1){
		if (coreStats[eMOB]!=3 || (coreStats[eMOB]==3 && mycellstats.occA==0)){
			var myCorpse: GameObject;
			myCorpse= Instantiate(obprefab,mycell.transform.position,Quaternion.identity);
			myCorpse.GetComponent(ObjectStats).coreStats[eObjno]=3401;
		}
	}
	if (coreStats[eCorpsetype]==2){
		if (mycellstats.occA==0){
			myCorpse= Instantiate(obprefab,mycell.transform.position,Quaternion.identity);
			myCorpse.GetComponent(ObjectStats).coreStats[eObjno]=2031;
			myCorpse.GetComponent(ObjectStats).coreStats[eOwner]=coreStats[eOwner];
			queue.queuelist.Add(myCorpse);
		}
	}
	
	//vacate cell
	mycellstats.occA=0;
	mycellstats.occB=0;
	//destroy object
	Destroy(gameObject);
}
/*
function OnTriggerEnter(object: Collider){
	if (object.gameObject.GetComponent(ObjectStats)){

		//Debug.Log("co-occupying objects");
		var spriteScript: SpriteDisplay1 = gameObject.GetComponentInChildren(SpriteDisplay1);
		spriteScript.other=object.gameObject;
	}
}
*/

function ClearActData(): IEnumerator{
	var i: byte; var j: byte;
	for (i=0; i<=9; i++){
		for (j=0; j<=1; j++){
			actText[i,j]="";
		}	
	}
	for (i=0; i<=9; i++){
		for (j=0; j<=9; j++){
			actNums[i,j]=0;
		}	
	}
	
	yield;
}