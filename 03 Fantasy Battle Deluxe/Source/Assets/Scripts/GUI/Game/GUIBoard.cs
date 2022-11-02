using UnityEngine;
using HOA;

public class GUIBoard : MonoBehaviour {
	static float scale = 100;
	static float minScale;
	static float maxScale;
	
	Vector2 scrollPos = new Vector2 (0,0);


	public static void ZoomOut () {zoomOut = true;}
	
	static bool zoomOut = false;
	
	public void Display(Panel p){
		p.x2 += 5;
		
		//GUI.Label(p.Box(0.2f), "BOARD");
		
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
		float center = 0;
		scrollPos = GUI.BeginScrollView(p.TallBox(externalH), scrollPos, new Rect(p.X+center, p.y2, internalSize, internalSize));
		
		if (Board.ready){
			
			Rect board = BoardRect(new Panel(p.TallBox(externalH), p.LineH, p.s));
			board.x += center;

			center = (p.W-board.width)/2;

			GUI.Box(board, "");
			
			for (int x=1; x<=Board.Size; x++) {
				for (int y=1; y<=Board.Size; y++) {
					Cell cell = Board.Cell(x,y);
					Rect cellRect = CellRect(board,x,y);

					GUICell.Draw(cell, cellRect);


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
}

