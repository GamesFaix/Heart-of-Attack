#pragma strict

var actions: Actions;
var actionCoord: ActionCoordinator;
var fightingstart: FightingStart;

var count: short;
var grid: byte;
var celllist: GameObject[];

function Awake(){
	actions=gameObject.GetComponent(Actions);
	actionCoord=gameObject.GetComponent(ActionCoordinator);
	fightingstart=GameObject.Find("GameIndexPrefab").GetComponent(FightingStart);
	count=fightingstart.count;
	grid=fightingstart.grid;
	celllist=GameObject.FindGameObjectsWithTag("cell");
	GameObject.Find("GUIPrefab").GetComponent(GUI_Game).targeting=this;
}

var legalunits= new List.<GameObject>();
var targetcell: GameObject;
var targetobject: GameObject;

function T000(cell: GameObject): boolean{//choose unit at targetcell
	targetobject=null;
	legalunits.Clear();
	
	//check for units at targetcell
	var unitlist=GameObject.FindGameObjectsWithTag("unit");
	var i: short;
	for (i=0; i<unitlist.length; i++){
		if (unitlist[i].GetComponent(ObjectStats).mycell==cell){
			legalunits.Add(unitlist[i]);
		}
	}
	var count: short = legalunits.Count;
	if (count>0){
		if (count==1){targetobject=legalunits[0];}
		return true;
	}
	else {
		actionCoord.Mlog("Cell empty");
		return false;
	}		
}
function T001(cell: GameObject){//auto-choose corpse at targetcell
	targetobject=null;
	
	var obArray: GameObject[];
	obArray=GameObject.FindGameObjectsWithTag("obstacle");
	var unitArray: GameObject[];
	unitArray=GameObject.FindGameObjectsWithTag("unit");
	
	var obList=new List.<GameObject>();
	
	var i: short;
	for (i=0; i<unitArray.length; i++){
		if (unitArray[i].GetComponent(ObjectStats).obclass){
			obList.Add(unitArray[i]);
		}
	}
	
	for (i=0; i<obArray.length; i++){
		if(obArray[i].GetComponent(ObjectStats).mycell==cell
		&& obArray[i].GetComponent(ObjectStats).obclass==4){
			targetobject=obList[i];
		}
	}	
	
	if (targetobject==null){return false;}
	else {return true;}
}
function T002(cell: GameObject){//auto-choose non-corpse destructible at targetcell
	targetobject=null;
	
	var obArray: GameObject[];
	obArray=GameObject.FindGameObjectsWithTag("obstacle");
	
	var i: short;
	for (i=0; i<obArray.length; i++){
		if(obArray[i].GetComponent(ObjectStats).mycell==cell
		&& obArray[i].GetComponent(ObjectStats).obclass==3){
			targetobject=obArray[i];
		}
	}
	if (targetobject==null){return false;}
	else {return true;}
}

