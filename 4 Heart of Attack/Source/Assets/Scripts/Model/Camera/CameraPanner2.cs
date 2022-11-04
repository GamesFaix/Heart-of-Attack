/* Controls camera pan and zoom. */

using UnityEngine;
using System.Collections;
using HOA;

public class CameraPanner2 : MonoBehaviour {
	
	void OnGUI () {
		if (Input.GetKey("s") || Input.GetKey("w")) {Zoom();}
		if (Input.GetKey("a") || Input.GetKey("d")) {Rotate();}
		if (Input.GetKey("q") || Input.GetKey("z")) {Altitude();}	
	}
	
	static Vector3 endPos;
	void Update () {
		if (gameObject.transform.position != endPos) {Move();}
	}
	
	float zoomSpeed = 0.5f;
	float minDist = 25f;
	void Zoom(){
		Vector3 camPos = Camera.main.transform.position;
		Vector3 rigPos = gameObject.transform.position;
		
		float dist = Vector3.Distance (camPos, rigPos);
		
		Vector3 dir = camPos - rigPos;
		dir = dir.normalized;
		
		Vector3 change = zoomSpeed*dir;
		if (Input.GetKey("w")) {change *= -1;}
		
		camPos += change;
		if (dist > minDist) {Camera.main.transform.position = camPos;}
		else {Camera.main.transform.position = rigPos + dir*(minDist + 0.001f);}
	}
	
	float rotateSpeed = 0.5f;
	void Rotate () {
		Vector3 rot = gameObject.transform.eulerAngles;
		float change = rotateSpeed;
		if (Input.GetKey("d")) {change *= -1;}
		rot.y += change;
		gameObject.transform.eulerAngles = rot;
	}
	
	float altitudeSpeed = 1f;
	void Altitude () {
		float change = altitudeSpeed/5f;
		if (Input.GetKey("z")) {change *= -1;}
		
		gameObject.transform.Rotate(new Vector3 (change,0,0));
		
		gameObject.transform.eulerAngles = AltitudeClamp(gameObject.transform.eulerAngles);
	}
	Vector3 AltitudeClamp (Vector3 v) {
		if (v.x > 70) {v.x = 70;}
		if (v.x < 20) {v.x = 20;}
		return v;
	}
	
	float moveSpeed = 0.1f;
	void Move () {
		Vector3 currentPos = gameObject.transform.position;
		Vector3 direction = endPos - currentPos;
		Vector3 newPos = currentPos + (direction * moveSpeed);
		gameObject.transform.position = newPos;
	}
	
	public static Target Target {get; set;}
	public static void Focus (Target t, bool effect=true) {
		Target = t;
		if (t is Cell) {
			endPos = ((Cell)t).Location;
		}
		if (t is Token) {
			endPos = ((Token)t).Body.Cell.Location;
            if (effect) AVEffect.Highlight.Play(t);
		}
	}
}
