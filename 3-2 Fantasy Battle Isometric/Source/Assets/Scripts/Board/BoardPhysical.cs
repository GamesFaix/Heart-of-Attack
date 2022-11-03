using UnityEngine; 

namespace HOA { 

	public class BoardPhysical {
	
		static GameObject boardPF = Resources.Load("Prefabs/BoardPrefab") as GameObject;
		static GameObject cellPF = Resources.Load("Prefabs/CellPrefab") as GameObject;
		//static GameObject exoPF = Resources.Load("Prefabs/ExoCellPrefab") as GameObject;

		public static int CellSize {get {return 25;} }

		GameObject prefab;
		GameObject cellParent;

		Board board;

		public BoardPhysical (Board board) {
			this.board = board;

			prefab = GameObject.Instantiate(boardPF, new Vector3(0,0,0), Quaternion.identity) as GameObject;
			prefab.name = "Board";
			cellParent = GameObject.Instantiate(cellPF, new Vector3(0,0,0), Quaternion.identity) as GameObject;
			cellParent.transform.parent = prefab.transform;
			cellParent.renderer.enabled = false;
			cellParent.name = "Cells";
		}
	
		public void AttachCellPrefabs () {
			foreach (Cell c in board.Cells) {
				//CellDisplay.Attach(c);
				c.Display.gameObject.transform.parent = cellParent.transform;
			}
		}

		public void Destroy () {GameObject.Destroy(prefab);}
	}
}
