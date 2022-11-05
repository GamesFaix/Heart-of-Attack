#pragma strict

var node: GameObject;
var spawn_number: byte;
var spawntype: byte;
var wait=0;
var grid: byte;
var count: short;
var nodeClone: GameObject;
var node_x: short; 
var node_y: short; 
var node_z: short;
var x_repeat: byte;
var y_repeat: byte; 
var z_repeat: byte;
var createWorld: CreateWorld;

function Awake(){
	createWorld=GameObject.Find("GameIndexPrefab").GetComponent(CreateWorld);
	grid=createWorld.grid;
	count=createWorld.count;
}
function Start(){

	if (spawntype==0){
		node_x=Mathf.RoundToInt(transform.position.x-2*grid);
		node_y=Mathf.RoundToInt(transform.position.y-2*grid);

		for (y_repeat=0; y_repeat<5; y_repeat++){
			for (x_repeat=0; x_repeat<5; x_repeat++){
				nodeClone=Instantiate(node,Vector3(node_x,node_y,transform.position.z),Quaternion.identity);
				nodeClone.transform.parent=this.transform;
				//nodeClone.transform.localScale=Vector3(0,0,0);
				node_x+=grid;
			}
			node_x=Mathf.RoundToInt(transform.position.x-2*grid);
			node_y+=grid;
		}
			
		if (transform.position.x==0-count/2*grid){spawn_number=1; transform.eulerAngles=Vector3(0,90,0);}
		if (transform.position.x==count/2*grid){spawn_number=2; transform.eulerAngles=Vector3(0,90,0);}
		if (transform.position.y==0-count/2*grid){spawn_number=3; transform.eulerAngles=Vector3(90,0,0);}
		if (transform.position.y==count/2*grid){spawn_number=4; transform.eulerAngles=Vector3(90,0,0);}
		if (transform.position.z==0-count/2*grid){spawn_number=5; transform.eulerAngles=Vector3(0,0,0);}
		if (transform.position.z==count/2*grid){spawn_number=6; transform.eulerAngles=Vector3(0,0,0);}
	}

	if (spawntype==1){
		node_x=transform.position.x-grid;
		node_y=transform.position.y-grid;
		node_z=transform.position.z-grid;
		
		for (y_repeat=0; y_repeat<3; y_repeat++){
			for (x_repeat=0; x_repeat<3; x_repeat++){
				nodeClone=Instantiate(node,Vector3(node_x,node_y,node_z),Quaternion.identity);
				nodeClone.transform.parent=transform;
				node_x+=grid;
			}
			node_x=transform.position.x-grid;
			node_y+=grid;
		}
		
		node_x=transform.position.x-grid;
		node_y=transform.position.y-grid;
		node_z=transform.position.z;
				
		for (z_repeat=0; z_repeat<2; z_repeat++){
			for (x_repeat=0; x_repeat<3; x_repeat++){
				nodeClone=Instantiate(node,Vector3(node_x,node_y,node_z),Quaternion.identity);
				nodeClone.transform.parent=transform;
				node_x+=grid;
			}
			node_x=transform.position.x-grid;
			node_z+=grid;
		}
			
		node_x=transform.position.x-grid;
		node_y=transform.position.y;
		node_z=transform.position.z;
			
		for (z_repeat=0; z_repeat<2; z_repeat++){
			for (y_repeat=0; y_repeat<2; y_repeat++){
				nodeClone=Instantiate(node,Vector3(node_x,node_y,node_z),Quaternion.identity);
				nodeClone.transform.parent=transform;
				node_y+=grid;
			}
			node_y=transform.position.y;
			node_z+=grid;
		}
			
		if (transform.position==Vector3(count/2*grid-grid,count/2*grid-grid,count/2*grid-grid)){spawn_number=1;
			transform.eulerAngles=Vector3(0,180,270);}
		if (transform.position==Vector3(0-(count/2*grid-grid),count/2*grid-grid,count/2*grid-grid)){spawn_number=2;
			transform.eulerAngles=Vector3(0,90,270);}
		if (transform.position==Vector3(count/2*grid-grid,0-(count/2*grid-grid),count/2*grid-grid)){spawn_number=3;
			transform.eulerAngles=Vector3(0,180,0);}
		if (transform.position==Vector3(count/2*grid-grid,count/2*grid-grid,0-(count/2*grid-grid))){spawn_number=4;
			transform.eulerAngles=Vector3(0,270,270);}
		if (transform.position==Vector3(0-(count/2*grid-grid),0-(count/2*grid-grid),count/2*grid-grid)){spawn_number=5;
			transform.eulerAngles=Vector3(0,90,0);}
		if (transform.position==Vector3(count/2*grid-grid,0-(count/2*grid-grid),0-(count/2*grid-grid))){spawn_number=6;
			transform.eulerAngles=Vector3(0,270,0);}
		if (transform.position==Vector3(0-(count/2*grid-grid),count/2*grid-grid,0-(count/2*grid-grid))){spawn_number=7;
			transform.eulerAngles=Vector3(90,0,0);}
		if (transform.position==Vector3(0-(count/2*grid-grid),0-(count/2*grid-grid),0-(count/2*grid-grid))){spawn_number=8;
			transform.eulerAngles=Vector3(0,0,0);}
	}
}
