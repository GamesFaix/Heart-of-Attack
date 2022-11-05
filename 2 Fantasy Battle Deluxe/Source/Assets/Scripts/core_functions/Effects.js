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
function DmgNRM(unit: GameObject, mag: float, targetunit:GameObject){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	var targetunitstats: ObjectStats = targetunit.GetComponent(ObjectStats);
	if(targetunitstats.def<mag){
		var dmg: int = mag-targetunitstats.def;
		targetunitstats.hp-=dmg;
		actionCoord.Mlog("Player"+unitstats.owner+"'s "+unitstats.objname+" dealt Player"
		+targetunitstats.owner+"'s "+targetunitstats.objname+" "+dmg+" damage.");
		SendPopUpText(targetunit, ("-"+dmg+"HP"));
		if (targetunitstats.hp<1){targetunitstats.Die();}
	}
}		
function DmgPSN(unit: GameObject, mag: float, dec: float, rad: float, targetunit: GameObject){//initial dmg only
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	var targetunitstats: ObjectStats = targetunit.GetComponent(ObjectStats);
	if(targetunitstats.def<mag){
		var dmg: byte = (mag-targetunitstats.def);
		targetunitstats.hp-=dmg;
		actionCoord.Mlog("Player"+unitstats.owner+"'s "+unitstats.objname+" dealt Player"
		+targetunitstats.owner+"'s "+targetunitstats.objname+" "+dmg+" damage.");
		SendPopUpText(targetunit, ("-"+dmg+"HP"));
		if (targetunitstats.hp<1){targetunitstats.Die();}
		if (targetunitstats.bio==true){
			targetunitstats.psnDMG=dmg;
			targetunitstats.psnRAD=rad;
			targetunitstats.psnDEC=dec;
		}
	}
}
function DmgELC(unit: GameObject, mag: float, rad: float, targetunit: GameObject){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	var targetunitstats: ObjectStats = targetunit.GetComponent(ObjectStats);
	if (targetunitstats.mech==true){mag+=Mathf.Ceil(mag*0.5);}
	var text: String;
	if(targetunitstats.def<mag){
		var dmg: byte = (mag-targetunitstats.def);
		targetunitstats.hp-=dmg;
		actionCoord.Mlog("Player"+unitstats.owner+"'s "+unitstats.objname+" dealt Player"
		+targetunitstats.owner+"'s "+targetunitstats.objname+" "+dmg+" damage.");
		text+=("-"+dmg+"HP");
		if (targetunitstats.hp<1){targetunitstats.Die();}
	}
	targetunitstats.init--;
	var spReduction: byte = Mathf.Ceil(targetunitstats.actNums[1,3]*0.5);
	targetunitstats.actNums[1,3]=targetunitstats.actNums[1,3]-spReduction;
	targetunitstats.elcSPreduction=spReduction;
	targetunitstats.elcRAD=rad;
	text+=("\n-1IN\n-"+spReduction+"Move RNG");
	SendPopUpText(targetunit,text);
}
function DmgEXP(unit: GameObject, mag: float, dec: float, rad: float, crz: float, targetcell: GameObject){
	//initial conditions
	var hitCells = new List.<GameObject>();
	var dmg: int = mag;
	
	//find CRZ
	var currentCells = new List.<GameObject>();
	currentCells=targeting.EXPFindCRZ(targetcell,crz);	

	var distance: byte;
	for (distance=0; distance<=rad; distance++){
		if (dmg>0){
			Debug.Log("current dmg: "+dmg);
			//find units in current zone
			var units = new List.<GameObject>();
			units = targeting.UnitsInZone(currentCells);
			//dmg all units in zone
			var i: byte;
			for (i=0; i<units.Count; i++){
				Debug.Log(units[i]+" hit");
				DmgNRM(unit,dmg,units[i]);
			}
			//mark all hit cells
			for (i=0; i<currentCells.Count; i++){
				hitCells.Add(currentCells[i]);
			}
			//reduce dmg
			dmg=Mathf.Floor(dmg*dec);
			//move to next RAD
			currentCells=targeting.EXPExpandRAD(targetcell, hitCells, currentCells);
		}
		else {break;}
	}
}
function DmgBarrage(unit: GameObject, mag: float, dec: float, rad: float, crz: float, targetcell: GameObject){
	//initial conditions
	var hitCells = new List.<GameObject>();
	var dmg: int = mag;
	
	//find CRZ
	var currentCells = new List.<GameObject>();
	currentCells=targeting.EXPFindCRZ(targetcell,crz);	
	Debug.Log("CRZ size: "+currentCells.Count);

	var distance: byte;
	for (distance=0; distance<=rad; distance++){
		if (dmg>0){
			Debug.Log("current dmg: "+dmg);
			//find units in current zone
			var units = new List.<GameObject>();
			units = targeting.UnitsInZone(currentCells);
			
			
			//remove flying and gas units
			var i: byte;
			for (i=0; i<units.Count; i++){
				var unitstats: ObjectStats = units[i].GetComponent(ObjectStats);
				if (unitstats.mob>2){
					units.Remove(units[i]);
				}
			}
						
			//dmg all units in zone
			for (i=0; i<units.Count; i++){
				Debug.Log(units[i]+" hit");
				DmgNRM(unit,dmg,units[i]);
			}
			//mark all hit cells
			for (i=0; i<currentCells.Count; i++){
				hitCells.Add(currentCells[i]);
			}
			//reduce dmg
			dmg=Mathf.Floor(dmg*dec);
			//move to next RAD
			currentCells=targeting.EXPExpandRAD(targetcell, hitCells, currentCells);
		}
		else {break;}
	}

}