//target cell - serpentine
function T111(rng: float, startcell: GameObject): boolean{//serpentine ground move/create unit
	targetcell=null;
	var distance: byte;
	var arrayflip: boolean = true;
	var cells1=new List.<GameObject>();
	var cells2=new List.<GameObject>();
	var list1=new List.<GameObject>();
	var list2=new List.<GameObject>();
	
	cells1.Add(startcell);
	for(distance=1; distance<=rng; distance++){
		if (arrayflip==true) {list1=cells1; list2=cells2;}
		else {list1=cells2; list2=cells1;}
		
		while(list1.Count>0){
			var cell: GameObject = list1[0];
			var i: short;
			for (i=0; i<celllist.length; i++){
				if (IsAdjacent(cell,celllist[i])==true 
				&& VacantGround(celllist[i])==true
				&& IsLegal(celllist[i])==false){
					list2.Add(celllist[i]);
					MakeLegal(celllist[i]);
				}		
			}
			list1.RemoveAt(0);
		}
		arrayflip=!arrayflip;
	}
	
	if (AnyCells()==false){return false;}
	else {return true;}
}
function T122(rng: int, startcell: GameObject): boolean{//serpentine trample move/create unit
	targetcell=null;
	var distance: byte;
	var arrayflip: boolean = true;
	var cells1=new List.<GameObject>();
	var cells2=new List.<GameObject>();
	var list1=new List.<GameObject>();
	var list2=new List.<GameObject>();
	
	cells1.Add(startcell);
	for(distance=1; distance<=rng; distance++){
		if (arrayflip==true) {list1=cells1; list2=cells2;}
		else {list1=cells2; list2=cells1;}
		
		while(list1.Count>0){
			var cell=list1[0];
			var i: short;
			for (i=0; i<celllist.length; i++){
				if (IsAdjacent(cell,celllist[i])==true 
				&& (VacantGround(celllist[i])==true || HasDestructible(celllist[i]))
				&& IsLegal(celllist[i])==false){
					list2.Add(celllist[i]);
					MakeLegal(celllist[i]);
				}		
			}
			list1.RemoveAt(0);
		}
		arrayflip=!arrayflip;
	}
	
	if (AnyCells()==false){return false;}
	else {return true;}
}
function T135(rng: int, startcell: GameObject): boolean{//serpentine attack/create item	
	targetcell=null;
	var distance: byte;
	var arrayflip: boolean = true;
	var cells1=new List.<GameObject>();
	var cells2=new List.<GameObject>();
	var list1= new List.<GameObject>();
	var list2= new List.<GameObject>();
	
	cells1.Add(startcell);
	for(distance=1; distance<=rng; distance++){
		if (arrayflip==true) {list1=cells1; list2=cells2;}
		else {list1=cells2; list2=cells1;}
		while(list1.Count>0){
			var cell: GameObject = list1[0];
			var i: short;
			for (i=0; i<celllist.length; i++){
				if (IsAdjacent(cell,celllist[i])==true
				&& IsLegal(celllist[i])==false){
					if(HasImpassible(celllist[i])==false){
						MakeLegal(celllist[i]);
					}
					if(Vacant(celllist[i])==true){
						list2.Add(celllist[i]);
					}
				}			
			}
			list1.RemoveAt(0);
		}
		if (arrayflip==true) {cells1=list1; cells2=list2;}
		else {cells1=list2; cells2=list1;}
		arrayflip=!arrayflip;
	}
	
	if (AnyCells()==false){return false;}
	else {return true;}
}
function T144(rng: int, startcell: GameObject): boolean{//serpentine flying move/create unit
	targetcell=null;
	var distance: byte;
	var arrayflip: boolean = true;
	var cells1=new List.<GameObject>();
	var cells2=new List.<GameObject>();
	var list1=new List.<GameObject>();
	var list2=new List.<GameObject>();
	
	cells1.Add(startcell);
	for(distance=1; distance<=rng; distance++){
		if (arrayflip==true) {list1=cells1; list2=cells2;}
		else {list1=cells2; list2=cells1;}

		while(list1.Count>0){
			var cell: GameObject = list1[0];
			var i: short;
			for (i=0; i<celllist.length; i++){
				if (IsAdjacent(cell,celllist[i])==true 
				&& VacantAir(celllist[i])==true
				&& IsLegal(celllist[i])==false){
					list2.Add(celllist[i]);
					MakeLegal(celllist[i]);
				}			
			}
			list1.RemoveAt(0);
		}
		arrayflip=!arrayflip;
	}
	
	if (AnyCells()==false){return false;}
	else {return true;}
}
function T155(rng: int, startcell: GameObject): boolean{//serpentine target space with corpse
	targetcell=null;
	var distance: byte;
	var arrayflip: boolean = true;
	var cells1=new List.<GameObject>();
	var cells2=new List.<GameObject>();
	var list1=new List.<GameObject>();
	var list2=new List.<GameObject>();
	
	cells1.Add(startcell);
	for(distance=1; distance<=rng; distance++){
		if (arrayflip==true) {list1=cells1; list2=cells2;}
		else {list1=cells2; list2=cells1;}

		while(list1.Count>0){
			var cell: GameObject = list1[0];
			var i: short;
			for (i=0; i<celllist.length; i++){
				if (IsAdjacent(cell,celllist[i])==true 
				&& HasCorpse(celllist[i])==true
				&& IsLegal(celllist[i])==false){
					list2.Add(celllist[i]);
					MakeLegal(celllist[i]);
				}			
			}
			list1.RemoveAt(0);
		}
		arrayflip=!arrayflip;
	}
	
	if (AnyCells()==false){return false;}
	else {return true;}
}
function T165(rng: int, startcell: GameObject): boolean{//serpentine target space with destructible obstacle (non-corpse)
	targetcell=null;
	var distance: byte;
	var arrayflip: boolean = true;
	var cells1=new List.<GameObject>();
	var cells2=new List.<GameObject>();
	var list1=new List.<GameObject>();
	var list2=new List.<GameObject>();
	
	cells1.Add(startcell);
	for(distance=1; distance<=rng; distance++){
		if (arrayflip==true) {list1=cells1; list2=cells2;}
		else {list1=cells2; list2=cells1;}

		while(list1.Count>0){
			var cell: GameObject = list1[0];
			var i: short;
			for (i=0; i<celllist.length; i++){
				if (IsAdjacent(cell,celllist[i])==true 
				&& HasNonCorpse(celllist[i])==true
				&& IsLegal(celllist[i])==false){
					list2.Add(celllist[i]);
					MakeLegal(celllist[i]);
				}			
			}
			list1.RemoveAt(0);
		}
		arrayflip=!arrayflip;
	}
	
	if (AnyCells()==false){return false;}
	else {return true;}
}
//target cell - linear
function T211(rng: int, startcell: GameObject): boolean{//linear ground move
	targetcell=null;
	var distance: byte;
	var arrayflip: boolean = true;
	var cells1=new List.<GameObject>();
	var cells2=new List.<GameObject>();
	var list1=new List.<GameObject>();
	var list2=new List.<GameObject>();
		
	var i: short;
	for (i=0; i<celllist.length; i++){
		if (IsAdjacent(startcell,celllist[i])==true
		&& VacantGround(celllist[i])==true
		&& IsLegal(celllist[i])==false){
			cells2.Add(celllist[i]);
			MakeLegal(celllist[i]);
		}
	}
	//check direction of first 8 cells
	for (i=0; i<cells2.Count; i++){		
		//start comparing startcell/1st 8 cell
		var cellA: GameObject = startcell;
		var cellB: GameObject = cells2[i];
		
		distance=2;//examining second row after startcell
		for (distance=2; distance<=rng; distance++){//scan until RNG hit
			var j: short;
			for (j=0; j<celllist.length; j++){//check all cells
				var position = cellB.transform.position;
				var direction:Vector3 = DirectionVector(cellA,cellB);
				if (celllist[j].transform.position==position+(LinearBend(cellB,direction)*grid)){
					if (VacantGround(celllist[j])==true
					&& IsLegal(celllist[j])==false){
						MakeLegal(celllist[j]);
						cellA=cellB;
						cellB=celllist[j];
					}
					break;
				}
			}
		}
	}
	if (AnyCells()==false){return false;}
	else {return true;}
}
function T222(rng: int, startcell: GameObject): boolean{//linear trample move
	targetcell=null;
	var distance: byte;
	var arrayflip: boolean = true;
	var cells1=new List.<GameObject>();
	var cells2=new List.<GameObject>();
	var list1=new List.<GameObject>();
	var list2=new List.<GameObject>();
		
	var i: short;
	for (i=0; i<celllist.length; i++){
		if (IsAdjacent(startcell,celllist[i])==true
		&& (VacantGround(celllist[i])==true || HasDestructible(celllist[i])==true)
		&& IsLegal(celllist[i])==false){
			cells2.Add(celllist[i]);
			MakeLegal(celllist[i]);
		}
	}
	//check direction of first 8 cells
	for (i=0; i<cells2.Count; i++){		
		//start comparing startcell/1st 8 cell
		var cellA: GameObject = startcell;
		var cellB: GameObject = cells2[i];
		
		distance=2;//examining second row after startcell
		for (distance=2; distance<=rng; distance++){//scan until RNG hit
			var j: short;
			for (j=0; j<celllist.length; j++){//check all cells
				var position = cellB.transform.position;
				var direction:Vector3 = DirectionVector(cellA,cellB);
				if (celllist[j].transform.position==position+(LinearBend(cellB,direction)*grid)){
					if ((VacantGround(celllist[j])==true || HasDestructible(celllist[j])==true)
					&& IsLegal(celllist[j])==false){
						MakeLegal(celllist[j]);
						cellA=cellB;
						cellB=celllist[j];
					}
					break;
				}
			}
		}
	}
	if (AnyCells()==false){return false;}
	else {return true;}
}
function T235(rng: float, startcell: GameObject): boolean{//linear attack
	targetcell=null;
	var distance: byte;
	var arrayflip: boolean = true;
	var cells1=new List.<GameObject>();
	var cells2=new List.<GameObject>();
	var list1=new List.<GameObject>();
	var list2=new List.<GameObject>();
		
	var i: short;
	for (i=0; i<celllist.length; i++){
		if (IsAdjacent(startcell,celllist[i])==true){
			if(Vacant(celllist[i])==true){cells1.Add(celllist[i]);}
			if(HasImpassible(celllist[i])==false){MakeLegal(celllist[i]);}
		}
	}
	//check direction of first 8 cells
	var j: short;
	for (j=0; j<cells1.Count; j++){
		//start comparing startcell/1st 8 cell
		var cellA: GameObject = startcell;
		var cellB: GameObject = cells1[j];
		
		for(distance=2; distance<=rng; distance++){//scan until RNG hit
			var k: short;
			for (k=0; k<celllist.length; k++){//check all cells
				var position: Vector3 = cellB.transform.position;
				var direction: Vector3 = DirectionVector(cellA,cellB);
				if (celllist[k].transform.position==position+(LinearBend(cellB,direction)*grid)){
					if(Vacant(celllist[k])){
						cellA=cellB;
						cellB=celllist[k];
					}
					if(HasImpassible(celllist[k])==false){
						MakeLegal(celllist[k]);
					}
					break;
				}
			}
		}
	}
	if (AnyCells()==false){return false;}
	else {return true;}
}
function T244(rng: float, startcell: GameObject): boolean{//linear flying move
	targetcell=null;
	var distance: byte;
	var arrayflip: boolean = true;
	var cells1=new List.<GameObject>();
	var cells2=new List.<GameObject>();
	var list1=new List.<GameObject>();
	var list2=new List.<GameObject>();
		
	var i: short;
	for (i=0; i<celllist.length; i++){
		if (IsAdjacent(startcell,celllist[i])==true
		&& VacantAir(celllist[i])==true
		&& IsLegal(celllist[i])==false){
			cells2.Add(celllist[i]);
			MakeLegal(celllist[i]);
		}
	}
	//check direction of first 8 cells
	for (i=0; i<cells2.Count; i++){		
		//start comparing startcell/1st 8 cell
		var cellA: GameObject = startcell;
		var cellB: GameObject = cells2[i];
		
		distance=2;//examining second row after startcell
		for (distance=2; distance<=rng; distance++){//scan until RNG hit
			var j: short;
			for (j=0; j<celllist.length; j++){//check all cells
				var position = cellB.transform.position;
				var direction:Vector3 = DirectionVector(cellA,cellB);
				if (celllist[j].transform.position==position+(LinearBend(cellB,direction)*grid)){
					if (VacantAir(celllist[j])==true
					&& IsLegal(celllist[j])==false){
						MakeLegal(celllist[j]);
						cellA=cellB;
						cellB=celllist[j];
					}
					break;
				}
			}
		}
	}
	if (AnyCells()==false){return false;}
	else {return true;}
}
//target cell - arc
/*function T300(rng: int, startcell: GameObject): boolean{//1-direction linear w/arc exclusion
	targetcell=null;
	var distance: byte;
	var arrayflip: boolean = true;
	var cells1=new List.<GameObject>();
	var cells2=new List.<GameObject>();
	var list1=new List.<GameObject>();
	var list2=new List.<GameObject>();
	
	var startProp: CellProperties = startcell.GetComponent(CellProperties);
	//var startCoord: Vector3 = Vector3(startProp.gameX,startProp.gameY,startProp.gameZ);
	//startcellCoord
	
	var i: short;
	for (i=0; i<celllist.length; i++){
		var otherProp: CellProperties = celllist[i].GetComponent(CellProperties);
		if (otherProp.gameX==startProp.gameX+1 &&
			otherProp.gameY==startProp.gameY &&
			otherProp.gameZ==startProp.gameZ){
				if(Vacant(celllist[i])==true){cells1.Add(celllist[i]);}
				if(HasImpassible(celllist[i])==false){MakeLegal(celllist[i]);}	
		}
	}
	
	//check direction of first 8 cells
	var j: short;
	for (j=0; j<cells1.Count; j++){
		//start comparing startcell/1st 8 cell
		var cellA: GameObject = startcell;
		var cellB: GameObject = cells1[j];
		
		for(distance=2; distance<=rng; distance++){//scan until RNG hit
			var k: short;
			for (k=0; k<celllist.length; k++){//check all cells
				var position: Vector3 = cellB.transform.position;
				var direction: Vector3 = DirectionVector(cellA,cellB);
				if (celllist[k].transform.position==position+(LinearBend(cellB,direction)*grid)){
					
					//if(Vacant(celllist[k])){
						cellA=cellB;
						cellB=celllist[k];
					//}
					//if(HasImpassible(celllist[k])==false){
					if (ArcExclusion(startcell,celllist[k])==true){
						MakeLegal(celllist[k]);
					}
					break;
				}
			}
		}
	}
	if (AnyCells()==false){return false;}
	else {return true;}
}*/
/*function T301(rng: int, startcell: GameObject): boolean{//coplanar checks
	var i: byte;
	for (i=0; i<celllist.length; i++){
		if (IsCoplanar(startcell, celllist[i])==true){
			MakeLegal(celllist[i]);
		}
	}
	if (AnyCells()==false){return false;}
	else {return true;}
}
*/
function T333(rng: int, startcell: GameObject): boolean{//arc attack
	targetcell=null;
	var distance: byte;
	var arrayflip: boolean = true;
	var cells1=new List.<GameObject>();
	var cells2=new List.<GameObject>();
	var list1= new List.<GameObject>();
	var list2= new List.<GameObject>();
	
	cells1.Add(startcell);
	for(distance=1; distance<=rng; distance++){
		if (arrayflip==true) {list1=cells1; list2=cells2;}
		else {list1=cells2; list2=cells1;}
		while(list1.Count>0){
			var cell: GameObject = list1[0];
			var i: short;
			for (i=0; i<celllist.length; i++){
				if (IsAdjacent(cell,celllist[i])==true
				&& IsLegal(celllist[i])==false){
					if(ArcExclusion(startcell,celllist[i])==true){
						MakeLegal(celllist[i]);
						list2.Add(celllist[i]);
					}
				}			
			}
			list1.RemoveAt(0);
		}
		if (arrayflip==true) {cells1=list1; cells2=list2;}
		else {cells1=list2; cells2=list1;}
		arrayflip=!arrayflip;
	}
	
	if (AnyCells()==false){return false;}
	else {return true;}
}
//utility functions
function AnyCells(){//returns false if there are no legal cells 
	var celllist: GameObject[] = GameObject.FindGameObjectsWithTag("cell");
	var anycells: short = 0;
	var i: short;
	for (i=0; i<celllist.length; i++){
		if (celllist[i].GetComponent(CellProperties).legal==true){
			anycells++;
		}
	}
	if (anycells==0){return false;}
	else {return true;}
}
function ArcExclusion(startcell: GameObject, othercell: GameObject): boolean{
	if (HasImpassible(othercell)==true){return false;}
	
	var rayDirection: Vector3 = startcell.transform.position - othercell.transform.position;
	var rayDist: float = Vector3.Distance(startcell.transform.position, othercell.transform.position);	
	var hits: RaycastHit[];
	hits = Physics.RaycastAll (othercell.transform.position, rayDirection, rayDist);
	
	var i: byte;
	for (i=0; i<hits.length; i++){
		var hitColl: Collider = hits[i].collider;
		var hitTrans: Transform = hitColl.transform;
		var hitObj: GameObject = hitTrans.gameObject;
		if (hitObj.CompareTag("cell")){
			if (HasImpassible(hitObj)==true){
				return false;
			}
		}
	}
	return true;
}

