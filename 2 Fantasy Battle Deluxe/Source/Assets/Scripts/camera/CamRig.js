#pragma strict

//prefabs
var camMainPrefab: GameObject;
var camOrbiterPrefab: GameObject;
//instances
var myCam: GameObject;
var myOrbiter: GameObject;
//references
var gui_game: GUI_Game;
var gui_master: GUI_Master;

function Awake(){
	gui_game=GameObject.Find("GUIPrefab").GetComponent(GUI_Game);
	gui_master=GameObject.Find("GUIPrefab").GetComponent(GUI_Master);

	myCam = Instantiate(camMainPrefab,Vector3(-20,-20,-20),Quaternion.identity);
	myCam.transform.parent=transform;
	
	myOrbiter = Instantiate(camOrbiterPrefab,transform.position,Quaternion.identity);
	myOrbiter.transform.parent=transform;
	myOrbiter.name="camOrbiter";
}


var x: float=0.0;
var y: float=0.0;
var rotation: Quaternion;
var rotStart: Quaternion;

function OnGUI(){	
	rotStart=transform.rotation;
	
    var e : Event = Event.current;
    if(e.button == 1){Drag();}
	
	myCam.transform.LookAt(Vector3.zero);
}

function Drag(){
	var mouseOnGUI: boolean = gui_game.MouseOnGUI();	
	if (gui_master.view=="game" && gui_game.showMenu==false && mouseOnGUI==false){
	
		y += Input.GetAxis("Mouse X") * xSpeed  * 0.01; //get change in position
		x -= Input.GetAxis("Mouse Y") * ySpeed * 0.01;
 		y = Yclamp(y, yMinLimit, yMaxLimit); //reset Y if over-rotated
 		    
		rotation = Quaternion.Euler(x, y, 0);
        	
		transform.rotation = rotation; //align camera to new rotation
	}
}

var relativePos: Quaternion;
var hit: RaycastHit;

function Focus(target: GameObject): IEnumerator{
	var damping: short = 3;
	
	myOrbiter.transform.position=target.transform.position;

	rotStart=transform.rotation;
	var startRotation: Quaternion = transform.rotation;
		
		relativePos=Quaternion.LookRotation(target.transform.position - transform.position);
	
		transform.rotation = Quaternion.Slerp(startRotation, relativePos, Time.deltaTime*damping);
	
		Physics.Raycast(transform.position,myOrbiter.transform.position, hit, Mathf.Infinity);
		if(hit.transform.gameObject==target.GetComponent(ObjectStats).mycell){
			//break;
		}
		
	
	x=transform.rotation.eulerAngles.x;
	y=transform.rotation.eulerAngles.y;	
	
	yield;
}




private var xSpeed: short = 200;
private var ySpeed: short = 150;
private var yMinLimit: short = -360; 
private var yMaxLimit: short = 360;
//resets angle if beyond 360
function Yclamp (angle : float, min : float, max : float) {
	if (angle < min)
		angle += 360;
	if (angle > max)
		angle -= 360;
	return Mathf.Clamp (angle, min, max);
}
