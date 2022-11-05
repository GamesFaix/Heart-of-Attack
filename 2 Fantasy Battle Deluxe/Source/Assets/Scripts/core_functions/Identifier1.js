#pragma strict

var iDtable = new List.<function(GameObject)>();

var actionCoord: ActionCoordinator;
var obstacleMaster: GameObject; //assigned by fightingstart
var stats: ObjectStats;

var mineTrigger: GameObject;
var sentShield: GameObject;
var portalPrefab: GameObject;

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
	//tar
static var eSerp: byte =1;
static var eLin: byte =2;
static var eArc: byte =3;
static var eRadial: byte =4;
	//dmg
static var eNRML: byte =1;
static var eEXP: byte =2;
static var eFIR: byte =3;
static var ePSN: byte =4;
static var eELC: byte =5;
static var eLSR: byte =6;


function Awake(){
	actionCoord=gameObject.GetComponent(ActionCoordinator);
}

function Start(){
	yield FillIDTable();
}

function Identity(object: GameObject, objno: short, owner: byte): IEnumerator{
	stats = object.GetComponent(ObjectStats);

	if (objno<=1999){//units

		stats.actText[1,eName]="Move";
			stats.actNums[1,eActAP]=1;
			stats.actNums[1,eActFP]=0;
		stats.actText[2,eName]="Focus";
			stats.actNums[2,eAction]=100020;
			stats.actText[2,eDesc]="+1FP";
			stats.actNums[2,eActAP]=1;
			stats.actNums[2,eActFP]=0;
			stats.actNums[2,eMAG]=1;
		stats.actNums[3,eActAP]=1;
			stats.actNums[3,eActFP]=0;	
		stats.coreStats[eCorpsetype]=1;//normal corpse
		stats.coreStats[eObclass]=0;
		
		iDtable[objno](object);
		
		object.tag="unit";
		object.name="Unit - "+stats.objname+" - Player "+owner;
		stats.coreStats[eCoreAP]=0; 
		stats.coreStats[eCoreFP]=0;
		stats.skipped=false;
		actionCoord.Mlog("Player"+owner+" created a "+stats.objname);
	
		if (stats.comp==[true,false]){stats.composition="Biological";}
		if (stats.comp==[false,true]){stats.composition="Mechanical";}
		if (stats.comp==[true,true]){stats.composition="Cybernetic";}
		if (stats.comp==[false,false]){stats.composition="Ethereal";}
	}
	
	if (objno>=2000 && objno<=2999){
	
		iDtable[objno](object);
	}
	
	if (objno>=3000 && objno<=3999){

		iDtable[objno](object);

		stats.coreStats[eCorpsetype]=0;
	
		if (objno>=3100 && objno<=3199){stats.coreStats[eObclass]=1;}
		if (objno>=3200 && objno<=3299){stats.coreStats[eObclass]=2;}
		if (objno>=3300 && objno<=3399){stats.coreStats[eObclass]=3;}
		if (objno>=3400 && objno<=3499){stats.coreStats[eObclass]=4;}

		switch (stats.coreStats[eObclass]){
			case 1: stats.obtype="Impassable\nIndestructible";
			case 2: stats.obtype="Passable\nIndestructible";
			case 3: stats.obtype="Passable\nDestructible";
			case 4: stats.obtype="Passable\nDestructible";
			default: stats.obtype=null;
		}
		object.tag="obstacle";
		object.name="Obstacle - "+stats.objname;
		object.transform.parent=obstacleMaster.transform;
	}

	yield;
}	

function FillIDTable(): IEnumerator{

	for (var i: short=0; i<=3500; i++){
		iDtable.Add(null);
	}

	iDtable[1011]=I1011;
	iDtable[1012]=I1012;
	iDtable[1013]=I1013;
	iDtable[1014]=I1014;
	iDtable[1021]=I1021;
	iDtable[1022]=I1022;
	iDtable[1023]=I1023;
	iDtable[1024]=I1024;
	iDtable[1031]=I1031;
	iDtable[1032]=I1032;
	iDtable[1033]=I1033;
	iDtable[1034]=I1034;
	iDtable[1041]=I1041;
	iDtable[1042]=I1042;
	iDtable[1043]=I1043;
	iDtable[1044]=I1044;
	iDtable[1051]=I1051;
	iDtable[1052]=I1052;
	iDtable[1053]=I1053;
	iDtable[1054]=I1054;
	iDtable[1060]=I1060;
	iDtable[1061]=I1061;
	iDtable[1062]=I1062;
	iDtable[1063]=I1063;
	iDtable[1064]=I1064;
	iDtable[1071]=I1071;
	iDtable[1072]=I1072;
	iDtable[1073]=I1073;
	iDtable[1074]=I1074;
	iDtable[1081]=I1081;
	iDtable[1082]=I1082;
	iDtable[1083]=I1083;
	iDtable[1084]=I1084;
	
	iDtable[2021]=I2021;
	iDtable[2031]=I2031;
	iDtable[2051]=I2051;
	
	iDtable[3101]=I3101;
	iDtable[3103]=I3103;
	iDtable[3104]=I3104;
	iDtable[3105]=I3105;
	iDtable[3201]=I3201;
	iDtable[3202]=I3202;
	iDtable[3203]=I3203;
	iDtable[3204]=I3204;
	iDtable[3205]=I3205;
	iDtable[3206]=I3206;
	iDtable[3207]=I3207;
	iDtable[3208]=I3208;
	iDtable[3209]=I3209;
	iDtable[3210]=I3210;
	iDtable[3301]=I3301;
	iDtable[3302]=I3302;
	iDtable[3303]=I3303;
	iDtable[3304]=I3304;
	iDtable[3305]=I3305;
	iDtable[3306]=I3306;
	iDtable[3307]=I3307;
	iDtable[3308]=I3308;
	iDtable[3309]=I3309;
	iDtable[3310]=I3310;
	iDtable[3311]=I3311;
	iDtable[3312]=I3312;
	iDtable[3401]=I3401;

	yield;
}

