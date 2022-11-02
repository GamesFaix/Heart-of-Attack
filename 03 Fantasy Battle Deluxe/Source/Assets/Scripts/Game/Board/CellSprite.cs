using UnityEngine;
using System.Collections;

namespace HOA {

	public class CellSprite {

		Cell parent;
		Texture2D tex;

		public CellSprite (Cell c) {
			parent = c;
			if (Even) {tex = ImageLoader.cells[0];}
			else {tex = ImageLoader.cells[1];}
		}

		bool Even {
			get {
				if ( (parent.X + parent.Y) %2 == 0) {return true;}
				return false;
			}
		}



		public Rect Box {
			get {
				Rect boardBox = GUIBoard.Box;
				float size = boardBox.width/Board.Size;
				
				float x = boardBox.x + (parent.X-1)*size;
				float y = boardBox.y + (parent.Y-1)*size;
			
				return new Rect(x,y,size,size);
			}
		}

		public void Draw () {
			GUI.Box(Box, tex, GUIMaster.S);
			
			if (Input.GetKey("left shift") || Input.GetKey("right shift")) {
				if (GUI.Button(Box, "", GUIMaster.S)) {
					GUIInspector.Inspected = parent;
					GUIMaster.PlaySound(EGUISound.INSPECT);
				}
			}

			if (parent.IsLegal()) {
				Color c = GUI.color;
				GUI.color = new Color (1,1,1,0.5f);
				if (GUI.Button(Box, ImageLoader.yellowBtn, GUIMaster.S)
				    && Input.GetMouseButtonUp(0)) {
					Targeter.Select(parent);
					GUIMaster.PlaySound(EGUISound.TARGET);
				}
				GUI.color = c;
			}
		}
	}
}