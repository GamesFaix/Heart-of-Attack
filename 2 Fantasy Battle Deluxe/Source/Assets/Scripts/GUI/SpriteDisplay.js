#pragma strict

var parentUnit: GameObject;
var myStats: ObjectStats;
var mycell: GameObject;
var cellStats: CellProperties;
var guiprefab: GameObject;
var gui_game: GUI_Game;
var targeting: Targeting;

var screenPos: Vector3;
var popUpText: String;
var popUpTime: float;

function Start(){
	parentUnit=transform.parent.gameObject;
	myStats=parentUnit.GetComponent(ObjectStats);
	guiprefab=GameObject.Find("GUIPrefab");
	gui_game=guiprefab.GetComponent(GUI_Game);
	targeting=GameObject.FindGameObjectWithTag("playmanager").GetComponent(Targeting);

}

function OnGUI(){

	transform.position=parentUnit.transform.position;
	
	if (renderer.isVisible){
		var sprite: Texture2D = myStats.sprite;
		var orthSize: float = Camera.main.orthographicSize;
		var size: float; //sprite size
		
		if (myStats.obclass && myStats.obclass<4){
			size=120*(1/(orthSize/5));
		}
		else{size=80*(1/(orthSize/5));}
		//get object position on camera & get camera position on screen
		var camPos: Vector3 = Camera.main.WorldToViewportPoint(transform.position);
		screenPos = Camera.main.ViewportToScreenPoint(camPos);
		
		//get cell info
		if(myStats.mycell){
			mycell=myStats.mycell;
			cellStats=mycell.GetComponent(CellProperties);
		}
		var spriteBox: Rect = new Rect(screenPos.x-(size/2),Screen.height-screenPos.y-(size/2),size,size);
		var box: Rect= new Rect(screenPos.x-(size/2),screenPos.y-(size/2),size,size);
		
		/*
		//if sharing cell, shrink and cascade sprites
		if (cellStats && cellStats.occA>0 && cellStats.occB>0){
			//shrink sprite
			size=65*(1/(orthSize/5));
			if (other){
				var otherStats=other.GetComponent(ObjectStats);
				var myStats=parentUnit.GetComponent(ObjectStats);
			//lower objno takes bottom-left
				if (otherStats.objno>myStats.objno){
					spriteBox = Rect(screenPos.x-(size*0.75),Screen.height-screenPos.y-(size*0.75),size,size);
					box = Rect(screenPos.x-(size*0.75),screenPos.y-(size*0.25),size,size);
				
				}
			//higher objno takes top-right
				if (otherStats.objno<myStats.objno){
					spriteBox = Rect(screenPos.x-(size*0.25),Screen.height-screenPos.y-(size*0.25),size,size);
					box = Rect(screenPos.x-(size*0.25),screenPos.y-(size*0.75),size,size);
				}
			}
		}
		*/
		
		//draw sprite
		if (guiprefab.GetComponent(GUI_Master).view=="game"){
			GUI.DrawTexture(spriteBox,sprite,ScaleMode.StretchToFill,true,0);
		
			//if sprite clicked, view
			if(Input.GetMouseButtonUp(0) && box.Contains(Input.mousePosition)
			&& gui_game.showMenu==false){
				targeting.targetobject=parentUnit;
			}
			if(Input.GetMouseButtonUp(1) && box.Contains(Input.mousePosition)
			&& gui_game.showMenu==false){
				gui_game.viewedobject=parentUnit;
			}
			if (popUpText!=null
			 && Time.time-popUpTime<1){
				var textBoxSize: int = 40;
				var textBox: Rect = new Rect(screenPos.x-(textBoxSize/2),Screen.height-screenPos.y-(textBoxSize/2),textBoxSize,textBoxSize);
				
				GUI.Label(textBox,popUpText);	
			}
		}	
	}
}




var other: GameObject;
/*
function OnTriggerEnter(sprite: Collider){
	if (sprite.CompareTag("sprite")){
		other=sprite.transform.parent.gameObject;
	}
}
*/
/*
function DoubleOccupation(){
	var unitlist=GameObject.FindGameObjectsWithTag("unit");
	var unitcheck=0;
	//Debug.Log(unitlist.length);
	while (unitcheck<unitlist.length){
		if (unitlist[unitcheck].GetComponent(ObjectStats).mycell==mycell && unitlist[unitcheck]!=parentUnit){
			other=unitlist[unitcheck];
		}
		unitcheck++;
	}
	var oblist=GameObject.FindGameObjectsWithTag("obstacle");
	var obcheck=0;
	//Debug.Log(oblist.length);
	while (obcheck<oblist.length){
		if (oblist[obcheck].GetComponent(ObjectStats).mycell==mycell && oblist[obcheck]!=parentUnit){
			other=oblist[obcheck];
		}
		obcheck++;
	}
	//Debug.Log(other);
}
*/