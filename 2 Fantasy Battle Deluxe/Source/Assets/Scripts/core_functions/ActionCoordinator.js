#pragma strict

var guiprefab: GameObject;
var gui_game: GUI_Game;
var queue: QueueScript;
var actions: Actions;

var currentunit: GameObject;
var currentunitstats: ObjectStats;
var currentact1to9: byte;

//enums
var eActionName: byte=0;
var eDesc: byte=1;

var eAction: byte=0;
var eAp: byte=1;
var eFp: byte=2;
var eRng: byte=3;
var eMag: byte=4;
var eDec: byte=5;
var eRad: byte=6;
var eCrz: byte=7;
//	

//setup
function Start(){
	queue=GameObject.Find("GameIndexPrefab").GetComponent(QueueScript);
	queue.actionCoord=this;
	StartCoroutine("CoStart");
}
function CoStart() : IEnumerator{
	while (true){yield CoUpdate();}
}
function CoUpdate() : IEnumerator{
	if (queue.currentunit){
		currentunit=queue.currentunit;
		currentunitstats=currentunit.GetComponent(ObjectStats);
	}
	if (currentact1to9!=0){
		PerformAction(currentunit,currentact1to9);
		ResetAction();
	}
	
}
function OnEnable(){
	guiprefab=GameObject.Find("GUIPrefab");
	gui_game=guiprefab.GetComponent(GUI_Game);
	actions=gameObject.GetComponent(Actions);
	gui_game.actionCoord=this;
}

