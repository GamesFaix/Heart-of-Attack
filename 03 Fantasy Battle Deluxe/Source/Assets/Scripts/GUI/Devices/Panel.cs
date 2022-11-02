using UnityEngine;

public class Panel {
	float x;
	float y;
	float w;
	float h;
	float dy;
	public float x2;
	public float y2;
	public GUIStyle s;

	public Panel (Rect rect, float lineH, GUIStyle style){
		x = rect.x;
		y = rect.y;
		w = rect.width;
		h = rect.height;
		dy = lineH;
		x2 = x;
		y2 = y;
		s = style;
	}
	
	public float X {get {return x;} }
	public float Y {get {return y;} }
	public float H {get {return h;} }
	public float W {get {return w;} }
	public float LineH {get {return dy;} }
	public void ResetY() {y2 = y;}
	public void ResetX() {x2 = x;}

	public void NextLine () {y2 += dy; x2 = x;}
	public void PrevLine () {y2 -= dy; x2 = x;}
	
	public int Lines {get {return (int)Mathf.Ceil(h/dy)-1;} }
	
	public Rect FullBox {get {return new Rect (x,y,w,h);} }
	
	public Rect TallBox (float n) {
		Rect rect;
		if (n <= 1) {rect = new Rect(x,y2,w,n*h);}
		else {rect = new Rect(x,y2,w,dy*n);}
		
		y2 += rect.height;
		x2 = x;
		return rect;		
	}
	
	public Rect LineBox {
		get {
			Rect rect = new Rect(x,y2,w,dy);
			y2 += dy;
			x2 = x;
			return rect;
		}
	}
	
	public Rect Box (float n) {
		Rect rect;
		if (n <= 1) {rect = new Rect(x2,y2,w*n,dy);}
		else {rect = new Rect(x2,y2,n,dy);}
			
		x2 += rect.width;
		return rect;
	}
	
	public Rect ScrollBox {get {return new Rect(x+w-15, y, 30, h);} }
	
}
