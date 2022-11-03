using UnityEngine;

namespace HOA {

	public class GameWorldCursor : MonoBehaviour {
		Ray rayFromCam;
		RaycastHit[] hitCells;
		RaycastHit[] hitTokens;
		
		void OnGUI(){

			if (Input.GetMouseButtonUp(0)){
				ITarget target = ClosestLegalTargetToCamera();
				if (target != null) {
					Targeter.Select(target);
					GUIMaster.PlaySound(EGUISound.TARGET);
				}
			}

			if (Input.GetMouseButtonUp(1)) { 
				ITarget target = null;
				if (Input.GetKey("left shift") || Input.GetKey("right shift")){
					target = ClosestCellToCamera();
				}
				else {target = ClosestTokenToCamera();}

				if (target != null) {
					GUIInspector.Inspected = target;
					GUIMaster.PlaySound(EGUISound.INSPECT);
				}
			}
		}

		ITarget ClosestLegalTargetToCamera () {
			return ClosestTarget(TargetsFromCamToMouse().Legal());
		}

		ITarget ClosestCellToCamera () {
			return ClosestTarget(TargetsFromCamToMouse().Cells());
		}
		ITarget ClosestTokenToCamera () {
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
				ITargetDisplay display = null;
				if (g.GetComponent("CellDisplay")) {
					display = g.GetComponent("CellDisplay") as ITargetDisplay;
				}
				if (g.GetComponent("TokenDisplay")) {
					display = g.GetComponent("TokenDisplay") as ITargetDisplay;
				}
				if (display != null) {targets.Add(display.Target());}
			}
			return targets;
		}

		ITarget ClosestTarget (TargetGroup targets) {
			float shortestDist = 100000;
			ITarget closest = null;

			foreach (ITarget t in targets) {
				GameObject go = t.TargetDisplay().GO();

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