function CellCount(count:byte): short{//counts cell objects
	return (2*( ((count+1)*(count+1)) + ((count+1)*(count-1)) + ((count-1)*(count-1)) ));
}
function CheckAbove(unit: GameObject): boolean{//checks air occupation for ground units taking off
	var cell: GameObject = unit.GetComponent(ObjectStats).mycell;
	if (cell.GetComponent(CellProperties).occB==0){return true;}
	else {return false;}
}
function CheckBelow(unit: GameObject): boolean{//checks ground occupation for landing flying units
	var cell: GameObject = unit.GetComponent(ObjectStats).mycell;
	if (cell.GetComponent(CellProperties).occA==0){return true;}
	else {return false;}
}
function DirectionVector(startcell:GameObject,othercell:GameObject): Vector3{//for linear direction determination
	var cellA: CellProperties = startcell.GetComponent(CellProperties);
	var cellB: CellProperties = othercell.GetComponent(CellProperties);	
	var dir: Vector3;
	dir.x=cellB.gameX-cellA.gameX;
	dir.y=cellB.gameY-cellA.gameY;
	dir.z=cellB.gameZ-cellA.gameZ;
	return dir;
}
///
function IsAdjacent(startcell:GameObject,othercell:GameObject): boolean{//check if cells adjacent (for serp targeting)
	var cellA: CellProperties = startcell.GetComponent(CellProperties);
	var cellB: CellProperties = othercell.GetComponent(CellProperties);
	
	if (((cellA.gameX==0 || cellA.gameX==count) && (Mathf.Abs(cellB.gameY-cellA.gameY)<=1 && Mathf.Abs(cellB.gameZ-cellA.gameZ)<=1) && cellB.gameX==cellA.gameX) || 
		((cellA.gameY==0 || cellA.gameY==count) && (Mathf.Abs(cellB.gameX-cellA.gameX)<=1 && Mathf.Abs(cellB.gameZ-cellA.gameZ)<=1) && cellB.gameY==cellA.gameY) || 
		((cellA.gameZ==0 || cellA.gameZ==count) && (Mathf.Abs(cellB.gameX-cellA.gameX)<=1 && Mathf.Abs(cellB.gameY-cellA.gameY)<=1) && cellB.gameZ==cellA.gameZ))
		{return true;}
	else {return false;}
}
function IsCoplanar(startcell: GameObject, othercell: GameObject): boolean{
	var cellA: CellProperties = startcell.GetComponent(CellProperties);
	var cellB: CellProperties = othercell.GetComponent(CellProperties);
	
	if (((cellA.gameX==0 || cellA.gameX==count) && cellA.gameX==cellB.gameX)
	 ||((cellA.gameY==0 || cellA.gameY==count) && cellA.gameY==cellB.gameY)
	 ||((cellA.gameZ==0 || cellA.gameZ==count) && cellA.gameZ==cellB.gameZ))
		{return true;}
	else {return false;}
}
function IsCollinear(startcell:GameObject,othercell:GameObject): boolean{//(unused)
	var cellA: CellProperties = startcell.GetComponent(CellProperties);
	var cellB: CellProperties = othercell.GetComponent(CellProperties);
	
	//check vertical/horizontal
	if (((cellA.gameX==0 || cellA.gameX==count) && ((cellA.gameY==cellB.gameY) || (cellA.gameZ==cellB.gameZ)))
		||((cellA.gameY==0 || cellA.gameY==count) && ((cellA.gameX==cellB.gameX) || (cellA.gameZ==cellB.gameZ)))
		||((cellA.gameZ==0 || cellA.gameZ==count) && ((cellA.gameX==cellB.gameX) || (cellA.gameY==cellB.gameY)))){
		return true;
	}
	//check diagonal
	//else if (){
	//	return true;}	
	else {return false;}
}
///
function LinearBend(startcell:GameObject,direction:Vector3): Vector3{//for bending linear targeting around corners
	var cell: CellProperties = startcell.GetComponent(CellProperties);
	
	//corners
	if ((cell.gameX==0 || cell.gameX==count) && (cell.gameY==0 || cell.gameY==count) && (cell.gameZ==0 || cell.gameZ==count)){
		
		//front-bottom-left
		if (cell.gameX==0 && cell.gameY==0 && cell.gameZ==0){
			if(direction.x==-1 && direction.y==-1){direction=Vector3(0,0,1);}
			if(direction.y==-1 && direction.z==-1){direction=Vector3(1,0,0);}
			if(direction.x==-1 && direction.z==-1){direction=Vector3(0,1,0);}
		}
		//front-bottom-right
		if (cell.gameX==count && cell.gameY==0 && cell.gameZ==0){
			if(direction.x==1 && direction.y==-1){direction=Vector3(0,0,1);}
			if(direction.y==-1 && direction.z==-1){direction=Vector3(-1,0,0);}
			if(direction.x==1 && direction.z==-1){direction=Vector3(0,1,0);}
		}
		//front-top-left
		if (cell.gameX==0 && cell.gameY==count && cell.gameZ==0){
			if(direction.x==-1 && direction.y==1){direction=Vector3(0,0,1);}
			if(direction.y==1 && direction.z==-1){direction=Vector3(1,0,0);}
			if(direction.x==-1 && direction.z==-1){direction=Vector3(0,-1,0);}
		}
		//front-top-right
		if (cell.gameX==count && cell.gameY==count && cell.gameZ==0){
			if(direction.x==1 && direction.y==1){direction=Vector3(0,0,1);}
			if(direction.y==1 && direction.z==-1){direction=Vector3(-1,0,0);}
			if(direction.x==1 && direction.z==-1){direction=Vector3(0,-1,0);}
		}
		//back-bottom-left
		if (cell.gameX==0 && cell.gameY==0 && cell.gameZ==count){
			if(direction.x==-1 && direction.y==-1){direction=Vector3(0,0,-1);}
			if(direction.y==-1 && direction.z==1){direction=Vector3(1,0,0);}
			if(direction.x==-1 && direction.z==1){direction=Vector3(0,1,0);}
		}
		//back-bottom-right
		if (cell.gameX==count && cell.gameY==0 && cell.gameZ==count){
			if(direction.x==1 && direction.y==-1){direction=Vector3(0,0,-1);}
			if(direction.y==-1 && direction.z==1){direction=Vector3(-1,0,0);}
			if(direction.x==1 && direction.z==1){direction=Vector3(0,1,0);}
		}
		//back-top-left
		if (cell.gameX==0 && cell.gameY==count && cell.gameZ==count){
			if(direction.x==-1 && direction.y==1){direction=Vector3(0,0,-1);}
			if(direction.y==1 && direction.z==1){direction=Vector3(1,0,0);}
			if(direction.x==-1 && direction.z==1){direction=Vector3(0,-1,0);}
		}	
		//back-top-right
		if (cell.gameX==count && cell.gameY==count && cell.gameZ==count){
			if(direction.x==1 && direction.y==1){direction=Vector3(0,0,-1);}
			if(direction.y==1 && direction.z==1){direction=Vector3(-1,0,0);}
			if(direction.x==1 && direction.z==1){direction=Vector3(0,-1,0);}
		}	
	}
	//edges
	else {
		//front
		if (cell.gameZ==0){
			if ((cell.gameX==0 && direction.x==-1)||(cell.gameX==count && direction.x==1)){
				direction.x=0; direction.z=1;}
			if ((cell.gameY==0 && direction.y==-1)||(cell.gameY==count && direction.y==1)){
				direction.y=0; direction.z=1;}
		}
		//back
		if (cell.gameZ==count){
			if ((cell.gameX==0 && direction.x==-1)||(cell.gameX==count && direction.x==1)){
				direction.x=0; direction.z=-1;}
			if ((cell.gameY==0 && direction.y==-1)||(cell.gameY==count && direction.y==1)){
				direction.y=0; direction.z=-1;}
		}
		//left
		if (cell.gameX==0){
			if ((cell.gameY==0 && direction.y==-1)||(cell.gameY==count && direction.y==1)){
				direction.y=0; direction.x=1;}
			if ((cell.gameZ==0 && direction.z==-1)||(cell.gameZ==count && direction.z==1)){
				direction.z=0; direction.x=1;}
		}
		//right
		if (cell.gameX==count){
			if ((cell.gameY==0 && direction.y==-1)||(cell.gameY==count && direction.y==1)){
				direction.y=0; direction.x=-1;}
			if ((cell.gameZ==0 && direction.z==-1)||(cell.gameZ==count && direction.z==1)){
				direction.z=0; direction.x=-1;}
		}
		//bottom
		if (cell.gameY==0){
			if ((cell.gameX==0 && direction.x==-1)||(cell.gameX==count && direction.x==1)){
				direction.x=0; direction.y=1;}
			if ((cell.gameZ==0 && direction.z==-1)||(cell.gameZ==count && direction.z==1)){
				direction.z=0; direction.y=1;}
			}
		//top	
		if (cell.gameY==count){
			if ((cell.gameX==0 && direction.x==-1)||(cell.gameX==count && direction.x==1)){
				direction.x=0; direction.y=-1;}
			if ((cell.gameZ==0 && direction.z==-1)||(cell.gameZ==count && direction.z==1)){
				direction.z=0; direction.y=-1;}
		}
	
	}
	return direction;
}
function NoMoves(){//action1 refund if no targets
	actionCoord.Refund(1,"No legal moves.");
	actionCoord.ResetAction();
}
function NoTargets(){//action3 refund if no targets
	actionCoord.Refund(3,"No legal targets.");
	actionCoord.ResetAction();
}
function ResetCells(){//reset all cells legality
	celllist=GameObject.FindGameObjectsWithTag("cell");
	var i: short;
	for (i=0; i<celllist.length; i++){
		celllist[i].GetComponent(CellProperties).legal=false;
	}	
}
function WaitForTargetcell(): IEnumerator{
	while (true){
		if(targetcell!=null){break;}
		yield;
	}
}
function WaitForTargetobject(): IEnumerator{
	while (true){
		if(targetobject!=null){break;}
		yield;
	}
}
//occupation checks
function IsLegal(cell: GameObject): boolean{
	if (cell.GetComponent(CellProperties).legal==true){return true;}
	else {return false;}
}
function MakeLegal(cell: GameObject){
	cell.GetComponent(CellProperties).legal=true;
}
function Vacant(cell: GameObject): boolean{
	var cellproperties: CellProperties = cell.GetComponent(CellProperties);
	if (cellproperties.occA==0 && cellproperties.occB==0){
		return true;
	}
	else {return false;}
}
function VacantAir(cell: GameObject): boolean{
	var cellproperties: CellProperties = cell.GetComponent(CellProperties);
	if (cellproperties.occB==0){
		return true;
	}
	else {return false;}

}
function VacantGround(cell: GameObject): boolean{
	var cellproperties: CellProperties = cell.GetComponent(CellProperties);
	if (cellproperties.occA==0){
		return true;
	}
	else {return false;}
}
function HasDestructible(cell: GameObject): boolean{
	var cellproperties: CellProperties = cell.GetComponent(CellProperties);
	if (cellproperties.occA==2 || cellproperties.occA==3){
		return true;
	}
	else {return false;}
}
function HasCorpse(cell: GameObject): boolean{
	var cellproperties: CellProperties = cell.GetComponent(CellProperties);
	if (cellproperties.occA==2){
		return true;
	}
	else {return false;}
}
function HasNonCorpse(cell: GameObject): boolean{
	var cellproperties: CellProperties = cell.GetComponent(CellProperties);
	if (cellproperties.occA==3){
		return true;
	}
	else {return false;}
}
function HasImpassible(cell: GameObject): boolean{
	var cellproperties: CellProperties = cell.GetComponent(CellProperties);
	if (cellproperties.occA==5){
		return true;
	}
	else {return false;}
}
function FindTeammates(unit: GameObject, self: boolean): List.<GameObject>{
	var unitstats: ObjectStats = unit.GetComponent(ObjectStats);
	var owner: byte = unitstats.owner;
	
	var teammates = new List.<GameObject>();
	var unitlist: GameObject[] = GameObject.FindGameObjectsWithTag("unit");
	
	var i: short;
	for (i=0; i<unitlist.length; i++){
		var otherstats: ObjectStats = unitlist[i].GetComponent(ObjectStats);
		if (otherstats.owner==owner && unitlist[i]!=unit){
			teammates.Add(unitlist[i]);
		}
	}
	if (self==true){teammates.Add(unit);}
	return teammates;
}