function PerformAction(unit: GameObject, act1to9: byte){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	
	var action: float = unitstats.actNums[act1to9,eAction];
	var ap: float = unitstats.actNums[act1to9,eAp]; 
	var fp: float = unitstats.actNums[act1to9,eFp];
	var rng: float = unitstats.actNums[act1to9,eRng]; 
	var mag: float = unitstats.actNums[act1to9,eMag]; 
	var dec: float = unitstats.actNums[act1to9,eDec]; 
	var rad: float = unitstats.actNums[act1to9,eRad]; 
	var crz: float = unitstats.actNums[act1to9,eCrz];
	var used: boolean = gui_game.actUsed[act1to9];

	//check if action valid
	if(used==false && unitstats.ap>=ap && unitstats.fp>=fp && unit==currentunit){
		//pay	
		DeductCost(unitstats,act1to9);
		//perform	
		//move
		if (action==100010){yield actions.MovSerpGND(unit, rng);}//serp GND
		if (action==100011){yield actions.MovSerpTRM(unit, rng);}//serp TRM
		if (action==100012){yield actions.MovSerpFLY(unit, rng);}//serp FLY
		if (action==100014){yield actions.MovLinGND(unit, rng);}//lin GND
		if (action==100015){yield actions.MovLinTRM(unit, rng);}//lin TRM
		if (action==100016){yield actions.MovLinFLY(unit, rng);}//lin FLY
		//focus
		if (action==100020){yield actions.Focus(unit, mag);}//focus
		//attack
		if (action==100030){yield actions.AtkSerpNRM(unit, rng, mag);}//serp normal
		if (action==100031){yield actions.AtkLinNRM(unit, rng, mag);}//lin normal
		if (action==100032){yield actions.AtkRage(unit, rng, mag, dec);}//rage
		if (action==100033){yield actions.AtkLeech(unit, rng, mag);}//leech life
		if (action==100034){yield actions.AtkSerpPSN(unit, rng, mag, dec, rad);}//serp psn
		if (action==100035){yield actions.AtkSerpELC(unit, rng, mag, rad);}//serp elc
		if (action==100036){yield actions.AtkArcNRM(unit, rng, mag);}//arc normal
		//creation
		if (action==100071){yield actions.CreateGND(unit, mag);}//create GND
		if (action==100072){yield actions.CreateTRM(unit, mag);}//create TRM
		if (action==100073){yield actions.CreateFLY(unit, mag);}//create FLY
		//specials
		if (action==101141){yield actions.A101141(unit);}//ninjoid - sprint
		if (action==101251){yield actions.A101251(unit);}//sentinel - fortify shield
		if (action==101341){yield actions.A101341(unit, mag, dec, rad, crz);}//pterror - barrage
		if (action==101351){yield actions.A101351(unit);}//pterror - stockpile
		if (action==101481){yield actions.A101481(unit);}//satellite - create sentinel
		//
		if (action==102131){yield actions.A102131(unit,rng,mag,dec,crz,rad);}//demolitia - grenade
		if (action==102141){yield actions.A102141(unit);}//demolitia - enhance armor	
		if (action==102341){yield actions.A102341(unit);}//panopticlops - fortify
		if (action==102461){yield actions.A102461(unit);}//robotank - mode flip
		//
		if (action==203110){yield actions.A203110(unit);}//ashes - arise
		if (action==103131){yield actions.A103131(unit,rng,mag);}//mournking - morningstar
		if (action==103141){yield actions.A103141(unit);}//mournking - wind up
		if (action==103461){yield actions.A103461(unit);}//castledragon - mode flip
		//
		if (action==104141){yield actions.A104141(unit);}//grizzly elder - conjure terrain
		if (action==104151){yield actions.A104151(unit, mag);}//grizzly - burial
		if (action==104341){yield actions.A104341(unit,mag);}//golem - consume terrain
		if (action==104451){yield actions.A104451(unit);}//yeti - aural discharge
		if (action==104461){yield actions.A104461(unit);}//yeti - torch of thaw
		if (action==104491){yield actions.A104491(unit);}//yeti - create meta-terrainean
		//
		if (action==105141){yield actions.A105141(unit);}//gunslinger - load
		if (action==105151){yield actions.A105151(unit,rng,mag,unitstats.bombs);}//gunslinger - quick draw
		if (action==105241){yield actions.A105241(unit);}//piecemaker - patience
		if (action==105251){yield actions.A105251(unit,mag);}//piecemaker - open portal gate
		if (action==105331){yield actions.A105331(unit,rng,mag,rad);}//chieftomaton - time-a-hawk
		if (action==105451){yield actions.AtkArcNRM(unit,rng,mag);}//grand dad - marksman
		if (action==105461){yield actions.A105461(unit,rng);}//grand dad - second in command
		//
		if (action==106041){yield actions.A106041(unit, mag);}//larva - evolve
		if (action==106141){yield actions.A106141(unit, rng, mag, dec, rad);}//bee - death sting
		if (action==106231){yield actions.A106231(unit, rng, mag, dec, rad);}//shrooman - spore
		if (action==106241){yield actions.A106241(unit);}//infest corpse
		//
		if (action==108141){yield actions.A108141(unit, mag);}//corpse fiend - cannibalize
		if (action==108341){yield actions.A108341(unit);}//magman - mode flip
		
		//mark action as used
		gui_game.actUsed[act1to9]=true;
	}
	else{
		if (unit!=currentunit){Error("It's not your turn.");}
		else if (used==true){Error("Action already used.");}
		else if (unitstats.ap<ap || unitstats.fp<fp){Error("Not enough ap/fp.");}
	}	
	ResetAction();
}

//utility functions
function DeductCost(unitstats:ObjectStats, act1to9: byte){//deducts ap/fp cost of actions
	var ap: byte = unitstats.actNums[act1to9,eAp]; 
	var fp: byte = unitstats.actNums[act1to9,eFp]; 
	
	unitstats.ap-=ap;
	unitstats.fp-=fp;
}

function Error(error: String){
	gui_game.error=error;
}

function Mlog(message: String){//adds message to message log
	gui_game.mlog.Add(message);
}

function Refund(act1to9:byte, error:String){//refunds cost of action if cancelled, displays errors
	currentunitstats.ap+=currentunitstats.actNums[act1to9,eAp];
	currentunitstats.fp+=currentunitstats.actNums[act1to9,eFp];
	gui_game.actUsed[act1to9]=false;
	Error(error);
}

function ResetAction(){//resets all main action variables
	currentact1to9=0;
	ResetCells();
}
function ResetCells(){//reset all cells legality
	var celllist: GameObject[] = GameObject.FindGameObjectsWithTag("cell");
	var i: short;
	for (i=0; i<celllist.length; i++){
		celllist[i].GetComponent(CellProperties).legal=false;
	}	
}


