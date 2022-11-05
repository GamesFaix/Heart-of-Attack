using UnityEngine;
using System.Collections;
using FBI.Map;
using FBI.Tokens;

public class Token : MonoBehaviour {
	GamePoint location;
	public GamePoint GetLocation() {return location;}
	public void SetLocation(GamePoint newLocation) {location = newLocation;}
	
	public Cell GetCell() {return location.GetCell();}
	
	
	CellZ height;
	public CellZ GetHeight() {return height;}
	public void SetHeight(CellZ h){height = h;} 

	public string name;
	public TokenValue tokenValue;
	public int owner;
	public TokenProperties properties;

	public Unit GetUnit() {return gameObject.GetComponent("Unit") as Unit;}
	
	public Timer GetTimer() {return gameObject.GetComponent("Timer") as Timer;}
	
	public TokenDisplay GetDisplay() {return gameObject.GetComponent("TokenDisplay") as TokenDisplay;}
	
	
	
	
	public TokenValue tokenOnDeath;
}
