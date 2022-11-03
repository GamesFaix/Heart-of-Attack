using UnityEngine;

namespace HOA {
	public class CellDisplay : MonoBehaviour {

		Cell cell;

		public Cell Cell {
			get {return cell;}
			set {cell = value;}
		}

		Texture2D normalTex;

		public Texture2D NormalTex {
			get {return normalTex;}
			set {
				normalTex = value;
				gameObject.renderer.material.SetTexture("_MainTex", normalTex);
			}
		}

		public void SetLegal (bool legal) {
			if (legal) {ShowLegal();}
			else {HideLegal();}
		}

		public void EnterSunken (Token t) {
			ShowSprite();
			spritePrefab.renderer.material.SetTexture("_MainTex", t.Thumb);
		}
		public void ExitSunken () {HideSprite();}

		void ShowLegal () {legalPrefab.renderer.enabled = true;}
		void HideLegal () {legalPrefab.renderer.enabled = false;}

		void ShowSprite () {spritePrefab.renderer.enabled = true;}
		void HideSprite () {spritePrefab.renderer.enabled = false;}

		GameObject spritePrefab;
		GameObject legalPrefab;

		void Awake () {
			Vector3 pos = gameObject.transform.position;

			GameObject spritePF = Resources.Load("Prefabs/SpritePrefab") as GameObject;
			spritePrefab = GameObject.Instantiate(spritePF, pos, Quaternion.identity) as GameObject;
			spritePrefab.transform.parent = gameObject.transform;
			spritePrefab.transform.position = new Vector3 (pos.x,0.01f,pos.z);
			HideSprite();

			GameObject effectPF = Resources.Load("Prefabs/EffectPrefab") as GameObject;
			legalPrefab = GameObject.Instantiate(spritePF, pos, Quaternion.identity) as GameObject;
			legalPrefab.transform.parent = gameObject.transform;
			legalPrefab.transform.position = new Vector3 (pos.x,0.02f,pos.z);
			Texture2D legalHighlight = Resources.Load("Textures/legal") as Texture2D;
			legalPrefab.renderer.material.SetTexture("_MainTex", legalHighlight);
			HideLegal();

		}
	}
}