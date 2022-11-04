using UnityEngine;
using HOA.Textures;

namespace HOA {

	public class GUILobby : MonoBehaviour {
		
		GUIStyle s;

		void Awake () {
			s = new GUIStyle();
			s.normal.textColor = Color.white;
			s.fontSize = 20;
		}
		
		public void Display() {
			float sH = Screen.height;
			float sW = Screen.width;

			GUI.DrawTexture(new Rect(0,0,sW,sH), Backgrounds.WoodLarge, ScaleMode.StretchToFill);


			float panelH = 600;
			float panelW = 400;
			float x1 = (sW-(panelW*2))/3;
			float x2 = x1*2+panelW;
			float y1 = (sH-panelH)/2;
			
			
			
			Panel playerPanel = new Panel (new Rect(x1,y1,panelW,panelH), 30, s);
			GUILobbyPlayers.Display(playerPanel);
			
			Panel mapPanel = new Panel (new Rect(x2,y1,panelW,panelH), 30, s);
			GUILobbyMap.Display(mapPanel);
			
		}
		
	}
}