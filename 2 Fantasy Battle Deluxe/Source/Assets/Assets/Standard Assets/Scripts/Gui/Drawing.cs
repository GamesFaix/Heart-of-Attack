/* Useful tools for drawing GUI stuff. */

using UnityEngine;
using System.Collections;
//using FBI.Gui.Game;

namespace FBI.Gui{
	
	public class Drawing {
	
		public static float ScreenRatio(){
			return Screen.width/Screen.height;	
		}
		
		public static Rect FullScreenRect(){
			return new Rect(0,0,Screen.width,Screen.height);	
		}
		
		public static Rect RectDouble(double x1, double y1, double x2, double y2){
			return new Rect((float)x1, (float)y1, (float)x2, (float)y2);
		}
		
		public static Rect RectFromScreenSize(double x1, double y1, double x2, double y2){
			return new Rect((float)(Screen.width*x1), 
							(float)(Screen.height*y1), 
							(float)(Screen.width*x2), 
							(float)(Screen.height*y2));
		}
		
		public static Rect RectAddMargin(Rect rec, short mar){
			return new Rect(rec.x+mar, 
							rec.y+mar, 
							rec.width-(2*mar), 
							rec.height-(2*mar));	
		}
		
		public static Vector3 WorldToScreenPoint(Vector3 world){
			Vector3 cam = Camera.main.WorldToViewportPoint(world);
			Vector3 screen = Camera.main.ViewportToScreenPoint(cam);
			return screen;
		}

	}
}