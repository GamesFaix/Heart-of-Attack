#pragma strict

var actionCoord: ActionCoordinator;
var obprefab: GameObject; //prefab
var targeting: Targeting;
var queue: QueueScript;
var newobject: GameObject; //instance

function OnEnable(){
	actionCoord=gameObject.GetComponent(ActionCoordinator);
	targeting=gameObject.GetComponent(Targeting);
	queue=GameObject.Find("GameIndexPrefab").GetComponent(QueueScript);
}

//damge effects
function DmgNRM(unit: GameObject, mag: float, targetunit:GameObject){//damage normal
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	var targetunitstats: ObjectStats = targetunit.GetComponent(ObjectStats);
	if(targetunitstats.def<mag){
		targetunitstats.hp-=(mag-targetunitstats.def);
		actionCoord.Mlog("Player"+unitstats.owner+"'s "+unitstats.objname+" dealt Player"
		+targetunitstats.owner+"'s "+targetunitstats.objname+" "+(mag-targetunitstats.def)+" damage.");
		if (targetunitstats.hp<1){targetunitstats.Die();}
	}
}		
function DmgPSN(unit: GameObject, mag: float, dec: float, rad: float, targetunit: GameObject){//damage poison - initial
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	var targetunitstats: ObjectStats = targetunit.GetComponent(ObjectStats);
	if(targetunitstats.def<mag){
		var dmg: byte = (mag-targetunitstats.def);
		targetunitstats.hp-=dmg;
		actionCoord.Mlog("Player"+unitstats.owner+"'s "+unitstats.objname+" dealt Player"
		+targetunitstats.owner+"'s "+targetunitstats.objname+" "+dmg+" damage.");
		if (targetunitstats.hp<1){targetunitstats.Die();}
		if (targetunitstats.bio==true){
			targetunitstats.psnDMG=dmg;
			targetunitstats.psnRAD=rad;
			targetunitstats.psnDEC=dec;
		}
	}
}
function DmgELC(unit: GameObject, mag: float, rad: float, targetunit: GameObject){//damage elec
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	var targetunitstats: ObjectStats = targetunit.GetComponent(ObjectStats);
	if (targetunitstats.mech==true){mag+=Mathf.Ceil(mag*0.5);}
	if(targetunitstats.def<mag){
		var dmg: byte = (mag-targetunitstats.def);
		targetunitstats.hp-=dmg;
		actionCoord.Mlog("Player"+unitstats.owner+"'s "+unitstats.objname+" dealt Player"
		+targetunitstats.owner+"'s "+targetunitstats.objname+" "+dmg+" damage.");
		if (targetunitstats.hp<1){targetunitstats.Die();}
	}
	targetunitstats.init--;
	var spReduction: byte = Mathf.Ceil(targetunitstats.actNums[1,1]*0.5);
	targetunitstats.actNums[1,1]=targetunitstats.actNums[1,1]-spReduction;
	targetunitstats.elcSPreduction=spReduction;
	targetunitstats.elcRAD=rad;
}
//object manipulation
function ObjCreate(unit: GameObject, objno: short,targetcell: GameObject){//create object
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	newobject = Instantiate(obprefab,targetcell.transform.position,Quaternion.identity);
	var newstats: ObjectStats = newobject.GetComponent(ObjectStats);
	newstats.objno=objno;
	newstats.owner=unitstats.owner;
	if (objno<2000 || objno==2031){
		queue.queuelist.Add(newobject);
	}
	yield;
}
function ObjMove(targetunit:GameObject,targetcell:GameObject){//move unit
	var targetunitstats: ObjectStats = targetunit.GetComponent(ObjectStats);
	targetunit.transform.position = targetcell.transform.position;
	actionCoord.Mlog("Player"+targetunitstats.owner+"'s "+targetunitstats.objname+" moved.");
}
function ObjTRM(unit: GameObject, targetcell: GameObject){//trample obstacle
	var obs: GameObject[] = GameObject.FindGameObjectsWithTag("obstacle");
	var i: short;
	for (i=0; i<obs.length; i++){
		var obstats: ObjectStats = obs[i].GetComponent(ObjectStats);
		if(obstats.obclass>2 && obstats.mycell==targetcell){
			actionCoord.Mlog("Player"+unit.GetComponent(ObjectStats).owner+"'s "+unit.GetComponent(ObjectStats).objname+" trampled a "+obstats.objname);
			obs[i].GetComponent(ObjectStats).Die();
			break;
		}
	}
	
	//check again for obstacle-units (corpse fiend)
	obs = GameObject.FindGameObjectsWithTag("unit");
	for (i=0; i<obs.length; i++){
		obstats = obs[i].GetComponent(ObjectStats);
		if(obstats.obclass && obstats.obclass>2 && obstats.mycell==targetcell){
			actionCoord.Mlog("Player"+unit.GetComponent(ObjectStats).owner+"'s "+unit.GetComponent(ObjectStats).objname+" trampled a "+obstats.objname);
			obs[i].GetComponent(ObjectStats).Die();
			break;
		}
	}
}
function ObjEvo(unit: GameObject, objno: short){//evolve (larva & phoenix ashes)
	if (((objno==1061 || objno==1032) && targeting.CheckAbove(unit)) || objno!=1061){
		var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
		newobject = Instantiate(obprefab,unitstats.mycell.transform.position,Quaternion.identity);
		var newstats: ObjectStats = newobject.GetComponent(ObjectStats);
		newstats.objno=objno;
		newstats.owner=unitstats.owner;
		queue.queuelist.Add(newobject);
		unitstats.Die();
	}	
	else {
		//actionCoord.Refund(4,"Air occupied.");
	}
	yield;
}
//stat modification
function StatHP(targetunit:GameObject,mag:float){//add/remove hp
	var unitStats: ObjectStats = targetunit.GetComponent(ObjectStats);
	var oldHP: byte = unitStats.hp;
	unitStats.hp+=mag;
	if (unitStats.hp>unitStats.mhp){unitStats.hp=unitStats.mhp;}
	var hpChange: short = unitStats.hp-oldHP;
	var msg: String;
	if (hpChange>0){msg=("Player"+unitStats.owner+"'s "+unitStats.objname+" +"+hpChange+"hp");}
	if (hpChange<0){msg="Player"+unitStats.owner+"'s "+unitStats.objname+" "+hpChange+"hp";}
	actionCoord.Mlog(msg);
	if (unitStats.hp<1){unitStats.Die();}
}
function StatFP(unit: GameObject, mag:float){//focus / change fp
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	unitstats.fp+=mag;
	if (mag>0){actionCoord.Mlog("Player"+unitstats.owner+"'s "+unitstats.objname+ " +"+mag+"fp");}
	if (mag<0){actionCoord.Mlog("Player"+unitstats.owner+"'s "+unitstats.objname+ " "+mag+"fp");}
}
