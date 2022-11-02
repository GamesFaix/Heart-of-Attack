using UnityEngine;

public class GUIMaster : MonoBehaviour {

	public static GUIGame game;
	public static GUILobby lobby;

	void Awake() {
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
