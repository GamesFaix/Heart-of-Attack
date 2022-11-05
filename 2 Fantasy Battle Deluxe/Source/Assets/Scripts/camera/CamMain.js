#pragma strict

var target : Transform;
var x: float=0.0;
var y: float=0.0;
private var xSpeed: short = 200;
private var ySpeed: short = 150;
private var xMinLimit: short = 90;
private var xMaxLimit: short = 270;
private var yMinLimit: short = -360; 
private var yMaxLimit: short = 360;

var rotation: Quaternion;
var relativePos: Quaternion;
var targetPos: Vector3;

var damping: short = 3;
var hit: RaycastHit;

var orbiter: GameObject;
var orbiterPos: Vector3;
var wait=0;

var rotStart: Quaternion;

var gui_game: GUI_Game;
var gui_master: GUI_Master;

function Awake(){
	gui_game=GameObject.Find("GUIPrefab").GetComponent(GUI_Game);
	gui_master=GameObject.Find("GUIPrefab").GetComponent(GUI_Master);
}

function Update(){

	rotStart=transform.rotation;
	orbiterPos=orbiter.transform.position;
	
	if (target!=null){//look at target when object selected, reset target
		var startRotation: Quaternion = transform.rotation;
		relativePos=Quaternion.LookRotation(target.position - transform.position);
	
		transform.rotation = Quaternion.Slerp(startRotation, relativePos, Time.deltaTime*damping);
	
		if(Physics.Raycast(transform.position,orbiterPos, hit, Mathf.Infinity)){
			if(hit.transform.gameObject==target.gameObject.GetComponent(ObjectStats).mycell){
				if (wait==1){
					target=null;
				}
				wait++;
			}
		}
	}
	x=transform.rotation.eulerAngles.x;
	y=transform.rotation.eulerAngles.y;	
	
	if (target==null){wait=0;}
}

function OnGUI(){	

	rotStart=transform.rotation;
	
    var e : Event = Event.current;
	var mouseOnGUI: boolean = gui_game.MouseOnGUI();
    if(e.button == 1 
    && gui_master.view=="game" 
    && gui_game.showMenu==false
	&& mouseOnGUI==false){
	
			y += Input.GetAxis("Mouse X") * xSpeed  * 0.01; //get change in position
        	x -= Input.GetAxis("Mouse Y") * ySpeed * 0.01;
 			
			y = Yclamp(y, yMinLimit, yMaxLimit); //reset Y if over-rotated
 		    
			//if (x>90 && x<270){x-=360;}
			
			//x = Xclamp(x, xMinLimit, xMaxLimit); //reset X if over-rotated

        	rotation = Quaternion.Euler(x, y, 0);
        	
        	transform.rotation = rotation; //align camera to new rotation
    }
	
	
	Camera.main.transform.LookAt(Vector3.zero);
}

//resets angle if beyond 360
static function Yclamp (angle : float, min : float, max : float) {
	if (angle < min)
		angle += 360;
	if (angle > max)
		angle -= 360;
	return Mathf.Clamp (angle, min, max);
}

static function Xclamp (angle : float, min : float, max : float) {
	if (angle > 90 && angle <270 )
		angle += 180;
	//if (angle > 270)
		//angle -= 180;
	return Mathf.Clamp (angle, min, max);
}
