/*#pragma strict

var occupied: boolean;
var parentstats: ObjectStats;
var otherGate: ObjectStats;
var otherChild: PortalScript;
var gui_game: GUI_Game;
var occupier: GameObject;
*/
var pass: boolean = false;
/*
function Awake(){
	gui_game=GameObject.Find("GUIPrefab").GetComponent(GUI_Game);
	var parent: GameObject=transform.parent.gameObject;
	parentstats=parent.GetComponent(ObjectStats);
}

function Start(){
	StartCoroutine("CoStart");
}
function CoStart() : IEnumerator{
	while (true){yield CoUpdate();}
}

function CoUpdate() : IEnumerator{
	if (pass==true){
		yield PassThru();
		pass=false;
	}
}
function OnCollisionEnter(overlap:Collision){
	var unit: GameObject = overlap.gameObject;
	if (parentstats.otherGate){
		otherGate=parentstats.otherGate;
		otherChild=otherGate.GetComponentInChildren(PortalScript);
		otherChild.otherChild=this;
	}
	
	if (unit.CompareTag("unit")){
		occupied=true;
		occupier=unit.gameObject;
		Debug.Log("unit in portal");
		if (otherChild && otherChild.occupied==false){
			//Debug.Log("unit may cross portal");
			gui_game.portalBtn=true;
			gui_game.gate=this;
		}
	}	
}

function OnTriggerExit(unit: Collider){
	if (unit.CompareTag("unit")){
		occupied=false;
		//Debug.Log("unit left portal");
	}
}

function PassThru(): IEnumerator{
	var unitstats=occupier.GetComponent(ObjectStats);
	Debug.Log("unit cell "+unitstats.mycell);
	var gatestats=otherGate.GetComponent(ObjectStats);
	Debug.Log(otherGate);
	Debug.Log("other gate cell "+gatestats.mycell);
	unitstats.mycell=gatestats.mycell;
	Debug.Log("units new cell "+unitstats.mycell);
	occupier.transform.position=otherGate.transform.position;
	yield;
}
*/