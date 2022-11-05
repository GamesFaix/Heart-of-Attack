#pragma strict

function OnGUI () {
	gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);


}