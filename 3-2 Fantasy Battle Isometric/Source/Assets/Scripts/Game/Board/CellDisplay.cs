using UnityEngine;

namespace HOA {
	public class CellDisplay : MonoBehaviour {

		public GameObject legalPlane;
		public GameObject effectPlane;
		public GameObject spritePlane;

		Cell cell;

		public Cell Parent {
			get {return cell;}
			set {cell = value;}
		}

		public void SetTex (Texture2D tex) {gameObject.renderer.material.SetTexture("_MainTex", tex);}

		public void Hide () {gameObject.renderer.enabled = false;}
		
		public void HideAll () {
			HideLegal();
			HideSprite();
			HideEffect();
		}

		public void SetLegal (bool legal) {
			if (legal) {ShowLegal();}
			else {HideLegal();}
		}
		void ShowLegal () {legalPlane.renderer.enabled = true;}
		void HideLegal () {legalPlane.renderer.enabled = false;}


		float effectedTime = 0;
		float EffectElapsedTime () {return Time.time - effectedTime;}
	//	static float fadeDuration = 1.5f;
//		EEffect currentEffect = EEffect.NONE;
		Texture2D effectTex;
		
		public void Effect (EEffect e) {
			effectedTime = Time.time;
			//currentEffect = e;
			effectTex = SpriteEffects.Effect(e);
			ShowEffect(effectTex);
		}
		void ShowEffect (Texture2D tex) {
			effectPlane.renderer.enabled = true;
			effectPlane.renderer.material.SetTexture("_MainTex", tex);
		}
		void HideEffect () {effectPlane.renderer.enabled = false;}


		void ShowSprite () {spritePlane.renderer.enabled = true;}
		void HideSprite () {spritePlane.renderer.enabled = false;}

		public void EnterSunken (Token t) {
			if (t.Display != default(TokenDisplay)) {
				ShowSprite();
				spritePlane.renderer.material.SetTexture("_MainTex", t.Display.Sprite);
			}
		}
		public void ExitSunken () {HideSprite();}
	}
}