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
var eFunc: byte=1;
var eDesc: byte=2;

var eAp: byte=0;
var eFp: byte=1;
var emRng: byte=2;
var eRng: byte=3;
var eMag: byte=4;
var eDec: byte=5;
var eRad: byte=6;
var eCrz: byte=7;
//	
//vars to make sure action isnt used twice
var actUsed: boolean[]=new boolean[10];
for (var i: byte=0; i<=9; i+=1){actUsed[i]=false;}

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
	
	var func: String = unitstats.actText[act1to9,eFunc];
	Debug.Log(func);
	var ap: float = unitstats.actNums[act1to9,eAp]; 
	var fp: float = unitstats.actNums[act1to9,eFp];
	var mrng: float = unitstats.actNums[act1to9,emRng];
	var rng: float = unitstats.actNums[act1to9,eRng]; 
	var mag: float = unitstats.actNums[act1to9,eMag]; 
	var dec: float = unitstats.actNums[act1to9,eDec]; 
	var rad: float = unitstats.actNums[act1to9,eRad]; 
	var crz: float = unitstats.actNums[act1to9,eCrz];

	//check if action valid
	if(actUsed[act1to9]==false && unitstats.ap>=ap && unitstats.fp>=fp && unit==currentunit){
		//pay	
		DeductCost(unitstats,act1to9);
		//perform	
		//move
		if (func=="MovSerpGND"){yield actions.MovSerpGND(unit, rng);}
		if (func=="MovSerpTRM"){yield actions.MovSerpTRM(unit, rng);}
		if (func=="MovSerpFLY"){yield actions.MovSerpFLY(unit, rng);}
		//if (func=="MovSerpGAS"){yield actions.MovSerpGAS(unit, rng);}
		if (func=="MovLinGND"){yield actions.MovLinGND(unit, rng);}
		if (func=="MovLinTRM"){yield actions.MovLinTRM(unit, rng);}
		if (func=="MovLinFLY"){yield actions.MovLinFLY(unit, rng);}
		
		//focus
		if (func=="Focus"){
			Debug.Log("focusing");
			yield actions.Focus(unit, mag);}
		
		//attack
		if (func=="AtkSerpNRM"){yield actions.AtkSerpNRM(unit, rng, mag);}
		if (func=="AtkSerpPSN"){yield actions.AtkSerpPSN(unit, rng, mag, dec, rad);}
		if (func=="AtkSerpELC"){yield actions.AtkSerpELC(unit, rng, mag, rad);}
		//if (func=="AtkSerpFIR"){yield actions.AtkSerpFIR(unit, rng, mag, dec, rad);}
		if (func=="AtkLinNRM"){yield actions.AtkLinNRM(unit, rng, mag);}
		//if (func=="AtkLinFIR"){yield actions.AtkLinFIR(unit, rng, mag, dec, rad);}
		//if (func=="AtkLinLSR"){yield actions.AtkLinLSR(unit, rng, mag, dec, rad);}
		if (func=="AtkArcNRM"){yield actions.AtkArcNRM(unit, rng, mag);}
		if (func=="AtkArcEXP"){yield actions.AtkArcEXP(unit,rng,mag,dec,crz,rad);}
		if (func=="AtkArcPSN"){yield actions.AtkArcPSN(unit, rng, mag, dec, rad);}
		if (func=="AtkRage"){yield actions.AtkRage(unit, rng, mag, dec);}
		if (func=="AtkLeech"){yield actions.AtkLeech(unit, rng, mag);}
		
		//creation
		if (func=="CreateGND"){yield actions.CreateGND(unit, mag);}
		if (func=="CreateTRM"){yield actions.CreateTRM(unit, mag);}
		if (func=="CreateFLY"){yield actions.CreateFLY(unit, mag);}
		//if (func=="CreateGAS"){yield actions.CreateGAS(unit, mag);}
		if (func=="CreateSentinel"){yield actions.CreateSentinel(unit);}
		if (func=="CreateMetaterrainean"){yield actions.CreateMetaterrainean(unit);}
		if (func=="CreatePortalGate"){yield actions.CreatePortalGate(unit,mag);}
		//if (func=="CreateVoidGate"){yield actions.CreateVoidGate(unit);}
		//if (func=="CreateCorpseFiend"){yield actions.CreateCorpseFiend(unit);}
		
		//mode flip
		if (func=="ModeTank"){yield actions.ModeTank(unit);}
		if (func=="ModeDragon"){yield actions.ModeDragon(unit);}
		if (func=="ModeMagman"){yield actions.ModeMagman(unit);}		
		
		//specials
		if (func=="SpcSprint"){yield actions.SpcSprint(unit);}
		//if (func=="SpcLaserSpin"){yield actions.SpcLaserSpin(unit,rng,mag,dec);}
		if (func=="SpcFortifyShield"){yield actions.SpcFortifyShield(unit);}
		if (func=="SpcBarrage"){yield actions.SpcBarrage(unit, mag, dec, rad, crz);}
		if (func=="SpcStockpile"){yield actions.SpcStockpile(unit);}
		//if (func=="SpcTeleportFriendly"){yield actions.SpcStockpile(unit, rng);}
		//if (func=="SpcAdvancedPlating"){yield actions.SpcStockpile(unit, rng);}		
		//
		if (func=="SpcEnhanceArmor"){yield actions.SpcEnhanceArmor(unit);}
		//if (func=="SpcDetonate"){yield actions.SpcDetonate(unit);}
		if (func=="SpcFortify"){yield actions.SpcFortify(unit);}
		//if (func=="SpcTacticleMissile"){yield actions.SpcTacticleMissile(unit, rng, mag);}		
		//
		if (func=="SpcArise"){yield actions.SpcArise(unit);}
		if (func=="SpcGoodMourning"){yield actions.SpcGoodMourning(unit,rng,mag);}
		if (func=="SpcWindUp"){yield actions.SpcWindUp(unit);}
		//if (func=="SpcPhoenixPickup"){yield actions.SpcPickup(unit, rng);}
		//if (func=="SpcPhoenixDrop"){yield actions.SpcDrop(unit, rng);}
		//if (func=="SpcRambuchetize"){yield actions.SpcRambuchetize(unit, rng, mag);}
		//if (func=="SpcMomentum"){yield actions.SpcDetonate(unit, rng, mag, dec);}
		//if (func=="SpcRamPickup"){yield actions.SpcDetonate(unit, rng);}
		//if (func=="SpcCorpseFling"){yield actions.SpcDetonate(unit, rng, mag)
		//if (func=="SpcTailSpin"){yield actions.SpcTailSpin(unit, rng, mag, dec);}
		//if (func=="SpcSlashBurn"){yield actions.SpcDetonate(unit, rng, mag, dec, rad);};}
		//
		if (func=="SpcConjureTerrain"){yield actions.SpcConjureTerrain(unit);}
		if (func=="SpcBurial"){yield actions.SpcBurial(unit, mag);}
		//if (func=="SpcTransportEnemy"){yield actions.SpcDetonate(unit, rng, mag);}
		if (func=="SpcConsumeTerrain"){yield actions.SpcConsumeTerrain(unit,mag);}
		//if (func=="SpcStampede"){yield actions.SpcStampede(unit, rng, rad);}
		if (func=="SpcAuralDischarge"){yield actions.SpcAuralDischarge(unit);}
		if (func=="SpcTorchOfThaw"){yield actions.SpcTorchOfThaw(unit);}
		//
		if (func=="SpcLoad"){yield actions.SpcLoad(unit);}
		if (func=="SpcQuickdraw"){yield actions.SpcQuickdraw(unit,rng,mag,unitstats.bombs);}
		if (func=="SpcPatience"){yield actions.SpcPatience(unit);}
		if (func=="SpcTimeahawk"){yield actions.SpcTimeahawk(unit,rng,mag,rad);}
		//if (func=="SpcSpiritTimeBomb"){yield actions.SpcSpiritTimeBomb(unit, rng, mag, rad);}
		if (func=="SpcSecondInCommand"){yield actions.SpcSecondInCommand(unit,rng);}
		//if (func=="SpcHourSavioiur"){yield actions.SpcHourSaviour(unit);}
		//
		if (func=="SpcEvolve"){yield actions.SpcEvolve(unit, mag);}
		if (func=="SpcDeathSting"){yield actions.SpcDeathSting(unit, rng, mag, dec, rad);}
		if (func=="SpcInfestCorpse"){yield actions.SpcInfestCorpse(unit);}
		//if (func=="SpcWebshot"){yield actions.SpcWebshot(unit, rng, mag, rad);}
		//if (func=="SpcSwarm"){yield actions.SpcSwarm(unit);}
		//if (func=="SpcMegadearth"){yield actions.SpcMegadearth(unit, dec, rad);}
		//
		//if (func=="SpcMnemonicPlague"){yield actions.SpcMnemonicPlague(unit, mag);}
		//if (func=="SpcAccumulate"){yield actions.SpcAccumulate(unit, rng);}
		//if (func=="SpcTractorGust"){yield actions.SpcTractorGust(unit, rng);}
		//if (func=="SpcForceShove"){yield actions.SpcForceShove(unit, rng, mag, dec);}
		//if (func=="SpcSuperShove"){yield actions.SpcSuperShove(unit, rng, mag, dec);}
		//if (func=="SpcRestoration"){yield actions.SpcRestoration(unit, rng, dec);}		
		//if (func=="SpcTeleportEnemy"){yield actions.SpcTeleportEnemy(unit, rng, rad);}		
		//if (func=="SpcLightningStorm"){yield actions.SpcLightningStorm(unit, rng, mag, dec, rad);}		
		//
		if (func=="SpcCannibalize"){yield actions.SpcCannibalize(unit, mag);}
		//if (func=="SpcExplode"){yield actions.SpcExplode(unit, mag, dec, rad, crz);}
		//if (func=="SpcMoveCorpse"){yield actions.SpcMoveCorpse(unit, rng);}
		//if (func=="SpcContagion"){yield actions.SpcContagion(unit);}		
		//if (func=="SpcSacrifize"){yield actions.SpcSacrifize(unit, rng);}
		//if (func=="SpcDevour"){yield actions.SpcDevour(unit, rng, dmg, dec);}		
		
		//mark action as used
		actUsed[act1to9]=true;
	}
	else{
		if (unit!=currentunit){Error("It's not your turn.");}
		else if (actUsed[act1to9]==true){Error("Action already used.");}
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
	actUsed[act1to9]=false;
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


