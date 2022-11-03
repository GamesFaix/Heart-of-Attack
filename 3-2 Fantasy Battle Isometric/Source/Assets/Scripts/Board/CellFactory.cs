using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public static class CellFactory {
		static GameObject cellPF = Resources.Load("Prefabs/CellPrefab") as GameObject;

		static GameObject parent;
		public static GameObject Parent {get {return parent;} }

		public static void CreateParent () {
			Reset();
			CreateParentCell();
		}

		public static void Reset () {GameObject.Destroy(parent);}


		static void CreateParentCell() {
			parent = GameObject.Instantiate(cellPF, new Vector3(0,0,0), Quaternion.identity) as GameObject;
			parent.renderer.enabled = false;
			parent.name = "Cell Parent";
		}

		public static void AttachPrefabs () {
			foreach (Cell c in Board.Cells) {
				c.Display.gameObject.transform.parent = parent.transform;
			}
		}
	}
}