using UnityEngine;

public class GUILobby : MonoBehaviour {
	
	GUIStyle s;
	GUILobbyGame game;
	//GUILobbyNetwork network;
	
	
	void Awake () {
		s = new GUIStyle();
		s.normal.textColor = Color.white;
		s.fontSize = 20;
		game = gameObject.AddComponent("GUILobbyGame") as GUILobbyGame;
		//network = gameObject.AddComponent("GUILobbyNetwork") as GUILobbyNetwork;
	}
	
	public void Display() {
		float sH = Screen.height;
		float sW = Screen.width;
		
		float panelH = 600;
		float panelW = 400;
		float x1 = (sW-(panelW*2))/3;
		//float x2 = x1*2+panelW;
		float y1 = (sH-panelH)/2;
		
		
		
		Panel gamePanel = new Panel (new Rect(x1,y1,panelW,panelH), 30, s);
		game.Display(gamePanel);
		
		//Panel networkPanel = new Panel (new Rect(x2,y1,panelW,panelH), 30, s);
		//network.Display(networkPanel);
		
	}
	
}
