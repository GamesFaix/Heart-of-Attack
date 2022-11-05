/* Creates and maintains game board. */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FBI.Map;
using FBI.Collections;


public class Board : MonoBehaviour{
	public GameObject cellPF;
	public static Texture[] cellTextures;
	public GameObject groundPlanePF;
	
	//public static ReDictionary<Vector2, Cell> cells;
	//public static ReDictionary<Vector3, GamePoint> gamePoints;		
	public static CellMatrix cellMatrix;
	
	
	void Awake(){
		cellPF = Resources.Load("Prefabs/CellPF") as GameObject;
		groundPlanePF = Resources.Load("Prefabs/GroundPlanePF") as GameObject;
		LoadCellTextures();
	}
	
	void LoadCellTextures(){
		cellTextures = new Texture[5];
		cellTextures[(int)CellStatus.NORMAL] = Resources.Load("Images/Cells/BlueCell") as Texture2D;
		cellTextures[(int)CellStatus.BORDER] = Resources.Load("Images/Cells/GlassCell") as Texture2D;		
		cellTextures[(int)CellStatus.MOVE] = Resources.Load("Images/Cells/GreenCell") as Texture2D;		
		cellTextures[(int)CellStatus.ATTACK] = Resources.Load("Images/Cells/RedCell") as Texture2D;
		cellTextures[(int)CellStatus.OTHER] = Resources.Load("Images/Cells/YellowCell") as Texture2D;		
	}

	
	
	public void New (byte chunksPerSide){
		
		GameObject[] oldCells = GameObject.FindGameObjectsWithTag("Cell");
		foreach (GameObject oldCell in oldCells) {Destroy(oldCell);}
		
		GameObject[] oldGrounds = GameObject.FindGameObjectsWithTag("GroundPlane");
		foreach (GameObject oldGround in oldGrounds) {Destroy(oldGround);}
		
		//cells = new ReDictionary<Vector2, Cell>();
		//gamePoints = new ReDictionary<Vector3, GamePoint>();
		cellMatrix = new CellMatrix();
		
		CreateAllChunks(chunksPerSide);
		CreateBorder(chunksPerSide);
	}
	
	void CreateGroundPlane(Vector2 chunk){
		Vector3 scale = new Vector3(cellsPerChunk,0,cellsPerChunk);
		
		Vector3 chunkCenter = new Vector3(0,0-MapScale.scale/2,0);
		chunkCenter.x = (chunk.x*cellsPerChunk*MapScale.scale) + (cellsPerChunk-1)*MapScale.scale/2;
		chunkCenter.z = (chunk.y*cellsPerChunk*MapScale.scale) + (cellsPerChunk-1)*MapScale.scale/2;
		
		GameObject groundPlane = Instantiate(groundPlanePF, chunkCenter, Quaternion.identity) as GameObject;
		groundPlane.transform.localScale = scale;
		groundPlane.transform.parent = gameObject.transform;
		groundPlane.name = "Ground Plane - Chunk "+chunk;
	}

	byte cellsPerChunk = 4;	
	void CreateAllChunks(byte chunksPerSide){
		
		for (byte i=0; i<chunksPerSide; i++){
			for (byte j=0; j<chunksPerSide; j++){
				CreateChunk(new Vector2(i,j));	
			}
		}
	}
	
	void CreateChunk(Vector2 chunk){
		Vector2 createV2 = new Vector2(0,0);
		createV2.x = chunk.x*cellsPerChunk;
		createV2.y = chunk.y*cellsPerChunk;
		
		Cell newCell;
		
		for (byte i = 0; i<cellsPerChunk; i++){
			for (byte j = 0; j<cellsPerChunk; j++){
				newCell = CreateCell(createV2, chunk);
				cellMatrix.AddCell(createV2, newCell);				
				createV2.x ++;	
			}
			createV2.x = chunk.x*cellsPerChunk;
			createV2.y++;
		}
		
		CreateGroundPlane(chunk);
	}
	
	
	Cell CreateCell(Vector2 location, Vector2 chunk){
		Vector3 worldPosition = new Vector3(0,0,0);
		worldPosition.x = location.x*MapScale.scale;
		worldPosition.z = location.y*MapScale.scale;
		
		GameObject cellGO = GameObject.Instantiate(cellPF, worldPosition, Quaternion.identity) as GameObject;
		cellGO.transform.parent = gameObject.transform;
		cellGO.name = "Cell - ("+location.x+", "+location.y+")";
		
		Cell newCell = cellGO.GetComponent("Cell") as Cell;
		newCell.chunk = chunk;
			
		return newCell;
	}
	
	void CreateBorder(byte chunksPerSide){
		byte cellsPerSide = (byte)(chunksPerSide*cellsPerChunk + 2);
		Vector2 startV2 = new Vector2(-1, -1);	
		
		Vector2 createV2 = startV2;
		Cell newCell;
		
		for (byte i=0; i<cellsPerSide; i++){
			newCell = CreateCell(createV2, new Vector2(-1,-1));
			newCell.SetStatus(CellStatus.BORDER);
			newCell.name = "Border " + newCell.name;	
			createV2.x++;
		}
		
		createV2.x = -1;
		createV2.y = cellsPerSide - 2;
		
		for (byte i=0; i<cellsPerSide; i++){
			newCell = CreateCell(createV2, new Vector2(-1,-1));
			newCell.SetStatus(CellStatus.BORDER);
			newCell.name = "Border " + newCell.name;	
			createV2.x++;
		}
		
		createV2.x = -1;
		createV2.y = 0;
		
		for (byte i=1; i<cellsPerSide-1; i++){
			newCell = CreateCell(createV2, new Vector2(-1,-1));
			newCell.SetStatus(CellStatus.BORDER);
			newCell.name = "Border " + newCell.name;	
			createV2.y++;
		}
		
		createV2.x = cellsPerSide -2;
		createV2.y = 0;
		
		for (byte i=1; i<cellsPerSide-1; i++){
			newCell = CreateCell(createV2, new Vector2(-1,-1));
			newCell.SetStatus(CellStatus.BORDER);
			newCell.name = "Border " + newCell.name;	
			createV2.y++;
		}	
	}
	
}