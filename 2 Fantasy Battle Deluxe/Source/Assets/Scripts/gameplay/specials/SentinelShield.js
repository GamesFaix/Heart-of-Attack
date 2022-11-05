#pragma strict
var def: byte;
static var eDEF: byte = 2;

function OnTriggerEnter(unit:Collider){
	if (unit.CompareTag("unit")){
		unit.GetComponent(ObjectStats).coreStats[eDEF]+=def;
	}
}

function OnTriggerExit(unit:Collider){
	if (unit.CompareTag("unit")){
		unit.GetComponent(ObjectStats).coreStats[eDEF]-=def;
	}
}