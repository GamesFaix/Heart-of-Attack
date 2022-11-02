using UnityEngine;

public class GUIMaster : MonoBehaviour {

	public static GUIGame game;
	public static GUILobby lobby;
	static GUIStyle s;
	public static GUIStyle S {get {return s;} }

	void Awake() {
		s = new GUIStyle();
		s.normal.textColor = Color.white;
		s.fontSize = 20;

		game = gameObject.AddComponent("GUIGame") as GUIGame;
		lobby = gameObject.AddComponent("GUILobby") as GUILobby;
	}

	static bool showGame = false;
	static bool showLobby = true;
	
	public static void Toggle () {
		showGame = !showGame;
		showLobby = !showLobby;
	}
	
	void OnGUI() {
		if (showGame) {game.Display();}
		if (showLobby) {lobby.Display();}
	}
}
