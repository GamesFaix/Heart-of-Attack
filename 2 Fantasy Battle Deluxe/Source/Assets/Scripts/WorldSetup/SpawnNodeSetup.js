#pragma strict

var spawn_number: byte;
var mycell: GameObject;
var myzone: GameObject;

function Start(){
	spawn_number=transform.parent.GetComponent(SpawnZoneSetup).spawn_number;
}

function OnTriggerStay(cell: Collider){
	if (cell.CompareTag("cell")){
		mycell=cell.gameObject;
	}
	if (cell.CompareTag("spawnzone")){
		myzone=cell.gameObject;
	}
}
