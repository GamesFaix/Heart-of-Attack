#pragma strict

var targeting: Targeting;
var effects: Effects;
var actionCoord: ActionCoordinator;

//enums
	//core
static var eHP: byte = 0;	
static var eMHP: byte = 1;	
static var eDEF: byte = 2;	
static var eINIT: byte = 3;	
static var eCoreAP: byte = 4;	
static var eCoreFP: byte = 5;	
static var eMOB: byte = 6;	
static var eObjno: byte = 7;	
static var eObclass: byte = 8;	
static var eOwner: byte = 9;	
static var eCorpsetype: byte = 10;	
	//act text
static var eName: byte = 0;	
static var eDesc: byte = 1;	
	//act nums
static var eAction: byte = 0;	
static var eActAP: byte = 1;	
static var eActFP: byte = 2;	
static var eRNG: byte = 3;	
static var eMAG: byte = 4;	
static var eDEC: byte = 5;	
static var eRAD: byte = 6;	
static var eCRZ: byte = 7;	
static var eTAR: byte = 8;	
static var eDMGType: byte = 9;	
	//mob
static var eGND: byte = 1;	
static var eTRM: byte = 2;	
static var eFLY: byte = 3;	
static var eGAS: byte = 4;	
	
	
function OnEnable(){
	targeting=gameObject.GetComponent(Targeting);
	effects=gameObject.GetComponent(Effects);
	actionCoord=gameObject.GetComponent(ActionCoordinator);
}

