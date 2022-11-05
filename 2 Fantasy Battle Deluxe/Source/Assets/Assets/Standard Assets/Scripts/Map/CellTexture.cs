/* Draws texture of cell based on cell status. */

using UnityEngine;
using FBI.Map;

public class CellTexture : MonoBehaviour {

	Cell cell;
	void Start () {
		cell = gameObject.GetComponent("Cell") as Cell;
	}
	
	Texture2D currentTexture;
	void OnGUI () {
		currentTexture = Board.cellTextures[(int)cell.GetStatus()] as Texture2D;
		gameObject.renderer.material.SetTexture("_MainTex", currentTexture);
		if (cell.GetStatus() == CellStatus.NORMAL){
			gameObject.renderer.enabled = false;	
		}
		else { gameObject.renderer.enabled = true;}
	}
}
