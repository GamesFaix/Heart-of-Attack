using UnityEngine;
using HOA;

public enum EGUISound {CLICK, INSPECT, TARGET}

public class GUIMaster : MonoBehaviour {

	public static GUIGame game;
	public static GUILobby lobby;
	static GUIStyle s;
	public static GUIStyle S {get {return s;} }
	static AudioSource sound;

	void Awake() {
		s = new GUIStyle();
		s.normal.textColor = Color.white;
		s.fontSize = 20;

		game = gameObject.AddComponent("GUIGame") as GUIGame;
		lobby = gameObject.AddComponent("GUILobby") as GUILobby;
		sound = gameObject.GetComponent("AudioSource") as AudioSource;
	}


	public static void PlaySound (EGUISound s) {
		if (s == EGUISound.CLICK) {sound.clip = SoundLoader.click;}
		if (s == EGUISound.INSPECT) {sound.clip = SoundLoader.inspect;}
		if (s == EGUISound.TARGET) {sound.clip = SoundLoader.target;}
		sound.Play();
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
