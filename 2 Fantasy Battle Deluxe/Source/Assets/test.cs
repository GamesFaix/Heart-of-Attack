using UnityEngine;
using System.Collections;
using FBI.Tokens;
using FBI.Map;

public class test : MonoBehaviour {

	void Start(){
		GameObject boardGO = GameObject.FindGameObjectWithTag("Board") as GameObject;
		Board b = boardGO.GetComponent("Board") as Board;
		b.New(3);
	}


	void OnGUI(){
		GamePoint gP = Board.cellMatrix.GetPoint(new Vector3(5,3,0));
		
		
		if (GUI.Button(new Rect(0,0,100,30), "Make ninjoid")){
			TokenFactory.Create(1, TokenValue.NINJOID, gP);	
			
		}
		if (GUI.Button(new Rect(0,30,100,30), "Make sentinel")){
			TokenFactory.Create(1, TokenValue.SENTINEL, gP);	
			
		}
		if (GUI.Button(new Rect(0,60,100,30), "Make moth")){
			TokenFactory.Create(1, TokenValue.MOTH, gP);	
			
		}
		if (GUI.Button(new Rect(0,90,100,30), "Make stagbot")){
			TokenFactory.Create(1, TokenValue.STAGBOT, gP);	
			
		}
	}
}
