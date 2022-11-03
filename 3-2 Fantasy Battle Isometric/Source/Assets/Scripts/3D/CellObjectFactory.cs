using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public static class CellObjectFactory {

		static int size = 25;
		public static GameObject parentCell;

		public static void Generate () {
			Reset();
			CreateParentCell();
			CreateBorder(Board.Size);
		}

		static void CreateParentCell() {
			GameObject cellPF = LoadPrefab("CellPrefab");
			
			parentCell = Instantiate(cellPF, new Vector3(0,0,0));
			parentCell.renderer.enabled = false;
			
			GameObject[] cells = GameObject.FindGameObjectsWithTag("Cell");
			foreach (GameObject cell in cells) {
				cell.transform.parent = parentCell.transform;
			}
		}

		static void CreateBorder(int count) {
			GameObject borderPF = LoadPrefab("BorderPrefab");

			for (int x=0; x<=count+1; x++) {
				for (int y=0; y<=count+1; y+=(count+1)) {
					Vector3 pos = new Vector3(0,0,0);
					pos.x = (x-1)*size;
					pos.z = (y-1)*size;
					
					GameObject border = Instantiate(borderPF, pos);
					border.transform.localScale = new Vector3 (size,size/2,size);
					border.transform.parent = parentCell.transform;
					border.name = "Border ("+x+","+y+")";
				}
			}
			
			for (int y=1; y<=count; y++) {
				for (int x=0; x<=count+1; x+=count+1) {
					Vector3 pos = new Vector3(0,0,0);
					pos.x = (x-1)*size;
					pos.z = (y-1)*size;
					
					GameObject border = Instantiate(borderPF, pos);
					border.transform.localScale = new Vector3 (size,size/2,size);
					border.transform.parent = parentCell.transform;
					border.name = "Border ("+x+","+y+")";
				}
			}
		}

		public static void Reset () {GameObject.Destroy(parentCell);}

		static GameObject LoadPrefab (string fileName) {
			return Resources.Load("Prefabs/"+fileName) as GameObject;
		}

		static GameObject Instantiate (GameObject prefab, Vector3 pos) {
			return GameObject.Instantiate(prefab, pos, Quaternion.identity) as GameObject;
		}
	}
}