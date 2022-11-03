/* Controls camera pan and zoom. */

using UnityEngine;
using System.Collections;
using HOA;

public class CameraPanner : MonoBehaviour {
	
	public float panSpeed = 0.25f;
	Transform myTransform;
	
	void Start() {
		myTransform = gameObject.transform;
	}
	
	void OnGUI () {
		Zoom();
		Rotate();
		Altitude();	
	}

	public float zoomSpeed = 0.5f;
	float minDistance = 25f;

	void Zoom(){
		float distance = Vector3.Distance (Camera.main.transform.position, gameObject.transform.position);

		Vector3 direction = Camera.main.transform.position - gameObject.transform.position;
		direction = direction.normalized;



		Vector3 change = zoomSpeed*direction;
		if (Input.GetKey("w")) {change *= -1;}

		if (Input.GetKey("s") || Input.GetKey("w")){
			Vector3 pos = Camera.main.transform.position;
			pos += change;
			if (distance > minDistance) {
				Camera.main.transform.position = pos;
			}
			else {
				Camera.main.transform.position = gameObject.transform.position + direction*(minDistance + 0.001f);
			}
		}
	}

	Vector3 ClampCamPos (Vector3 v3) {
		v3.x = Mathf.Min(0, v3.x);
		v3.z = Mathf.Min(0, v3.z);
		return v3;
	}

	float rotateSpeed = 0.5f;

	void Rotate () {
		if (Input.GetKey("a") || Input.GetKey("d")) {
			Vector3 rot = gameObject.transform.eulerAngles;
			float amount = rotateSpeed;
			if (Input.GetKey("d")) {amount *= -1;}
			rot.y += amount;
			gameObject.transform.eulerAngles = rot;
		}

	}

	float altitudeSpeed = 1f;
	void Altitude () {
		if (Input.GetKey("q") || Input.GetKey("z")) {
			Vector3 rot = gameObject.transform.eulerAngles;
			float amount = (float)altitudeSpeed/5;
			if (Input.GetKey("z")) {amount *= -1;}

			gameObject.transform.Rotate(new Vector3 (amount,0,0));

			Vector3 clamped = gameObject.transform.eulerAngles;

			if (clamped.x > 70) {clamped.x = 70;}
			if (clamped.x < 20) {clamped.x = 20;}
			gameObject.transform.eulerAngles = clamped;
		}

	}


	public static void Focus (ITargetable t) {
		GameObject myObject = Camera.main.transform.parent.transform.parent.gameObject;

		if (t is Cell) {
			endPos = ((Cell)t).Prefab.transform.position;
		}
		if (t is Token) {
			endPos = ((Token)t).Cell.Prefab.transform.position;
			((Token)t).SpriteEffect(EEffect.SHOW);
		}
	}

	static Vector3 endPos;
	static float moveSpeed = 0.1f;

	void Update () {
		Vector3 currentPos = gameObject.transform.position;
		Vector3 direction = endPos - currentPos;
		Vector3 newPos = currentPos + (direction * moveSpeed);
		gameObject.transform.position = newPos;
	}
}
