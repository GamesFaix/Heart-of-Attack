#pragma strict

var queuelist=new List.<GameObject>();
var currentunit: GameObject;
var lastunit: GameObject;
var upkeep: boolean=false;
var actionCoord: ActionCoordinator;
var gui_game: GUI_Game;

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
	if(lastStats.init<currentStats.init && lastStats.skipped==false){
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
	unitstats.ap=0;
	//electrical stun recover
	if (unitstats.elcRAD>0){
		unitstats.elcRAD-=1;
		if (unitstats.elcRAD==0){
			unitstats.init++;
			unitstats.actNums[1,1]=unitstats.actNums[1,1]+unitstats.elcSPreduction;
		}
	}	
	//ninjoid reset
	if (unitstats.objno==1011 && unitstats.endTimer>0){
		if (unitstats.endTimer%2==1){//if timer odd
			unitstats.actNums[1,1]=unitstats.actNums[1,1]-4;
		}
		unitstats.endTimer-=1;
	}
	//torch of thaw reset
	if (unitstats.thaw>0){
		unitstats.thaw-=1;
		unitstats.init-=1;
		unitstats.actNums[1,1]=unitstats.actNums[1,1]-1;
		actionCoord.Mlog("Player"+unitstats.owner+"'s "+unitstats.objname+" froze. (-1IN, -1SP)");
	}
	yield;
}
function UpkeepPhase(): IEnumerator{
	var unitstats: ObjectStats = currentunit.GetComponent(ObjectStats);
	unitstats.ap+=2;
	unitstats.skipped=false;
	if (unitstats.upkeepTimer>0){
		unitstats.upkeepTimer-=1;
	}
	//poison damage
	if (unitstats.psnDMG>0 && unitstats.psnRAD>0){
		var dmg: byte = Mathf.Floor(unitstats.psnDMG*unitstats.psnDEC);
		unitstats.hp-=dmg;
		unitstats.psnRAD-=1;
		unitstats.psnDMG=dmg;
		actionCoord.Mlog("Player"+unitstats.owner+"'s "+unitstats.objname+" took "+dmg+" poison damage.");
	}
	yield;
}
function ToTop(unit: GameObject){
	queuelist.Remove(unit);
	queuelist.Insert(1,unit);
}
function ToBtm(unit: GameObject){
	queuelist.Remove(unit);
	queuelist.Add(unit);
}