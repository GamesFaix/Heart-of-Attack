using UnityEngine;
using System.Collections;
using FBI.Tokens;

public class ResourceLoader : MonoBehaviour {

	public static GameObject spritePlanePF;
	
	void Start () {
		
		TokenFactory.Start();
		spritePlanePF = Resources.Load("Prefabs/SpritePlanePF") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
