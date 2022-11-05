/* Primary cell script. */

using UnityEngine;
using System;
using System.Collections;
using FBI.Map;
using FBI.Math;

public class Cell : MonoBehaviour{
	public Vector2 chunk;
	GamePoint[] gamePoints;
	Token[] inhabitants;
	
	public int x;
	public int y;
	Vector3 pos;
		
	void Start(){
		pos = gameObject.transform.position;
		x = Mathf.RoundToInt(pos.x/MapScale.scale);
		y = Mathf.RoundToInt(pos.z/MapScale.scale);
		
		int count = Enum.GetValues(typeof(CellZ)).Length;
		gamePoints = CreateGamePoints(count);
		inhabitants = new Token[count];
	}
	
	public Vector2 XY() {return new Vector2(x,y);}


	public CellStatus status = CellStatus.NORMAL;
	public CellStatus GetStatus() {return status;}
	public void SetStatus(CellStatus newStatus) {status = newStatus;}

	GamePoint[] CreateGamePoints(int count){
		GamePoint[] gps = new GamePoint[count];
		
		for (byte i = 0; i<count; i++){
			gps[i] = new GamePoint(this, (CellZ)i);
			Vector3 key = new Vector3 (x,y,i);
			Board.cellMatrix.AddPoint(key, gps[i]); 
		}
		return gps;
	}
	
	public void Occupy(Token token){
		if (LegalForEntryBy(token)){
			CellZ tokenZ = token.GetHeight();	
			inhabitants[(int)tokenZ] = token;
			
			Cell prevCell = token.GetCell();
			prevCell.Vacate(tokenZ);
			
			token.SetLocation(gamePoints[(int)tokenZ]);
		}
	}
	
	public void Vacate(CellZ tokenZ){
		inhabitants[(int)tokenZ] = null;		
	}
	
	
	bool LegalForEntryBy(Token token){
		CellZ zToken = token.GetHeight();
		switch (zToken){
			case CellZ.ITEM:
				if (!inhabitants[(int)CellZ.IMPASS]) {return true;}
				break;
			case CellZ.GND:
				if ((!inhabitants[(int)CellZ.IMPASS]) 
				&& (!inhabitants[(int)CellZ.GND])
				&& (!inhabitants[(int)CellZ.TRM])
				&& (!inhabitants[(int)CellZ.FLAT])) {return true;}
				break;
			case CellZ.TRM:
				//////////////////
				break;
			case CellZ.FLY:
				if (!inhabitants[(int)CellZ.IMPASS]
				&& (!inhabitants[(int)CellZ.FLY])) {return true;}
				break;
			case CellZ.GAS:
				if (!inhabitants[(int)CellZ.GAS]) {return true;}
				break;
		}
		return false;
	}
	
	sbyte HighestInhabitant(){
		int count = Enum.GetValues(typeof(CellZ)).Length;
		sbyte highest = 0;
		for (sbyte i = 0; i<count; i++){
			if (inhabitants[i]) {highest = i;}
		}
		return highest;
	}	
	
	public void GameTick(){
					
		
	}
}
