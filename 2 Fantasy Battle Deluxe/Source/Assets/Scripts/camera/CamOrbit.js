#pragma strict

function Update(){
	transform.position=Camera.main.ViewportToWorldPoint(Vector3(0.5,0.5,20));
	transform.rotation=Camera.main.transform.rotation;
}