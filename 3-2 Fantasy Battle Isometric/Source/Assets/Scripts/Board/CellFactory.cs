using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public static class CellFactory {
		static GameObject cellPF = Resources.Load("Prefabs/CellPrefab") as GameObject;
		static GameObject spritePF = Resources.Load("Prefabs/SpritePrefab") as GameObject;
//		static GameObject effectPF = Resources.Load("Prefabs/EffectPrefab") as GameObject;
		static Texture2D legalHighlight = Resources.Load("Textures/legal") as Texture2D;
		static Texture2D whiteCell = Resources.Load("Textures/mc grass") as Texture2D;
		static Texture2D blackCell = Resources.Load("Textures/mc dirt") as Texture2D;

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
			int size = Board.CellSize;

			foreach (Cell c in Board.Cells) {
				GameObject prefab = GameObject.Instantiate (cellPF, PrefabPos(c), Quaternion.identity) as GameObject;
				prefab.transform.localScale = new Vector3 ((float)size/10,0,(float)size/10);
				prefab.transform.parent = parent.transform;
				prefab.name = "Cell ("+c.X+","+c.Y+")";

				CellDisplay cd = prefab.GetComponent("CellDisplay") as CellDisplay;
				cd.Parent = c;
				c.Display = cd;

				AttachPlanes(c);

				SetTex (c);
			}
		}
		
		static Vector3 PrefabPos (Cell c) {
			Vector3 pos = new Vector3(0,0,0);
			pos.x = (c.X-1)*Board.CellSize;
			pos.z = (c.Y-1)*Board.CellSize;
			return pos;
		}

		static void AttachPlanes (Cell c) {
			GameObject cellPrefab = c.Display.gameObject;
			Vector3 pos = c.Location;
			
			GameObject plane = GameObject.Instantiate(spritePF, pos, Quaternion.identity) as GameObject;
			plane.transform.parent = cellPrefab.transform;
			plane.transform.position = new Vector3 (pos.x,0.01f,pos.z);
			plane.transform.localScale = new Vector3(1,1,1);
			c.Display.spritePlane = plane;

			plane = GameObject.Instantiate(spritePF, pos, Quaternion.identity) as GameObject;
			plane.transform.parent = cellPrefab.transform;
			plane.transform.position = new Vector3 (pos.x,0.02f,pos.z);
			plane.renderer.material.SetTexture("_MainTex", legalHighlight);
			plane.transform.localScale = new Vector3(1,1,1);
			c.Display.legalPlane = plane;

			plane = GameObject.Instantiate(spritePF, pos, Quaternion.identity) as GameObject;
			plane.transform.parent = cellPrefab.transform;
			plane.transform.position = new Vector3 (pos.x,0.03f,pos.z);
			plane.transform.localScale = new Vector3(1,1,1);
			c.Display.effectPlane = plane;

			c.Display.HideAll();
		}

		static void SetTex (Cell c) {
			if ((c.X+c.Y)%2 == 0) {c.Display.SetTex(blackCell);}
			else {c.Display.SetTex(whiteCell);}
			if (c.X < 0) {c.Display.Hide();}
		}
	}
}