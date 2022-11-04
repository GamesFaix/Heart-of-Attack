using UnityEngine;
using HOA;
using HOA.Sounds;
using System;

using HOA.Textures;
public class Core : MonoBehaviour {
	//public static NetworkConsole nc;

	public GameObject guiPrefab;
	public GameObject mixerPrefab;

    public static AudioSource Music { get; private set; }
	
	void Start () {
        //nc = gameObject.AddComponent("NetworkConsole") as NetworkConsole;

        Instantiate(guiPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(mixerPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        LoadPublish();

        HOA.Sounds.GUI.Load();
        
        Map.LoadMaps();

        Roster.OnGameStart();
        Token.OnGameStart();
        TokenRegistry.OnGameStart();
        GUIInspector.Load();

        gameObject.AddComponent("EffectQueue");
        gameObject.AddComponent("GameWorldCursor");
        Music = gameObject.GetComponent("AudioSource") as AudioSource;
        
        GUILog.ScrollToBottom();

        DebugShortcut();
	}
	
	void DebugShortcut () {
		for (int i=0; i<8; i++) {
			Roster.Add(new Player(i));
		}
		Roster.ForceRandomFactions();
		Game.Map = Map.Maps[1];
		Game.Start();
		
		
	}

    public static event EventHandler<LoadEventArgs> Load;

    public static void LoadPublish()
    {
        if (Load != null) 
        { 
            Load(null, new LoadEventArgs());
            Debug.Log("Unfinished code: Core.Load sender null.");
        }

	}

    
}


public class LoadEventArgs : EventArgs
{
    public LoadEventArgs()
    {
    }
}