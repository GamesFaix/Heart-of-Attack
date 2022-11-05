using UnityEngine;
using System.Collections;
//using FBI.Tokens;

public class TokenDisplay : MonoBehaviour {
	public Texture2D thumb;
	public GameObject spritePlane;
	
	
	
	public void CreateSprite(){
		Vector3 pos = gameObject.transform.position;
		spritePlane = Instantiate(ResourceLoader.spritePlanePF, pos, Quaternion.identity) as GameObject;
		spritePlane.transform.parent = gameObject.transform;
		spritePlane.name = "Sprite Plane";
	}
	
	public void SetSprite(Texture2D tex){
		spritePlane.renderer.material.SetTexture("_MainTex", tex);
	}
	
	public void Update(){
		PivotSprite();
	}
	
	void PivotSprite(){
		if (Camera.main){
			Vector3 camRot = Camera.main.transform.eulerAngles;
			transform.eulerAngles = new Vector3(camRot.x-90,camRot.y,camRot.z);
		}
	}
}
