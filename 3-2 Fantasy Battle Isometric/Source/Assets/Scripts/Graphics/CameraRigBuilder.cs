using UnityEngine;
using System.Collections;

public class CameraRigBuilder : MonoBehaviour {

	void Awake () {
		GameObject boomPF = Resources.Load("Prefabs/CameraRig/CameraBoom") as GameObject;
		GameObject boom = GameObject.Instantiate(boomPF, gameObject.transform.position, Quaternion.identity) as GameObject;

		boom.transform.Rotate (new Vector3 (90,0,0));

		boom.transform.parent = gameObject.transform;


		GameObject cameraPF = Resources.Load("Prefabs/CameraRig/Main Camera") as GameObject;

		GameObject camera = GameObject.Instantiate(cameraPF, new Vector3(0,0,-130), Quaternion.identity) as GameObject; 

		camera.transform.Rotate (new Vector3(-15,0,0));

		camera.transform.parent = boom.transform;

		gameObject.transform.Rotate (new Vector3(30,45,0) );

		SkyboxControl sbc = camera.AddComponent("SkyboxControl") as SkyboxControl;
		sbc.SetTexture("space");
	}
}