//movement
function A100010(unit: GameObject, rng: float){//serp GND
	if(targeting.T111(rng, unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoMoves();}
	effects.E202(unit,targeting.targetcell);
	yield;
}
function A100011(unit: GameObject, rng: float){//serp TRM
	if(targeting.T122(rng, unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoMoves();}
	effects.E202(unit,targeting.targetcell);
	effects.E205(unit,targeting.targetcell);
	yield;
}
function A100012(unit: GameObject, rng: float){//serp FLY
	if(targeting.T144(rng, unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoMoves();}
	effects.E202(unit,targeting.targetcell);
	yield;
}
function A100014(unit: GameObject, rng: float){//lin GND
	if(targeting.T211(rng, unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoMoves();}
	effects.E202(unit,targeting.targetcell);
	yield;
}				
function A100015(unit: GameObject, rng: float){//lin TRM
	if(targeting.T222(rng, unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoMoves();}
	effects.E202(unit,targeting.targetcell);
	effects.E205(unit,targeting.targetcell);
	yield;
}				
function A100016(unit: GameObject, rng: float){//lin FLY
	if(targeting.T244(rng, unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoMoves();}
	effects.E202(unit,targeting.targetcell);
	yield;
}
//focus
function A100020(unit: GameObject, mag: float){//focus
	effects.E305(unit,mag);
	yield;
}				
//attack
function A100030(unit: GameObject, rng: float, mag: float){//serp - normal damage
	if(targeting.T135(rng,unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.T000(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		effects.E100(unit,mag,targeting.targetobject);
	}
	yield;
}					
function A100031(unit: GameObject, rng: float, mag: float){//lin - normal damage
	if(targeting.T235(rng,unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.T000(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		effects.E100(unit,mag,targeting.targetobject);
	}
	yield;
}				
function A100032(unit: GameObject, rng: float, mag: float, dec: float){//rage - attack and damage self
	if(targeting.T135(rng,unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.T000(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		effects.E100(unit,mag,targeting.targetobject);
		effects.E300(unit,dec);
	}
	yield;
}				
function A100033(unit: GameObject, rng: float, mag: float){//leech life
	if(targeting.T135(rng,unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.T000(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		var targetstats=targeting.targetobject.GetComponent(ObjectStats);
		var preLeech=targetstats.coreStats[eHP];
		effects.E100(unit,mag,targeting.targetobject);
		var hpChange=preLeech-targetstats.coreStats[eHP];
		if (hpChange>0){effects.E300(unit,hpChange);}
	}
	yield;
}				
function A100034(unit: GameObject, rng: float, mag: float, dec: float, rad: float){//serp - psn damaga
	if(targeting.T135(rng,unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.T000(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		effects.E103(unit,mag,dec,rad,targeting.targetobject);
	}
	yield;
}
function A100035(unit: GameObject, rng: float, mag: float, rad: float){//serp - elc damage
	if(targeting.T135(rng,unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.T000(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		effects.E104(unit,mag,rad,targeting.targetobject);
	}
	yield;
}//unit creation
//create
function A100071(unit: GameObject, mag: float){//create unit GND
	if(targeting.T111(1, unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoMoves();}
	effects.E200(unit,mag,targeting.targetcell);
	yield;
}		
function A100072(unit: GameObject, mag: float){//create unit TRM
	if(targeting.T122(1, unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	effects.E200(unit,mag,targeting.targetcell);
	effects.E205(targeting.targetobject,targeting.targetcell);
	yield;
}
function A100073(unit: GameObject, mag: float){//create unit FLY
	if(targeting.T144(1, unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	effects.E200(unit,mag,targeting.targetcell);
	yield;
}

//ninjoid
function A101141(unit: GameObject){//sprint - reset incomplete
	var unitstats=unit.GetComponent(ObjectStats);
	unitstats.actNums[1,eRNG]=unitstats.actNums[1,eRNG]+4;
	unitstats.endTimer+=2;
	actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s Ninjoid +4SP ("+unitstats.actNums[1,eRNG]+") until end of next turn.");
	yield;
}
//sentinel
function A101251(unit: GameObject){//fortify shield - all unit not just friendlies
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	var childTrans: Transform = unit.GetComponentInChildren(Transform);
	var child: GameObject = childTrans.gameObject;
	if (child.GetComponent(SentinelShield)){
		var shield: GameObject = child;
	}
	//fortify
	if (shield.GetComponent(SentinelShield).def<6){
		shield.GetComponent(SentinelShield).def++;
		var unitlist: GameObject[] = GameObject.FindGameObjectsWithTag("unit");
		var i: short;
		for (i=0; i<unitlist.length; i++){
			var otherstats=unitlist[i].GetComponent(ObjectStats);
			if (targeting.IsAdjacent(unitstats.mycell,otherstats.mycell)==true
			&& unit!=unitlist[i]){
				otherstats.coreStats[eDEF]++;
			}
		}
		actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s Sentinel's shield +1DEF ("+shield.GetComponent(SentinelShield).def+").");
	}
	else {actionCoord.Refund(5,"Shield maxed.");}
	yield;
}
//pterrordactyl
function A101351(unit: GameObject){//stockpile
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	unitstats.bombs+=1;
	actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s "+unitstats.objname+" +1Bomb ("+unitstats.bombs+").");
	yield;
}		
//satellite
function A101481(unit: GameObject){//create sentinel + shield
	if(targeting.T111(1, unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoMoves();}
	effects.E200(unit,1012,targeting.targetcell);
	effects.newobject.GetComponent(ObjectStats).mycell=targeting.targetcell;
	yield;
}
//demolitia
/*
function A102131(unit: GameObject, rng, mag, dec, crz, rad){//grenade
	var unitstats=unit.GetComponent(ObjectStats);
	if(targeting.T333(unit,rng,unitstats.mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.T000(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		effects.E100(unit,mag,targeting.targetobject);
	}
	yield;
}	*/	
	
function A102141(unit: GameObject){//enhance armor
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if (unitstats.armor<4){
		unitstats.armor+=1;
		unitstats.coreStats[eDEF]+=1;
		actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s Demolitia +1DEF ("+unitstats.coreStats[eDEF]+")");
	}
	else {
		actionCoord.Refund(4,"Max armor.");
	}
	yield;
}
//condor
/*
function A102241(){//lay mine
					if (phase==1){
						targetcell=null;
						targetfunctions.ResetTarget();
						targetfunctions.Target135(1,startcell);
						phase++;}
					if (targetcell) {phase+=1;}
					if (phase==3){
						effect.E200(2021,targetcell);
						newob.GetComponent(ObjectStats).mycell=targetcell;
						phase++;}
					if (phase==4){
						actionCoord.ResetAction();
					}

				}
*/

//panopticlops
function A102341(unit: GameObject){//fortify
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if (unitstats.armor<2){
		unitstats.armor++;
		if (unitstats.armor==1){unitstats.sprite=Resources.Load("sprites/sprite1023B") as Texture2D;}
		if (unitstats.armor==2){unitstats.sprite=Resources.Load("sprites/sprite1023C") as Texture2D;}
		unitstats.coreStats[eDEF]++;
		unitstats.actNums[3,eRNG]=unitstats.actNums[3,eRNG]+1;
		unitstats.actNums[5,eRNG]=unitstats.actNums[5,eRNG]+1;
		actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s Panopticlops +1DEF ("+unitstats.coreStats[eDEF]+") / +1RNG (Lob "+unitstats.actNums[3,eRNG]+" / Tactical Missile "+unitstats.actNums[5,eRNG]+").");
	}
	else {
		actionCoord.Refund(4,"Max armor.");
	}
	yield;
}
//robotank
function A102461(unit: GameObject){//mode flip
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if(unitstats.morph==0){
		unitstats.coreStats[eDEF]+=3;
		unitstats.actNums[1,eAction]=0;
		unitstats.actText[6,eName]="Tank Mode";
		unitstats.thumb=Resources.Load("thumbs/thumb1024B") as Texture2D;
		unitstats.sprite=Resources.Load("thumbs/thumb1024B") as Texture2D;
		actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s RoboTank Fortress +3DEF ("+unitstats.coreStats[eDEF]+") / Immobilized.");
	}
	if(unitstats.morph==1){
		unitstats.coreStats[eDEF]-=3;
		unitstats.actNums[1,eAction]=100015;
		unitstats.actText[6,eName]="Siege Mode";
		unitstats.thumb=Resources.Load("thumbs/thumb1024A") as Texture2D;
		unitstats.sprite=Resources.Load("sprites/sprite1024A") as Texture2D;
		actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s RoboTank Fortress -3DEF ("+unitstats.coreStats[eDEF]+") / Mobilized.");
	}
	if(unitstats.morph==0){unitstats.morph=1;}
	else {unitstats.morph=0;}
	yield;
}

//phoenix ashes
function A203110(unit: GameObject){//arise
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	yield effects.E206(unit,1032);
	actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s Phoenix arose from it's ashes.");
}
//mournking
function A103131(unit: GameObject, rng: float, mag: float){//morningstar
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if(targeting.T135(rng,unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.T000(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		effects.E100(unit,mag,targeting.targetobject);
	}
	unitstats.actNums[3,eRNG]=1;
	yield;
}
function A103141(unit: GameObject){//wind up
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	unitstats.actNums[3,eRNG]=unitstats.actNums[3,eRNG]+1;
	actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s Mournking +1RNG ("+unitstats.actNums[3,eRNG]+").");
	yield;
}			
//castle dragon
function A103461(unit: GameObject){//mode flip
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if (unitstats.morph==0){ 
		if(targeting.CheckBelow(unit)==false){
			actionCoord.Refund(6,"Ground occupied.");
		}
		if(targeting.CheckBelow(unit)==true){
			unitstats.coreStats[eMOB]=eGND;
			unitstats.mycell.GetComponent(CellProperties).occB=0;
			unitstats.coreStats[eDEF]+=3;
			unitstats.actNums[1,eAction]=0;
			unitstats.actNums[5,eAction]=0;
			unitstats.actText[6,eName]="Take Wing";
			unitstats.thumb=Resources.Load("thumbs/thumb1034B") as Texture2D;
			unitstats.sprite=Resources.Load("thumbs/thumb1034B") as Texture2D;
			actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s Castle Dragon +3DEF ("+unitstats.coreStats[eDEF]+") / Immobilized.");
			unitstats.morph=1;
		}
	}
	else if (unitstats.morph==1){
		if (targeting.CheckAbove(unit)==false){
			actionCoord.Refund(6,"Air occupied.");
		}
		if (targeting.CheckAbove(unit)==true){
			unitstats.coreStats[eMOB]=eFLY;
			unitstats.mycell.GetComponent(CellProperties).occA=0;
			unitstats.coreStats[eDEF]-=3;
			unitstats.actNums[1,eAction]=100012;
			unitstats.actNums[5,eAction]=103451;
			unitstats.actText[6,eName]="Castlize";
			unitstats.thumb=Resources.Load("thumbs/thumb1034A") as Texture2D;
			unitstats.sprite=Resources.Load("thumbs/thumb1034A") as Texture2D;
			actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s Castle Dragon +3DEF ("+unitstats.coreStats[eDEF]+") / Mobilized.");
			unitstats.morph=0;
		}
	}
	yield;
}
//grizzly elder				
function A104141(unit: GameObject){//conjure terrain
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if(targeting.T111(1, unitstats.mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoMoves();}
	var terrain: byte = Mathf.Floor(Random.value*2);
	var mag: float;
	if (terrain==0){mag=3301;}//tree
	if (terrain==1){mag=3302;}//boulder
	yield effects.E200(unit,mag,targeting.targetcell);
	actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s Grizzly Elder conjured a "+effects.newobject.GetComponent(ObjectStats).objname);
	yield;
}				
function A104151(unit: GameObject, mag: float){//burial
	var unitstats=unit.GetComponent(ObjectStats);
	if(targeting.T155(1,unitstats.mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {
		actionCoord.Refund(4,"No corpses.");
		actionCoord.ResetAction();
	}
	targeting.T001(targeting.targetcell);
	yield targeting.WaitForTargetobject();
	actionCoord.Mlog("Corpse buried.");
	targeting.targetobject.GetComponent(ObjectStats).Die();
	effects.E300(unit,mag);
	yield;
}
//meta-terrainean
function A104341(unit: GameObject, mag: float){//consume terrain
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if(targeting.T165(1,unitstats.mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {
		actionCoord.Refund(4,"No obstacles.");
		actionCoord.ResetAction();
	}
	targeting.T002(targeting.targetcell);
	yield targeting.WaitForTargetobject();
	var targetstats: ObjectStats = targeting.targetobject.GetComponent(ObjectStats);
	actionCoord.Mlog(targetstats.objname+" consumed.");
	if (targetstats.coreStats[eObjno]==3301){
		unitstats.thumb=Resources.Load("thumbs/thumb1043B") as Texture2D;
		unitstats.sprite=Resources.Load("thumbs/thumb1043B") as Texture2D;
	}
	if (targetstats.coreStats[eObjno]==3302){
		unitstats.thumb=Resources.Load("thumbs/thumb1043A") as Texture2D;
		unitstats.sprite=Resources.Load("thumbs/thumb1043A") as Texture2D;
	}
	if (targetstats.coreStats[eObjno]==3303){
		unitstats.thumb=Resources.Load("thumbs/thumb1043C") as Texture2D;
		unitstats.sprite=Resources.Load("thumbs/thumb1043C") as Texture2D;
	}
	targetstats.Die();
	effects.E300(unit,mag);
	yield;
}
//yeti
function A104451(unit: GameObject){//aural discharge
	var teammates = new List.<GameObject>();
	teammates= targeting.FindTeammates(unit,false);
	
	var i: short;
	for (i=0; i<teammates.Count; i++){
		var unitstats: ObjectStats = teammates[i].GetComponent(ObjectStats);
		var dmg: byte = Mathf.Ceil(unitstats.actNums[3,eMAG]*0.5);
		
		var unitlist: GameObject[] = GameObject.FindGameObjectsWithTag("unit");
		var j: short;
		for (j=0; j<unitlist.length; j++){
			var otherstats: ObjectStats = unitlist[j].GetComponent(ObjectStats);
			if (targeting.IsAdjacent(otherstats.mycell,unitstats.mycell)==true
			&& unitstats.coreStats[eOwner]!=otherstats.coreStats[eOwner]){
				effects.E300(unitlist[j],(0-dmg));
			}
		}
		effects.E300(teammates[i],(0-3));
	}
	yield;
}
function A104461(unit: GameObject){//torch of thaw
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	
	var teammates = new List.<GameObject>();
	teammates= targeting.FindTeammates(unit,false);
	
	var i: short;
	for (i=0; i<teammates.Count; i++){
		var otherstats: ObjectStats;
		otherstats=teammates[i].GetComponent(ObjectStats);
		otherstats.coreStats[eINIT]+=1;
		otherstats.actNums[1,eRNG]=unitstats.actNums[1,eRNG]+1;
		otherstats.thaw=1;
	}
	actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s units thawed. (+1IN, Move +1 RNG)");
	yield;
}
function A104491(unit: GameObject){//create golem
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if(targeting.T165(1,unitstats.mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {
		actionCoord.Refund(4,"No obstacles.");
		actionCoord.ResetAction();
	}
	targeting.T002(targeting.targetcell);
	var targetstats: ObjectStats = targeting.targetobject.GetComponent(ObjectStats);
	yield effects.E200(unit,1043,targeting.targetcell);
	var newstats: ObjectStats = effects.newobject.GetComponent(ObjectStats);
	if (targetstats.coreStats[eObjno]==3301){
		newstats.thumb=Resources.Load("thumbs/thumb1043B") as Texture2D;
		newstats.sprite=Resources.Load("thumbs/thumb1043B") as Texture2D;
	}
	if (targetstats.coreStats[eObjno]==3302){
		newstats.thumb=Resources.Load("thumbs/thumb1043A") as Texture2D;
		newstats.sprite=Resources.Load("thumbs/thumb1043A") as Texture2D;
	}
	if (targetstats.coreStats[eObjno]==3303){
		newstats.thumb=Resources.Load("thumbs/thumb1043C") as Texture2D;
		newstats.sprite=Resources.Load("thumbs/thumb1043C") as Texture2D;
	}
	targetstats.Die();
	yield;
}
//gunslinger
function A105141(unit: GameObject){//load
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	unitstats.bombs+=1;
	actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s Gunslinger +1Bullet ("+unitstats.bombs+").");
	yield;
}
function A105151(unit: GameObject, rng: float, mag: float, bombs: byte){//quick draw
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	var i: byte;
	for (i=0; i<bombs; i++){
		yield A100031(unit,rng,mag);
	}
	unitstats.bombs=0;
}
//piecemaker
function A105241(unit: GameObject){//patience
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	var unitlist: GameObject[] = GameObject.FindGameObjectsWithTag("unit");
	var i: short;
	for (i=0; i<unitlist.length; i++){
		var otherstats: ObjectStats = unitlist[i].GetComponent(ObjectStats);
		if (targeting.IsAdjacent(unitstats.mycell,otherstats.mycell)==true
		&& unit!=unitlist[i]
		&& unitstats.coreStats[eOwner]==otherstats.coreStats[eOwner]){
			effects.E300(unitlist[i],6);
		}
	}
	yield;
}
function A105251(unit: GameObject, mag: float){//open portal gate
	var unitstats: ObjectStats =  unit.GetComponent(ObjectStats);
	//find cell
	if(targeting.T111(1, unitstats.mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoMoves();}
	//destroy conflicting gates
	var gates: GameObject[] = GameObject.FindGameObjectsWithTag("portal");
	var i: byte;
	for(i=0; i<gates.length; i++){
		var gatestats: ObjectStats = gates[i].GetComponent(ObjectStats);
		if (gatestats.portalA==true && mag==0
		|| gatestats.portalA==false && mag==1){
			Destroy(gates[i]);
		}
	}
	//create new gate
	effects.E200(unit,2051,targeting.targetcell);
	var gate: GameObject = effects.newobject;
	gatestats = gate.GetComponent(ObjectStats);
	if (mag==0){
		gatestats.portalA=true;
		gate.name="Portal Gate (+)";
	}
	if (mag==1){
		gatestats.portalA=false;
		gate.name="Portal Gate (-)";
	}
	//find other gate
	gates = GameObject.FindGameObjectsWithTag("portal");
	for (i=0; i<gates.length; i++){
		if (gates[i]!=gate){
			gate.GetComponent(ObjectStats).otherGate=gates[i];
			gates[i].GetComponent(ObjectStats).otherGate=gate;
		}
	}
	yield;
}
//chieftomaton
function A105331(unit: GameObject, rng: float, mag: float, rad: float){//time-a-hawk
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if(targeting.T235(rng,unitstats.mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.T000(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		effects.E100(unit,mag,targeting.targetobject);
	}
	var targetstats: ObjectStats = targeting.targetobject.GetComponent(ObjectStats);
	if (targetstats.coreStats[eCoreFP]>0){
		targetstats.coreStats[eCoreFP]-=rad;
		actionCoord.Mlog("Player"+targetstats.coreStats[eOwner]+"'s "+targetstats.objname+" -1FP.");
	}
	yield;
}
//larva
function A106041(unit: GameObject, mag: float){//evolve
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	var owner: byte = unitstats.coreStats[eOwner];
	yield effects.E206(unit,mag);
	var newstats: ObjectStats = effects.newobject.GetComponent(ObjectStats);
	actionCoord.Mlog("Player"+owner+"'s larva evolved into a(n) "+newstats.objname+".");
	yield;
}
//beessassin
function A106141(unit: GameObject, rng: float, mag: float, dec: float, rad: float){//death sting
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if(targeting.T135(rng,unitstats.mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.T000(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		effects.E103(unit,mag,dec,rad,targeting.targetobject);
	}
	unitstats.Die();
	yield;
}
//corpse fiend
function A108141(unit: GameObject, mag: float){//cannibalize
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if (targeting.T155(1,unitstats.mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {
		actionCoord.Refund(4,"No corpses.");
		actionCoord.ResetAction();
	}
	if (targeting.T001(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		actionCoord.Mlog("Corpse consumed.");
		targeting.targetobject.GetComponent(ObjectStats).Die();
		unitstats.coreStats[eMHP]+=mag;
		effects.E300(unit,mag);
		unitstats.actNums[3,eMAG]=unitstats.actNums[3,eMAG]+2;
	}
	yield;
}
//magman
function A108341(unit: GameObject){//mode flip
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if(unitstats.morph==0){
		unitstats.actNums[1,eRNG]=unitstats.actNums[1,eRNG]-1;
		unitstats.coreStats[eHP]+=15;
		unitstats.coreStats[eMHP]+=15;
		unitstats.actNums[3,eAction]=100030;
		unitstats.actText[3,eName]="Rock Fist";
		unitstats.actNums[3,eActAP]=1;
		unitstats.actNums[3,eMAG]=13;
		unitstats.actNums[3,eRNG]=1;
		unitstats.actText[4,eName]="Melt";
		unitstats.actNums[4,eActAP]=2;
		unitstats.thumb=Resources.Load("thumbs/thumb1083B") as Texture2D;
		unitstats.sprite=Resources.Load("thumbs/thumb1083B") as Texture2D;
		actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s Magman +15HP ("+unitstats.coreStats[eHP]+") / Move -1 RNG ("+unitstats.actNums[1,eRNG]+" / Learned Rock Fist.");
	}
	if (unitstats.morph==1){
		unitstats.actNums[1,eRNG]=unitstats.actNums[1,eRNG]+1;
		unitstats.coreStats[eHP]-=15;
		unitstats.coreStats[eMHP]-=15;
		unitstats.actNums[3,eAction]=108431;
		unitstats.actText[3,eName]="Magma Fist";
		unitstats.actNums[3,eActAP]=2;
		unitstats.actNums[3,eMAG]=20;
		unitstats.actNums[3,eRNG]=2;
		unitstats.actText[4,eName]="Solidify";
		unitstats.coreStats[eCoreAP]=1;
		unitstats.thumb=Resources.Load("thumbs/thumb1083A") as Texture2D;
		unitstats.sprite=Resources.Load("thumbs/thumb1083A") as Texture2D;
		actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s Magman -15HP ("+unitstats.coreStats[eHP]+") / Move +1 RNG ("+unitstats.actText[1,eRNG]+" / Learned Magma Fist.");
	}
	if (unitstats.morph==0){unitstats.morph=1;}
	else {unitstats.morph=0;}
	yield;
}
//cthulhoid
/*	function A108471(){//create corpse fiend
					if (phase==1){
						targetcell=null;
						RNG=1;
						targetfunctions.Target155(startcell);
						phase++;
					}
					if (phase==2){ 
						//if no fp or corpses
						if (targetfunctions.AnyCells()==false && currentunitstats.fp==0){
							actionCoord.Refund(1,0,7,"No corpses or fp.");
							actionCoord.ResetAction();
						}
						//if fp, no corpses
						if (targetfunctions.AnyCells()==false && currentunitstats.fp>0){
							//check vacant cells
							targetfunctions.Target111(startcell);
							//if none
							if (targetfunctions.AnyCells()==false){
								actionCoord.Refund(1,0,7,"No empty cells.");
								actionCoord.ResetAction();
							}
							//if vacancies
							if (targetfunctions.AnyCells()==true){
								currentunitstats.fp-=1;
								phase++;
							}
						}
						//if corpses
						if (targetfunctions.AnyCells()==true){
							//if fp
							if (currentunitstats.fp>0){
								//check vacant cells
								targetfunctions.ResetTarget();
								targetfunctions.Target111(startcell);
								//if none
								if (targetfunctions.AnyCells()==false){
									var fpBtn=false;
									phase++;
								}
								//if vacancies
								if (targetfunctions.AnyCells()==true){
									fpBtn=true;
									phase++;
								}
							}
							if (currentunitstats.fp==0){
								fpBtn=false;
							}
							phase++;						
						}
					}
					if (phase==3){
						if (fpBtn==true){
							//draw fp button
							if (GUI.Button(Rect(190,0,100,100),"-1FP")){
								currentunitstats.fp-=1;
								targetfunctions.ResetTarget();
								targetfunctions.Target111(startcell);
								phase++;
							}	
						}
						targetfunctions.Target155(startcell);
					}
					if (phase==3 && targetcell){
						phase++;
					}
					if (phase==4){
						if(targetfunctions.Target001(targetcell)==true){
							if (targetobstacle.GetComponent(ObjectStats).objno==1081){targetobstacle.GetComponent(ObjectStats).Die();}
							if (targetobstacle.GetComponent(ObjectStats).objno==3401){Destroy(targetobstacle);}
							Mlog(targetobstacle.GetComponent(ObjectStats).objame+" destroyed.");
						}
						effect.E200(1081,targetcell);
						actionCoord.ResetAction();
					}
				}*/

