using UnityEngine;
using HOA;

public class Core : MonoBehaviour {
	//public static NetworkConsole nc;

	public GameObject guiPrefab;
	public GameObject mixerPrefab;

	public static GUISelectors guiSelectors;
	static AudioSource music;
	
	void Start () {
		Setup();
		DebugShortcut();
	
		GUILog.ScrollToBottom();
	}

	void Setup () {
		//nc = gameObject.AddComponent("NetworkConsole") as NetworkConsole;
	
		Instantiate(guiPrefab, new Vector3(0,0,0),Quaternion.identity);
		Instantiate(mixerPrefab, new Vector3(0,0,0), Quaternion.identity);

		ImageLoader.Load();
		SoundLoader.Load();
		TemplateFactory.MakeTemplates();
		guiSelectors = gameObject.AddComponent("GUISelectors") as GUISelectors;
		gameObject.AddComponent("EffectQueue");
		gameObject.AddComponent("GameWorldCursor");
		SetupMusic();
	}

	void SetupMusic () {
		music = gameObject.GetComponent("AudioSource") as AudioSource;


	}

	public static AudioSource Music {
		get {return music;}
	}

	
	void DebugShortcut () {
		for (int i=0; i<8; i++) {
			Roster.Add(new Player(i));
		}
		Roster.ForceRandomFactions();
		Game.Start(6);
		
		
	}
}
