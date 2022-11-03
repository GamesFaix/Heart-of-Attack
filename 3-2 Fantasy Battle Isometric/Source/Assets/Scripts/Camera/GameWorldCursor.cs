using UnityEngine;

namespace HOA {

	public class GameWorldCursor : MonoBehaviour {
		Ray rayFromCam;
		RaycastHit[] hitCells;
		RaycastHit[] hitTokens;

		public static Rect gameWindow = new Rect(0,0,0,0);

		static Vector2 mouseStart;

		void Update(){
			if (gameWindow.Contains(Input.mousePosition)) {
				Vector2 mouseCurrent = Input.mousePosition;

				if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
					mouseStart = mouseCurrent;
				}

				else if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
					if (mouseCurrent.x < mouseStart.x-50) {CameraPanner.Rotate(-1);}
					if (mouseCurrent.x > mouseStart.x+50) {CameraPanner.Rotate(1);}
					if (mouseCurrent.y < mouseStart.y-50) {CameraPanner.Pitch(-1);}
					if (mouseCurrent.y > mouseStart.y+50) {CameraPanner.Pitch(1);}

				}

				if (mouseCurrent == mouseStart) {
					if (Input.GetMouseButtonUp(0)){Select();}
					if (Input.GetMouseButtonUp(1)) {Inspect();}
				}
			}
		}

		void Select () {
			Target target = ClosestLegalTargetToCamera();
			if (target != null) {
				Targeter.Select(target);
				GUIMaster.PlaySound(EGUISound.TARGET);
			}
		}

		void Inspect () {
			Target target = null;
			if (Input.GetKey("left shift") || Input.GetKey("right shift")){
				target = ClosestCellToCamera();
			}
			else {
				target = ClosestTokenToCamera();
			}
			
			if (target != null) {
				GUIInspector.Inspected = target;
				GUIMaster.PlaySound(EGUISound.INSPECT);
			}
		}

		Target ClosestLegalTargetToCamera () {
			return ClosestTarget(TargetsFromCamToMouse().Legal());
		}

		Target ClosestCellToCamera () {
			return ClosestTarget(TargetsFromCamToMouse().Cells());
		}
		Target ClosestTokenToCamera () {
			return ClosestTarget(TargetsFromCamToMouse().Tokens());
		}

		TargetGroup TargetsFromCamToMouse () {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit[] hits = Physics.RaycastAll(ray, 500);
			GameObject[] objects = HitObjects(hits);
			return TargetableGameObjects(objects);
		}

		GameObject HitObject (RaycastHit hit) {return hit.collider.gameObject;}
		GameObject[] HitObjects (RaycastHit[] hits) {
			GameObject[] objects = new GameObject[hits.Length];
			for (int i=0; i<hits.Length; i++){
				objects[i] = HitObject(hits[i]);
			}
			return objects;
		}
		
		TargetGroup TargetableGameObjects (GameObject[] objects) {
			TargetGroup targets = new TargetGroup();
			foreach (GameObject g in objects) {
				TargetDisplay display = null;
				if (g.GetComponent("CellDisplay")) {
					display = g.GetComponent("CellDisplay") as TargetDisplay;
					Cell c = (Cell)display.Parent;
					targets.Add(c);
					Token sunk;
					if (c.Contains(EPlane.SUNK, out sunk)) {targets.Add(sunk);}
				}
				if (g.GetComponent("TokenDisplay")) {
					display = g.GetComponent("TokenDisplay") as TargetDisplay;
					targets.Add(display.Parent);
				}
			}
			return targets;
		}

		Target ClosestTarget (TargetGroup targets) {
			float shortestDist = 100000;
			Target closest = null;

			foreach (Target t in targets) {
				GameObject go = t.Display.GO;

				float dist = Vector3.Distance(go.transform.position, Camera.main.transform.position);
				if (dist < shortestDist) {
					shortestDist = dist;
					closest = t;
				}
			}
			return closest;
		}


	}
}