using UnityEngine; 

namespace HOA { 

	public class BoardPhysical {
	
		static GameObject boardPF = Resources.Load("Prefabs/BoardPrefab") as GameObject;
		static GameObject cellPF = Resources.Load("Prefabs/CellPrefab") as GameObject;
		static GameObject borderPF = Resources.Load("Prefabs/BorderPrefab") as GameObject;
		
		public static int CellSize {get {return 25;} }

		GameObject prefab;
		GameObject cellParent;
		GameObject borderParent;

		Board board;

		public BoardPhysical (Board board) {
			this.board = board;

			prefab = GameObject.Instantiate(boardPF, new Vector3(0,0,0), Quaternion.identity) as GameObject;
			prefab.name = "Board";
			cellParent = GameObject.Instantiate(cellPF, new Vector3(0,0,0), Quaternion.identity) as GameObject;
			cellParent.transform.parent = prefab.transform;
			cellParent.renderer.enabled = false;
			cellParent.name = "Cells";
			borderParent = GameObject.Instantiate(borderPF, new Vector3(0,0,0), Quaternion.identity) as GameObject;
			borderParent.transform.parent = prefab.transform;
			borderParent.renderer.enabled = false;
			borderParent.name = "Border Blocks";
			CreateBorder();
		}
	
		public void AttachCellPrefabs () {
			foreach (Cell c in board.Cells) {
				c.Display.gameObject.transform.parent = cellParent.transform;
			}
		}

		public void CreateBorder() {
			int2 first = new int2(-1,-1);
			int2 last = board.CellCount;

			for (int i=first.x; i<=last.x; i++) {
				for (int j=first.y; j<=last.y; j+=last.y+1) {
					CreateBorderCell(new int2(i,j));
				}
			}
			for (int j=first.y+1; j<=last.y-1; j++) {
				for (int i=first.x; i<=last.x; i+=last.x+1) {
					CreateBorderCell(new int2(i,j));
				}
			}
		}
		
		public void CreateBorderCell (int2 index) {
			Vector3 pos = BorderPos(index);
			
			GameObject border = GameObject.Instantiate(borderPF, pos, Quaternion.identity) as GameObject;
			border.transform.localScale = new Vector3 (CellSize,CellSize/2,CellSize);
			border.transform.parent = borderParent.transform;
			border.name = "Border "+index;
		}

		static Vector3 BorderPos (int2 index) {
			float x = index.x * CellSize;
			float z = index.y * CellSize;
			return new Vector3 (x,0,z);
		}

		public void Destroy () {GameObject.Destroy(prefab);}
	}
}
