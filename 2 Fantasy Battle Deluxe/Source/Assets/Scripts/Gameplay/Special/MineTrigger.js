var mine: GameObject;
var mineStats: Component;
var owner:int;
var wait=0;

function Start(){
	mine=transform.parent.gameObject;
	mineStats=mine.GetComponent(ObjectStats);
	owner=mineStats.owner;
	wait=1;
	}

function OnTriggerEnter(triggerer:Collider){
	var intruder: GameObject;
	intruder=triggerer.gameObject;
	if (intruder.CompareTag("unit") && wait==1){
		var intruderStats=intruder.GetComponent(ObjectStats);
		Debug.Log(intruder);
		if (!(intruderStats.objno==1022 && intruderStats.owner==owner)){
			mineStats.hp-=1;
		}
	}
}
