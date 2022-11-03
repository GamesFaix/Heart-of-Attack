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
			
			box = BoardRect(new Panel(p.TallBox(externalH), p.LineH, p.s));
			box.x += center;

			center = (p.W-box.width)/2;

			//GUI.Box(box, "");

			//foreach (Cell c in Board.Cells) {c.Sprite.Draw();}
			//foreach (Token t in TokenFactory.Tokens) {t.Draw();}

		}
		GUI.EndScrollView();
		
		if (zoomOut) {
			scale = minScale;
			zoomOut = false;			
		}
	}

	static Rect box;

	public static Rect Box {get {return box;} }

	Rect BoardRect (Panel p) {
		float boardSize = Board.Size*scale;
		float boardX = p.X;
		float boardY = p.Y;
		return new Rect(boardX, boardY, boardSize, boardSize);
	}
}

