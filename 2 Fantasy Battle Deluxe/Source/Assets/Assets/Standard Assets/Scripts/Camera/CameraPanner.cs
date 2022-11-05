/* Controls camera pan and zoom. */

using UnityEngine;
using System.Collections;

public class CameraPanner : MonoBehaviour {

	public float panSpeed = 0.25f;
	Transform myTransform;
	
	void Start() {
		myTransform = gameObject.transform;
	}

	void OnGUI () {
		Move();
		Zoom();
	
	}
	
	void Move(){
		Vector3 pos = myTransform.position;
		
		if (Input.GetKey("w")) {
			pos.x += panSpeed;
			pos.z += panSpeed;
		}
		if (Input.GetKey("s")) {
			pos.x -= panSpeed;
			pos.z -= panSpeed;
		}
		if (Input.GetKey("a")) {
			pos.x -= panSpeed;
			pos.z += panSpeed;
		}
		if (Input.GetKey("d")) {
			pos.x += panSpeed;
			pos.z -= panSpeed;
		}
		
		myTransform.position = pos;
	}
	
	float fovMin = 30;
	float fovMax = 70;
	public float zoomSpeed = 0.5f;
	
	void Zoom(){
		float fov = Camera.main.fieldOfView;
		Vector3 pos = myTransform.position;
		
		if (Input.GetKey("q")){
			if (fov > fovMin) {
				Camera.main.fieldOfView -= zoomSpeed;
				pos.y -= zoomSpeed/10;
				myTransform.position = pos;
			}
		}
		if (Input.GetKey("z")){
			if (fov < fovMax) {
				Camera.main.fieldOfView += zoomSpeed;
				pos.y += zoomSpeed/10;
				myTransform.position = pos;
			}
		}
	}
}
