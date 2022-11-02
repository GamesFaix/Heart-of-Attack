using UnityEngine;
using HOA.Map;
using HOA.Tokens;

public class GUIBoard : MonoBehaviour {
	static float scale = 100;
	static float minScale;
	static float maxScale;
	
	Vector2 scrollPos = new Vector2 (0,0);
	
	public static void ZoomOut () {zoomOut = true;}
	
	static bool zoomOut = false;
	
	public void Display(Panel p){
		GUI.Box(p.FullBox,"");
		p.x2 += 5;
		
		GUI.Label(p.Box(0.2f), "BOARD");
		
		float minDimension = Mathf.Min(p.W, p.H-p.LineH);
		minScale = minDimension/Board.Size;
		maxScale = minDimension/3;
		
		p.x2 += p.W*0.2f;
		GUI.Label(p.Box(50), "Zoom - ");
		p.y2 += 7;
		scale = GUI.HorizontalSlider(p.Box(0.25f), scale, minScale, maxScale);
		p.y2 -= 7;
		GUI.Label(p.Box(20), " + ");
		
		p.NextLine();
		
		float internalSize = Board.Size*scale;
		float externalH = (p.H-p.LineH) / p.H;
		
		scrollPos = GUI.BeginScrollView(p.TallBox(externalH), scrollPos, new Rect(p.X, p.y2, internalSize, internalSize));
		
		if (Board.ready){
			
			Rect board = BoardRect(new Panel(p.TallBox(externalH), p.LineH, p.s));
			GUI.Box(board, "");
			
			for (int x=1; x<=Board.Size; x++) {
				for (int y=1; y<=Board.Size; y++) {
					Cell cell = Board.Cell(x,y);
					Rect cellRect = CellRect(board,x,y);
					
					if (cell.IsEmpty()) {
						if (GUI.Button(cellRect, cell.ToString()) && 
						   Input.GetMouseButtonUp(0) && GUISelectors.WaitingForCell()){
							GUISelectors.Cell = cell;	
						}
					}
					else {DrawOccupants(cellRect, cell, p.s);}
					
					if (cell.Legal) {
						Color c = GUI.color;
						GUI.color = new Color (1,1,1,0.25f);
						GUI.Box(cellRect, ImageLoader.yellowBtn, p.s);
						GUI.color = c;
					}
				}
			}
		}
		GUI.EndScrollView();
		
		if (zoomOut) {
			scale = minScale;
			zoomOut = false;			
		}
	}
	
	Rect BoardRect (Panel p) {
		float boardSize = Board.Size*scale;
		float boardX = p.X;
		float boardY = p.Y;
		return new Rect(boardX, boardY, boardSize, boardSize);
	}
	
	Rect CellRect (Rect board, int x, int y) {
		float size = board.width/Board.Size;
		
		float x2 = board.x + (x-1)*size;
		float y2 = board.y + (y-1)*size;
		
		return new Rect(x2,y2,size,size);
	}
						
	Rect TokenRect (Rect cellRect, Cell cell, int n) {
		if (cell.TokenCount > 1) {
			float size = cellRect.width/2;
			Rect tokenRect = new Rect (cellRect.x, cellRect.y, size, size);
			if (n==1 || n==3) {tokenRect.x += size;}
			if (n==2 || n==3) {tokenRect.y += size;}
			return tokenRect;
		}
		else {return cellRect;}
	}
	
	string Coordinates (int x, int y) {
		return "("+x+","+y+")";
	}

	void DrawOccupants (Rect cellRect, Cell cell, GUIStyle s) {
		GUI.Box(cellRect, cell.ToString());
		Rect tokenRect;
			
		TokenGroup tg = cell.Occupants;
		
		for (int i=0; i<4; i++) {
			if (i < tg.Count) {
				Token t = tg[i];
				if (t != default(Token)) {
					tokenRect = TokenRect(cellRect, cell ,i);
					t.Draw(tokenRect);
					if (GUI.Button(tokenRect, "", s)){
						if (Input.GetMouseButtonUp(0) && GUISelectors.WaitingForCell()) {
							GUISelectors.Cell = cell;
						}
						else if (Input.GetMouseButtonUp(0) && GUISelectors.WaitingForInstance()) {
							GUISelectors.Instance = t;
						}
						else if (Input.GetMouseButtonUp(1)) {
							GUIInspector.Inspect(t);
						}
					}
				}
			}
			else {
				tokenRect = TokenRect(cellRect, cell, i);
				if (GUI.Button(tokenRect, "", s)
					&& Input.GetKey("left shift")) {
						GUISelectors.Cell = cell;
				}
			}
		}
	}
}

