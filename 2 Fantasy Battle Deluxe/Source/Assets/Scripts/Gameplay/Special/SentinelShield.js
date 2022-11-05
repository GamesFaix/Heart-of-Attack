#pragma strict

var def: byte;

function OnTriggerEnter(unit:Collider){
	if (unit.CompareTag("unit")){
		unit.GetComponent(ObjectStats).def+=def;
	}
}

function OnTriggerExit(unit:Collider){
	if (unit.CompareTag("unit")){
		unit.GetComponent(ObjectStats).def-=def;
	}
}