/*function DmgFIR(unit: GameObject, mag: float, dec: float, rad: float, targetunit: GameObject){
	var hitUnits = new List.<GameObject>();
	var dmg: byte = mag;
	
	DmgNRM(unit, mag, targetunit);
	hitUnits.Add(targetunit);
	dmg = Mathf.Floor(dmg*dec);
	
	

}

*/
//object manipulation
function ObjCreate(unit: GameObject, objno: short,targetcell: GameObject){
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
function ObjMove(object:GameObject,targetcell:GameObject){
	var obStats: ObjectStats = object.GetComponent(ObjectStats);

	var end: Vector3 = targetcell.transform.position;
	
	StartCoroutine(MoveLerp(object, end, 0.5));
	
	if (obStats.owner){
		actionCoord.Mlog("Player"+obStats.owner+"'s "+obStats.objname+" moved.");
	}
	else{
		actionCoord.Mlog(obStats.objname+" was moved.");
	}
}
function MoveLerp(object: GameObject, finish: Vector3, time: float){
	var elapsedTime: float =0;
	var start: Vector3 = object.transform.position;
	var dist: float = Vector3.Distance(start,finish);
	var speed: float = 15;
	var i: float;
	for (i=0.0; i<1.0; i+= (speed*Time.deltaTime)/dist){
		object.transform.position = Vector3.Lerp(start, finish, i);
		yield;
	}
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
function StatFP(unit: GameObject, mag:float){//change fp
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	unitstats.fp+=mag;
	if (mag>0){actionCoord.Mlog("Player"+unitstats.owner+"'s "+unitstats.objname+ " +"+mag+"fp");}
	if (mag<0){actionCoord.Mlog("Player"+unitstats.owner+"'s "+unitstats.objname+ " "+mag+"fp");}
}
//
function SendPopUpText(object: GameObject, text: String){
	var objectStats: ObjectStats = object.GetComponent(ObjectStats);
	var spriteDisplay: SpriteDisplay1 = objectStats.mySprite.GetComponent(SpriteDisplay1);
	spriteDisplay.popUpText=text;
	spriteDisplay.popUpTime=Time.time;
}