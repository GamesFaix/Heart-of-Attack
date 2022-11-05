#pragma strict

var cell: GameObject;
var cellproperties: CellProperties;
var hit: RaycastHit;

var playmanager: GameObject;
var gui_game: GUI_Game;
var targeting: Targeting;

function Awake(){
	gui_game=GameObject.Find("GUIPrefab").GetComponent(GUI_Game);
}
function Start(){
	cell=transform.parent.gameObject;
	cellproperties=cell.GetComponent(CellProperties);
}

function OnGUI () {
	if (cellproperties.legal==true && gui_game.MouseOnGUI()==false) {
		if(Input.GetMouseButtonDown(0) && collider.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), hit, Mathf.Infinity)){ 
			if (hit.collider==collider){
				playmanager=GameObject.FindGameObjectWithTag("playmanager");
				targeting=playmanager.GetComponent(Targeting);
				targeting.targetcell=cell;
				cellproperties.selected=true;		
			}
		}
  	}
}
