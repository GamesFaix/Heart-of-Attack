/* Controls camera pan and zoom. */

using UnityEngine;
using System.Collections;
using HOA;

public class CameraPanner : MonoBehaviour {
	static float zoomSpeed = 0.5f;
	static float zoomMin = 25f;
	static float rotateSpeed = 0.5f;
	static float pitchSpeed = 1f;
//	static float moveSpeed = 0.1f;
	static float panSpeed = 0.003f;
	static Transform trans;
	static Transform camTrans {get {return Camera.main.transform;} }

	static Vector3 forward {
		get {
			return new Vector3 (trans.position.x-camTrans.position.x, 0, trans.position.z-camTrans.position.z);
		}
	}
	static Vector3 backward {
		get {return new Vector3 (-forward.x, 0, -forward.z);}
	}
	static Vector3 left {
		get {return new Vector3 (-forward.z, 0, forward.x);}
	}
	static Vector3 right {
		get {return new Vector3 (forward.z, 0, -forward.x);}
	}


	void Start () {
		trans = gameObject.transform;
	}

	void OnGUI () {
		if (Input.GetKey("+") || Input.GetKey("=") || Input.GetKey("[+]")) {Zoom(1);}
		if (Input.GetKey("-") || Input.GetKey("_") || Input.GetKey("[-]")) {Zoom(-1);}

		if (Input.GetKey("left")) {Rotate(-1);}
		if (Input.GetKey("right")) {Rotate(1);}

		if (Input.GetKey("up")) {Pitch(1);}
		if (Input.GetKey("down")) {Pitch(-1);}	

		if (Input.GetKey("w")) {Pan(new Int2(0,1));}
		if (Input.GetKey("s")) {Pan(new Int2(0,-1));}
		if (Input.GetKey("a")) {Pan(new Int2(-1,0));}
		if (Input.GetKey("d")) {Pan(new Int2(1,0));}
	}

//	static Vector3 endPos;
	/*void Update () {
		if (trans.position != endPos) {Move();}
	}*/

	void Pan (Int2 dir) {
		Vector3 oldPos = trans.position;
		Vector3 newPos = new Vector3 (oldPos.x, oldPos.y, oldPos.z);

		if (dir.x < 0) {
			newPos = oldPos + (panSpeed*left);
//			newPos.x=oldPos.x-panSpeed;
//			newPos.z=oldPos.z+panSpeed;
		}
		if (dir.x > 0) {
			newPos = oldPos + (panSpeed*right);
//			newPos.x = oldPos.x+panSpeed;
//			newPos.z = oldPos.z-panSpeed;
		}
		if (dir.y < 0) {
			newPos = oldPos + (panSpeed*backward);
//			newPos.x = oldPos.x-panSpeed;
//			newPos.z = oldPos.z-panSpeed;
		}
		if (dir.y > 0) {			
			newPos = oldPos + (panSpeed*forward);
//			newPos.x = oldPos.x+panSpeed;
//			newPos.z = oldPos.z+panSpeed;
		}

		if (newPos.x < Game.Board.Cell(1,1).Location.x) {newPos.x = Game.Board.Cell(1,1).Location.x;}
		if (newPos.z < Game.Board.Cell(1,1).Location.z) {newPos.z = Game.Board.Cell(1,1).Location.z;}
		Int2 last = Game.Board.CellCount-1;
		if (newPos.x > Game.Board.Cell(last).Location.x) {newPos.x = Game.Board.Cell(last).Location.x;}
		if (newPos.z > Game.Board.Cell(last).Location.z) {newPos.z = Game.Board.Cell(last).Location.z;}

		trans.position = newPos;
	}

	void Zoom(int n){
		Vector3 camPos = Camera.main.transform.position;
		Vector3 rigPos = trans.position;

		float dist = Vector3.Distance (camPos, rigPos);

		Vector3 dir = camPos - rigPos;
		dir = dir.normalized;

		Vector3 change = zoomSpeed*dir;
		if (n > 0) {change *= -1;}

		camPos += change;
		if (dist > zoomMin) {Camera.main.transform.position = camPos;}
		else {Camera.main.transform.position = rigPos + dir*(zoomMin + 0.001f);}
	}

	public static void Rotate (int n) {
		Vector3 rot = trans.eulerAngles;
		float change = rotateSpeed;
		if (n > 0) {change *= -1;}
		rot.y += change;
		trans.eulerAngles = rot;
	}

	public static void Pitch (int n) {
		float change = pitchSpeed/5f;
		if (n < 0) {change *= -1;}

		trans.Rotate(new Vector3 (change,0,0));

		trans.eulerAngles = AltitudeClamp(trans.eulerAngles);
	}
	static Vector3 AltitudeClamp (Vector3 v) {
		if (v.x > 70) {v.x = 70;}
		if (v.x < 20) {v.x = 20;}
		return v;
	}

	/*
	void Move () {
		Vector3 currentPos = trans.position;
		Vector3 direction = endPos - currentPos;
		Vector3 newPos = currentPos + (direction * moveSpeed);
		trans.position = newPos;
	}
*/
	/*public static Target Target {get; set;}
	public static void Focus (Target t, bool effect=true) {
		Target = t;
		if (t is Cell) {
			endPos = ((Cell)t).Location;
		}
		if (t is Token) {
			endPos = ((Token)t).Body.Cell.Location;
			if (effect) {((Token)t).Display.Effect(EEffect.SHOW);}
		}
	}
	*/
}
