using UnityEngine;
using System.Collections;

public static class FancyText {
	
	static float slur = 0.5f;
	
	public static void Highlight (Rect rect, string text, GUIStyle s, Color[] colors) {
		Rect newRect = new Rect(rect.x-slur, rect.y-slur, rect.width, rect.height);
		Color oldColor = s.normal.textColor;
		
		s.normal.textColor = colors[1];
		
		GUI.Label(newRect, text, s);
		newRect.y += 2*slur;
		GUI.Label(newRect, text, s);
		newRect.x += 3*slur;
		GUI.Label(newRect, text, s);
		newRect.y -= 2*slur;
		GUI.Label(newRect, text, s);
		
		s.normal.textColor = colors[0];
		GUI.Label(rect, text, s);
		
		s.normal.textColor = oldColor;
		
	}

	
}
