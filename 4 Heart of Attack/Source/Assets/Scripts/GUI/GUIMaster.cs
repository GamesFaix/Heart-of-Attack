using UnityEngine;
using HOA;
using HOA.Sounds;

public enum GUISounds {Click, Inspect, Target}

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


	public static void PlaySound (GUISounds s) {
		if (s == GUISounds.Click) 
            sound.clip = HOA.Sounds.GUI.Click;
		if (s == GUISounds.Inspect) 
            sound.clip = HOA.Sounds.GUI.Inspect;
		if (s == GUISounds.Target) 
            sound.clip = HOA.Sounds.GUI.Target;
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