//units
function I1011(object: GameObject){			
	stats.objname="Ninjoid";	
	stats.thumb=Resources.Load("thumbs/thumb1011") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1011") as Texture2D;
	stats.comp=[false,true];
	stats.coreStats[eINIT]=3; 
	stats.coreStats[eMOB]=eGND;
	stats.coreStats[eHP]=8; stats.coreStats[eMHP]=8; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100014;
		stats.actNums[1,eTAR]=eLin;
		stats.actNums[1,eRNG]=4;
	stats.actNums[3,eAction]=100030; //melee
		stats.actText[3,eName]="Slash";
		stats.actNums[3,eTAR]=eSerp;
		stats.actNums[3,eRNG]=1; 
		stats.actNums[3,eDMGType]=eNRML;
		stats.actNums[3,eMAG]=6;
	stats.actNums[4,eAction]=101141; //sprint
		stats.actText[4,eName]="Sprint";
		stats.actText[4,eDesc]="+4SP until end of next turn.";
		stats.actNums[4,eActAP]=0; stats.actNums[4,eActFP]=1;
		stats.actNums[4,eMAG]=4;
	stats.actNums[5,eAction]=101151; //laser spin
		stats.actText[5,eName]="Laser Spin";
		stats.actNums[5,eTAR]=eRadial;
		stats.actNums[5,eRNG]=1;
		stats.actNums[5,eActAP]=1; stats.actNums[5,eActFP]=1;
		stats.actNums[5,eDMGType]=eLSR;
		stats.actNums[5,eMAG]=8;
}		
function I1012(object: GameObject){
	stats.objname="Sentinel";
	stats.thumb=Resources.Load("thumbs/thumb1012") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1012") as Texture2D;
	stats.comp=[false,true];
	stats.coreStats[eINIT]=2; stats.coreStats[eMOB]=eGND;
	stats.coreStats[eHP]=13; stats.coreStats[eMHP]=13; stats.coreStats[eDEF]=5;
	stats.actNums[1,eAction]=100010;
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=3;
	stats.actNums[3,eAction]=100035; //serp elc
		stats.actText[3,eName]="Shock";
		stats.actNums[3,eTAR]=eSerp;
		stats.actNums[3,eRNG]=1; stats.actNums[3,eMAG]=2;
		stats.actNums[3,eDMGType]=eELC;
		stats.actNums[3,eRAD]=1;
	stats.actNums[4,eAction]=101241; //shield
		stats.actText[4,eName]="Shield";
		stats.actText[4,eDesc]="(Passive effect)\nNeighboring units +3DEF.";
		stats.actNums[4,eActAP]=0; stats.actNums[4,eActFP]=0;
	stats.actNums[5,eAction]=101251; //fortify shield
		stats.actText[5,eName]="Fortify Shield";
		stats.actText[5,eDesc]="Shield gives neighboring units +1DEF.\nLimit: 2";
		stats.actNums[5,eActAP]=1; stats.actNums[5,eActFP]=2;

	var mycell: GameObject = stats.mycell;
	
	var myShield: GameObject;
	myShield=Instantiate(sentShield,mycell.transform.position,Quaternion.identity);
	myShield.transform.parent=transform;
	myShield.GetComponent(SentinelShield).def=3;
}
function I1013(object: GameObject){
	stats.objname="Pterrordactyl";
	stats.comp=[false,true];
	stats.thumb=Resources.Load("thumbs/thumb1013") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1013") as Texture2D;
	stats.coreStats[eINIT]=2; stats.coreStats[eMOB]=eFLY;
	stats.coreStats[eHP]=22; stats.coreStats[eMHP]=22; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100016;
		stats.actNums[1,eTAR]=eLin;
		stats.actNums[1,eRNG]=6;	
	stats.actNums[3,eAction]=101331; //lin
		stats.actText[3,eName]="Laser Gun";
		stats.actNums[3,eTAR]=eLin;
		stats.actNums[3,eRNG]=3; stats.actNums[3,eMAG]=12;
		stats.actNums[3,eDMGType]=eLSR;
	stats.actNums[4,eAction]=101341; //barrage
		stats.actText[4,eName]="Barrage";
		stats.actText[4,eDesc]="All bombs are removed.";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=1;
		stats.actNums[4,eTAR]=eSerp;
		stats.actNums[4,eRNG]=0; stats.actNums[4,eMAG]=9;
		stats.actNums[4,eDMGType]=eEXP;
		stats.actNums[4,eDEC]=50;
		stats.actNums[4,eCRZ]=1; stats.actNums[4,eRAD]=1;
	stats.actNums[5,eAction]=101351; //stockpile
		stats.actText[5,eName]="Stockpile";
		stats.actText[5,eDesc]="Add 1 bomb.\nBarrage DMG, RAD, & CRZ +1.";
		stats.actNums[5,eActAP]=0; stats.actNums[5,eActFP]=1;
	stats.bombs=0;	
}		
function I1014(object: GameObject){	
	stats.objname="Satellite Mech Ninja";
	stats.thumb=Resources.Load("thumbs/thumb1014") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1014") as Texture2D;
	stats.comp=[false,true];
	stats.coreStats[eINIT]=3; stats.coreStats[eMOB]=eFLY;
	stats.coreStats[eHP]=45; stats.coreStats[eMHP]=45; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100016;
		stats.actNums[1,eTAR]=eLin;
		stats.actNums[1,eRNG]=4;
	stats.actNums[3,eAction]=100031;//lin
		stats.actText[3,eName]="Shoot";
		stats.actNums[3,eTAR]=eLin;
		stats.actNums[3,eRNG]=2; stats.actNums[3,eMAG]=15;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=101441;
		stats.actText[4,eName]="Teleport Friendly";
		stats.actText[4,eDesc]="Move any unit on your team to any cell it can occupy.";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=1;
	stats.actNums[5,eAction]=101451;
		stats.actText[5,eName]="Advanced Plating";
		stats.actText[5,eDesc]="Target unit +5HP & MHP.\nLimit: 2 per unit.";
		stats.actNums[5,eActAP]=1; stats.actNums[5,eActFP]=2;
		stats.actNums[5,eTAR]=eSerp; 
		stats.actNums[5,eRNG]=1;
	stats.actNums[6,eAction]=101461;
		stats.actText[6,eName]="Superdirectional Laser";
		stats.actNums[6,eActAP]=1; stats.actNums[6,eActFP]=2;
		stats.actNums[6,eTAR]=eLin;
		stats.actNums[6,eRNG]=20;
		stats.actNums[6,eMAG]=16;
		stats.actNums[6,eDMGType]=eLSR;
		stats.actNums[6,eDEC]=75;
		stats.actNums[6,eRAD]=40;
	stats.actNums[7,eAction]=100071;
		stats.actText[7,eName]="Create Ninjoid";
		stats.actText[7,eDesc]="Ground unit\nFast, but fragile infantry droid.";
		stats.actNums[7,eActAP]=1; stats.actNums[7,eActFP]=0;
		stats.actNums[7,eMAG]=1011;
	stats.actNums[8,eAction]=101481;
		stats.actText[8,eName]="Create Sentinel";
		stats.actText[8,eDesc]="Ground unit\nShield-generating support droid.";
		stats.actNums[8,eActAP]=0; stats.actNums[8,eActFP]=2;
		stats.actNums[8,eMAG]=1012;
	stats.actNums[9,eAction]=100073;
		stats.actText[9,eName]="Create Pterrordactyl";
		stats.actText[9,eDesc]="Flying unit\nSpeedy bomber.";
		stats.actNums[9,eActAP]=1; stats.actNums[9,eActFP]=2;
		stats.actNums[9,eMAG]=1013;
}	
function I1021(object: GameObject){
	stats.objname="Demolitia";
	stats.thumb=Resources.Load("thumbs/thumb1021") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1021") as Texture2D;
	stats.comp=[true,false];
	stats.coreStats[eINIT]=2; stats.coreStats[eMOB]=eGND;
	stats.coreStats[eHP]=10; stats.coreStats[eMHP]=10; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100010;//serp gnd
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=3;
	stats.actNums[3,eAction]=102131;
		stats.actText[3,eName]="Grenade";
		stats.actNums[3,eTAR]=eArc;
		stats.actNums[3,eRNG]=4; stats.actNums[3,eMAG]=8;
		stats.actNums[3,eDMGType]=eEXP;
		stats.actNums[3,eDEC]=50; 
		stats.actNums[3,eCRZ]=0; stats.actNums[3,eRAD]=1;
	stats.actNums[4,eAction]=102141; //enhance stats.armor
		stats.actText[4,eName]="Enhance stats.armor";
		stats.actText[4,eDesc]="+1DEF\nLimit: 4";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=1;
	stats.armor=0;
}
function I1022(object: GameObject){	
	stats.objname="Condor";
	stats.thumb=Resources.Load("thumbs/thumb1022") as Texture2D;
	stats.sprite=Resources.Load("sprites/sprite1022") as Texture2D;
	stats.comp=[false,true];
	stats.coreStats[eINIT]=2; stats.coreStats[eMOB]=eGND;
	stats.coreStats[eHP]=15; stats.coreStats[eMHP]=15; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100014; //gnd lin
		stats.actNums[1,eTAR]=eLin;
		stats.actNums[1,eRNG]=5;	
	stats.actNums[3,eAction]=100031; //lin
		stats.actText[3,eName]="Shoot";
		stats.actNums[3,eTAR]=eLin;
		stats.actNums[3,eRNG]=1; stats.actNums[3,eMAG]=8;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=102241; //lay mine
		stats.actText[4,eName]="Lay Mine";
		stats.actText[4,eDesc]="Item";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=1;
		stats.actNums[4,eMAG]=2021;
	stats.actNums[5,eAction]=102251; //detonate
		stats.actText[5,eName]="Detonate";
		stats.actText[5,eDesc]="Destroy all mines on team.";
		stats.actNums[5,eActAP]=1; stats.actNums[5,eActFP]=0;
}
function I1023(object: GameObject){
	stats.objname="Panopticlops";
	stats.thumb=Resources.Load("thumbs/thumb1023") as Texture2D;
	stats.sprite=Resources.Load("sprites/sprite1023A") as Texture2D;
	stats.comp=[false,true];
	stats.coreStats[eINIT]=1; stats.coreStats[eMOB]=eTRM;
	stats.coreStats[eHP]=38; stats.coreStats[eMHP]=38; stats.coreStats[eDEF]=1;
	stats.actNums[1,eAction]=100011; //trm serp
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=2;	
	stats.actNums[3,eAction]=102331; //arc
		stats.actText[3,eName]="Lob";
		stats.actNums[3,eTAR]=eArc;
		stats.actNums[3,eRNG]=3; stats.actNums[3,eMAG]=10;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=102341; //fortify
		stats.actText[4,eName]="Fortify";
		stats.actText[4,eDesc]="+1DEF \nLob & Tactical Missle +1RNG\nLimit: 2";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=1;
	stats.armor=0;
	stats.actNums[5,eAction]=102351; //tactical missile
		stats.actText[5,eName]="Tactical Missile";
		stats.actText[5,eDesc]="Ignores target's DEF.";
		stats.actNums[5,eActAP]=1; stats.actNums[5,eActFP]=2;
		stats.actNums[5,eTAR]=eArc;
		stats.actNums[5,eRNG]=4; stats.actNums[5,eMAG]=12;
		stats.actNums[5,eDMGType]=eNRML;
}		
function I1024(object: GameObject){
	stats.objname="RoboTank Fortress";
	stats.thumb=Resources.Load("thumbs/thumb1024A") as Texture2D;
	stats.sprite=Resources.Load("sprites/sprite1024A") as Texture2D;
	stats.comp=[false,true];
	stats.morph=0;
	stats.coreStats[eINIT]=2; stats.coreStats[eMOB]=eTRM;
	stats.coreStats[eHP]=60; stats.coreStats[eMHP]=60; stats.coreStats[eDEF]=1;
	stats.actNums[1,eAction]=100015;//lin trm
		stats.actNums[1,eTAR]=eLin;
		stats.actNums[1,eRNG]=2;
	stats.actNums[3,eAction]=100031;//lin
		stats.actText[3,eName]="Shoot";
		stats.actNums[3,eTAR]=eLin;
		stats.actNums[3,eRNG]=3; stats.actNums[3,eMAG]=11;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=102441;
		stats.actText[4,eName]="Artillery Mortar";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=2;
		stats.actNums[4,eTAR]=eArc;
		stats.actNums[4,eRNG]=8; stats.actNums[4,eMAG]=14;
		stats.actNums[4,eDMGType]=eEXP;
		stats.actNums[4,eDEC]=50;
		stats.actNums[4,eCRZ]=0; stats.actNums[4,eRAD]=4;
	stats.actNums[5,eAction]=102451;
		stats.actText[5,eName]="Incendiary Fire";
		stats.actNums[5,eActAP]=1; stats.actNums[5,eActFP]=1;
		stats.actNums[5,eTAR]=eLin;
		stats.actNums[5,eRNG]=2; stats.actNums[5,eMAG]=12;
		stats.actNums[5,eDMGType]=eFIR;
		stats.actNums[5,eDEC]=50;
		stats.actNums[5,eRAD]=1;
	stats.actNums[6,eAction]=102461;
		stats.actText[6,eName]="Siege Mode";
		stats.actText[6,eDesc]="+3DEF\nImmobilize";
		stats.actNums[6,eActAP]=1; stats.actNums[6,eActFP]=1;
	stats.actNums[7,eAction]=100071;
		stats.actText[7,eName]="Create Demolitia";
		stats.actText[7,eDesc]="Ground unit\nGrenade-lobing infantry.";
		stats.actNums[7,eActAP]=1; stats.actNums[7,eActFP]=0;
		stats.actNums[7,eMAG]=1021;
	stats.actNums[8,eAction]=100071;
		stats.actText[8,eName]="Create Condor";
		stats.actText[8,eDesc]="Ground unit\nQuick, mine-laying speeder.";
		stats.actNums[8,eActAP]=1; stats.actNums[8,eActFP]=1;
		stats.actNums[8,eMAG]=1022;
	stats.actNums[9,eAction]=100072;
		stats.actText[9,eName]="Create Panopticlops";
		stats.actText[9,eDesc]="Trample unit\nSlow, but devasting monolith tank.";
		stats.actNums[9,eActAP]=1; stats.actNums[9,eActFP]=2;
		stats.actNums[9,eMAG]=1023;
}
function I1031(object: GameObject){
	stats.objname="Mournking";
	stats.thumb=Resources.Load("thumbs/thumb1031") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1031") as Texture2D;
	stats.comp=[true,false];
	stats.coreStats[eINIT]=3; stats.coreStats[eMOB]=eGND;
	stats.coreStats[eHP]=12; stats.coreStats[eMHP]=12; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100010; //serp gnd
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=2;	
	stats.actNums[3,eAction]=100030; //melee
		stats.actText[3,eName]="Good Mourning";
		stats.actNums[3,eTAR]=eSerp;
		stats.actNums[3,eRNG]=1; stats.actNums[3,eMAG]=10;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=103141; //wind up
		stats.actText[4,eName]="Wind Up";
		stats.actText[4,eDesc]="Good Mourning +1RNG.\nResets on next use.";
		stats.actNums[4,eActAP]=0; stats.actNums[4,eActFP]=1;
}
function I1032(object: GameObject){	
	stats.objname="Phoenix";
	stats.thumb=Resources.Load("thumbs/thumb1032") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1032") as Texture2D;
	stats.comp=[true,false];
	stats.coreStats[eINIT]=2; stats.coreStats[eMOB]=eFLY;
	stats.coreStats[eHP]=15; stats.coreStats[eMHP]=15; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100012; //fly serp
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=4;
	stats.actNums[3,eAction]=100030; //melee
		stats.actText[3,eName]="Claw";
		stats.actNums[3,eTAR]=eSerp;
		stats.actNums[3,eRNG]=1; stats.actNums[3,eMAG]=8;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=103241; //pickup
	stats.actNums[5,eAction]=103251; //drop
	stats.coreStats[eCorpsetype]=2; //become ashes
}	
function I1033(object: GameObject){
	stats.objname="Rambuchet";
	stats.thumb=Resources.Load("thumbs/thumb1033") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1033") as Texture2D;
	stats.comp=[false,true];
	stats.coreStats[eINIT]=1; stats.coreStats[eMOB]=eTRM;
	stats.coreStats[eHP]=36; stats.coreStats[eMHP]=36; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100011; //trm serp
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=3;	
	stats.actNums[3,eAction]=103331; //ram/lob
		stats.actText[3,eName]="Rambuchetize";
		stats.actText[3,eDesc]="10DMG may be dealt to an additional target exactly 2 cells behind first target.";
		stats.actNums[3,eTAR]=eLin;
		stats.actNums[3,eRNG]=1; stats.actNums[3,eMAG]=14;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=103341; //momentum
		stats.actText[4,eName]="Momentum";
		stats.actText[4,eDesc]="Move up to 5 cells.  If path is blocked by a unit, deal 5DMG per cell moved.";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=1;
		stats.actNums[4,eTAR]=eLin;
		stats.actNums[4,eRNG]=5;
		stats.actNums[4,eDMGType]=eNRML;
	stats.actNums[5,eAction]=103351; //pickup corpse
		stats.actText[5,eName]="Pick up Corpse";
		stats.actText[5,eDesc]="Pick up any neighboring Corpse."; 
		stats.actNums[5,eActAP]=0; stats.actNums[5,eActFP]=1;
		stats.actNums[5,eRNG]=1;
	stats.actNums[6,eAction]=103361; //fling corpse
		stats.actText[6,eName]="Fling Corpse";
		stats.actText[6,eDesc]="Move carried Corpse to target cell.  If cell is occupied, destroy Corpse and do 8DMG to occupying unit.";
		stats.actNums[6,eActAP]=1; stats.actNums[6,eActFP]=1;
		stats.actNums[6,eTAR]=eArc;
		stats.actNums[6,eRNG]=5; stats.actNums[6,eMAG]=8;
		stats.actNums[6,eDMGType]=eNRML;
}		
function I1034(object: GameObject){
	stats.objname="Castle Dragon";		
	stats.thumb=Resources.Load("thumbs/thumb1034A") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1034A") as Texture2D;
	stats.comp=[true,false];
	stats.morph=0;
	stats.coreStats[eINIT]=2; stats.coreStats[eMOB]=eFLY;
	stats.coreStats[eHP]=55; stats.coreStats[eMHP]=55; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100012;//serp fly
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=5;	
	stats.actNums[3,eAction]=100030;//melee
		stats.actText[3,eName]="Maul";
		stats.actNums[3,eTAR]=eSerp;
		stats.actNums[3,eRNG]=1; stats.actNums[3,eMAG]=15;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=103441;
		stats.actText[4,eName]="Tail Spin";
		stats.actText[4,eDesc]="Knockback: 1";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=1;
		stats.actNums[4,eTAR]=eRadial;
		stats.actNums[4,eRNG]=1; stats.actNums[4,eMAG]=8;
		stats.actNums[4,eDMGType]=eNRML;
	stats.actNums[5,eAction]=103451;
		stats.actText[5,eName]="Slash and Burn";
		stats.actText[5,eDesc]="Move up to 5 cells.  Deal damage to each unit crossed.";
		stats.actNums[5,eActAP]=1; stats.actNums[5,eActFP]=1;
		stats.actNums[5,eTAR]=eLin;
		stats.actNums[5,eRNG]=5; stats.actNums[5,eMAG]=8;
		stats.actNums[5,eDMGType]=eFIR;
		stats.actNums[5,eDEC]=50; stats.actNums[5,eRAD]=1;
	stats.actNums[6,eAction]=103461;
		stats.actText[6,eName]="Castlize";
		stats.actText[6,eDesc]="+3DEF \t Ground\nImmobilize\n+CNT (DEC 50%)";
		stats.actNums[6,eActAP]=1; stats.actNums[6,eActFP]=1;
	stats.actNums[7,eAction]=100071;
		stats.actText[7,eName]="Create Mournking";
		stats.actText[7,eDesc]="Ground unit\nOrcish infantry brute.";
		stats.actNums[7,eActAP]=1; stats.actNums[7,eActFP]=0;
		stats.actNums[7,eMAG]=1031;
	stats.actNums[8,eAction]=100073;
		stats.actText[8,eName]="Create Phoenix";
		stats.actText[8,eDesc]="Flying unit\nFast, regenerating fire-breather.";
		stats.actNums[8,eActAP]=1; stats.actNums[8,eActFP]=1;
		stats.actNums[8,eMAG]=1032;
	stats.actNums[9,eAction]=100072;
		stats.actText[9,eName]="Create Rambuchet";
		stats.actText[9,eDesc]="Trample unit\nSlow, crushing siege weapon.";
		stats.actNums[9,eActAP]=1; stats.actNums[9,eActFP]=2;
		stats.actNums[9,eMAG]=1033;
}
function I1041(object: GameObject){
	stats.objname="Grizzly Elder";
	stats.thumb=Resources.Load("thumbs/thumb1041") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1041") as Texture2D;
	stats.comp=[true,false];
	stats.coreStats[eINIT]=2; stats.coreStats[eMOB]=eGND;
	stats.coreStats[eHP]=11; stats.coreStats[eMHP]=11; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100010;//serp gnd
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=2;	
	stats.actNums[3,eAction]=100030;//melee
		stats.actText[3,eName]="Claw";
		stats.actNums[3,eTAR]=eSerp;
		stats.actNums[3,eRNG]=1; stats.actNums[3,eMAG]=5;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=104141;//conjure terrain
		stats.actText[4,eName]="Conjure Terrain";
		stats.actText[4,eDesc]="Create destructible obstacle.";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=1;
		stats.actNums[4,eTAR]=eSerp;
		stats.actNums[4,eRNG]=1;
	stats.actNums[5,eAction]=104151;//burial
		stats.actText[5,eName]="Burial";
		stats.actText[5,eDesc]="Destroy neighboring Corpse.\n+5HP";
		stats.actNums[5,eActAP]=1; stats.actNums[5,eActFP]=0;
		stats.actNums[5,eMAG]=5;
}
function I1042(object: GameObject){
	stats.objname="Laughing Owl";
	stats.thumb=Resources.Load("thumbs/thumb1042") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1042") as Texture2D;
	stats.comp=[true,false];
	stats.coreStats[eINIT]=2; stats.coreStats[eMOB]=eFLY;
	stats.coreStats[eHP]=16; stats.coreStats[eMHP]=16; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100016; //fly lin
		stats.actNums[1,eTAR]=eLin;
		stats.actNums[1,eRNG]=6;
	stats.actNums[3,eAction]=100030; //melee
		stats.actText[3,eName]="Claw";
		stats.actNums[3,eTAR]=1;
		stats.actNums[3,eRNG]=1; stats.actNums[3,eMAG]=7;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=104241; //transport enemy
		stats.actText[4,eName]="Transport Enemy";
		stats.actText[4,eDesc]="Move up to 6 cells and return to starting cell.  Pickup a target unit crossed along the way, and drop in any vacant cell.  Target unit takes 9DMG.";
		stats.actNums[4,eTAR]=eLin;
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=1;
		stats.actNums[4,eRNG]=6; stats.actNums[4,eMAG]=9;
		stats.actNums[4,eDMGType]=eNRML;
}		
function I1043(object: GameObject){
	stats.objname="Meta-Terrainean";
	stats.comp=[true,false];
	stats.coreStats[eINIT]=1; stats.coreStats[eMOB]=eTRM;
	stats.coreStats[eHP]=40; stats.coreStats[eMHP]=40; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100011; //trm serp
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=2;
	stats.actNums[3,eAction]=100030; //melee
		stats.actText[3,eName]="Punch";
		stats.actNums[3,eTAR]=eSerp;
		stats.actNums[3,eRNG]=1; stats.actNums[3,eMAG]=11;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=104341; //consume terrain
		stats.actText[4,eName]="Consume Terrain";
		stats.actText[4,eDesc]="Destroy any neighboring non-Corpse destructible obstacle.\n+8HP";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=1;
		stats.actNums[4,eTAR]=eSerp;
		stats.actNums[4,eRNG]=1; stats.actNums[4,eMAG]=8;
}		
function I1044(object: GameObject){
	stats.objname="Yeti Mtn Sloth Beast";
	stats.thumb=Resources.Load("thumbs/thumb1044") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1044") as Texture2D;
	stats.comp=[true,false];
	stats.coreStats[eINIT]=2; stats.coreStats[eMOB]=eGND;
	stats.coreStats[eHP]=50; stats.coreStats[eMHP]=50; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100010;//serp gnd
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=3;	
	stats.actNums[3,eAction]=100030;//melee
		stats.actText[3,eName]="Claw";
		stats.actNums[3,eTAR]=eSerp;
		stats.actNums[3,eRNG]=1; stats.actNums[3,eMAG]=9;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=104441;
		stats.actText[4,eName]="Stampede";
		stats.actNums[4,eActAP]=2; stats.actNums[4,eActFP]=1;
	stats.actNums[5,eAction]=104451;
		stats.actText[5,eName]="Aural Discharge";
		stats.actText[5,eDesc]="All other units on team:\n Deal 50% of Action3 DMG to neighboring units & take 3DMG. (Ignore all units' def.)";
		stats.actNums[5,eActAP]=1; stats.actNums[5,eActFP]=2;
	stats.actNums[6,eAction]=104461;
		stats.actText[6,eName]="Torch of Thaw";
		stats.actText[6,eDesc]="All other units on team +1IN & Move RNG, until end of next turn.";
		stats.actNums[6,eActAP]=0; stats.actNums[6,eActFP]=2;
	stats.actNums[7,eAction]=100071;
		stats.actText[7,eName]="Create Grizzly Elder";
		stats.actText[7,eDesc]="Ground unit\nWeak, terrain-manipulating infantry.";
		stats.actNums[7,eActAP]=1; stats.actNums[7,eActFP]=0;
		stats.actNums[7,eMAG]=1041;
	stats.actNums[8,eAction]=100073;
		stats.actText[8,eName]="Create Laughing Owl";
		stats.actText[8,eDesc]="Flying unit\nFast transporter.";
		stats.actNums[8,eActAP]=2; stats.actNums[8,eActFP]=0;
		stats.actNums[8,eMAG]=1042;
	stats.actNums[9,eAction]=104491;
		stats.actText[9,eName]="Create Meta-Terrainean";
		stats.actText[9,eDesc]="Trample unit\nSlow, regenerating brute.";
		stats.actNums[9,eActAP]=1; stats.actNums[9,eActFP]=2;
		stats.actNums[9,eMAG]=1043;
}
function I1051(object: GameObject){
	stats.objname="Gunslinger";
	stats.thumb=Resources.Load("thumbs/thumb1051") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1051") as Texture2D;
	stats.comp=[true,false];
	stats.coreStats[eINIT]=2; stats.coreStats[eMOB]=eGND;
	stats.coreStats[eHP]=11; stats.coreStats[eMHP]=11; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100010;//serp gnd
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=3;	
	stats.actNums[3,eAction]=100031;//lin
		stats.actText[3,eName]="Shoot";
		stats.actNums[3,eTAR]=eLin;
		stats.actNums[3,eRNG]=2; stats.actNums[3,eMAG]=8;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=105141; //load
		stats.actText[4,eName]="Load";
		stats.actText[4,eDesc]="Add 1 bullet.";
		stats.actNums[4,eActAP]=0; stats.actNums[4,eActFP]=1;
		stats.bombs=0;
	stats.actNums[5,eAction]=105151; //quickdraw
		stats.actText[5,eName]="Quickdraw";
		stats.actText[5,eDesc]="Repeat for each bullet, then remove all bullets.";
		stats.actNums[5,eActAP]=1; stats.actNums[5,eActFP]=1;
		stats.actNums[5,eTAR]=eLin;
		stats.actNums[5,eRNG]=1; stats.actNums[5,eMAG]=10;
		stats.actNums[5,eDMGType]=eNRML;
}	
function I1052(object: GameObject){
	stats.objname="Piecemaker";
	stats.thumb=Resources.Load("thumbs/thumb1052") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1052") as Texture2D;
	stats.comp=[true,true];
	stats.coreStats[eINIT]=1; stats.coreStats[eMOB]=eGND;
	stats.coreStats[eHP]=17; stats.coreStats[eMHP]=17; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100010; //gnd serp
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=4;	
	stats.actNums[3,eAction]=100030; //melee
		stats.actText[3,eName]="Bludgeon";
		stats.actNums[3,eTAR]=eSerp;
		stats.actNums[3,eRNG]=1; stats.actNums[3,eMAG]=2;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=105241; //patience
		stats.actText[4,eName]="Patience";
		stats.actText[4,eDesc]="Neighboring allies +6HP.";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=1;
	stats.actNums[5,eAction]=105251; //create gateA
		stats.actText[5,eName]="Open Gate (+)";
		stats.actText[5,eDesc]="Creates a (+) portal gate.\nAny other (+) gates are destroyed.";
		stats.actNums[5,eActAP]=1; stats.actNums[5,eActFP]=1;
		stats.actNums[5,eMAG]=0;
	stats.actNums[6,eAction]=105251; //create gateB
		stats.actText[6,eName]="Open Gate (-)";
		stats.actText[6,eDesc]="Creates a (-) portal gate.\nAny other (-) gates are destroyed.";
		stats.actNums[6,eActAP]=1; stats.actNums[6,eActFP]=1;
		stats.actNums[6,eMAG]=1;
}		
function I1053(object: GameObject){
	stats.objname="Chieftomaton";
	stats.thumb=Resources.Load("thumbs/thumb1053") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1053") as Texture2D;
	stats.comp=[false,true];
	stats.coreStats[eINIT]=1; stats.coreStats[eMOB]=eGND;
	stats.coreStats[eHP]=20; stats.coreStats[eMHP]=20; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100010; //gnd serp
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=3;	
	stats.actNums[3,eAction]=105331; //timeahawk
		stats.actText[3,eName]="Time-a-hawk";
		stats.actText[3,eDesc]="Target -1FP";
		stats.actNums[3,eTAR]=eLin;
		stats.actNums[3,eRNG]=2; stats.actNums[3,eMAG]=10;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=105341; //spirit time bomb
		stats.actText[4,eName]="Spirit Time Bomb";
		stats.actText[4,eDesc]="Target +2IN(if friendly)/-2IN(if enemy) until end of next turn. \nUnits neighboring target +1IN(if friendly)/-1IN(if enemy) until end of next turn.";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=1;
		stats.actNums[4,eTAR]=eArc;
		stats.actNums[4,eRNG]=5; stats.actNums[4,eMAG]=6;
		stats.actNums[4,eDMGType]=eNRML;
}		
function I1054(object: GameObject){
	stats.objname="Old GrandDad";
	stats.thumb=Resources.Load("thumbs/thumb1054") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1054") as Texture2D;
	stats.comp=[false,true];
	stats.coreStats[eINIT]=2; stats.coreStats[eMOB]=eGND;
	stats.coreStats[eHP]=57; stats.coreStats[eMHP]=57; stats.coreStats[eDEF]=1;
	stats.actNums[1,eAction]=100010;//serp gnd
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=2;	
	stats.actNums[3,eAction]=100031;//lin
		stats.actText[3,eName]="Shoot";
		stats.actNums[3,eTAR]=eLin;
		stats.actNums[3,eRNG]=3; stats.actNums[3,eMAG]=10;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=105441;
		stats.actText[4,eName]="Hour Saviour";
		stats.actText[4,eDesc]="All units on your team advance in the Queue.  No unit may be skipped more than once.";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=0;
	stats.actNums[5,eAction]=105451;
		stats.actText[5,eName]="Marksman";
		stats.actNums[5,eActAP]=1; stats.actNums[5,eActFP]=1;
		stats.actNums[5,eTAR]=eArc;
		stats.actNums[5,eRNG]=5; stats.actNums[5,eMAG]=15;
		stats.actNums[5,eDMGType]=eNRML;
	stats.actNums[6,eAction]=105461;
		stats.actText[6,eName]="Second in Command";
		stats.actText[6,eDesc]="Target unit takes a turn next. It only gets 1AP that turn.";
		stats.actNums[6,eActAP]=1; stats.actNums[6,eActFP]=2;
	stats.actNums[7,eAction]=100071;
		stats.actText[7,eName]="Create Gunslinger";
		stats.actText[7,eDesc]="Ground unit\nDamaging infantry.";
		stats.actNums[7,eActAP]=1; stats.actNums[7,eActFP]=0;
		stats.actNums[7,eMAG]=1051;
	stats.actNums[8,eAction]=100071;
		stats.actText[8,eName]="Create Piecemaker";
		stats.actText[8,eDesc]="Ground unit\nHealing support droid.";
		stats.actNums[8,eActAP]=1; stats.actNums[8,eActFP]=1;
		stats.actNums[8,eMAG]=1052;
	stats.actNums[9,eAction]=100071;
		stats.actText[9,eName]="Create Chieftomaton";
		stats.actText[9,eDesc]="Ground unit\nTime-bending mecha-elder";
		stats.actNums[9,eActAP]=1; stats.actNums[9,eActFP]=2;
		stats.actNums[9,eMAG]=1053;
}
function I1060(object: GameObject){
	stats.objname="Larva";
	stats.thumb=Resources.Load("thumbs/thumb1060") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1060") as Texture2D;
	stats.comp=[true,false];
	stats.coreStats[eINIT]=2; stats.coreStats[eMOB]=eGND;
	stats.coreStats[eHP]=10; stats.coreStats[eMHP]=10; stats.coreStats[eDEF]=2;
	stats.actNums[1,eAction]=0;
	stats.actNums[3,eAction]=100033;//leech life
		stats.actText[3,eName]="Leech Life";
		stats.actText[3,eDesc]="+1HP per DMG dealt.";
		stats.actNums[3,eTAR]=eSerp;
		stats.actNums[3,eRNG]=1; stats.actNums[3,eMAG]=5;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=106041;//evolve bee
		stats.actText[4,eName]="Evolve: Beesassin";
		stats.actText[4,eDesc]="Flying unit\nFast, but fragile assassin.";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=0;
		stats.actNums[4,eMAG]=1061;
	stats.actNums[5,eAction]=106041;//evolve shrooman
		stats.actText[5,eName]="Evolve: Shrooman";
		stats.actText[5,eDesc]="Ground unit\nInfectious support unit.";
		stats.actNums[5,eActAP]=2; stats.actNums[5,eActFP]=1;
		stats.actNums[5,eMAG]=1062;
	stats.actNums[6,eAction]=106041;//evolve flytrap
		stats.actText[6,eName]="Evolve: Itza Trap";
		stats.actText[6,eDesc]="Trample unit\nSlow, carnivorous plant.";
		stats.actNums[6,eActAP]=0; stats.actNums[6,eActFP]=3;
		stats.actNums[6,eMAG]=1063;
	stats.coreStats[eCorpsetype]=0;
}	
function I1061(object: GameObject){
	stats.objname="Beesassin";
	stats.thumb=Resources.Load("thumbs/thumb1061") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1061") as Texture2D;
	stats.comp=[true,false];
	stats.coreStats[eINIT]=3; stats.coreStats[eMOB]=eFLY;
	stats.coreStats[eHP]=9; stats.coreStats[eMHP]=9; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100012; //fly serp
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=5;	
	stats.actNums[3,eAction]=100034; //serp psn
		stats.actText[3,eName]="Poison Sting";
		stats.actNums[3,eTAR]=eSerp;
		stats.actNums[3,eRNG]=1; stats.actNums[3,eMAG]=6;
		stats.actNums[3,eDMGType]=ePSN;
		stats.actNums[3,eDEC]=50; stats.actNums[3,eRAD]=1;
	stats.actNums[4,eAction]=106141; //death sting
		stats.actText[4,eName]="Death Sting";
		stats.actText[4,eDesc]="Beesassin is sacrificed.";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=1;
		stats.actNums[4,eTAR]=eSerp;
		stats.actNums[4,eRNG]=1; stats.actNums[4,eMAG]=12;
		stats.actNums[4,eDMGType]=ePSN;
		stats.actNums[4,eDEC]=50; stats.actNums[4,eRAD]=5;
}
function I1062(object: GameObject){
	stats.objname="Shrooman";
	stats.thumb=Resources.Load("thumbs/thumb1062") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1062") as Texture2D;	
	stats.comp=[true,false];
	stats.coreStats[eINIT]=2; stats.coreStats[eMOB]=eGND;
	stats.coreStats[eHP]=15; stats.coreStats[eMHP]=15; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100010; //gnd serp
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=2;	
	stats.actNums[3,eAction]=106231; //psn spores
		stats.actText[3,eName]="Sporatic Emission";
		stats.actNums[3,eTAR]=eArc;
		stats.actNums[3,eRNG]=2; stats.actNums[3,eMAG]=9;
		stats.actNums[3,eDMGType]=ePSN;
		stats.actNums[3,eDEC]=50; stats.actNums[3,eRAD]=3;
	stats.actNums[4,eAction]=106241; //infest corpse
		stats.actText[4,eName]="Infest Corpse";
		stats.actText[4,eDesc]="Turn neighboring Corpse into a Larva.";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=0;
}		
function I1063(object: GameObject){
	stats.objname="Itza Trap";
	stats.thumb=Resources.Load("thumbs/thumb1063") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1063") as Texture2D;
	stats.comp=[true,false];
	stats.coreStats[eINIT]=3; stats.coreStats[eMOB]=eFLY;
	stats.coreStats[eHP]=30; stats.coreStats[eMHP]=30; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100011; //trm serp
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=1;	
	stats.actNums[3,eAction]=106331; //ensnare
		stats.actText[3,eName]="Ensnare";
		stats.actNums[3,eRNG]=1; stats.actNums[3,eMAG]=10;
		stats.actNums[3,eDMGType]=eNRML;
}		
function I1064(object: GameObject){
	stats.objname="Spider Queen";
	stats.thumb=Resources.Load("thumbs/thumb1064") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1064") as Texture2D;
	stats.comp=[true,false];
	stats.coreStats[eINIT]=2; stats.coreStats[eMOB]=eGND;
	stats.coreStats[eHP]=45; stats.coreStats[eMHP]=45; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100010;//serp gnd
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=4;	
	stats.actNums[3,eAction]=106431;//melee
		stats.actText[3,eName]="Bite";
		stats.actNums[3,eTAR]=eSerp;
		stats.actNums[3,eRNG]=1; stats.actNums[3,eMAG]=10;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=106441;
		stats.actText[4,eName]="Webshot";
		stats.actText[4,eDesc]="Target -2 Move RNG until end of next turn, -1 Move RNG until end of two turns."; 
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=1;
		stats.actNums[4,eTAR]=eArc;
		stats.actNums[4,eRNG]=3; stats.actNums[4,eMAG]=12;
		stats.actNums[4,eDMGType]=ePSN;
		stats.actNums[4,eDEC]=50; stats.actNums[4,eRAD]=1;
	stats.actNums[5,eAction]=106451;
		stats.actText[5,eName]="Swarm";
		stats.actText[5,eDesc]="All non-Larva friendly units get +1DMG on Action3, for each non-Larva friendly unit, until the end of their next turn.";
		stats.actNums[5,eActAP]=1; stats.actNums[5,eActFP]=2;
	stats.actNums[6,eAction]=106461;
		stats.actText[6,eName]="Megadearth";
		stats.actText[6,eDesc]="All units of target team recieve 2PSN DMG per unit on their team.";
		stats.actNums[6,eActAP]=2; stats.actNums[6,eActFP]=2;
		stats.actNums[6,eDMGType]=ePSN;
		stats.actNums[6,eDEC]=50; stats.actNums[6,eRAD]=1;
	stats.actNums[7,eAction]=100071;
		stats.actText[7,eName]="Create Larva";
		stats.actText[7,eDesc]="Ground unit\nWeak, evolvable hatchling.";
		stats.actNums[7,eActAP]=0; stats.actNums[7,eActFP]=0;
		stats.actNums[7,eMAG]=1060;
}
function I1071(object: GameObject){
	stats.objname="Scarabot";
	stats.thumb=Resources.Load("thumbs/thumb1071") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1071") as Texture2D;
	stats.comp=[false,true];
	stats.coreStats[eINIT]=2; stats.coreStats[eMOB]=eTRM;
	stats.coreStats[eHP]=12; stats.coreStats[eMHP]=12; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100011;//serp trm
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=3;	
	stats.actNums[3,eAction]=100031;//lin
		stats.actText[3,eName]="Shoot";
		stats.actNums[3,eTAR]=eLin;
		stats.actNums[3,eRNG]=2; stats.actNums[3,eMAG]=6;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=107141; //prism cannon
		stats.actText[4,eName]="Prism Cannon";
		stats.actNums[4,eTAR]=eLin;
		stats.actNums[4,eRNG]=4; stats.actNums[4,eMAG]=10;
		stats.actNums[4,eDMGType]=eLSR;
		stats.actNums[4,eDEC]=50; stats.actNums[4,eRAD]=3;
}
function I1072(object: GameObject){
	stats.objname="Haze";
	stats.thumb=Resources.Load("thumbs/thumb1072") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1072") as Texture2D;
	stats.comp=[false,false];
	stats.coreStats[eINIT]=1; stats.coreStats[eMOB]=eGAS;
	stats.coreStats[eHP]=22; stats.coreStats[eMHP]=22; stats.coreStats[eDEF]=1;
	stats.actNums[1,eAction]=100013; //gas serp
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=2;	
	stats.actNums[3,eAction]=107231; //melee
		stats.actText[3,eName]="Mnemonic Plague";
		stats.actText[3,eDesc]="Self +1HP per 2 damage dealt.  You may give target co-occupying unit +1HP per damage dealt.";
		stats.actNums[3,eRNG]=0; stats.actNums[3,eMAG]=7;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=107241; //accumulation
		stats.actText[4,eName]="Accumulate";
		stats.actText[4,eDesc]="Merge with any co-occupying Haze.  Gain half accumulated Haze's HP.  Gain accumulated Haze's FP, DEF. Size increases.";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=1;
	stats.actNums[5,eAction]=107251; //mirage
		stats.actText[5,eName]="Mirage";
		stats.actText[5,eDesc]="(Passive effect)\nCo-occupying friendly units have a 25% dodge rate.";
		stats.actNums[5,eActAP]=0; stats.actNums[5,eActFP]=0;
	stats.actNums[6,eAction]=107261; //tractor gust
		stats.actText[6,eName]="Tractor Gust";
		stats.actText[6,eDesc]="Move target unit into a space occupied by Haze.";
		stats.actNums[6,eActAP]=0; stats.actNums[6,eActFP]=1;
		stats.actNums[6,eTAR]=eSerp;
		stats.actNums[6,eRNG]=2;
	stats.coreStats[eCorpsetype]=0; //no corpse
}
function I1073(object: GameObject){
	stats.objname="Priest of Naja";
	stats.comp=[true,true];
	stats.thumb=Resources.Load("thumbs/thumb1073") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1073") as Texture2D;
	stats.coreStats[eINIT]=3; stats.coreStats[eMOB]=eGND;
	stats.coreStats[eHP]=28; stats.coreStats[eMHP]=28; stats.coreStats[eDEF]=1;
	stats.actNums[1,eAction]=100010; //gnd serp
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=4;
	stats.actNums[3,eAction]=100030; //melee
		stats.actText[3,eName]="Bludgeon";
		stats.actNums[3,eTAR]=eSerp;
		stats.actNums[3,eRNG]=1; stats.actNums[3,eMAG]=9;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=107341; //force shove
	stats.actNums[5,eAction]=107351; //super shove
}
function I1074(object: GameObject){
	stats.objname="Cyborg Super Sultan";
	stats.thumb=Resources.Load("thumbs/thumb1074") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1074") as Texture2D;
	stats.comp=[true,true];
	stats.coreStats[eINIT]=2; stats.coreStats[eMOB]=eFLY;
	stats.coreStats[eHP]=55; stats.coreStats[eMHP]=55; stats.coreStats[eDEF]=2;
	stats.actNums[1,eAction]=100012;//serp fly
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=4;	
	stats.actNums[3,eAction]=100031;//lin
		stats.actText[3,eName]="Psi Beam";
		stats.actNums[3,eTAR]=eLin;
		stats.actNums[3,eRNG]=3; stats.actNums[3,eMAG]=9;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=107441;
		stats.actText[4,eName]="Restoration";
		stats.actText[4,eDesc]="Target unit gains HP equal to half it's MHP.";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=1;
		stats.actNums[4,eTAR]=eArc;
		stats.actNums[4,eRNG]=7;
	stats.actNums[5,eAction]=107451;
		stats.actText[5,eName]="Teleport Enemy";
		stats.actText[5,eDesc]="Move an enemy unit to any legal space.";
		stats.actNums[5,eActAP]=1; stats.actNums[5,eActFP]=1;		
		stats.actNums[5,eTAR]=eArc;
		stats.actNums[5,eRNG]=3;
	stats.actNums[6,eAction]=107461;
		stats.actText[6,eName]="Lightning Storm";
		stats.actText[6,eDesc]="If a Haze is co-occupying the target's cell, repeat with 50% DMG.";
		stats.actNums[6,eActAP]=2; stats.actNums[6,eActFP]=2;		
		stats.actNums[6,eTAR]=eArc;
		stats.actNums[6,eRNG]=5; stats.actNums[6,eMAG]=15;
		stats.actNums[6,eDMGType]=eELC;
		stats.actNums[6,eDEC]=50;
	stats.actNums[7,eAction]=100072;
		stats.actText[7,eName]="Create Scarabot";
		stats.actText[7,eDesc]="Trample unit\nRugged infantry droid.";
		stats.actNums[7,eActAP]=1; stats.actNums[7,eActFP]=0;
		stats.actNums[7,eMAG]=1071;
	stats.actNums[8,eAction]=100074;
		stats.actText[8,eName]="Create Haze";
		stats.actText[8,eDesc]="Gaseous unit\nSlow, shielding support unit.";
		stats.actNums[8,eActAP]=1; stats.actNums[8,eActFP]=1;
		stats.actNums[8,eMAG]=1072;
	stats.actNums[9,eAction]=100071;
		stats.actText[9,eName]="Create Priest of Naja";
		stats.actText[9,eDesc]="Ground unit\nFast, telekinetic brute.";
		stats.actNums[9,eActAP]=1; stats.actNums[9,eActFP]=2;
		stats.actNums[9,eMAG]=1073;
}
function I1081(object: GameObject){
	stats.objname="Corpse Fiend";
	stats.thumb=Resources.Load("thumbs/thumb1081") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1081") as Texture2D;
	stats.comp=[true,false];
	stats.coreStats[eObclass]=4;
	stats.coreStats[eINIT]=2; stats.coreStats[eMOB]=eGND;
	stats.coreStats[eHP]=15; stats.coreStats[eMHP]=15; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100010;//serp gnd
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=2;	
	stats.actNums[3,eAction]=100032;
		stats.actText[3,eName]="Rage";
		stats.actText[3,eDesc]="-3HP";
		stats.actNums[3,eTAR]=eSerp;
		stats.actNums[3,eRNG]=1; stats.actNums[3,eMAG]=10; stats.actNums[3,eDEC]=(-3);
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=108141; //cannibalize
		stats.actText[4,eName]="Cannibalize";
		stats.actText[4,eDesc]="Destroy any neighboring Corpse.\n+5HP & MHP, +2DMG on Action3.";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=0;
		stats.actNums[4,eRNG]=1; stats.actNums[4,eMAG]=5;
	stats.actNums[5,eAction]=108151; //explode
		stats.actText[5,eName]="Explode";
		stats.actNums[5,eActAP]=1; stats.actNums[5,eActFP]=1;
		stats.actNums[5,eRNG]=0; stats.actNums[5,eMAG]=15;
		stats.actNums[5,eDMGType]=eEXP;
		stats.actNums[5,eDEC]=50;
		stats.actNums[5,eCRZ]=0; stats.actNums[5,eRAD]=3;
	stats.coreStats[eCorpsetype]=0;//no corpse
}
function I1082(object: GameObject){
	stats.objname="Necrochancellor";
	stats.thumb=Resources.Load("thumbs/thumb1082") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1082") as Texture2D;
	stats.comp=[true,false];
	stats.coreStats[eINIT]=2; stats.coreStats[eMOB]=eGND;
	stats.coreStats[eHP]=17; stats.coreStats[eMHP]=17; stats.coreStats[eDEF]=2;
	stats.actNums[1,eAction]=100010; //gnd serp
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=3;	
	stats.actNums[3,eAction]=100030; //melee
		stats.actText[3,eName]="Bludgeon";
		stats.actNums[3,eTAR]=eSerp;
		stats.actNums[3,eRNG]=1; stats.actNums[3,eMAG]=6;
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=108241; //move corpse
		stats.actText[4,eName]="Move Corpse";
		stats.actText[4,eDesc]="Move any neighboring Corpse to any legal cell.";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=1;
		stats.actNums[4,eRNG]=1;
		stats.actNums[4,eTAR]=eSerp;
	stats.actNums[5,eAction]=108251; //contagion
	stats.actNums[6,eAction]=108261; //open void gate
		stats.actText[6,eName]="Open Void Gate";
		stats.actText[6,eDesc]="Create minion-spawning gate from the Void.";
		stats.actNums[6,eActAP]=1; stats.actNums[6,eActFP]=2;
		stats.actNums[6,eRNG]=1;
		stats.actNums[6,eTAR]=eSerp;
}
function I1083(object: GameObject){
	stats.objname="Magman";
	stats.thumb=Resources.Load("thumbs/thumb1083A") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1083A") as Texture2D;
	stats.comp=[true,false];
	stats.morph=0;
	stats.coreStats[eINIT]=1; stats.coreStats[eMOB]=eTRM;
	stats.coreStats[eHP]=25; stats.coreStats[eMHP]=25; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100011; //trm serp
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=4;	
	stats.actNums[3,eAction]=108431; 
		stats.actText[3,eName]="Magma Fist";
		stats.actNums[3,eActAP]=2;
		stats.actNums[3,eTAR]=eLin;
		stats.actNums[3,eRNG]=2; stats.actNums[3,eMAG]=20;
		stats.actNums[3,eDMGType]=eFIR;
		stats.actNums[3,eDEC]=50; stats.actNums[3,eRAD]=1;
		
	stats.actNums[4,eAction]=108341; //transform
		stats.actText[4,eName]="Solidify";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=1;
}		
function I1084(object: GameObject){
	stats.objname="Cthulhoid";
	stats.thumb=Resources.Load("thumbs/thumb1084") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1084") as Texture2D;
	stats.comp=[true,false];
	stats.coreStats[eINIT]=3; stats.coreStats[eMOB]=eFLY;
	stats.coreStats[eHP]=60; stats.coreStats[eMHP]=60; stats.coreStats[eDEF]=0;
	stats.actNums[1,eAction]=100012;//serp fly
		stats.actNums[1,eTAR]=eSerp;
		stats.actNums[1,eRNG]=4;	
	stats.actNums[3,eAction]=100032;
		stats.actText[3,eName]="Rage";
		stats.actText[3,eDesc]="-5HP";
		stats.actNums[3,eTAR]=eSerp;
		stats.actNums[3,eRNG]=1; stats.actNums[3,eMAG]=15; stats.actNums[3,eDEC]=(-5);
		stats.actNums[3,eDMGType]=eNRML;
	stats.actNums[4,eAction]=108441;
		stats.actText[4,eName]="Sacrifize";
		stats.actText[4,eDesc]="Destroy neighboring friendly unit.\n+1DEF, +1FP";
		stats.actNums[4,eActAP]=1; stats.actNums[4,eActFP]=0;
		stats.actNums[4,eRNG]=1;
		stats.actNums[4,eTAR]=eSerp;
	stats.actNums[5,eAction]=108451;
		stats.actText[5,eName]="Devour";
		stats.actText[5,eDesc]="+1HP per damage dealt.";
		stats.actNums[5,eActAP]=1; stats.actNums[5,eActFP]=0;
		stats.actNums[5,eTAR]=eSerp;
		stats.actNums[5,eRNG]=1; stats.actNums[5,eMAG]=10;
		stats.actNums[5,eDMGType]=eNRML;
	stats.actNums[6,eAction]=108461;
		stats.actText[6,eName]="Eternal Flame";
		stats.actNums[6,eActAP]=1; stats.actNums[6,eActFP]=2;
		stats.actNums[6,eTAR]=eLin;
		stats.actNums[6,eRNG]=2; stats.actNums[6,eMAG]=12;
		stats.actNums[6,eDMGType]=eFIR;
		stats.actNums[6,eDEC]=100; stats.actNums[6,eRAD]=10;
	stats.actNums[7,eAction]=100071;
		stats.actText[7,eName]="Create Corpse Fiend";
		stats.actText[7,eDesc]="Ground unit\nStrong, suicidal infantry.";
		stats.actNums[7,eActAP]=1; stats.actNums[7,eActFP]=0;
		stats.actNums[7,eMAG]=1081;
	stats.actNums[8,eAction]=100071;
		stats.actText[8,eName]="Create Necrochancellor";
		stats.actText[8,eDesc]="Ground unit\nResilient Corpse-pedler.";
		stats.actNums[8,eActAP]=2; stats.actNums[8,eActFP]=0;
		stats.actNums[8,eMAG]=1082;
	stats.actNums[9,eAction]=100071;
		stats.actText[9,eName]="Create Magman";
		stats.actText[9,eDesc]="Trample unit\nDamaging rock brute.";
		stats.actNums[9,eActAP]=2; stats.actNums[9,eActFP]=1;
		stats.actNums[9,eMAG]=1083;
}	
//items
function I2021(object: GameObject){
	stats.objname="Mine";
	stats.thumb=Resources.Load("thumbs/thumb2021") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb2021") as Texture2D;
	stats.coreStats[eHP]=1; stats.coreStats[eMHP]=1;
	object.tag="obstacle";
	stats.coreStats[eObclass]=3;
	actionCoord.Mlog("Player"+stats.coreStats[eOwner]+" laid a mine.");
	object.name="Mine - Player "+stats.coreStats[eOwner];
	
	var mycell: GameObject = stats.mycell;
	
	var triggerZone: GameObject;
	triggerZone=Instantiate(mineTrigger,mycell.transform.position,Quaternion.identity);
	triggerZone.transform.parent=transform;
}
function I2031(object: GameObject){
	stats.objname="Phoenix Ashes";
	stats.thumb=Resources.Load("thumbs/thumb2031") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb2031") as Texture2D;
	stats.coreStats[eCoreAP]=0; stats.coreStats[eCoreFP]=0;
	stats.coreStats[eINIT]=2; stats.coreStats[eMOB]=eGND;
	stats.actNums[1,eAction]=203110;
	stats.actText[1,eName]="Arise";
		stats.actNums[1,eActAP]=0; stats.actNums[1,eActFP]=2;
	stats.actText[2,eName]="Focus";
		stats.actText[2,eDesc]="+1FP";
		stats.actNums[2,eActAP]=1; stats.actNums[2,eActFP]=0;
	stats.coreStats[eCorpsetype]=0;
	object.tag="obstacle";
	stats.coreStats[eObclass]=4;
	stats.obtype="Corpse";	
	stats.skipped=false;
	actionCoord.Mlog("Player"+stats.coreStats[eOwner]+"'s Phoenix burnt to ash.");
}
function I2051(object: GameObject){
	stats.objname="Portal Gate";
	object.tag="portal";
	if (stats.portalA==true){
		stats.thumb=Resources.Load("thumbs/thumb2051A") as Texture2D;
		stats.sprite=Resources.Load("sprites/sprite2051A") as Texture2D;
	}
	if (stats.portalA==false){
		stats.thumb=Resources.Load("thumbs/thumb2051B") as Texture2D;
		stats.sprite=Resources.Load("sprites/sprite2051B") as Texture2D;
	}
	var portal: GameObject = Instantiate(portalPrefab,transform.position,Quaternion.identity);
	portal.transform.parent=transform;
}
//obstacles
function I3101(object: GameObject){
	stats.objname="Mountain";
	stats.thumb=Resources.Load("thumbs/thumb3101") as Texture2D;
	stats.sprite=Resources.Load("sprites/sprite3101") as Texture2D;
}
function I3103(object: GameObject){//pyramid
	stats.objname="Pyramid";
	stats.thumb=Resources.Load("thumbs/thumb3103") as Texture2D;
	stats.sprite=Resources.Load("sprites/sprite3103") as Texture2D;
}
function I3104(object: GameObject){//tower - old
	stats.objname="Tower";
	stats.thumb=Resources.Load("thumbs/thumb3104") as Texture2D;
	stats.sprite=Resources.Load("sprites/sprite3104") as Texture2D;
}
function I3105(object: GameObject){//tower - new
	stats.objname="Tower";
	stats.thumb=Resources.Load("thumbs/thumb3105") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb3105") as Texture2D;
}
function I3201(object: GameObject){//hill		
	stats.objname="Hill";
	stats.thumb=Resources.Load("thumbs/thumb3201") as Texture2D;
	stats.sprite=Resources.Load("sprites/sprite3201") as Texture2D;
}
function I3202(object: GameObject){//water	
	stats.objname="Water";
	stats.thumb=Resources.Load("thumbs/thumb3202") as Texture2D;
	stats.sprite=Resources.Load("sprites/sprite3202") as Texture2D;
}
function I3203(object: GameObject){//water - dirty	
	stats.objname="Water";
	stats.thumb=Resources.Load("thumbs/thumb3203") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb3203") as Texture2D;
}
function I3204(object: GameObject){//sand dunes	
	stats.objname="Sand Dune";
	stats.thumb=Resources.Load("thumbs/thumb3204") as Texture2D;
	stats.sprite=Resources.Load("sprites/sprite3204") as Texture2D;
}
function I3205(object: GameObject){//snow	
	stats.objname="Snow Bank";
	stats.thumb=Resources.Load("thumbs/thumb3205") as Texture2D;
	stats.sprite=Resources.Load("sprites/sprite3205") as Texture2D;
}
function I3206(object: GameObject){//chasm	
	stats.objname="Chasm";
	stats.thumb=Resources.Load("thumbs/thumb3206") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb3206") as Texture2D;
}
function I3207(object: GameObject){//crater	
	stats.objname="Crater";
	stats.thumb=Resources.Load("thumbs/thumb3207") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb3207") as Texture2D;
}
function I3208(object: GameObject){//lava	
	stats.objname="Lava";
	stats.thumb=Resources.Load("thumbs/thumb3208") as Texture2D;
	stats.sprite=Resources.Load("sprites/sprite3208") as Texture2D;
}
function I3209(object: GameObject){//wall old	
	stats.objname="Wall";
	stats.thumb=Resources.Load("thumbs/thumb3209") as Texture2D;
	stats.sprite=Resources.Load("sprites/sprite3209") as Texture2D;
}
function I3210(object: GameObject){//wall new	
	stats.objname="Wall";
	stats.thumb=Resources.Load("thumbs/thumb3210") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb3210") as Texture2D;
}
function I3301(object: GameObject){//tree	
	stats.objname="Tree";
	stats.thumb=Resources.Load("thumbs/thumb3301A") as Texture2D;
	stats.sprite=Resources.Load("sprites/sprite3301A") as Texture2D;
}
function I3302(object: GameObject){//boulder	
	stats.objname="Boulder";
	stats.thumb=Resources.Load("thumbs/thumb3302A") as Texture2D;		
	stats.sprite=Resources.Load("sprites/sprite3302A") as Texture2D;		
}
function I3303(object: GameObject){//building		
	stats.objname="Building";
	stats.thumb=Resources.Load("thumbs/thumb3303A") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb3303A") as Texture2D;
}
function I3304(object: GameObject){//tree - pine		
	stats.objname="Tree";
	stats.thumb=Resources.Load("thumbs/thumb3304A") as Texture2D;
	stats.sprite=Resources.Load("sprites/sprite3304") as Texture2D;
}
function I3305(object: GameObject){//tree - palm		
	stats.objname="Tree";
	stats.thumb=Resources.Load("thumbs/thumb3305A") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb3305A") as Texture2D;
}
function I3306(object: GameObject){//cactus		
	stats.objname="Cactus";
	stats.thumb=Resources.Load("thumbs/thumb3306") as Texture2D;
	stats.sprite=Resources.Load("sprites/sprite3306") as Texture2D;
}
function I3307(object: GameObject){//volcanic rock		
	stats.objname="Rock Formation";
	stats.thumb=Resources.Load("thumbs/thumb3307A") as Texture2D;
	stats.sprite=Resources.Load("sprites/sprite3307") as Texture2D;
}
function I3308(object: GameObject){//building - shack		
	stats.objname="Shack";
	stats.thumb=Resources.Load("thumbs/thumb3308") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb3308") as Texture2D;
}
function I3309(object: GameObject){//building - old		
	stats.objname="Building";
	stats.thumb=Resources.Load("thumbs/thumb3309") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb3309") as Texture2D;
}
function I3310(object: GameObject){//building - new		
	stats.objname="Building";
	stats.thumb=Resources.Load("thumbs/thumb3310") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb3310") as Texture2D;
}
function I3311(object: GameObject){//cart		
	stats.objname="Cart";
	stats.thumb=Resources.Load("thumbs/thumb3311") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb3311") as Texture2D;
}	
function I3312(object: GameObject){//ice		
	stats.objname="Ice";
	stats.thumb=Resources.Load("thumbs/thumb3312") as Texture2D;
	stats.sprite=Resources.Load("sprites/sprite3312") as Texture2D;
}	
function I3401(object: GameObject){//corpse	
	stats.objname="Corpse";
	stats.thumb=Resources.Load("thumbs/thumb3401A") as Texture2D;
	stats.sprite=Resources.Load("sprites/sprite3401") as Texture2D;
}

function ClearActData(){
	var i: byte; var j: byte;
	for (i=0; i<=9; i++){
		for (j=0; j<=1; j++){
			stats.actText[i,j]="";
		}	
	}
	for (i=0; i<=9; i++){
		for (j=0; j<=7; j++){
			stats.actNums[i,j]=0;
		}	
	}
}