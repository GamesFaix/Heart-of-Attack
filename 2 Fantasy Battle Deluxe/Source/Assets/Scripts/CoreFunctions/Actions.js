#pragma strict

var targeting: Targeting;
var effects: Effects;
var actionCoord: ActionCoordinator;
var queue: QueueScript;

//enums
var eActionName: byte=0;
var eFunc: byte=1;
var eDesc: byte=2;

var eAp: byte=0;
var eFp: byte=1;
var emRng: float=2;
var eRng: float=3;
var eMag: float=4;
var eDec: byte=5;
var eRad: byte=6;
var eCrz: byte=7;
//

function OnEnable(){
	targeting=gameObject.GetComponent(Targeting);
	effects=gameObject.GetComponent(Effects);
	actionCoord=gameObject.GetComponent(ActionCoordinator);
	queue=GameObject.Find("GameIndexPrefab").GetComponent(QueueScript);
}

//movement
function MovSerpGND(unit: GameObject, rng: float){
	var cell: GameObject = unit.GetComponent(ObjectStats).mycell;
	if(targeting.SerpGND(rng, cell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoMoves();}
	effects.ObjMove(unit,targeting.targetcell);
	yield;
}
function MovSerpTRM(unit: GameObject, rng: float){
	var cell: GameObject = unit.GetComponent(ObjectStats).mycell;
	if(targeting.SerpTRM(rng, cell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoMoves();}
	effects.ObjMove(unit,targeting.targetcell);
	effects.ObjTRM(unit,targeting.targetcell);
	yield;
}
function MovSerpFLY(unit: GameObject, rng: float){
	var cell: GameObject = unit.GetComponent(ObjectStats).mycell;
	if(targeting.SerpFLY(rng, cell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoMoves();}
	effects.ObjMove(unit,targeting.targetcell);
	yield;
}
function MovLinGND(unit: GameObject, rng: float){
	var cell: GameObject = unit.GetComponent(ObjectStats).mycell;
	if(targeting.LinGND(rng, cell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoMoves();}
	effects.ObjMove(unit,targeting.targetcell);
	yield;
}				
function MovLinTRM(unit: GameObject, rng: float){
	var cell: GameObject = unit.GetComponent(ObjectStats).mycell;	
	if(targeting.LinTRM(rng, cell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoMoves();}
	effects.ObjMove(unit,targeting.targetcell);
	effects.ObjTRM(unit,targeting.targetcell);
	yield;
}				
function MovLinFLY(unit: GameObject, rng: float){
	var cell: GameObject = unit.GetComponent(ObjectStats).mycell;
	if(targeting.LinFLY(rng, cell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoMoves();}
	effects.ObjMove(unit,targeting.targetcell);
	yield;
}
//focus
function Focus(unit: GameObject, mag: float){
	Debug.Log("focusing");
	effects.StatFP(unit,mag);
	yield;
}				
//attack
function AtkSerpNRM(unit: GameObject, rng: float, mag: float){
	var cell: GameObject = unit.GetComponent(ObjectStats).mycell;
	if(targeting.AtkSerp(rng, cell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.ChooseUnit(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		effects.DmgNRM(unit,mag,targeting.targetobject);
	}
	yield;
}					
function AtkSerpPSN(unit: GameObject, rng: float, mag: float, dec: float, rad: float){
	if(targeting.AtkSerp(rng,unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.ChooseUnit(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		effects.DmgPSN(unit,mag,dec,rad,targeting.targetobject);
	}
	yield;
}
function AtkSerpELC(unit: GameObject, rng: float, mag: float, rad: float){
	if(targeting.AtkSerp(rng,unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.ChooseUnit(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		effects.DmgELC(unit,mag,rad,targeting.targetobject);
	}
	yield;
}//unit creation
function AtkLinNRM(unit: GameObject, rng: float, mag: float){
	if(targeting.AtkLin(rng,unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.ChooseUnit(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		effects.DmgNRM(unit,mag,targeting.targetobject);
	}
	yield;
}				
function AtkArcNRM(unit: GameObject, rng: float, mag: float){
	if(targeting.AtkArc(rng,unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.ChooseUnit(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		effects.DmgNRM(unit,mag,targeting.targetobject);
	}
	yield;

}
function AtkArcEXP(unit: GameObject, rng: float, mag: float, dec: float, crz: float, rad: float){
	var unitstats=unit.GetComponent(ObjectStats);
	if(targeting.AtkArc(rng, unitstats.mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	effects.DmgEXP(unit,mag,dec,rad,crz,targeting.targetcell);
	yield;
}

function AtkArcPSN(unit: GameObject, rng: float, mag: float, dec: float, rad: float){//sporatic emission
	if(targeting.AtkArc(rng,unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.ChooseUnit(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		effects.DmgPSN(unit,mag,dec,rad,targeting.targetobject);
	}
	yield;
}
function AtkRage(unit: GameObject, rng: float, mag: float, dec: float){
	if(targeting.AtkSerp(rng,unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.ChooseUnit(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		effects.DmgNRM(unit,mag,targeting.targetobject);
		effects.StatHP(unit,dec);
	}
	yield;
}				
function AtkLeech(unit: GameObject, rng: float, mag: float){
	if(targeting.AtkSerp(rng,unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.ChooseUnit(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		var targetstats=targeting.targetobject.GetComponent(ObjectStats);
		var preLeech=targetstats.hp;
		effects.DmgNRM(unit,mag,targeting.targetobject);
		var hpChange=preLeech-targetstats.hp;
		if (hpChange>0){effects.StatHP(unit,hpChange);}
	}
	yield;
}				

//create
function CreateGND(unit: GameObject, mag: float){
	if(targeting.SerpGND(1, unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoMoves();}
	effects.ObjCreate(unit,mag,targeting.targetcell);
	yield;
}		
function CreateTRM(unit: GameObject, mag: float){
	if(targeting.SerpTRM(1, unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	effects.ObjCreate(unit,mag,targeting.targetcell);
	effects.ObjTRM(targeting.targetobject,targeting.targetcell);
	yield;
}
function CreateFLY(unit: GameObject, mag: float){
	if(targeting.SerpFLY(1, unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	effects.ObjCreate(unit,mag,targeting.targetcell);
	yield;
}
function CreateSentinel(unit: GameObject){
	if(targeting.SerpGND(1, unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoMoves();}
	effects.ObjCreate(unit,1012,targeting.targetcell);
	effects.newobject.GetComponent(ObjectStats).mycell=targeting.targetcell;
	yield;
}



/*function CreateMine(){
					if (phase==1){
						targetcell=null;
						targetfunctions.ResetTarget();
						targetfunctions.Target135(1,startcell);
						phase++;}
					if (targetcell) {phase+=1;}
					if (phase==3){
						effect.ObjCreate(2021,targetcell);
						newob.GetComponent(ObjectStats).mycell=targetcell;
						phase++;}
					if (phase==4){
						actionCoord.ResetAction();
					}

				}
*/


function CreateMetaterrainean(unit: GameObject){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if(targeting.DestSerp(1,unitstats.mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {
		actionCoord.Refund(4,"No obstacles.");
		actionCoord.ResetAction();
	}
	targeting.AutoChooseDest(targeting.targetcell);
	var targetstats: ObjectStats = targeting.targetobject.GetComponent(ObjectStats);
	yield effects.ObjCreate(unit,1043,targeting.targetcell);
	var newstats: ObjectStats = effects.newobject.GetComponent(ObjectStats);
	if (targetstats.objno==3301){
		newstats.thumb=Resources.Load("thumbs/thumb1043B") as Texture2D;
		newstats.sprite=Resources.Load("thumbs/thumb1043B") as Texture2D;
	}
	if (targetstats.objno==3302){
		newstats.thumb=Resources.Load("thumbs/thumb1043A") as Texture2D;
		newstats.sprite=Resources.Load("thumbs/thumb1043A") as Texture2D;
	}
	if (targetstats.objno==3303){
		newstats.thumb=Resources.Load("thumbs/thumb1043C") as Texture2D;
		newstats.sprite=Resources.Load("thumbs/thumb1043C") as Texture2D;
	}
	targetstats.Die();
	yield;
}

function CreatePortalGate(unit: GameObject, mag: float){
	var unitstats: ObjectStats =  unit.GetComponent(ObjectStats);
	//find cell
	if(targeting.SerpGND(1, unitstats.mycell)==true){
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
	effects.ObjCreate(unit,2051,targeting.targetcell);
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
/*	function CreateCorpseFiend(){
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
						effect.ObjCreate(1081,targetcell);
						actionCoord.ResetAction();
					}
				}*/


//mode flip
function ModeTank(unit: GameObject){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if(unitstats.morph==0){
		unitstats.def+=3;
		unitstats.actText[1,eFunc]=null;
		unitstats.actText[6,eActionName]="Tank Mode";
		unitstats.thumb=Resources.Load("thumbs/thumb1024B") as Texture2D;
		unitstats.sprite=Resources.Load("thumbs/thumb1024B") as Texture2D;
		actionCoord.Mlog("Player"+unitstats.owner+"'s RoboTank Fortress +3DEF ("+unitstats.def+") / Immobilized.");
	}
	if(unitstats.morph==1){
		unitstats.def-=3;
		unitstats.actText[1,eFunc]="MovLinTRM";
		unitstats.actText[6,eActionName]="Siege Mode";
		unitstats.thumb=Resources.Load("thumbs/thumb1024A") as Texture2D;
		unitstats.sprite=Resources.Load("sprites/sprite1024A") as Texture2D;
		actionCoord.Mlog("Player"+unitstats.owner+"'s RoboTank Fortress -3DEF ("+unitstats.def+") / Mobilized.");
	}
	if(unitstats.morph==0){unitstats.morph=1;}
	else {unitstats.morph=0;}
	yield;
}





function ModeDragon(unit: GameObject){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if (unitstats.morph==0){ 
		if(targeting.CheckBelow(unit)==false){
			actionCoord.Refund(6,"Ground occupied.");
		}
		if(targeting.CheckBelow(unit)==true){
			unitstats.mob=1;
			unitstats.mycell.GetComponent(CellProperties).occB=0;
			unitstats.def+=3;
			unitstats.actText[1,eFunc]=null;
			unitstats.actText[5,eFunc]=null;
			unitstats.actText[6,eActionName]="Take Wing";
			unitstats.thumb=Resources.Load("thumbs/thumb1034B") as Texture2D;
			unitstats.sprite=Resources.Load("thumbs/thumb1034B") as Texture2D;
			actionCoord.Mlog("Player"+unitstats.owner+"'s Castle Dragon +3DEF ("+unitstats.def+") / Immobilized.");
			unitstats.morph=1;
		}
	}
	else if (unitstats.morph==1){
		if (targeting.CheckAbove(unit)==false){
			actionCoord.Refund(6,"Air occupied.");
		}
		if (targeting.CheckAbove(unit)==true){
			unitstats.mob=3;
			unitstats.mycell.GetComponent(CellProperties).occA=0;
			unitstats.def-=3;
			unitstats.actText[1,eFunc]="MovSerpFLY";
			unitstats.actText[5,eFunc]="SpcSlashBurn";
			unitstats.actText[6,eActionName]="Castlize";
			unitstats.thumb=Resources.Load("thumbs/thumb1034A") as Texture2D;
			unitstats.sprite=Resources.Load("thumbs/thumb1034A") as Texture2D;
			actionCoord.Mlog("Player"+unitstats.owner+"'s Castle Dragon +3DEF ("+unitstats.def+") / Mobilized.");
			unitstats.morph=0;
		}
	}
	yield;
}
function ModeMagman(unit: GameObject){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if(unitstats.morph==0){
		unitstats.actNums[1,eRng]=unitstats.actNums[1,eRng]-1;
		unitstats.hp+=15;
		unitstats.mhp+=15;
		unitstats.actText[3,eFunc]="AtkSerpNRM";
		unitstats.actText[3,eActionName]="Rock Fist";
		unitstats.actNums[3,eAp]=1;
		unitstats.actNums[3,eMag]=13;
		unitstats.actNums[3,eRng]=1;
		unitstats.actText[4,eActionName]="Melt";
		unitstats.actNums[4,eAp]=2;
		unitstats.thumb=Resources.Load("thumbs/thumb1083B") as Texture2D;
		unitstats.sprite=Resources.Load("thumbs/thumb1083B") as Texture2D;
		actionCoord.Mlog("Player"+unitstats.owner+"'s Magman +15HP ("+unitstats.hp+") / -1SP ("+unitstats.actNums[1,1]+" / Learned Rock Fist.");
	}
	if (unitstats.morph==1){
		unitstats.actNums[1,eRng]=unitstats.actNums[1,eRng]+1;
		unitstats.hp-=15;
		unitstats.mhp-=15;
		unitstats.actText[3,eFunc]="AtkLinFIR";
		unitstats.actText[3,eActionName]="Magma Fist";
		unitstats.actNums[3,eAp]=2;
		unitstats.actNums[3,eMag]=20;
		unitstats.actNums[3,eRng]=2;
		unitstats.actText[4,eActionName]="Solidify";
		unitstats.ap=1;
		unitstats.thumb=Resources.Load("thumbs/thumb1083A") as Texture2D;
		unitstats.sprite=Resources.Load("thumbs/thumb1083A") as Texture2D;
		actionCoord.Mlog("Player"+unitstats.owner+"'s Magman -15HP ("+unitstats.hp+") / +1SP ("+unitstats.actText[1,1]+" / Learned Magma Fist.");
	}
	if (unitstats.morph==0){unitstats.morph=1;}
	else {unitstats.morph=0;}
	yield;
}
////
function SpcSprint(unit: GameObject){//reset incomplete
	var unitstats=unit.GetComponent(ObjectStats);
	unitstats.actNums[1,eRng]=unitstats.actNums[1,eRng]+4;
	unitstats.endTimer+=2;
	actionCoord.Mlog("Player"+unitstats.owner+"'s Ninjoid +4SP ("+unitstats.actNums[1,eRng]+") until end of next turn.");
	yield;
}
function SpcFortifyShield(unit: GameObject){//all unit not just friendlies
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
			if (targeting.Adjacent(unitstats.mycell,otherstats.mycell)==true
			&& unit!=unitlist[i]){
				otherstats.def++;
			}
		}
		actionCoord.Mlog("Player"+unitstats.owner+"'s Sentinel's shield +1DEF ("+shield.GetComponent(SentinelShield).def+").");
	}
	else {actionCoord.Refund(5,"Shield maxed.");}
	yield;
}
function SpcBarrage(unit: GameObject, mag: float, dec: float, rad: float, crz: float){
	var unitstats = unit.GetComponent(ObjectStats);
	var cell: GameObject = unitstats.mycell;
	//Debug.Log("barrage CRZ: "+crz);
	effects.DmgBarrage(unit,mag,dec,rad,crz,cell);//EXP DMG - does not affect FLY/GAS
	
	unitstats.actNums[4,eCrz]=0;
	unitstats.actNums[4,eMag]=9;
	unitstats.bombs=0;
	yield;
}
function SpcStockpile(unit: GameObject){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	unitstats.bombs+=1;
	unitstats.actNums[4,eCrz]=unitstats.actNums[4,eCrz]+1;
	unitstats.actNums[4,eMag]=unitstats.actNums[4,eMag]+1;
	actionCoord.Mlog("Player"+unitstats.owner+"'s "+unitstats.objname+" +1Bomb ("+unitstats.bombs+").");
	yield;
}		
//
function SpcEnhanceArmor(unit: GameObject){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if (unitstats.armor<4){
		unitstats.armor+=1;
		unitstats.def+=1;
		actionCoord.Mlog("Player"+unitstats.owner+"'s Demolitia +1DEF ("+unitstats.def+")");
	}
	else {
		actionCoord.Refund(4,"Max armor.");
	}
	yield;
}
function SpcFortify(unit: GameObject){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if (unitstats.armor<2){
		unitstats.armor++;
		if (unitstats.armor==1){unitstats.sprite=Resources.Load("sprites/sprite1023B") as Texture2D;}
		if (unitstats.armor==2){unitstats.sprite=Resources.Load("sprites/sprite1023C") as Texture2D;}
		unitstats.def++;
		unitstats.actNums[3,eRng]=unitstats.actNums[3,eRng]+1;
		unitstats.actNums[5,eRng]=unitstats.actNums[5,eRng]+1;
		actionCoord.Mlog("Player"+unitstats.owner+"'s Panopticlops +1DEF ("+unitstats.def+") / +1RNG (Lob "+unitstats.actNums[3,eRng]+" / Tactical Missile "+unitstats.actNums[5,eRng]+").");
	}
	else {
		actionCoord.Refund(4,"Max armor.");
	}
	yield;
}
//
function SpcArise(unit: GameObject){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	yield effects.ObjEvo(unit,1032);
	actionCoord.Mlog("Player"+unitstats.owner+"'s Phoenix arose from it's ashes.");
}
function SpcGoodMourning(unit: GameObject, rng: float, mag: float){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if(targeting.AtkSerp(rng,unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.ChooseUnit(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		effects.DmgNRM(unit,mag,targeting.targetobject);
	}
	unitstats.actNums[3,eRng]=1;
	yield;
}
function SpcWindUp(unit: GameObject){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	unitstats.actNums[3,eRng]=unitstats.actNums[3,eRng]+1;
	actionCoord.Mlog("Player"+unitstats.owner+"'s Mournking +1RNG ("+unitstats.actNums[3,eRng]+").");
	yield;
}			
//			
function SpcConjureTerrain(unit: GameObject){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if(targeting.SerpGND(1, unitstats.mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoMoves();}
	var terrain: byte = Mathf.Floor(Random.value*2);
	var mag: float;
	if (terrain==0){mag=3301;}//tree
	if (terrain==1){mag=3302;}//boulder
	yield effects.ObjCreate(unit,mag,targeting.targetcell);
	actionCoord.Mlog("Player"+unitstats.owner+"'s Grizzly Elder conjured a "+effects.newobject.GetComponent(ObjectStats).objname);
	yield;
}				
function SpcBurial(unit: GameObject, mag: float){
	var unitstats=unit.GetComponent(ObjectStats);
	if(targeting.CorpseSerp(1,unitstats.mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {
		actionCoord.Refund(4,"No corpses.");
		actionCoord.ResetAction();
	}
	targeting.AutoChooseCorpse(targeting.targetcell);
	yield targeting.WaitForTargetobject();
	actionCoord.Mlog("Corpse buried.");
	targeting.targetobject.GetComponent(ObjectStats).Die();
	effects.StatHP(unit,mag);
	yield;
}
function SpcConsumeTerrain(unit: GameObject, mag: float){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if(targeting.DestSerp(1,unitstats.mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {
		actionCoord.Refund(4,"No obstacles.");
		actionCoord.ResetAction();
	}
	targeting.AutoChooseDest(targeting.targetcell);
	yield targeting.WaitForTargetobject();
	var targetstats: ObjectStats = targeting.targetobject.GetComponent(ObjectStats);
	actionCoord.Mlog(targetstats.objname+" consumed.");
	if (targetstats.objno==3301){
		unitstats.thumb=Resources.Load("thumbs/thumb1043B") as Texture2D;
		unitstats.sprite=Resources.Load("thumbs/thumb1043B") as Texture2D;
	}
	if (targetstats.objno==3302){
		unitstats.thumb=Resources.Load("thumbs/thumb1043A") as Texture2D;
		unitstats.sprite=Resources.Load("thumbs/thumb1043A") as Texture2D;
	}
	if (targetstats.objno==3303){
		unitstats.thumb=Resources.Load("thumbs/thumb1043C") as Texture2D;
		unitstats.sprite=Resources.Load("thumbs/thumb1043C") as Texture2D;
	}
	targetstats.Die();
	effects.StatHP(unit,mag);
	yield;
}
function SpcAuralDischarge(unit: GameObject){
	var teammates = new List.<GameObject>();
	teammates= targeting.FindTeammates(unit,false);
	
	var i: short;
	for (i=0; i<teammates.Count; i++){
		var unitstats: ObjectStats = teammates[i].GetComponent(ObjectStats);
		var dmg: byte = Mathf.Ceil(unitstats.actNums[3,eMag]*0.5);
		
		var unitlist: GameObject[] = GameObject.FindGameObjectsWithTag("unit");
		var j: short;
		for (j=0; j<unitlist.length; j++){
			var otherstats: ObjectStats = unitlist[j].GetComponent(ObjectStats);
			if (targeting.Adjacent(otherstats.mycell,unitstats.mycell)==true
			&& unitstats.owner!=otherstats.owner){
				effects.StatHP(unitlist[j],(0-dmg));
			}
		}
		effects.StatHP(teammates[i],(0-3));
	}
	yield;
}
function SpcTorchOfThaw(unit: GameObject){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	
	var teammates = new List.<GameObject>();
	teammates= targeting.FindTeammates(unit,false);
	
	var i: short;
	for (i=0; i<teammates.Count; i++){
		var otherstats: ObjectStats;
		otherstats=teammates[i].GetComponent(ObjectStats);
		otherstats.init+=1;
		otherstats.actNums[1,eRng]=unitstats.actNums[1,eRng]+1;
		otherstats.thaw=1;
	}
	actionCoord.Mlog("Player"+unitstats.owner+"'s units thawed. (+1IN, +1SP)");
	yield;
}
//
function SpcLoad(unit: GameObject){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	unitstats.bombs+=1;
	actionCoord.Mlog("Player"+unitstats.owner+"'s Gunslinger +1Bullet ("+unitstats.bombs+").");
	yield;
}
function SpcQuickdraw(unit: GameObject, rng: float, mag: float, bombs: byte){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	var i: byte;
	for (i=0; i<bombs; i++){
		yield AtkLinNRM(unit,rng,mag);
	}
	unitstats.bombs=0;
}
function SpcPatience(unit: GameObject){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	var unitlist: GameObject[] = GameObject.FindGameObjectsWithTag("unit");
	var i: short;
	for (i=0; i<unitlist.length; i++){
		var otherstats: ObjectStats = unitlist[i].GetComponent(ObjectStats);
		if (targeting.Adjacent(unitstats.mycell,otherstats.mycell)==true
		&& unit!=unitlist[i]
		&& unitstats.owner==otherstats.owner){
			effects.StatHP(unitlist[i],6);
		}
	}
	yield;
}
function SpcTimeahawk(unit: GameObject, rng: float, mag: float, rad: float){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if(targeting.AtkLin(rng,unitstats.mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.ChooseUnit(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		effects.DmgNRM(unit,mag,targeting.targetobject);
	}
	var targetstats: ObjectStats = targeting.targetobject.GetComponent(ObjectStats);
	if (targetstats.fp>0){
		targetstats.fp-=rad;
		actionCoord.Mlog("Player"+targetstats.owner+"'s "+targetstats.objname+" -1FP.");
	}
	yield;
}
function SpcSecondInCommand(unit: GameObject, rng: float){
	if (targeting.AtkArc(rng, unit.GetComponent(ObjectStats).mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.ChooseUnit(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		
		var targetStats: ObjectStats = targeting.targetobject.GetComponent(ObjectStats);
		targetStats.ap-=1;
		queue.ToTop(targeting.targetobject);
	}
	yield;
}

//
function SpcEvolve(unit: GameObject, mag: float){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	var owner: byte = unitstats.owner;
	yield effects.ObjEvo(unit,mag);
	var newstats: ObjectStats = effects.newobject.GetComponent(ObjectStats);
	actionCoord.Mlog("Player"+owner+"'s larva evolved into a(n) "+newstats.objname+".");
	yield;
}
function SpcDeathSting(unit: GameObject, rng: float, mag: float, dec: float, rad: float){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if(targeting.AtkSerp(rng,unitstats.mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {targeting.NoTargets();}
	
	if(targeting.ChooseUnit(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		effects.DmgPSN(unit,mag,dec,rad,targeting.targetobject);
	}
	unitstats.Die();
	yield;
}
function SpcInfestCorpse(unit: GameObject){
	var unitstats=unit.GetComponent(ObjectStats);
	if(targeting.CorpseSerp(1,unitstats.mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {
		actionCoord.Refund(4,"No corpses.");
		actionCoord.ResetAction();
	}
	targeting.AutoChooseCorpse(targeting.targetcell);
	yield targeting.WaitForTargetobject();
	actionCoord.Mlog("Corpse infested.");
	
	var targetStats: ObjectStats = targeting.targetobject.GetComponent(ObjectStats);
	targetStats.Die();
	effects.ObjCreate(unit,1060,targeting.targetcell);
	
	yield;
}
//
function SpcCannibalize(unit: GameObject, mag: float){
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	if (targeting.CorpseSerp(1,unitstats.mycell)==true){
		yield targeting.WaitForTargetcell();
	}
	else {
		actionCoord.Refund(4,"No corpses.");
		actionCoord.ResetAction();
	}
	if (targeting.AutoChooseCorpse(targeting.targetcell)==true){
		yield targeting.WaitForTargetobject();
		actionCoord.Mlog("Corpse consumed.");
		targeting.targetobject.GetComponent(ObjectStats).Die();
		unitstats.mhp+=mag;
		effects.StatHP(unit,mag);
		unitstats.actNums[3,eMag]=unitstats.actNums[3,eMag]+2;
	}
	yield;
}