#pragma strict

var iDtable = new List.<function(GameObject)>();

var actionCoord: ActionCoordinator;
var obstacleMaster: GameObject; //assigned by fightingstart
var stats: ObjectStats;

var mineTrigger: GameObject;
var sentShield: GameObject;
var portalPrefab: GameObject;

//enums
var actionName: byte=0;
var desc: byte=1;

var action: byte=0;
var ap: byte=1;
var fp: byte=2;
var rng: byte=3;
var mag: byte=4;
var dec: byte=5;
var rad: byte=6;
var crz: byte=7;
var tar: byte=8;
var dmgtype: byte=9;

var im: byte=0;
var gnd: byte=1;
var trm: byte=2;
var fly: byte=3;
var gas: byte=4;

var serp: byte=1;
var lin: byte=2;
var arc: byte=3;
var radial: byte=4;

var nrml: byte=1;
var exp: byte=2;
var fir: byte=3;
var psn: byte=4;
var elc: byte=5;
var lsr: byte=6;
//

function Awake(){
	actionCoord=gameObject.GetComponent(ActionCoordinator);
}

function Start(){
	yield FillIDTable();
}

function Identity(object: GameObject, objno: short, owner: byte): IEnumerator{
	stats = object.GetComponent(ObjectStats);

	if (objno<=1999){//units

		stats.actText[1,actionName]="Move";
			stats.actNums[1,ap]=1;
			stats.actNums[1,fp]=0;
		stats.actText[2,actionName]="Focus";
			stats.actNums[2,action]=100020;
			stats.actText[2,desc]="+1FP";
			stats.actNums[2,ap]=1;
			stats.actNums[2,fp]=0;
			stats.actNums[2,mag]=1;
		stats.actNums[3,ap]=1;
			stats.actNums[3,fp]=0;	
		stats.corpsetype=1;//normal corpse
		stats.obclass=0;
		
		iDtable[objno](object);
		
		object.tag="unit";
		object.name="Unit - "+stats.objname+" - Player "+owner;
		stats.ap=0; stats.fp=0;
		stats.skipped=false;
		actionCoord.Mlog("Player"+owner+" created a "+stats.objname);
	
		if (stats.bio==true && stats.mech==false){stats.composition="Biological";}
		if (stats.bio==false && stats.mech==true){stats.composition="Mechanical";}
		if (stats.bio==true && stats.mech==true){stats.composition="Cybernetic";}
		if (stats.bio==false && stats.mech==false){stats.composition="Ethereal";}
	}
	
	if (objno>=2000 && objno<=2999){
	
		iDtable[objno](object);
	}
	
	if (objno>=3000 && objno<=3999){

		iDtable[objno](object);

		stats.corpsetype=0;
	
		if (objno>=3100 && objno<=3199){stats.obclass=1;}
		if (objno>=3200 && objno<=3299){stats.obclass=2;}
		if (objno>=3300 && objno<=3399){stats.obclass=3;}
		if (objno>=3400 && objno<=3499){stats.obclass=4;}

		switch (stats.obclass){
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
	stats.bio=false; stats.mech=true;
	stats.init=3; stats.mob=gnd;
	stats.hp=8; stats.mhp=8; stats.def=0;
	stats.actNums[1,action]=100014; //gnd lin
		stats.actNums[1,tar]=lin;
		stats.actNums[1,rng]=4;
	stats.actNums[3,action]=100030; //melee
		stats.actText[3,actionName]="Slash";
		stats.actNums[3,tar]=serp;
		stats.actNums[3,rng]=1; 
		stats.actNums[3,dmgtype]=nrml;
		stats.actNums[3,mag]=6;
	stats.actNums[4,action]=101141; //sprint
		stats.actText[4,actionName]="Sprint";
		stats.actText[4,desc]="+4SP until end of next turn.";
		stats.actNums[4,ap]=0; stats.actNums[4,fp]=1;
		stats.actNums[4,mag]=4;
	stats.actNums[5,action]=101151; //laser spin
		stats.actText[5,actionName]="Laser Spin";
		stats.actNums[5,tar]=radial;
		stats.actNums[5,rng]=1;
		stats.actNums[5,ap]=1; stats.actNums[5,fp]=1;
		stats.actNums[5,dmgtype]=lsr;
		stats.actNums[5,mag]=8;
}		
function I1012(object: GameObject){
	stats.objname="Sentinel";
	stats.thumb=Resources.Load("thumbs/thumb1012") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1012") as Texture2D;
	stats.bio=false; stats.mech=true;
	stats.init=2; stats.mob=gnd;
	stats.hp=13; stats.mhp=13; stats.def=5;
	stats.actNums[1,action]=100010; //gnd serp
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=3;
	stats.actNums[3,action]=100035; //serp elc
		stats.actText[3,actionName]="Shock";
		stats.actNums[3,tar]=serp;
		stats.actNums[3,rng]=1; stats.actNums[3,mag]=2;
		stats.actNums[3,dmgtype]=elc;
		stats.actNums[3,rad]=1;
	stats.actNums[4,action]=101241; //shield
		stats.actText[4,actionName]="Shield";
		stats.actText[4,desc]="(Passive effect)\nNeighboring units +3DEF.";
		stats.actNums[4,ap]=0; stats.actNums[4,fp]=0;
	stats.actNums[5,action]=101251; //fortify shield
		stats.actText[5,actionName]="Fortify Shield";
		stats.actText[5,desc]="Shield gives neighboring units +1DEF.\nLimit: 2";
		stats.actNums[5,ap]=1; stats.actNums[5,fp]=2;

	var mycell: GameObject = stats.mycell;
	
	var myShield: GameObject;
	myShield=Instantiate(sentShield,mycell.transform.position,Quaternion.identity);
	myShield.transform.parent=transform;
	myShield.GetComponent(SentinelShield).def=3;
}
function I1013(object: GameObject){
	stats.objname="Pterrordactyl";
	stats.bio=false; stats.mech=true;
	stats.thumb=Resources.Load("thumbs/thumb1013") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1013") as Texture2D;
	stats.init=2; stats.mob=fly;
	stats.hp=22; stats.mhp=22; stats.def=0;
	stats.actNums[1,action]=100016; //fly lin
		stats.actNums[1,tar]=lin;
		stats.actNums[1,rng]=6;	
	stats.actNums[3,action]=101331; //lin
		stats.actText[3,actionName]="Laser Gun";
		stats.actNums[3,tar]=lin;
		stats.actNums[3,rng]=3; stats.actNums[3,mag]=12;
		stats.actNums[3,dmgtype]=lsr;
	stats.actNums[4,action]=101341; //barrage
		stats.actText[4,actionName]="Barrage";
		stats.actText[4,desc]="All bombs are removed.";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=1;
		stats.actNums[4,tar]=serp;
		stats.actNums[4,rng]=0; stats.actNums[4,mag]=9;
		stats.actNums[4,dmgtype]=exp;
		stats.actNums[4,dec]=0.5;
		stats.actNums[4,crz]=1; stats.actNums[4,rad]=1;
	stats.actNums[5,action]=101351; //stockpile
		stats.actText[5,actionName]="Stockpile";
		stats.actText[5,desc]="Add 1 bomb.\nBarrage DMG, RAD, & CRZ +1.";
		stats.actNums[5,ap]=0; stats.actNums[5,fp]=1;
	stats.bombs=0;	
}		
function I1014(object: GameObject){	
	stats.objname="Satellite Mech Ninja";
	stats.thumb=Resources.Load("thumbs/thumb1014") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1014") as Texture2D;
	stats.bio=false; stats.mech=true;
	stats.init=3; stats.mob=fly;
	stats.hp=45; stats.mhp=45; stats.def=0;
	stats.actNums[1,action]=100016;//lin fly
		stats.actNums[1,tar]=lin;
		stats.actNums[1,rng]=4;
	stats.actNums[3,action]=100031;//lin
		stats.actText[3,actionName]="Shoot";
		stats.actNums[3,tar]=lin;
		stats.actNums[3,rng]=2; stats.actNums[3,mag]=15;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=101441;
		stats.actText[4,actionName]="Teleport Friendly";
		stats.actText[4,desc]="Move any unit on your team to any cell it can occupy.";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=1;
	stats.actNums[5,action]=101451;
		stats.actText[5,actionName]="Advanced Plating";
		stats.actText[5,desc]="Target unit +5HP & MHP.\nLimit: 2 per unit.";
		stats.actNums[5,ap]=1; stats.actNums[5,fp]=2;
		stats.actNums[5,tar]=serp; 
		stats.actNums[5,rng]=1;
	stats.actNums[6,action]=101461;
		stats.actText[6,actionName]="Superdirectional Laser";
		stats.actNums[6,ap]=1; stats.actNums[6,fp]=2;
		stats.actNums[6,tar]=lin;
		stats.actNums[6,rng]=20;
		stats.actNums[6,mag]=16;
		stats.actNums[6,dmgtype]=lsr;
		stats.actNums[6,dec]=0.75;
		stats.actNums[6,rad]=40;
	stats.actNums[7,action]=100071;
		stats.actText[7,actionName]="Create Ninjoid";
		stats.actText[7,desc]="Ground unit\nFast, but fragile infantry droid.";
		stats.actNums[7,ap]=1; stats.actNums[7,fp]=0;
		stats.actNums[7,mag]=1011;
	stats.actNums[8,action]=101481;
		stats.actText[8,actionName]="Create Sentinel";
		stats.actText[8,desc]="Ground unit\nShield-generating support droid.";
		stats.actNums[8,ap]=0; stats.actNums[8,fp]=2;
		stats.actNums[8,mag]=1012;
	stats.actNums[9,action]=100073;
		stats.actText[9,actionName]="Create Pterrordactyl";
		stats.actText[9,desc]="Flying unit\nSpeedy bomber.";
		stats.actNums[9,ap]=1; stats.actNums[9,fp]=2;
		stats.actNums[9,mag]=1013;
}	
function I1021(object: GameObject){
	stats.objname="Demolitia";
	stats.thumb=Resources.Load("thumbs/thumb1021") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1021") as Texture2D;
	stats.bio=true; stats.mech=false;
	stats.init=2; stats.mob=gnd;
	stats.hp=10; stats.mhp=10; stats.def=0;
	stats.actNums[1,action]=100010;//serp gnd
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=3;
	stats.actNums[3,action]=102131;
		stats.actText[3,actionName]="Grenade";
		stats.actNums[3,tar]=arc;
		stats.actNums[3,rng]=4; stats.actNums[3,mag]=8;
		stats.actNums[3,dmgtype]=exp;
		stats.actNums[3,dec]=0.5; 
		stats.actNums[3,crz]=0; stats.actNums[3,rad]=1;
	stats.actNums[4,action]=102141; //enhance stats.armor
		stats.actText[4,actionName]="Enhance stats.armor";
		stats.actText[4,desc]="+1DEF\nLimit: 4";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=1;
	stats.armor=0;
}
function I1022(object: GameObject){	
	stats.objname="Condor";
	stats.thumb=Resources.Load("thumbs/thumb1022") as Texture2D;
	stats.sprite=Resources.Load("sprites/sprite1022") as Texture2D;
	stats.bio=false; stats.mech=true;
	stats.init=2; stats.mob=gnd;
	stats.hp=15; stats.mhp=15; stats.def=0;
	stats.actNums[1,action]=100014; //gnd lin
		stats.actNums[1,tar]=lin;
		stats.actNums[1,rng]=5;	
	stats.actNums[3,action]=100031; //lin
		stats.actText[3,actionName]="Shoot";
		stats.actNums[3,tar]=lin;
		stats.actNums[3,rng]=1; stats.actNums[3,mag]=8;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=102241; //lay mine
		stats.actText[4,actionName]="Lay Mine";
		stats.actText[4,desc]="Item";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=1;
		stats.actNums[4,mag]=2021;
	stats.actNums[5,action]=102251; //detonate
		stats.actText[5,actionName]="Detonate";
		stats.actText[5,desc]="Destroy all mines on team.";
		stats.actNums[5,ap]=1; stats.actNums[5,fp]=0;
}
function I1023(object: GameObject){
	stats.objname="Panopticlops";
	stats.thumb=Resources.Load("thumbs/thumb1023") as Texture2D;
	stats.sprite=Resources.Load("sprites/sprite1023A") as Texture2D;
	stats.bio=false; stats.mech=true;
	stats.init=1; stats.mob=trm;
	stats.hp=38; stats.mhp=38; stats.def=1;
	stats.actNums[1,action]=100011; //trm serp
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=2;	
	stats.actNums[3,action]=102331; //arc
		stats.actText[3,actionName]="Lob";
		stats.actNums[3,tar]=arc;
		stats.actNums[3,rng]=3; stats.actNums[3,mag]=10;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=102341; //fortify
		stats.actText[4,actionName]="Fortify";
		stats.actText[4,desc]="+1DEF \nLob & Tactical Missle +1RNG\nLimit: 2";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=1;
	stats.armor=0;
	stats.actNums[5,action]=102351; //tactical missile
		stats.actText[5,actionName]="Tactical Missile";
		stats.actText[5,desc]="Ignores target's DEF.";
		stats.actNums[5,ap]=1; stats.actNums[5,fp]=2;
		stats.actNums[5,tar]=arc;
		stats.actNums[5,rng]=4; stats.actNums[5,mag]=12;
		stats.actNums[5,dmgtype]=nrml;
}		
function I1024(object: GameObject){
	stats.objname="RoboTank Fortress";
	stats.thumb=Resources.Load("thumbs/thumb1024A") as Texture2D;
	stats.sprite=Resources.Load("sprites/sprite1024A") as Texture2D;
	stats.bio=false; stats.mech=true;
	stats.morph=0;
	stats.init=2; stats.mob=trm;
	stats.hp=60; stats.mhp=60; stats.def=1;
	stats.actNums[1,action]=100015;//lin trm
		stats.actNums[1,tar]=lin;
		stats.actNums[1,rng]=2;
	stats.actNums[3,action]=100031;//lin
		stats.actText[3,actionName]="Shoot";
		stats.actNums[3,tar]=lin;
		stats.actNums[3,rng]=3; stats.actNums[3,mag]=11;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=102441;
		stats.actText[4,actionName]="Artillery Mortar";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=2;
		stats.actNums[4,tar]=arc;
		stats.actNums[4,rng]=8; stats.actNums[4,mag]=14;
		stats.actNums[4,dmgtype]=exp;
		stats.actNums[4,dec]=0.5;
		stats.actNums[4,crz]=0; stats.actNums[4,rad]=4;
	stats.actNums[5,action]=102451;
		stats.actText[5,actionName]="Incendiary Fire";
		stats.actNums[5,ap]=1; stats.actNums[5,fp]=1;
		stats.actNums[5,tar]=lin;
		stats.actNums[5,rng]=2; stats.actNums[5,mag]=12;
		stats.actNums[5,dmgtype]=fir;
		stats.actNums[5,dec]=0.5;
		stats.actNums[5,rad]=1;
	stats.actNums[6,action]=102461;
		stats.actText[6,actionName]="Siege Mode";
		stats.actText[6,desc]="+3DEF\nImmobilize";
		stats.actNums[6,ap]=1; stats.actNums[6,fp]=1;
	stats.actNums[7,action]=100071;
		stats.actText[7,actionName]="Create Demolitia";
		stats.actText[7,desc]="Ground unit\nGrenade-lobing infantry.";
		stats.actNums[7,ap]=1; stats.actNums[7,fp]=0;
		stats.actNums[7,mag]=1021;
	stats.actNums[8,action]=100071;
		stats.actText[8,actionName]="Create Condor";
		stats.actText[8,desc]="Ground unit\nQuick, mine-laying speeder.";
		stats.actNums[8,ap]=1; stats.actNums[8,fp]=1;
		stats.actNums[8,mag]=1022;
	stats.actNums[9,action]=100072;
		stats.actText[9,actionName]="Create Panopticlops";
		stats.actText[9,desc]="Trample unit\nSlow, but devasting monolith tank.";
		stats.actNums[9,ap]=1; stats.actNums[9,fp]=2;
		stats.actNums[9,mag]=1023;
}
function I1031(object: GameObject){
	stats.objname="Mournking";
	stats.thumb=Resources.Load("thumbs/thumb1031") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1031") as Texture2D;
	stats.bio=true; stats.mech=false;
	stats.init=3; stats.mob=gnd;
	stats.hp=12; stats.mhp=12; stats.def=0;
	stats.actNums[1,action]=100010; //serp gnd
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=2;	
	stats.actNums[3,action]=100030; //melee
		stats.actText[3,actionName]="Good Mourning";
		stats.actNums[3,tar]=serp;
		stats.actNums[3,rng]=1; stats.actNums[3,mag]=10;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=103141; //wind up
		stats.actText[4,actionName]="Wind Up";
		stats.actText[4,desc]="Good Mourning +1RNG.\nResets on next use.";
		stats.actNums[4,ap]=0; stats.actNums[4,fp]=1;
}
function I1032(object: GameObject){	
	stats.objname="Phoenix";
	stats.thumb=Resources.Load("thumbs/thumb1032") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1032") as Texture2D;
	stats.bio=true; stats.mech=false;
	stats.init=2; stats.mob=fly;
	stats.hp=15; stats.mhp=15; stats.def=0;
	stats.actNums[1,action]=100012; //fly serp
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=4;
	stats.actNums[3,action]=100030; //melee
		stats.actText[3,actionName]="Claw";
		stats.actNums[3,tar]=serp;
		stats.actNums[3,rng]=1; stats.actNums[3,mag]=8;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=103241; //pickup
	stats.actNums[5,action]=103251; //drop
	stats.corpsetype=2; //become ashes
}	
function I1033(object: GameObject){
	stats.objname="Rambuchet";
	stats.thumb=Resources.Load("thumbs/thumb1033") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1033") as Texture2D;
	stats.bio=false; stats.mech=true;
	stats.init=1; stats.mob=trm;
	stats.hp=36; stats.mhp=36; stats.def=0;
	stats.actNums[1,action]=100011; //trm serp
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=3;	
	stats.actNums[3,action]=103331; //ram/lob
		stats.actText[3,actionName]="Rambuchetize";
		stats.actText[3,desc]="10DMG may be dealt to an additional target exactly 2 cells behind first target.";
		stats.actNums[3,tar]=lin;
		stats.actNums[3,rng]=1; stats.actNums[3,mag]=14;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=103341; //momentum
		stats.actText[4,actionName]="Momentum";
		stats.actText[4,desc]="Move up to 5 cells.  If path is blocked by a unit, deal 5DMG per cell moved.";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=1;
		stats.actNums[4,tar]=lin;
		stats.actNums[4,rng]=5;
		stats.actNums[4,dmgtype]=nrml;
	stats.actNums[5,action]=103351; //pickup corpse
		stats.actText[5,actionName]="Pick up Corpse";
		stats.actText[5,desc]="Pick up any neighboring Corpse."; 
		stats.actNums[5,ap]=0; stats.actNums[5,fp]=1;
		stats.actNums[5,rng]=1;
	stats.actNums[6,action]=103361; //fling corpse
		stats.actText[6,actionName]="Fling Corpse";
		stats.actText[6,desc]="Move carried Corpse to target cell.  If cell is occupied, destroy Corpse and do 8DMG to occupying unit.";
		stats.actNums[6,ap]=1; stats.actNums[6,fp]=1;
		stats.actNums[6,tar]=arc;
		stats.actNums[6,rng]=5; stats.actNums[6,mag]=8;
		stats.actNums[6,dmgtype]=nrml;
}		
function I1034(object: GameObject){
	stats.objname="Castle Dragon";		
	stats.thumb=Resources.Load("thumbs/thumb1034A") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1034A") as Texture2D;
	stats.bio=true; stats.mech=false;
	stats.morph=0;
	stats.init=2; stats.mob=fly;
	stats.hp=55; stats.mhp=55; stats.def=0;
	stats.actNums[1,action]=100012;//serp fly
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=5;	
	stats.actNums[3,action]=100030;//melee
		stats.actText[3,actionName]="Maul";
		stats.actNums[3,tar]=serp;
		stats.actNums[3,rng]=1; stats.actNums[3,mag]=15;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=103441;
		stats.actText[4,actionName]="Tail Spin";
		stats.actText[4,desc]="Knockback: 1";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=1;
		stats.actNums[4,tar]=radial;
		stats.actNums[4,rng]=1; stats.actNums[4,mag]=8;
		stats.actNums[4,dmgtype]=nrml;
	stats.actNums[5,action]=103451;
		stats.actText[5,actionName]="Slash and Burn";
		stats.actText[5,desc]="Move up to 5 cells.  Deal damage to each unit crossed.";
		stats.actNums[5,ap]=1; stats.actNums[5,fp]=1;
		stats.actNums[5,tar]=lin;
		stats.actNums[5,rng]=5; stats.actNums[5,mag]=8;
		stats.actNums[5,dmgtype]=fir;
		stats.actNums[5,dec]=0.5; stats.actNums[5,rad]=1;
	stats.actNums[6,action]=103461;
		stats.actText[6,actionName]="Castlize";
		stats.actText[6,desc]="+3DEF \t Ground\nImmobilize\n+CNT (DEC 50%)";
		stats.actNums[6,ap]=1; stats.actNums[6,fp]=1;
	stats.actNums[7,action]=100071;
		stats.actText[7,actionName]="Create Mournking";
		stats.actText[7,desc]="Ground unit\nOrcish infantry brute.";
		stats.actNums[7,ap]=1; stats.actNums[7,fp]=0;
		stats.actNums[7,mag]=1031;
	stats.actNums[8,action]=100073;
		stats.actText[8,actionName]="Create Phoenix";
		stats.actText[8,desc]="Flying unit\nFast, regenerating fire-breather.";
		stats.actNums[8,ap]=1; stats.actNums[8,fp]=1;
		stats.actNums[8,mag]=1032;
	stats.actNums[9,action]=100072;
		stats.actText[9,actionName]="Create Rambuchet";
		stats.actText[9,desc]="Trample unit\nSlow, crushing siege weapon.";
		stats.actNums[9,ap]=1; stats.actNums[9,fp]=2;
		stats.actNums[9,mag]=1033;
}
function I1041(object: GameObject){
	stats.objname="Grizzly Elder";
	stats.thumb=Resources.Load("thumbs/thumb1041") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1041") as Texture2D;
	stats.bio=true; stats.mech=false;
	stats.init=2; stats.mob=gnd;
	stats.hp=11; stats.mhp=11; stats.def=0;
	stats.actNums[1,action]=100010;//serp gnd
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=2;	
	stats.actNums[3,action]=100030;//melee
		stats.actText[3,actionName]="Claw";
		stats.actNums[3,tar]=serp;
		stats.actNums[3,rng]=1; stats.actNums[3,mag]=5;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=104141;//conjure terrain
		stats.actText[4,actionName]="Conjure Terrain";
		stats.actText[4,desc]="Create destructible obstacle.";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=1;
		stats.actNums[4,tar]=serp;
		stats.actNums[4,rng]=1;
	stats.actNums[5,action]=104151;//burial
		stats.actText[5,actionName]="Burial";
		stats.actText[5,desc]="Destroy neighboring Corpse.\n+5HP";
		stats.actNums[5,ap]=1; stats.actNums[5,fp]=0;
		stats.actNums[5,mag]=5;
}
function I1042(object: GameObject){
	stats.objname="Laughing Owl";
	stats.thumb=Resources.Load("thumbs/thumb1042") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1042") as Texture2D;
	stats.bio=true; stats.mech=false;
	stats.init=2; stats.mob=fly;
	stats.hp=16; stats.mhp=16; stats.def=0;
	stats.actNums[1,action]=100016; //fly lin
		stats.actNums[1,tar]=lin;
		stats.actNums[1,rng]=6;
	stats.actNums[3,action]=100030; //melee
		stats.actText[3,actionName]="Claw";
		stats.actNums[3,tar]=1;
		stats.actNums[3,rng]=1; stats.actNums[3,mag]=7;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=104241; //transport enemy
		stats.actText[4,actionName]="Transport Enemy";
		stats.actText[4,desc]="Move up to 6 cells and return to starting cell.  Pickup a target unit crossed along the way, and drop in any vacant cell.  Target unit takes 9DMG.";
		stats.actNums[4,tar]=lin;
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=1;
		stats.actNums[4,rng]=6; stats.actNums[4,mag]=9;
		stats.actNums[4,dmgtype]=nrml;
}		
function I1043(object: GameObject){
	stats.objname="Meta-Terrainean";
	stats.bio=true; stats.mech=false;
	stats.init=1; stats.mob=trm;
	stats.hp=40; stats.mhp=40; stats.def=0;
	stats.actNums[1,action]=100011; //trm serp
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=2;
	stats.actNums[3,action]=100030; //melee
		stats.actText[3,actionName]="Punch";
		stats.actNums[3,tar]=serp;
		stats.actNums[3,rng]=1; stats.actNums[3,mag]=11;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=104341; //consume terrain
		stats.actText[4,actionName]="Consume Terrain";
		stats.actText[4,desc]="Destroy any neighboring non-Corpse destructible obstacle.\n+8HP";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=1;
		stats.actNums[4,tar]=serp;
		stats.actNums[4,rng]=1; stats.actNums[4,mag]=8;
}		
function I1044(object: GameObject){
	stats.objname="Yeti Mtn Sloth Beast";
	stats.thumb=Resources.Load("thumbs/thumb1044") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1044") as Texture2D;
	stats.bio=true; stats.mech=false;
	stats.init=2; stats.mob=gnd;
	stats.hp=50; stats.mhp=50; stats.def=0;
	stats.actNums[1,action]=100010;//serp gnd
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=3;	
	stats.actNums[3,action]=100030;//melee
		stats.actText[3,actionName]="Claw";
		stats.actNums[3,tar]=serp;
		stats.actNums[3,rng]=1; stats.actNums[3,mag]=9;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=104441;
		stats.actText[4,actionName]="Stampede";
		stats.actNums[4,ap]=2; stats.actNums[4,fp]=1;
	stats.actNums[5,action]=104451;
		stats.actText[5,actionName]="Aural Discharge";
		stats.actText[5,desc]="All other units on team:\n Deal 50% of Action3 DMG to neighboring units & take 3DMG. (Ignore all units' def.)";
		stats.actNums[5,ap]=1; stats.actNums[5,fp]=2;
	stats.actNums[6,action]=104461;
		stats.actText[6,actionName]="Torch of Thaw";
		stats.actText[6,desc]="All other units on team +1IN & Move RNG, until end of next turn.";
		stats.actNums[6,ap]=0; stats.actNums[6,fp]=2;
	stats.actNums[7,action]=100071;
		stats.actText[7,actionName]="Create Grizzly Elder";
		stats.actText[7,desc]="Ground unit\nWeak, terrain-manipulating infantry.";
		stats.actNums[7,ap]=1; stats.actNums[7,fp]=0;
		stats.actNums[7,mag]=1041;
	stats.actNums[8,action]=100073;
		stats.actText[8,actionName]="Create Laughing Owl";
		stats.actText[8,desc]="Flying unit\nFast transporter.";
		stats.actNums[8,ap]=2; stats.actNums[8,fp]=0;
		stats.actNums[8,mag]=1042;
	stats.actNums[9,action]=104491;
		stats.actText[9,actionName]="Create Meta-Terrainean";
		stats.actText[9,desc]="Trample unit\nSlow, regenerating brute.";
		stats.actNums[9,ap]=1; stats.actNums[9,fp]=2;
		stats.actNums[9,mag]=1043;
}
function I1051(object: GameObject){
	stats.objname="Gunslinger";
	stats.thumb=Resources.Load("thumbs/thumb1051") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1051") as Texture2D;
	stats.bio=true; stats.mech=false;
	stats.init=2; stats.mob=gnd;
	stats.hp=11; stats.mhp=11; stats.def=0;
	stats.actNums[1,action]=100010;//serp gnd
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=3;	
	stats.actNums[3,action]=100031;//lin
		stats.actText[3,actionName]="Shoot";
		stats.actNums[3,tar]=lin;
		stats.actNums[3,rng]=2; stats.actNums[3,mag]=8;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=105141; //load
		stats.actText[4,actionName]="Load";
		stats.actText[4,desc]="Add 1 bullet.";
		stats.actNums[4,ap]=0; stats.actNums[4,fp]=1;
		stats.bombs=0;
	stats.actNums[5,action]=105151; //quickdraw
		stats.actText[5,actionName]="Quickdraw";
		stats.actText[5,desc]="Repeat for each bullet, then remove all bullets.";
		stats.actNums[5,ap]=1; stats.actNums[5,fp]=1;
		stats.actNums[5,tar]=lin;
		stats.actNums[5,rng]=1; stats.actNums[5,mag]=10;
		stats.actNums[5,dmgtype]=nrml;
}	
function I1052(object: GameObject){
	stats.objname="Piecemaker";
	stats.thumb=Resources.Load("thumbs/thumb1052") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1052") as Texture2D;
	stats.bio=true; stats.mech=true;
	stats.init=1; stats.mob=gnd;
	stats.hp=17; stats.mhp=17; stats.def=0;
	stats.actNums[1,action]=100010; //gnd serp
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=4;	
	stats.actNums[3,action]=100030; //melee
		stats.actText[3,actionName]="Bludgeon";
		stats.actNums[3,tar]=serp;
		stats.actNums[3,rng]=1; stats.actNums[3,mag]=2;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=105241; //patience
		stats.actText[4,actionName]="Patience";
		stats.actText[4,desc]="Neighboring allies +6HP.";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=1;
	stats.actNums[5,action]=105251; //create gateA
		stats.actText[5,actionName]="Open Gate (+)";
		stats.actText[5,desc]="Creates a (+) portal gate.\nAny other (+) gates are destroyed.";
		stats.actNums[5,ap]=1; stats.actNums[5,fp]=1;
		stats.actNums[5,mag]=0;
	stats.actNums[6,action]=105251; //create gateB
		stats.actText[6,actionName]="Open Gate (-)";
		stats.actText[6,desc]="Creates a (-) portal gate.\nAny other (-) gates are destroyed.";
		stats.actNums[6,ap]=1; stats.actNums[6,fp]=1;
		stats.actNums[6,mag]=1;
}		
function I1053(object: GameObject){
	stats.objname="Chieftomaton";
	stats.thumb=Resources.Load("thumbs/thumb1053") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1053") as Texture2D;
	stats.bio=false; stats.mech=true;
	stats.init=1; stats.mob=gnd;
	stats.hp=20; stats.mhp=20; stats.def=0;
	stats.actNums[1,action]=100010; //gnd serp
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=3;	
	stats.actNums[3,action]=105331; //timeahawk
		stats.actText[3,actionName]="Time-a-hawk";
		stats.actText[3,desc]="Target -1FP";
		stats.actNums[3,tar]=lin;
		stats.actNums[3,rng]=2; stats.actNums[3,mag]=10;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=105341; //spirit time bomb
		stats.actText[4,actionName]="Spirit Time Bomb";
		stats.actText[4,desc]="Target +2IN(if friendly)/-2IN(if enemy) until end of next turn. \nUnits neighboring target +1IN(if friendly)/-1IN(if enemy) until end of next turn.";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=1;
		stats.actNums[4,tar]=arc;
		stats.actNums[4,rng]=5; stats.actNums[4,mag]=6;
		stats.actNums[4,dmgtype]=nrml;
}		
function I1054(object: GameObject){
	stats.objname="Old GrandDad";
	stats.thumb=Resources.Load("thumbs/thumb1054") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1054") as Texture2D;
	stats.bio=false; stats.mech=true;
	stats.init=2; stats.mob=gnd;
	stats.hp=57; stats.mhp=57; stats.def=1;
	stats.actNums[1,action]=100010;//serp gnd
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=2;	
	stats.actNums[3,action]=100031;//lin
		stats.actText[3,actionName]="Shoot";
		stats.actNums[3,tar]=lin;
		stats.actNums[3,rng]=3; stats.actNums[3,mag]=10;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=105441;
		stats.actText[4,actionName]="Hour Saviour";
		stats.actText[4,desc]="All units on your team advance in the Queue.  No unit may be skipped more than once.";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=0;
	stats.actNums[5,action]=105451;
		stats.actText[5,actionName]="Marksman";
		stats.actNums[5,ap]=1; stats.actNums[5,fp]=1;
		stats.actNums[5,tar]=arc;
		stats.actNums[5,rng]=5; stats.actNums[5,mag]=15;
		stats.actNums[5,dmgtype]=nrml;
	stats.actNums[6,action]=105461;
		stats.actText[6,actionName]="Second in Command";
		stats.actText[6,desc]="Target unit takes a turn next. It only gets 1AP that turn.";
		stats.actNums[6,ap]=1; stats.actNums[6,fp]=2;
	stats.actNums[7,action]=100071;
		stats.actText[7,actionName]="Create Gunslinger";
		stats.actText[7,desc]="Ground unit\nDamaging infantry.";
		stats.actNums[7,ap]=1; stats.actNums[7,fp]=0;
		stats.actNums[7,mag]=1051;
	stats.actNums[8,action]=100071;
		stats.actText[8,actionName]="Create Piecemaker";
		stats.actText[8,desc]="Ground unit\nHealing support droid.";
		stats.actNums[8,ap]=1; stats.actNums[8,fp]=1;
		stats.actNums[8,mag]=1052;
	stats.actNums[9,action]=100071;
		stats.actText[9,actionName]="Create Chieftomaton";
		stats.actText[9,desc]="Ground unit\nTime-bending mecha-elder";
		stats.actNums[9,ap]=1; stats.actNums[9,fp]=2;
		stats.actNums[9,mag]=1053;
}
function I1060(object: GameObject){
	stats.objname="Larva";
	stats.thumb=Resources.Load("thumbs/thumb1060") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1060") as Texture2D;
	stats.bio=true; stats.mech=false;
	stats.init=2; stats.mob=gnd;
	stats.hp=10; stats.mhp=10; stats.def=2;
	stats.actNums[1,action]=0;
	stats.actNums[3,action]=100033;//leech life
		stats.actText[3,actionName]="Leech Life";
		stats.actText[3,desc]="+1HP per DMG dealt.";
		stats.actNums[3,tar]=serp;
		stats.actNums[3,rng]=1; stats.actNums[3,mag]=5;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=106041;//evolve bee
		stats.actText[4,actionName]="Evolve: Beesassin";
		stats.actText[4,desc]="Flying unit\nFast, but fragile assassin.";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=0;
		stats.actNums[4,mag]=1061;
	stats.actNums[5,action]=106041;//evolve shrooman
		stats.actText[5,actionName]="Evolve: Shrooman";
		stats.actText[5,desc]="Ground unit\nInfectious support unit.";
		stats.actNums[5,ap]=2; stats.actNums[5,fp]=1;
		stats.actNums[5,mag]=1062;
	stats.actNums[6,action]=106041;//evolve flytrap
		stats.actText[6,actionName]="Evolve: Itza Trap";
		stats.actText[6,desc]="Trample unit\nSlow, carnivorous plant.";
		stats.actNums[6,ap]=0; stats.actNums[6,fp]=3;
		stats.actNums[6,mag]=1063;
	stats.corpsetype=0;
}	
function I1061(object: GameObject){
	stats.objname="Beesassin";
	stats.thumb=Resources.Load("thumbs/thumb1061") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1061") as Texture2D;
	stats.bio=true; stats.mech=false;
	stats.init=3; stats.mob=fly;
	stats.hp=9; stats.mhp=9; stats.def=0;
	stats.actNums[1,action]=100012; //fly serp
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=5;	
	stats.actNums[3,action]=100034; //serp psn
		stats.actText[3,actionName]="Poison Sting";
		stats.actNums[3,tar]=serp;
		stats.actNums[3,rng]=1; stats.actNums[3,mag]=6;
		stats.actNums[3,dmgtype]=psn;
		stats.actNums[3,dec]=0.5; stats.actNums[3,rad]=1;
	stats.actNums[4,action]=106141; //death sting
		stats.actText[4,actionName]="Death Sting";
		stats.actText[4,desc]="Beesassin is sacrificed.";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=1;
		stats.actNums[4,tar]=serp;
		stats.actNums[4,rng]=1; stats.actNums[4,mag]=12;
		stats.actNums[4,dmgtype]=psn;
		stats.actNums[4,dec]=0.5; stats.actNums[4,rad]=5;
}
function I1062(object: GameObject){
	stats.objname="Shrooman";
	stats.thumb=Resources.Load("thumbs/thumb1062") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1062") as Texture2D;	
	stats.bio=true; stats.mech=false;
	stats.init=2; stats.mob=gnd;
	stats.hp=15; stats.mhp=15; stats.def=0;
	stats.actNums[1,action]=100010; //gnd serp
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=2;	
	stats.actNums[3,action]=106231; //psn spores
		stats.actText[3,actionName]="Sporatic Emission";
		stats.actNums[3,tar]=arc;
		stats.actNums[3,rng]=2; stats.actNums[3,mag]=9;
		stats.actNums[3,dmgtype]=psn;
		stats.actNums[3,dec]=0.5; stats.actNums[3,rad]=3;
	stats.actNums[4,action]=106241; //infest corpse
		stats.actText[4,actionName]="Infest Corpse";
		stats.actText[4,desc]="Turn neighboring Corpse into a Larva.";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=0;
}		
function I1063(object: GameObject){
	stats.objname="Itza Trap";
	stats.thumb=Resources.Load("thumbs/thumb1063") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1063") as Texture2D;
	stats.bio=true; stats.mech=false;
	stats.init=3; stats.mob=fly;
	stats.hp=30; stats.mhp=30; stats.def=0;
	stats.actNums[1,action]=100011; //trm serp
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=1;	
	stats.actNums[3,action]=106331; //ensnare
		stats.actText[3,actionName]="Ensnare";
		stats.actNums[3,rng]=1; stats.actNums[3,mag]=10;
		stats.actNums[3,dmgtype]=nrml;
}		
function I1064(object: GameObject){
	stats.objname="Spider Queen";
	stats.thumb=Resources.Load("thumbs/thumb1064") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1064") as Texture2D;
	stats.bio=true; stats.mech=false;
	stats.init=2; stats.mob=gnd;
	stats.hp=45; stats.mhp=45; stats.def=0;
	stats.actNums[1,action]=100010;//serp gnd
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=4;	
	stats.actNums[3,action]=106431;//melee
		stats.actText[3,actionName]="Bite";
		stats.actNums[3,tar]=serp;
		stats.actNums[3,rng]=1; stats.actNums[3,mag]=10;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=106441;
		stats.actText[4,actionName]="Webshot";
		stats.actText[4,desc]="Target -2 Move RNG until end of next turn, -1 Move RNG until end of two turns."; 
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=1;
		stats.actNums[4,tar]=arc;
		stats.actNums[4,rng]=3; stats.actNums[4,mag]=12;
		stats.actNums[4,dmgtype]=psn;
		stats.actNums[4,dec]=0.5; stats.actNums[4,rad]=1;
	stats.actNums[5,action]=106451;
		stats.actText[5,actionName]="Swarm";
		stats.actText[5,desc]="All non-Larva friendly units get +1DMG on Action3, for each non-Larva friendly unit, until the end of their next turn.";
		stats.actNums[5,ap]=1; stats.actNums[5,fp]=2;
	stats.actNums[6,action]=106461;
		stats.actText[6,actionName]="Megadearth";
		stats.actText[6,desc]="All units of target team recieve 2PSN DMG per unit on their team.";
		stats.actNums[6,ap]=2; stats.actNums[6,fp]=2;
		stats.actNums[6,dmgtype]=psn;
		stats.actNums[6,dec]=0.5; stats.actNums[6,rad]=1;
	stats.actNums[7,action]=100071;
		stats.actText[7,actionName]="Create Larva";
		stats.actText[7,desc]="Ground unit\nWeak, evolvable hatchling.";
		stats.actNums[7,ap]=0; stats.actNums[7,fp]=0;
		stats.actNums[7,mag]=1060;
}
function I1071(object: GameObject){
	stats.objname="Scarabot";
	stats.thumb=Resources.Load("thumbs/thumb1071") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1071") as Texture2D;
	stats.bio=false; stats.mech=true;
	stats.init=2; stats.mob=trm;
	stats.hp=12; stats.mhp=12; stats.def=0;
	stats.actNums[1,action]=100011;//serp trm
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=3;	
	stats.actNums[3,action]=100031;//lin
		stats.actText[3,actionName]="Shoot";
		stats.actNums[3,tar]=lin;
		stats.actNums[3,rng]=2; stats.actNums[3,mag]=6;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=107141; //prism cannon
		stats.actText[4,actionName]="Prism Cannon";
		stats.actNums[4,tar]=lin;
		stats.actNums[4,rng]=4; stats.actNums[4,mag]=10;
		stats.actNums[4,dmgtype]=lsr;
		stats.actNums[4,dec]=0.5; stats.actNums[4,rad]=3;
}
function I1072(object: GameObject){
	stats.objname="Haze";
	stats.thumb=Resources.Load("thumbs/thumb1072") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1072") as Texture2D;
	stats.bio=false; stats.mech=false;
	stats.init=1; stats.mob=gas;
	stats.hp=22; stats.mhp=22; stats.def=1;
	stats.actNums[1,action]=100013; //gas serp
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=2;	
	stats.actNums[3,action]=107231; //melee
		stats.actText[3,actionName]="Mnemonic Plague";
		stats.actText[3,desc]="Self +1HP per 2 damage dealt.  You may give target co-occupying unit +1HP per damage dealt.";
		stats.actNums[3,rng]=0; stats.actNums[3,mag]=7;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=107241; //accumulation
		stats.actText[4,actionName]="Accumulate";
		stats.actText[4,desc]="Merge with any co-occupying Haze.  Gain half accumulated Haze's HP.  Gain accumulated Haze's FP, DEF. Size increases.";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=1;
	stats.actNums[5,action]=107251; //mirage
		stats.actText[5,actionName]="Mirage";
		stats.actText[5,desc]="(Passive effect)\nCo-occupying friendly units have a 25% dodge rate.";
		stats.actNums[5,ap]=0; stats.actNums[5,fp]=0;
	stats.actNums[6,action]=107261; //tractor gust
		stats.actText[6,actionName]="Tractor Gust";
		stats.actText[6,desc]="Move target unit into a space occupied by Haze.";
		stats.actNums[6,ap]=0; stats.actNums[6,fp]=1;
		stats.actNums[6,tar]=serp;
		stats.actNums[6,rng]=2;
	stats.corpsetype=0; //no corpse
}
function I1073(object: GameObject){
	stats.objname="Priest of Naja";
	stats.bio=true; stats.mech=true;
	stats.thumb=Resources.Load("thumbs/thumb1073") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1073") as Texture2D;
	stats.init=3; stats.mob=gnd;
	stats.hp=28; stats.mhp=28; stats.def=1;
	stats.actNums[1,action]=100010; //gnd serp
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=4;
	stats.actNums[3,action]=100030; //melee
		stats.actText[3,actionName]="Bludgeon";
		stats.actNums[3,tar]=serp;
		stats.actNums[3,rng]=1; stats.actNums[3,mag]=9;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=107341; //force shove
	stats.actNums[5,action]=107351; //super shove
}
function I1074(object: GameObject){
	stats.objname="Cyborg Super Sultan";
	stats.thumb=Resources.Load("thumbs/thumb1074") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1074") as Texture2D;
	stats.bio=true; stats.mech=true;
	stats.init=2; stats.mob=fly;
	stats.hp=55; stats.mhp=55; stats.def=2;
	stats.actNums[1,action]=100012;//serp fly
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=4;	
	stats.actNums[3,action]=100031;//lin
		stats.actText[3,actionName]="Psi Beam";
		stats.actNums[3,tar]=lin;
		stats.actNums[3,rng]=3; stats.actNums[3,mag]=9;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=107441;
		stats.actText[4,actionName]="Restoration";
		stats.actText[4,desc]="Target unit gains HP equal to half it's MHP.";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=1;
		stats.actNums[4,tar]=arc;
		stats.actNums[4,rng]=7;
	stats.actNums[5,action]=107451;
		stats.actText[5,actionName]="Teleport Enemy";
		stats.actText[5,desc]="Move an enemy unit to any legal space.";
		stats.actNums[5,ap]=1; stats.actNums[5,fp]=1;		
		stats.actNums[5,tar]=arc;
		stats.actNums[5,rng]=3;
	stats.actNums[6,action]=107461;
		stats.actText[6,actionName]="Lightning Storm";
		stats.actText[6,desc]="If a Haze is co-occupying the target's cell, repeat with 50% DMG.";
		stats.actNums[6,ap]=2; stats.actNums[6,fp]=2;		
		stats.actNums[6,tar]=arc;
		stats.actNums[6,rng]=5; stats.actNums[6,mag]=15;
		stats.actNums[6,dmgtype]=elc;
		stats.actNums[6,dec]=0.5;
	stats.actNums[7,action]=100072;
		stats.actText[7,actionName]="Create Scarabot";
		stats.actText[7,desc]="Trample unit\nRugged infantry droid.";
		stats.actNums[7,ap]=1; stats.actNums[7,fp]=0;
		stats.actNums[7,mag]=1071;
	stats.actNums[8,action]=100074;
		stats.actText[8,actionName]="Create Haze";
		stats.actText[8,desc]="Gaseous unit\nSlow, shielding support unit.";
		stats.actNums[8,ap]=1; stats.actNums[8,fp]=1;
		stats.actNums[8,mag]=1072;
	stats.actNums[9,action]=100071;
		stats.actText[9,actionName]="Create Priest of Naja";
		stats.actText[9,desc]="Ground unit\nFast, telekinetic brute.";
		stats.actNums[9,ap]=1; stats.actNums[9,fp]=2;
		stats.actNums[9,mag]=1073;
}
function I1081(object: GameObject){
	stats.objname="Corpse Fiend";
	stats.thumb=Resources.Load("thumbs/thumb1081") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1081") as Texture2D;
	stats.bio=true; stats.mech=false;
	stats.obclass=4;
	stats.init=2; stats.mob=gnd;
	stats.hp=15; stats.mhp=15; stats.def=0;
	stats.actNums[1,action]=100010;//serp gnd
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=2;	
	stats.actNums[3,action]=100032;
		stats.actText[3,actionName]="Rage";
		stats.actText[3,desc]="-3HP";
		stats.actNums[3,tar]=serp;
		stats.actNums[3,rng]=1; stats.actNums[3,mag]=10; stats.actNums[3,dec]=(-3);
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=108141; //cannibalize
		stats.actText[4,actionName]="Cannibalize";
		stats.actText[4,desc]="Destroy any neighboring Corpse.\n+5HP & MHP, +2DMG on Action3.";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=0;
		stats.actNums[4,rng]=1; stats.actNums[4,mag]=5;
	stats.actNums[5,action]=108151; //explode
		stats.actText[5,actionName]="Explode";
		stats.actNums[5,ap]=1; stats.actNums[5,fp]=1;
		stats.actNums[5,rng]=0; stats.actNums[5,mag]=15;
		stats.actNums[5,dmgtype]=exp;
		stats.actNums[5,dec]=0.5;
		stats.actNums[5,crz]=0; stats.actNums[5,rad]=3;
	stats.corpsetype=0;//no corpse
}
function I1082(object: GameObject){
	stats.objname="Necrochancellor";
	stats.thumb=Resources.Load("thumbs/thumb1082") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1082") as Texture2D;
	stats.bio=true; stats.mech=false;
	stats.init=2; stats.mob=gnd;
	stats.hp=17; stats.mhp=17; stats.def=2;
	stats.actNums[1,action]=100010; //gnd serp
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=3;	
	stats.actNums[3,action]=100030; //melee
		stats.actText[3,actionName]="Bludgeon";
		stats.actNums[3,tar]=serp;
		stats.actNums[3,rng]=1; stats.actNums[3,mag]=6;
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=108241; //move corpse
		stats.actText[4,actionName]="Move Corpse";
		stats.actText[4,desc]="Move any neighboring Corpse to any legal cell.";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=1;
		stats.actNums[4,rng]=1;
		stats.actNums[4,tar]=serp;
	stats.actNums[5,action]=108251; //contagion
	stats.actNums[6,action]=108261; //open void gate
		stats.actText[6,actionName]="Open Void Gate";
		stats.actText[6,desc]="Create minion-spawning gate from the Void.";
		stats.actNums[6,ap]=1; stats.actNums[6,fp]=2;
		stats.actNums[6,rng]=1;
		stats.actNums[6,tar]=serp;
}
function I1083(object: GameObject){
	stats.objname="Magman";
	stats.thumb=Resources.Load("thumbs/thumb1083A") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1083A") as Texture2D;
	stats.bio=true; stats.mech=false;
	stats.morph=0;
	stats.init=1; stats.mob=trm;
	stats.hp=25; stats.mhp=25; stats.def=0;
	stats.actNums[1,action]=100011; //trm serp
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=4;	
	stats.actNums[3,action]=108431; 
		stats.actText[3,actionName]="Magma Fist";
		stats.actNums[3,ap]=2;
		stats.actNums[3,tar]=lin;
		stats.actNums[3,rng]=2; stats.actNums[3,mag]=20;
		stats.actNums[3,dmgtype]=fir;
		stats.actNums[3,dec]=0.5; stats.actNums[3,rad]=1;
		
	stats.actNums[4,action]=108341; //transform
		stats.actText[4,actionName]="Solidify";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=1;
}		
function I1084(object: GameObject){
	stats.objname="Cthulhoid";
	stats.thumb=Resources.Load("thumbs/thumb1084") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb1084") as Texture2D;
	stats.bio=true; stats.mech=false;
	stats.init=3; stats.mob=fly;
	stats.hp=60; stats.mhp=60; stats.def=0;
	stats.actNums[1,action]=100012;//serp fly
		stats.actNums[1,tar]=serp;
		stats.actNums[1,rng]=4;	
	stats.actNums[3,action]=100032;
		stats.actText[3,actionName]="Rage";
		stats.actText[3,desc]="-5HP";
		stats.actNums[3,tar]=serp;
		stats.actNums[3,rng]=1; stats.actNums[3,mag]=15; stats.actNums[3,dec]=(-5);
		stats.actNums[3,dmgtype]=nrml;
	stats.actNums[4,action]=108441;
		stats.actText[4,actionName]="Sacrifize";
		stats.actText[4,desc]="Destroy neighboring friendly unit.\n+1DEF, +1FP";
		stats.actNums[4,ap]=1; stats.actNums[4,fp]=0;
		stats.actNums[4,rng]=1;
		stats.actNums[4,tar]=serp;
	stats.actNums[5,action]=108451;
		stats.actText[5,actionName]="Devour";
		stats.actText[5,desc]="+1HP per damage dealt.";
		stats.actNums[5,ap]=1; stats.actNums[5,fp]=0;
		stats.actNums[5,tar]=serp;
		stats.actNums[5,rng]=1; stats.actNums[5,mag]=10;
		stats.actNums[5,dmgtype]=nrml;
	stats.actNums[6,action]=108461;
		stats.actText[6,actionName]="Eternal Flame";
		stats.actNums[6,ap]=1; stats.actNums[6,fp]=2;
		stats.actNums[6,tar]=lin;
		stats.actNums[6,rng]=2; stats.actNums[6,mag]=12;
		stats.actNums[6,dmgtype]=fir;
		stats.actNums[6,dec]=1.0; stats.actNums[6,rad]=10;
	stats.actNums[7,action]=100071;
		stats.actText[7,actionName]="Create Corpse Fiend";
		stats.actText[7,desc]="Ground unit\nStrong, suicidal infantry.";
		stats.actNums[7,ap]=1; stats.actNums[7,fp]=0;
		stats.actNums[7,mag]=1081;
	stats.actNums[8,action]=100071;
		stats.actText[8,actionName]="Create Necrochancellor";
		stats.actText[8,desc]="Ground unit\nResilient Corpse-pedler.";
		stats.actNums[8,ap]=2; stats.actNums[8,fp]=0;
		stats.actNums[8,mag]=1082;
	stats.actNums[9,action]=100071;
		stats.actText[9,actionName]="Create Magman";
		stats.actText[9,desc]="Trample unit\nDamaging rock brute.";
		stats.actNums[9,ap]=2; stats.actNums[9,fp]=1;
		stats.actNums[9,mag]=1083;
}	
//items
function I2021(object: GameObject){
	stats.objname="Mine";
	stats.thumb=Resources.Load("thumbs/thumb2021") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb2021") as Texture2D;
	stats.hp=1; stats.mhp=1;
	object.tag="obstacle";
	stats.obclass=3;
	actionCoord.Mlog("Player"+stats.owner+" laid a mine.");
	object.name="Mine - Player "+stats.owner;
	
	var mycell: GameObject = stats.mycell;
	
	var triggerZone: GameObject;
	triggerZone=Instantiate(mineTrigger,mycell.transform.position,Quaternion.identity);
	triggerZone.transform.parent=transform;
}
function I2031(object: GameObject){
	stats.objname="Phoenix Ashes";
	stats.thumb=Resources.Load("thumbs/thumb2031") as Texture2D;
	stats.sprite=Resources.Load("thumbs/thumb2031") as Texture2D;
	stats.ap=0; stats.fp=0;
	stats.init=2; stats.mob=gnd;
	stats.actNums[1,action]=203110;
	stats.actText[1,actionName]="Arise";
		stats.actNums[1,ap]=0; stats.actNums[1,fp]=2;
	stats.actText[2,actionName]="Focus";
		stats.actText[2,desc]="+1FP";
		stats.actNums[2,ap]=1; stats.actNums[2,fp]=0;
	stats.corpsetype=0;
	object.tag="obstacle";
	stats.obclass=4;
	stats.obtype="Corpse";	
	stats.skipped=false;
	actionCoord.Mlog("Player"+stats.owner+"'s Phoenix burnt to ash.");
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