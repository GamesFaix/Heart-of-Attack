using UnityEngine;

namespace HOA {

	public static class GUICell {

		public static void Draw (Cell cell, Rect cellRect) {
			GUI.Box(cellRect, CellTex(cell), GUIMaster.S);

			if (Input.GetKey("left shift") || Input.GetKey("right shift")) {
				if (GUI.Button(cellRect, "", GUIMaster.S)) {
			    	GUIInspector.Inspected = cell;
			
				}
			}

			DrawOccupants(cellRect, cell);
			
			if (cell.IsLegal()) {
				Color c = GUI.color;
				GUI.color = new Color (1,1,1,0.5f);
				if (GUI.Button(cellRect, ImageLoader.yellowBtn, GUIMaster.S)
				    && Input.GetMouseButtonUp(0)) {
					Targeter.Select(cell);
				}
				GUI.color = c;
			}

		}

		static Texture2D CellTex (Cell c) {
			if ( (c.X + c.Y) %2 == 0) {return ImageLoader.cells[0];}
			else {return ImageLoader.cells[1];}
		}

		static void DrawOccupants (Rect cellRect, Cell cell) {
			Rect tokenRect;
			TokenGroup tg = cell.Occupants;
			
			for (int i=0; (i<4 && i<tg.Count); i++) {
				Token t = tg[i];
				//if (t != default(Token)) {
				tokenRect = TokenRect(cellRect, cell ,i);
				t.Draw(tokenRect);


				if (GUI.Button(tokenRect, "", GUIMaster.S)){
					if (Input.GetMouseButtonUp(0) && t.IsLegal()){Targeter.Select(t);}
					else if (Input.GetMouseButtonUp(1)) {
					//	if (Input.GetKey("left shift") || Input.GetKey("right shift")) {
					//		GUIInspector.Inspected = cell;
					//	}
						//else {
							GUIInspector.Inspected = t;
						//}
					}
					else if (Input.GetMouseButtonUp(0) && cell.IsLegal()) {Targeter.Select(cell);} 
				}
			}
		}

		static Rect TokenRect (Rect cellRect, Cell cell, int n) {
			if (cell.TokenCount > 1) {
				float size = cellRect.width/2;
				Rect tokenRect = new Rect (cellRect.x, cellRect.y, size, size);
				if (n==1 || n==3) {tokenRect.x += size;}
				if (n==2 || n==3) {tokenRect.y += size;}
				return tokenRect;
			}
			else {return cellRect;}
		}
	}
}