#pragma strict

import System.Collections.Generic;

var celllist: GameObject[];
var objectPrefab: GameObject;
var fightingStart: FightingStart;
var map_id: byte;
var density: byte;
var count: byte;

//enums
static var eObjno: byte = 7;

function Awake(){
	var gameindexprefab: GameObject = GameObject.Find("GameIndexPrefab");
	celllist= GameObject.FindGameObjectsWithTag("cell");
	fightingStart=gameindexprefab.GetComponent(FightingStart);
	map_id=fightingStart.map_id;
	density=gameindexprefab.GetComponent(GameIndex).density;
	count=fightingStart.count;
}

function ObstacleGenerator(): IEnumerator{
	if (map_id==1){yield Level1B();}//grass
	if (map_id==2){yield Level2();}//sand
	if (map_id==3){yield Level3();}//lava
	if (map_id==4){yield Level4();}//ice
	
	yield;
}

function FindEmpty(): List.<GameObject>{
//output list of unoccupied cells

	var emptycells = new List.<GameObject>();
	for (var i: short=0; i<celllist.length; i++){
		var cellstats: CellProperties = celllist[i].GetComponent(CellProperties);
		if (cellstats.occA==0 && cellstats.occB==0){
			emptycells.Add(celllist[i]);
		}
	}
	return emptycells;
}

