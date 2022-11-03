using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public static class BorderFactory {

		static GameObject borderPF = Resources.Load("Prefabs/BorderPrefab") as GameObject;


		public static GameObject parent;
		
		public static void Generate (int count) {
			Reset();
			CreateParent();
			CreateBorder(count);
		}
		
		public static void Reset () {GameObject.Destroy(parent);}

		static void CreateParent() {
			parent = GameObject.Instantiate(borderPF, new Vector3(0,0,0), Quaternion.identity) as GameObject;
			parent.renderer.enabled = false;
			parent.name = "Border Parent";
		}
		
		static void CreateBorder(int count) {
			for (int x=0; x<=count+1; x++) {
				for (int y=0; y<=count+1; y+=(count+1)) {
					Create(x,y);
				}
			}
			
			for (int y=1; y<=count; y++) {
				for (int x=0; x<=count+1; x+=count+1) {
					Create(x,y);
				}
			}
		}
		
		static void Create (int x, int y) {
			int size = Board.CellSize;
			Vector3 pos = new Vector3(0,0,0);
			pos.x = (x-1)*size;
			pos.z = (y-1)*size;
			
			GameObject border = GameObject.Instantiate(borderPF, pos, Quaternion.identity) as GameObject;
			border.transform.localScale = new Vector3 (size,size/2,size);
			border.transform.parent = parent.transform;
			border.name = "Border ("+x+","+y+")";
		}
	}
}