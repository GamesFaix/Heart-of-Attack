using UnityEngine;

namespace HOA {


	public class GameWorldCursor : MonoBehaviour {
		Ray rayFromCam;
		RaycastHit[] hitCells;
		RaycastHit[] hitTokens;
		
		void OnGUI(){
			
			if (Input.GetMouseButtonUp(0)){
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				//Debug.Log(ray);
				//Debug.DrawRay(ray.origin, ray.direction*200, Color.red);
				
				RaycastHit[] hits = Physics.RaycastAll(ray, 500);
				foreach (RaycastHit hit in hits){
					GameObject go = hit.collider.gameObject;
					
					if (go.GetComponent("CellDisplay")){	
						CellDisplay cd = go.GetComponent("CellDisplay") as CellDisplay;
						//Debug.Log(cd.Cell);
						if (cd.Cell.IsLegal()) {
							Targeter.Select(cd.Cell);
							GUIMaster.PlaySound(EGUISound.TARGET);
						}
					}
					if (go.GetComponent("TokenDisplay")){	
						TokenDisplay td = go.GetComponent("TokenDisplay") as TokenDisplay;
						//Debug.Log(cd.Cell);
						if (td.Token.IsLegal()) {
							Targeter.Select(td.Token);
							GUIMaster.PlaySound(EGUISound.TARGET);
						}
					}
				}
			}
			if (Input.GetMouseButtonUp(1)) { 
				if (Input.GetKey("left shift") || Input.GetKey("right shift")){
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					//Debug.Log(ray);
					//Debug.DrawRay(ray.origin, ray.direction*200, Color.red);
					
					RaycastHit[] hits = Physics.RaycastAll(ray, 500);
					foreach (RaycastHit hit in hits){
						GameObject cellGO = hit.collider.gameObject;
						
						if (cellGO.GetComponent("CellDisplay")){	
							CellDisplay cd = cellGO.GetComponent("CellDisplay") as CellDisplay;
							//Debug.Log(cd.Cell);
							GUIInspector.Inspected = cd.Cell;
							GUIMaster.PlaySound(EGUISound.INSPECT);
						}
					}
				}
				else {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					//Debug.Log(ray);
					//Debug.DrawRay(ray.origin, ray.direction*200, Color.red);
					
					RaycastHit[] hits = Physics.RaycastAll(ray, 500);
					foreach (RaycastHit hit in hits){
						GameObject go = hit.collider.gameObject;
						
						if (go.GetComponent("TokenDisplay")){	
							TokenDisplay td = go.GetComponent("TokenDisplay") as TokenDisplay;
							//Debug.Log(cd.Cell);
							GUIInspector.Inspected = td.Token;
							GUIMaster.PlaySound(EGUISound.INSPECT);
						}
						if (go.GetComponent("CellDisplay")){	
							CellDisplay cd = go.GetComponent("CellDisplay") as CellDisplay;
							Token t;
							if (cd.Cell.Contains(EPlane.SUNK, out t)) {
								GUIInspector.Inspected = t;
								GUIMaster.PlaySound(EGUISound.INSPECT);
							}
						}
					}

				}
			}
		
		}

	}

}