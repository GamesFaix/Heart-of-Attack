#pragma strict

var actionCoord: ActionCoordinator;
var obprefab: GameObject; //prefab
var targeting: Targeting;
var queue: QueueScript;
var newobject: GameObject; //instance

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

	//act nums
static var eRNG: byte = 3;	

function OnEnable(){
	actionCoord=gameObject.GetComponent(ActionCoordinator);
	targeting=gameObject.GetComponent(Targeting);
	queue=GameObject.Find("GameIndexPrefab").GetComponent(QueueScript);
}

//damge effects
function E100(unit: GameObject, mag: float, targetunit:GameObject){//damage normal
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	var targetunitstats: ObjectStats = targetunit.GetComponent(ObjectStats);
	if(targetunitstats.coreStats[eDEF]<mag){
		targetunitstats.coreStats[eHP]-=(mag-targetunitstats.coreStats[eDEF]);
		actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s "+unitstats.objname+" dealt Player"
		+targetunitstats.coreStats[eOwner]+"'s "+targetunitstats.objname+" "+(mag-targetunitstats.coreStats[eDEF])+" damage.");
		if (targetunitstats.coreStats[eHP]<1){targetunitstats.Die();}
	}
}		
function E103(unit: GameObject, mag: float, dec: float, rad: float, targetunit: GameObject){//damage poison - initial
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	var targetunitstats: ObjectStats = targetunit.GetComponent(ObjectStats);
	if(targetunitstats.coreStats[eDEF]<mag){
		var dmg: byte = (mag-targetunitstats.coreStats[eDEF]);
		targetunitstats.coreStats[eHP]-=dmg;
		actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s "+unitstats.objname+" dealt Player"
		+targetunitstats.coreStats[eOwner]+"'s "+targetunitstats.objname+" "+dmg+" damage.");
		if (targetunitstats.coreStats[eHP]<1){targetunitstats.Die();}
		if (targetunitstats.comp[0]==true){
			targetunitstats.psnDMG=dmg;
			targetunitstats.psnRAD=rad;
			targetunitstats.psnDEC=dec;
		}
	}
}
function E104(unit: GameObject, mag: float, rad: float, targetunit: GameObject){//damage elec
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	var targetunitstats: ObjectStats = targetunit.GetComponent(ObjectStats);
	if (targetunitstats.comp[1]==true){mag+=Mathf.Ceil(mag*0.5);}
	if(targetunitstats.coreStats[eDEF]<mag){
		var dmg: byte = (mag-targetunitstats.coreStats[eDEF]);
		targetunitstats.coreStats[eHP]-=dmg;
		actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s "+unitstats.objname+" dealt Player"
		+targetunitstats.coreStats[eOwner]+"'s "+targetunitstats.objname+" "+dmg+" damage.");
		if (targetunitstats.coreStats[eHP]<1){targetunitstats.Die();}
	}
	targetunitstats.coreStats[eINIT]--;
	var spReduction: byte = Mathf.Ceil(targetunitstats.actNums[1,eRNG]*0.5);
	targetunitstats.actNums[1,eRNG]=targetunitstats.actNums[1,eRNG]-spReduction;
	targetunitstats.elcSPreduction=spReduction;
	targetunitstats.elcRAD=rad;
}
//object manipulation
function E200(unit: GameObject, objno: short,targetcell: GameObject){//create object
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	newobject = Instantiate(obprefab,targetcell.transform.position,Quaternion.identity);
	var newstats: ObjectStats = newobject.GetComponent(ObjectStats);
	newstats.coreStats[eObjno]=objno;
	newstats.coreStats[eOwner]=unitstats.coreStats[eOwner];
	if (objno<2000 || objno==2031){
		queue.queuelist.Add(newobject);
	}
	yield;
}
function E202(targetunit:GameObject,targetcell:GameObject){//move unit
	var targetunitstats: ObjectStats = targetunit.GetComponent(ObjectStats);
	targetunit.transform.position = targetcell.transform.position;
	actionCoord.Mlog("Player"+targetunitstats.coreStats[eOwner]+"'s "+targetunitstats.objname+" moved.");
}
function E205(unit: GameObject, targetcell: GameObject){//trample obstacle
	var obs: GameObject[] = GameObject.FindGameObjectsWithTag("obstacle");
	var i: short;
	for (i=0; i<obs.length; i++){
		var obstats: ObjectStats = obs[i].GetComponent(ObjectStats);
		if(obstats.coreStats[eObclass]>2 && obstats.mycell==targetcell){
			actionCoord.Mlog("Player"+unit.GetComponent(ObjectStats).coreStats[eOwner]+"'s "+unit.GetComponent(ObjectStats).objname+" trampled a "+obstats.objname);
			obs[i].GetComponent(ObjectStats).Die();
			break;
		}
	}
	
	//check again for obstacle-units (corpse fiend)
	obs = GameObject.FindGameObjectsWithTag("unit");
	for (i=0; i<obs.length; i++){
		obstats = obs[i].GetComponent(ObjectStats);
		if(obstats.coreStats[eObclass] && obstats.coreStats[eObclass]>2 && obstats.mycell==targetcell){
			actionCoord.Mlog("Player"+unit.GetComponent(ObjectStats).coreStats[eOwner]+"'s "+unit.GetComponent(ObjectStats).objname+" trampled a "+obstats.objname);
			obs[i].GetComponent(ObjectStats).Die();
			break;
		}
	}
}
function E206(unit: GameObject, objno: short){//evolve (larva & phoenix ashes)
	if (((objno==1061 || objno==1032) && targeting.CheckAbove(unit)) || objno!=1061){
		var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
		newobject = Instantiate(obprefab,unitstats.mycell.transform.position,Quaternion.identity);
		var newstats: ObjectStats = newobject.GetComponent(ObjectStats);
		newstats.coreStats[eObjno]=objno;
		newstats.coreStats[eOwner]=unitstats.coreStats[eOwner];
		queue.queuelist.Add(newobject);
		unitstats.Die();
	}	
	else {
		//actionCoord.Refund(4,"Air occupied.");
	}
	yield;
}
//stat modification
function E300(targetunit:GameObject,mag:float){//add/remove hp
	var unitStats: ObjectStats = targetunit.GetComponent(ObjectStats);
	var oldHP: byte = unitStats.coreStats[eHP];
	unitStats.coreStats[eHP]+=mag;
	if (unitStats.coreStats[eHP]>unitStats.coreStats[eMHP]){unitStats.coreStats[eHP]=unitStats.coreStats[eMHP];}
	var hpChange: short = unitStats.coreStats[eHP]-oldHP;
	var msg: String;
	if (hpChange>0){msg=("Player"+unitStats.coreStats[eOwner]+"'s "+unitStats.objname+" +"+hpChange+"HP");}
	if (hpChange<0){msg="Player"+unitStats.coreStats[eOwner]+"'s "+unitStats.objname+" "+hpChange+"HP";}
	actionCoord.Mlog(msg);
	if (unitStats.coreStats[eHP]<1){unitStats.Die();}
}
function E305(unit: GameObject, mag:float){//focus / change fp
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	unitstats.coreStats[eCoreFP]+=mag;
	if (mag>0){actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s "+unitstats.objname+ " +"+mag+"FP");}
	if (mag<0){actionCoord.Mlog("Player"+unitstats.coreStats[eOwner]+"'s "+unitstats.objname+ " "+mag+"FP");}
}
