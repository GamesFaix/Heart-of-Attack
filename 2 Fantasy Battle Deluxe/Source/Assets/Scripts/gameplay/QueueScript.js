#pragma strict

var queuelist=new List.<GameObject>();
var currentunit: GameObject;
var lastunit: GameObject;
var upkeep: boolean=false;
var actionCoord: ActionCoordinator;
var gui_game: GUI_Game;

//enums
	//core
static var eHP: byte = 0;
static var eINIT: byte = 3;
static var eAP: byte = 4;
static var eOwner: byte = 9;
static var eObjno: byte = 7;	

	//actNums
static var eRNG: byte = 1;

function Awake(){
	gui_game=GameObject.Find("GUIPrefab").GetComponent(GUI_Game);
}

function Update () {
	if (queuelist.Count>1){
		currentunit=queuelist[0];
		lastunit=queuelist[queuelist.Count-1];
	}
}

function Advance(): IEnumerator{
	yield EndPhase();
	var lastStats: ObjectStats = lastunit.GetComponent(ObjectStats);
	var currentStats: ObjectStats = lastunit.GetComponent(ObjectStats);
	queuelist.RemoveAt(0);
	if(lastStats.coreStats[eINIT]<currentStats.coreStats[eINIT] && lastStats.skipped==false){
		queuelist.Add(lastunit);
		queuelist[queuelist.Count-2]=currentunit;
		lastunit.GetComponent(ObjectStats).skipped=true;
	}
	else{
		queuelist.Add(currentunit);
	}
	currentunit=queuelist[0];
	gui_game.currentunit=currentunit;
	yield UpkeepPhase();
}

function ListShuffle(list: List.<GameObject>): IEnumerator{
	//shuffle starting at beginning
	for (var i: short=0; i<list.Count; i++){
		//pick random array index
		var random: short = Mathf.Floor(Random.value*list.Count);
		//remember current index entry
		var temp: GameObject = list[i];
		//fill current index with random index entry
		list[i] = list[random];
		//fill random index with remembered entry
		list[random] = temp;
	}
	
	//shuffle again starting at end, just to be good...and thorough
	for (i=list.Count-1; i>=0; i--){
		random = Mathf.Floor(Random.value*list.Count);
		temp = list[i];
		list[i]=list[random];
		list[random]=temp;
	}
}
function EndPhase(): IEnumerator{
	var unitstats: ObjectStats = currentunit.GetComponent(ObjectStats);
	unitstats.coreStats[eAP]=0;
	//electrical stun recover
	if (unitstats.elcRAD>0){
		unitstats.elcRAD-=1;
		if (unitstats.elcRAD==0){
			unitstats.coreStats[eINIT]++;
			unitstats.actNums[1,eRNG]=unitstats.actNums[1,eRNG]+unitstats.elcSPreduction;
		}
	}	
	//ninjoid reset
	if (unitstats.coreStats[eObjno]==1011 && unitstats.endTimer>0){
		if (unitstats.endTimer%2==1){//if timer odd
			unitstats.actNums[1,eRNG]=unitstats.actNums[1,eRNG]-4;
		}
		unitstats.endTimer-=1;
	}
	//torch of thaw reset
	if (unitstats.thaw>0){
		unitstats.thaw-=1;
		unitstats.coreStats[eINIT]-=1;
		unitstats.actNums[1,eRNG]=unitstats.actNums[1,eRNG]-1;
		actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s "+unitstats.objname+" froze. (-1IN, -1SP)");
	}
	yield;
}
function UpkeepPhase(): IEnumerator{
	var unitstats: ObjectStats = currentunit.GetComponent(ObjectStats);
	unitstats.coreStats[eAP]+=2;
	unitstats.skipped=false;
	if (unitstats.upkeepTimer>0){
		unitstats.upkeepTimer-=1;
	}
	//poison damage
	if (unitstats.psnDMG>0 && unitstats.psnRAD>0){
		var dmg: byte = Mathf.Floor(unitstats.psnDMG*unitstats.psnDEC);
		unitstats.coreStats[eHP]-=dmg;
		unitstats.psnRAD-=1;
		unitstats.psnDMG=dmg;
		actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s "+unitstats.objname+" took "+dmg+" poison damage.");
	}
	yield;
}