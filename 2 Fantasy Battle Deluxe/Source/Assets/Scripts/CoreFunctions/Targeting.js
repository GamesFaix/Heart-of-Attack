#pragma strict

var actions: Actions;
var actionCoord: ActionCoordinator;
var createWorld: CreateWorld;

var count: short;
var grid: byte;
var celllist: GameObject[];

var legalunits= new List.<GameObject>();
var targetcell: GameObject;
var targetobject: GameObject;

function Awake(){
	actions=gameObject.GetComponent(Actions);
	actionCoord=gameObject.GetComponent(ActionCoordinator);
	createWorld=GameObject.Find("GameIndexPrefab").GetComponent(CreateWorld);
	count=createWorld.count;
	grid=createWorld.grid;
	celllist=GameObject.FindGameObjectsWithTag("cell");
	GameObject.Find("GUIPrefab").GetComponent(GUI_Game).targeting=this;
}
//movement/object creation
function SerpGND(rng: float, startcell: GameObject): boolean{//serpentine ground move/create unit
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
				if (Adjacent(cell,celllist[i])==true 
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
function SerpTRM(rng: int, startcell: GameObject): boolean{//serpentine trample move/create unit
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
				if (Adjacent(cell,celllist[i])==true 
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
function SerpFLY(rng: int, startcell: GameObject): boolean{//serpentine flying move/create unit
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
				if (Adjacent(cell,celllist[i])==true 
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



function LinGND(rng: int, startcell: GameObject): boolean{//linear ground move
	targetcell=null;
	var distance: byte;
	var arrayflip: boolean = true;
	var cells1=new List.<GameObject>();
	var cells2=new List.<GameObject>();
	var list1=new List.<GameObject>();
	var list2=new List.<GameObject>();
		
	var i: short;
	for (i=0; i<celllist.length; i++){
		if (Adjacent(startcell,celllist[i])==true
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
function LinTRM(rng: int, startcell: GameObject): boolean{//linear trample move
	targetcell=null;
	var distance: byte;
	var arrayflip: boolean = true;
	var cells1=new List.<GameObject>();
	var cells2=new List.<GameObject>();
	var list1=new List.<GameObject>();
	var list2=new List.<GameObject>();
		
	var i: short;
	for (i=0; i<celllist.length; i++){
		if (Adjacent(startcell,celllist[i])==true
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

function LinFLY(rng: float, startcell: GameObject): boolean{//linear flying move
	targetcell=null;
	var distance: byte;
	var arrayflip: boolean = true;
	var cells1=new List.<GameObject>();
	var cells2=new List.<GameObject>();
	var list1=new List.<GameObject>();
	var list2=new List.<GameObject>();
		
	var i: short;
	for (i=0; i<celllist.length; i++){
		if (Adjacent(startcell,celllist[i])==true
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

//attack
function AtkSerp(rng: int, startcell: GameObject): boolean{//serpentine attack/create item	
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
				if (Adjacent(cell,celllist[i])==true
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
function AtkLin(rng: float, startcell: GameObject): boolean{//linear attack
	targetcell=null;
	var distance: byte;
	var arrayflip: boolean = true;
	var cells1=new List.<GameObject>();
	var cells2=new List.<GameObject>();
	var list1=new List.<GameObject>();
	var list2=new List.<GameObject>();
		
	var i: short;
	for (i=0; i<celllist.length; i++){
		if (Adjacent(startcell,celllist[i])==true){
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

function AtkArc(rng: float, startcell: GameObject): boolean{//arc attack
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
				if (Adjacent(cell,celllist[i])==true
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

function EXPFindCRZ(startcell: GameObject, crz: int): List.<GameObject>{
	//Debug.Log("FindCRZ-size: "+crz);
	var crzCells = new List.<GameObject>(); //list of cells in CRZ
	crzCells.Add(startcell);

	//temp lists
	var arrayflip: boolean = true;
	var cells1=new List.<GameObject>();
	var cells2=new List.<GameObject>();
	var list1=new List.<GameObject>();
	var list2=new List.<GameObject>();

	cells1.Add(startcell);

	var distance: byte;
	for (distance=1; distance<=crz; distance++){
		if (arrayflip==true) {list1=cells1; list2=cells2;}
		else {list1=cells2; list2=cells1;}
		
		while(list1.Count>0){
			var cell: GameObject = list1[0];
			var i: short;
			for (i=0; i<celllist.length; i++){
				if (Adjacent(cell,celllist[i])==true
				&& CheckList(crzCells, celllist[i])==false
				&& ArcExclusion(startcell,celllist[i])==true){
					list2.Add(celllist[i]);
					crzCells.Add(celllist[i]);
				}		
			}
			list1.RemoveAt(0);
		}
		arrayflip=!arrayflip;
	}
	return crzCells;
}
function EXPExpandRAD(startcell: GameObject, hitCells: List.<GameObject>, lastRAD: List.<GameObject>): List.<GameObject>{
	var newRAD = new List.<GameObject>();
	
	var i: short;
	for (i=0; i<lastRAD.Count; i++){
	
		var j: short;
		for (j=0; j<celllist.length; j++){
			if (Adjacent(lastRAD[i],celllist[j])
			 && (CheckList(hitCells,celllist[j])==false)
			 && (CheckList(newRAD,celllist[j])==false)
			 && (ArcExclusion(startcell,celllist[j])==true)
			 ){
				newRAD.Add(celllist[j]);
			}
		}		
	}
	return newRAD;
}
function FIRExpandRAD(startunit: GameObject, hitUnits: List.<GameObject>): List.<GameObject>{
	var newRAD = new List.<GameObject>();
	
	


}

//target space with object
function CorpseSerp(rng: int, startcell: GameObject): boolean{//serpentine target space with corpse
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
				if (Adjacent(cell,celllist[i])==true 
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
function CorpseArc(rng: int, startcell: GameObject): boolean{
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
				if (Adjacent(cell,celllist[i])==true
				&& IsLegal(celllist[i])==false){
					if(ArcExclusion(startcell,celllist[i])==true
					&& HasCorpse(celllist[i])==true){
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
function DestSerp(rng: int, startcell: GameObject): boolean{//serpentine target space with destructible obstacle (non-corpse)
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
				if (Adjacent(cell,celllist[i])==true 
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
/*function UnitArc(rng: int, startcell: GameObject): boolean{//arc target unit
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
				if (Adjacent(cell,celllist[i])==true
				&& IsLegal(celllist[i])==false){
					if(ArcExclusion(startcell,celllist[i])==true
					 //&& HasUnit(celllist[i])==true
					 ){
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
*/
//target selection
function ChooseUnit(cell: GameObject): boolean{//choose unit at targetcell
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
function AutoChooseCorpse(cell: GameObject){//auto-choose corpse at targetcell
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
			obList.Add(obArray[i]);
		}
	}	
	
	if (obList.Count>0){
		targetobject=obList[0];
	}
	
	if (targetobject==null){return false;}
	else {return true;}
}
function AutoChooseDest(cell: GameObject){//auto-choose non-corpse destructible at targetcell
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
function UnitsInZone(cells: List.<GameObject>): List.<GameObject>{//returns all units occupying a given list of cells
	var zoneUnits = new List.<GameObject>();
	
	var occupiedCells = new List.<GameObject>();
	var i: byte;
	for (i=0; i<cells.Count; i++){
		if (Vacant(cells[i])==false){
			occupiedCells.Add(cells[i]);
		}
	}
	
	var unitList = GameObject.FindGameObjectsWithTag("unit");
	for (i=0; i<occupiedCells.Count; i++){
		var j: byte;
		for (j=0; j<unitList.length; j++){
			var unitStats: ObjectStats = unitList[j].GetComponent(ObjectStats);
			if (unitStats.mycell==occupiedCells[i]){
				zoneUnits.Add(unitList[j]);
			}
		}
	}
	
	return zoneUnits;
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

//debug functions
/*function T300(rng: int, startcell: GameObject): boolean{//1-direction linear w/arc exclusion
	targetcell=null;
	var distance: byte;
	var arrayflip: boolean = true;
	var cells1=new List.<GameObject>();
	var cells2=new List.<GameObject>();
	var list1=new List.<GameObject>();
	var list2=new List.<GameObject>();
	
	var startProp: CellProperties = startcell.GetComponent(CellProperties);
	//var startCoord: Vector3 = Vector3(startProp.gameCoord.x,startProp.gameCoord.y,startProp.gameCoord.Z);
	//startcellCoord
	
	var i: short;
	for (i=0; i<celllist.length; i++){
		var otherProp: CellProperties = celllist[i].GetComponent(CellProperties);
		if (otherProp.gameCoord.x==startProp.gameCoord.x+1 &&
			otherProp.gameCoord.y==startProp.gameCoord.y &&
			otherProp.gameCoord.z==startProp.gameCoord.z){
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
		if (Coplanar(startcell, celllist[i])==true){
			MakeLegal(celllist[i]);
		}
	}
	if (AnyCells()==false){return false;}
	else {return true;}
}
*/
///
///relative position
function Adjacent(startcell:GameObject,othercell:GameObject): boolean{//check if cells adjacent (for serp targeting)
	var cellA: CellProperties = startcell.GetComponent(CellProperties);
	var cellB: CellProperties = othercell.GetComponent(CellProperties);
	
	if (((cellA.gameCoord.x==0 || cellA.gameCoord.x==count) && (Mathf.Abs(cellB.gameCoord.y-cellA.gameCoord.y)<=1 && Mathf.Abs(cellB.gameCoord.z-cellA.gameCoord.z)<=1) && cellB.gameCoord.x==cellA.gameCoord.x) || 
		((cellA.gameCoord.y==0 || cellA.gameCoord.y==count) && (Mathf.Abs(cellB.gameCoord.x-cellA.gameCoord.x)<=1 && Mathf.Abs(cellB.gameCoord.z-cellA.gameCoord.z)<=1) && cellB.gameCoord.y==cellA.gameCoord.y) || 
		((cellA.gameCoord.z==0 || cellA.gameCoord.z==count) && (Mathf.Abs(cellB.gameCoord.x-cellA.gameCoord.x)<=1 && Mathf.Abs(cellB.gameCoord.y-cellA.gameCoord.y)<=1) && cellB.gameCoord.z==cellA.gameCoord.z))
		{return true;}
	else {return false;}
}
function Coplanar(startcell: GameObject, othercell: GameObject): boolean{
	var cellA: CellProperties = startcell.GetComponent(CellProperties);
	var cellB: CellProperties = othercell.GetComponent(CellProperties);
	
	if (((cellA.gameCoord.x==0 || cellA.gameCoord.x==count) && cellA.gameCoord.x==cellB.gameCoord.x)
	 ||((cellA.gameCoord.y==0 || cellA.gameCoord.y==count) && cellA.gameCoord.y==cellB.gameCoord.y)
	 ||((cellA.gameCoord.z==0 || cellA.gameCoord.z==count) && cellA.gameCoord.z==cellB.gameCoord.z))
		{return true;}
	else {return false;}
}
function Collinear(startcell:GameObject,othercell:GameObject): boolean{//(unused)
	var cellA: CellProperties = startcell.GetComponent(CellProperties);
	var cellB: CellProperties = othercell.GetComponent(CellProperties);
	
	//check vertical/horizontal
	if (((cellA.gameCoord.x==0 || cellA.gameCoord.x==count) && ((cellA.gameCoord.y==cellB.gameCoord.y) || (cellA.gameCoord.z==cellB.gameCoord.z)))
		||((cellA.gameCoord.y==0 || cellA.gameCoord.y==count) && ((cellA.gameCoord.x==cellB.gameCoord.x) || (cellA.gameCoord.z==cellB.gameCoord.z)))
		||((cellA.gameCoord.z==0 || cellA.gameCoord.z==count) && ((cellA.gameCoord.x==cellB.gameCoord.x) || (cellA.gameCoord.y==cellB.gameCoord.y)))){
		return true;
	}
	//check diagonal
	//else if (){
	//	return true;}	
	else {return false;}
}
///trajectory
function ArcExclusion(startcell: GameObject, othercell: GameObject): boolean{//false if excluded
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
function DirectionVector(startcell:GameObject,othercell:GameObject): Vector3{//for linear direction determination
	var cellA: CellProperties = startcell.GetComponent(CellProperties);
	var cellB: CellProperties = othercell.GetComponent(CellProperties);	
	var dir: Vector3;
	dir.x=cellB.gameCoord.x-cellA.gameCoord.x;
	dir.y=cellB.gameCoord.y-cellA.gameCoord.y;
	dir.z=cellB.gameCoord.z-cellA.gameCoord.z;
	return dir;
}
function LinearBend(startcell:GameObject,direction:Vector3): Vector3{//for bending linear targeting around corners
	var cell: CellProperties = startcell.GetComponent(CellProperties);
	
	//corners
	if ((cell.gameCoord.x==0 || cell.gameCoord.x==count) && (cell.gameCoord.y==0 || cell.gameCoord.y==count) && (cell.gameCoord.z==0 || cell.gameCoord.z==count)){
		
		//front-bottom-left
		if (cell.gameCoord.x==0 && cell.gameCoord.y==0 && cell.gameCoord.z==0){
			if(direction.x==-1 && direction.y==-1){direction=Vector3(0,0,1);}
			if(direction.y==-1 && direction.z==-1){direction=Vector3(1,0,0);}
			if(direction.x==-1 && direction.z==-1){direction=Vector3(0,1,0);}
		}
		//front-bottom-right
		if (cell.gameCoord.x==count && cell.gameCoord.y==0 && cell.gameCoord.z==0){
			if(direction.x==1 && direction.y==-1){direction=Vector3(0,0,1);}
			if(direction.y==-1 && direction.z==-1){direction=Vector3(-1,0,0);}
			if(direction.x==1 && direction.z==-1){direction=Vector3(0,1,0);}
		}
		//front-top-left
		if (cell.gameCoord.x==0 && cell.gameCoord.y==count && cell.gameCoord.z==0){
			if(direction.x==-1 && direction.y==1){direction=Vector3(0,0,1);}
			if(direction.y==1 && direction.z==-1){direction=Vector3(1,0,0);}
			if(direction.x==-1 && direction.z==-1){direction=Vector3(0,-1,0);}
		}
		//front-top-right
		if (cell.gameCoord.x==count && cell.gameCoord.y==count && cell.gameCoord.z==0){
			if(direction.x==1 && direction.y==1){direction=Vector3(0,0,1);}
			if(direction.y==1 && direction.z==-1){direction=Vector3(-1,0,0);}
			if(direction.x==1 && direction.z==-1){direction=Vector3(0,-1,0);}
		}
		//back-bottom-left
		if (cell.gameCoord.x==0 && cell.gameCoord.y==0 && cell.gameCoord.z==count){
			if(direction.x==-1 && direction.y==-1){direction=Vector3(0,0,-1);}
			if(direction.y==-1 && direction.z==1){direction=Vector3(1,0,0);}
			if(direction.x==-1 && direction.z==1){direction=Vector3(0,1,0);}
		}
		//back-bottom-right
		if (cell.gameCoord.x==count && cell.gameCoord.y==0 && cell.gameCoord.z==count){
			if(direction.x==1 && direction.y==-1){direction=Vector3(0,0,-1);}
			if(direction.y==-1 && direction.z==1){direction=Vector3(-1,0,0);}
			if(direction.x==1 && direction.z==1){direction=Vector3(0,1,0);}
		}
		//back-top-left
		if (cell.gameCoord.x==0 && cell.gameCoord.y==count && cell.gameCoord.z==count){
			if(direction.x==-1 && direction.y==1){direction=Vector3(0,0,-1);}
			if(direction.y==1 && direction.z==1){direction=Vector3(1,0,0);}
			if(direction.x==-1 && direction.z==1){direction=Vector3(0,-1,0);}
		}	
		//back-top-right
		if (cell.gameCoord.x==count && cell.gameCoord.y==count && cell.gameCoord.z==count){
			if(direction.x==1 && direction.y==1){direction=Vector3(0,0,-1);}
			if(direction.y==1 && direction.z==1){direction=Vector3(-1,0,0);}
			if(direction.x==1 && direction.z==1){direction=Vector3(0,-1,0);}
		}	
	}
	//edges
	else {
		//front
		if (cell.gameCoord.z==0){
			if ((cell.gameCoord.x==0 && direction.x==-1)||(cell.gameCoord.x==count && direction.x==1)){
				direction.x=0; direction.z=1;}
			if ((cell.gameCoord.y==0 && direction.y==-1)||(cell.gameCoord.y==count && direction.y==1)){
				direction.y=0; direction.z=1;}
		}
		//back
		if (cell.gameCoord.z==count){
			if ((cell.gameCoord.x==0 && direction.x==-1)||(cell.gameCoord.x==count && direction.x==1)){
				direction.x=0; direction.z=-1;}
			if ((cell.gameCoord.y==0 && direction.y==-1)||(cell.gameCoord.y==count && direction.y==1)){
				direction.y=0; direction.z=-1;}
		}
		//left
		if (cell.gameCoord.x==0){
			if ((cell.gameCoord.y==0 && direction.y==-1)||(cell.gameCoord.y==count && direction.y==1)){
				direction.y=0; direction.x=1;}
			if ((cell.gameCoord.z==0 && direction.z==-1)||(cell.gameCoord.z==count && direction.z==1)){
				direction.z=0; direction.x=1;}
		}
		//right
		if (cell.gameCoord.x==count){
			if ((cell.gameCoord.y==0 && direction.y==-1)||(cell.gameCoord.y==count && direction.y==1)){
				direction.y=0; direction.x=-1;}
			if ((cell.gameCoord.z==0 && direction.z==-1)||(cell.gameCoord.z==count && direction.z==1)){
				direction.z=0; direction.x=-1;}
		}
		//bottom
		if (cell.gameCoord.y==0){
			if ((cell.gameCoord.x==0 && direction.x==-1)||(cell.gameCoord.x==count && direction.x==1)){
				direction.x=0; direction.y=1;}
			if ((cell.gameCoord.z==0 && direction.z==-1)||(cell.gameCoord.z==count && direction.z==1)){
				direction.z=0; direction.y=1;}
			}
		//top	
		if (cell.gameCoord.y==count){
			if ((cell.gameCoord.x==0 && direction.x==-1)||(cell.gameCoord.x==count && direction.x==1)){
				direction.x=0; direction.y=-1;}
			if ((cell.gameCoord.z==0 && direction.z==-1)||(cell.gameCoord.z==count && direction.z==1)){
				direction.z=0; direction.y=-1;}
		}
	
	}
	return direction;
}
///legal targets?
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
function IsLegal(cell: GameObject): boolean{
	if (cell.GetComponent(CellProperties).legal==true){return true;}
	else {return false;}
}
function MakeLegal(cell: GameObject){
	cell.GetComponent(CellProperties).legal=true;
}
function NoMoves(){//action1 refund if no targets
	actionCoord.Refund(1,"No legal moves.");
	actionCoord.ResetAction();
}
function NoTargets(){//action3 refund if no targets
	actionCoord.Refund(3,"No legal targets.");
	actionCoord.ResetAction();
}
///occupation status
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
function HasUnit(cell: GameObject): boolean{
	var unitList = GameObject.FindGameObjectsWithTag("unit");
	var i: byte;
	for (i=0; i<unitList.length; i++){
		var unitStats: ObjectStats = unitList[i].GetComponent(ObjectStats);
		if (unitStats.mycell==cell){	
			return true;
		}
	}
	return false;
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
///
function CellCount(count:byte): short{//counts cell objects
	return (2*( ((count+1)*(count+1)) + ((count+1)*(count-1)) + ((count-1)*(count-1)) ));
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
function ResetCells(){//reset all cells legality
	celllist=GameObject.FindGameObjectsWithTag("cell");
	var i: short;
	for (i=0; i<celllist.length; i++){
		celllist[i].GetComponent(CellProperties).legal=false;
	}	
}
function CheckList(list: List.<GameObject>, object: GameObject): boolean{//check if object is in list (RTN true if object is in list)
	var i: byte;
	for (i=0; i<list.Count; i++){
		if (list[i]==object){
			return true;
		}	
	}
	return false;
}