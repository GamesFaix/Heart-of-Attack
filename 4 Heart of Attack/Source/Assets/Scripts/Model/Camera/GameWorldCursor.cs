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
			Target Target = ClosestLegalTargetToCamera();
			if (Target != null) {
				Targeter.Select(Target);
				GUIMaster.PlaySound(EGUISound.BombingRangeET);
			}
		}

		void Inspect () {
			Target Target = null;
			if (Input.GetKey("left shift") || Input.GetKey("right shift")){
				Target = ClosestCellToCamera();
			}
			else {
				Target = ClosestTokenToCamera();
			}
			
			if (Target != null) {
				GUIInspector.Inspected = Target;
				GUIMaster.PlaySound(EGUISound.INSPECT);
			}
		}

		Target ClosestLegalTargetToCamera () {
			return ClosestTarget(TargetsFromCamToMouse() - TargetFilter.Legal);
		}

		Target ClosestCellToCamera () {
			return ClosestTarget(TargetsFromCamToMouse() - TargetFilter.Cell);
		}
		Target ClosestTokenToCamera () {
			return ClosestTarget(TargetsFromCamToMouse() - TargetFilter.Token);
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
			TargetGroup Targets = new TargetGroup();
			foreach (GameObject g in objects) {
				TargetDisplay display = null;
				if (g.GetComponent("CellDisplay")) {
					display = g.GetComponent("CellDisplay") as TargetDisplay;
					Cell c = (Cell)display.Parent;
					Targets.Add(c);
                    TargetGroup group = c.Occupants - TargetFilter.Plane(Plane.Sunken, true);
                    foreach (Token t in group)
                        Targets.Add(t);
				}
				if (g.GetComponent("TokenDisplay")) {
					display = g.GetComponent("TokenDisplay") as TargetDisplay;
					Targets.Add(display.Parent);
				}
			}
			return Targets;
		}

		Target ClosestTarget (TargetGroup Targets) {
			float shortestDist = 100000;
			Target closest = null;

			foreach (Target t in Targets) {
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