function MakeObstacle(count: short, objno: short): IEnumerator{
	var emptyCells: List.<GameObject> = FindEmpty();

	for (var i: short=0; i<count; i++){
		
		var random: short = Mathf.Floor(Random.value*emptyCells.Count);
		var randomcell: GameObject = emptyCells[random];
		
		var obstacle: GameObject = Instantiate(objectPrefab,randomcell.transform.position,Quaternion.identity);
		obstacle.GetComponent(ObjectStats).coreStats[eObjno]=objno;
		emptyCells.RemoveAt(random);
	}	
	yield;
}
function Level1(): IEnumerator{
	var maxobs: short = Mathf.Floor(celllist.length*density/100);
	
	var mountains: short = Mathf.Floor(maxobs*0.1);
	var hills: short = Mathf.Floor(maxobs*0.2);
	var rocks: short = Mathf.Floor(maxobs*0.2);
	var water: short = Mathf.Floor(maxobs*0.1);
	var trees: short = maxobs-mountains-hills-rocks-water;
	
	yield MakeObstacle(mountains,3101);
	yield MakeObstacle(hills,3201);
	yield MakeObstacle(rocks,3302);
	yield MakeObstacle(water,3202);
	yield MakeObstacle(trees,3301);

	yield;
}
function Level2(): IEnumerator{
	var maxobs: short = Mathf.Floor(celllist.length*density/100);
	
	var pyramids: short = Mathf.Floor(maxobs*0.1);
	var cacti: short = Mathf.Floor(maxobs*0.25);
	var dunes: short = Mathf.Floor(maxobs*0.6);
	var water: short = maxobs-pyramids-cacti-dunes;
	
	yield MakeObstacle(pyramids,3103);
	yield MakeObstacle(cacti,3306);
	yield MakeObstacle(dunes,3204);
	yield MakeObstacle(water,3202);

	yield;
}
function Level3(): IEnumerator{
	var maxobs: short = Mathf.Floor(celllist.length*density/100);
	
	var mountains: short = Mathf.Floor(maxobs*0.1);
	var lava: short = Mathf.Floor(maxobs*0.25);
	var rock: short = Mathf.Floor(maxobs*0.2);
	var rockvolcanic: short = Mathf.Floor(maxobs*0.25);
	var corpses: short = maxobs-mountains-lava-rock-rockvolcanic;
	
	yield MakeObstacle(mountains,3101);
	yield MakeObstacle(lava,3208);
	yield MakeObstacle(rock,3302);
	yield MakeObstacle(rockvolcanic,3307);
	yield MakeObstacle(corpses,3401);
	
	yield;
}
function Level4(): IEnumerator{
	var maxobs: short = Mathf.Floor(celllist.length*density/100);
	
	var mountains: short = Mathf.Floor(maxobs*0.2);
	var ice: short = Mathf.Floor(maxobs*0.25);
	var snow: short = Mathf.Floor(maxobs*0.25);
	var rock: short = Mathf.Floor(maxobs*0.1);
	var treepine: short = Mathf.Floor(maxobs*0.15);
	var water: short = maxobs-mountains-ice-snow-rock-treepine;
	
	yield MakeObstacle(mountains,3101);
	yield MakeObstacle(ice,3312);
	yield MakeObstacle(snow,3205);
	yield MakeObstacle(rock,3302);
	yield MakeObstacle(treepine,3304);
	yield MakeObstacle(water,3202);

	yield;
}
function Level1B(): IEnumerator{
	var towerCells = new List.<GameObject>();
	var wallCells = new List.<GameObject>();
	
	//outer corners
	towerCells.Add(GameObject.Find("Cell - (0,0,0)"));
	towerCells.Add(GameObject.Find("Cell - (0,10,0)"));
	towerCells.Add(GameObject.Find("Cell - (0,0,10)"));
	towerCells.Add(GameObject.Find("Cell - (0,10,10)"));
	//openings on outer wall
	towerCells.Add(GameObject.Find("Cell - (0,4,0)"));
	towerCells.Add(GameObject.Find("Cell - (0,6,0)"));
	towerCells.Add(GameObject.Find("Cell - (0,0,3)"));
	towerCells.Add(GameObject.Find("Cell - (0,0,7)"));
	towerCells.Add(GameObject.Find("Cell - (0,10,3)"));
	towerCells.Add(GameObject.Find("Cell - (0,10,7)"));
	towerCells.Add(GameObject.Find("Cell - (0,4,10)"));
	towerCells.Add(GameObject.Find("Cell - (0,6,10)"));
	//outer walls
	wallCells.Add(GameObject.Find("Cell - (0,0,1)"));
	wallCells.Add(GameObject.Find("Cell - (0,0,2)"));
	wallCells.Add(GameObject.Find("Cell - (0,0,8)"));
	wallCells.Add(GameObject.Find("Cell - (0,0,9)"));
	wallCells.Add(GameObject.Find("Cell - (0,10,1)"));
	wallCells.Add(GameObject.Find("Cell - (0,10,2)"));
	wallCells.Add(GameObject.Find("Cell - (0,10,8)"));
	wallCells.Add(GameObject.Find("Cell - (0,10,9)"));
	wallCells.Add(GameObject.Find("Cell - (0,1,0)"));
	wallCells.Add(GameObject.Find("Cell - (0,2,0)"));
	wallCells.Add(GameObject.Find("Cell - (0,3,0)"));
	wallCells.Add(GameObject.Find("Cell - (0,7,0)"));
	wallCells.Add(GameObject.Find("Cell - (0,8,0)"));
	wallCells.Add(GameObject.Find("Cell - (0,9,0)"));
	wallCells.Add(GameObject.Find("Cell - (0,1,10)"));
	wallCells.Add(GameObject.Find("Cell - (0,2,10)"));
	wallCells.Add(GameObject.Find("Cell - (0,3,10)"));
	wallCells.Add(GameObject.Find("Cell - (0,7,10)"));
	wallCells.Add(GameObject.Find("Cell - (0,8,10)"));
	wallCells.Add(GameObject.Find("Cell - (0,9,10)"));
	//inner corners
	towerCells.Add(GameObject.Find("Cell - (0,3,3)"));
	towerCells.Add(GameObject.Find("Cell - (0,7,3)"));
	towerCells.Add(GameObject.Find("Cell - (0,3,7)"));
	towerCells.Add(GameObject.Find("Cell - (0,7,7)"));
	//inner walls
	wallCells.Add(GameObject.Find("Cell - (0,3,4)"));
	wallCells.Add(GameObject.Find("Cell - (0,3,6)"));
	wallCells.Add(GameObject.Find("Cell - (0,7,4)"));
	wallCells.Add(GameObject.Find("Cell - (0,7,6)"));
	//wallCells.Add(GameObject.Find("Cell - (0,4,3)"));
	//wallCells.Add(GameObject.Find("Cell - (0,6,3)"));
	//wallCells.Add(GameObject.Find("Cell - (0,4,7)"));
	//wallCells.Add(GameObject.Find("Cell - (0,6,7)"));
	//
	for (var i: short=0; i<wallCells.Count; i++){
		var obstacle: GameObject;
		var cell: GameObject = wallCells[i];
		obstacle=Instantiate(objectPrefab, cell.transform.position, Quaternion.identity);
		obstacle.GetComponent(ObjectStats).coreStats[eObjno]=3209;
	}
	for (i=0; i<towerCells.Count; i++){
		cell = towerCells[i];
		obstacle=Instantiate(objectPrefab, cell.transform.position, Quaternion.identity);
		obstacle.GetComponent(ObjectStats).coreStats[eObjno]=3104;
	}
	var emptyCells = new List.<GameObject>();
	emptyCells=FindEmpty();
	
	var maxobs: short = Mathf.Floor(emptyCells.Count*density/100);
	
	var mountains: short = Mathf.Floor(maxobs*0.1);
	var hills: short = Mathf.Floor(maxobs*0.2);
	var rocks: short = Mathf.Floor(maxobs*0.2);
	var water: short = Mathf.Floor(maxobs*0.1);
	var trees: short = maxobs-mountains-hills-rocks-water;
	
	yield MakeObstacle(mountains,3101);
	yield MakeObstacle(hills,3201);
	yield MakeObstacle(rocks,3302);
	yield MakeObstacle(water,3202);
	yield MakeObstacle(trees,3301);

	yield;
	
}