using UnityEngine;

namespace HOA {
	public class TokenDisplay : MonoBehaviour {

		void OnGUI () {FaceCamera();}
		void Update () {FadeEffect();}

		Token token;
		
		public Token Token {
			get {return token;}
			set {token = value;}
		}

		GameObject effectPlane;
		public GameObject EffectPlane {set {effectPlane = value;} }
		GameObject spritePlane;
		public GameObject SpritePlane {set {spritePlane = value;} }
		GameObject legalPlane;
		public GameObject LegalPlane {set {legalPlane = value;} }

		Texture2D sprite = default(Texture2D);
		public Texture2D Sprite {
			get {return sprite;}
			set {
				sprite = value;
				spritePlane.renderer.material.SetTexture("_MainTex", sprite);
			}
		}
		public void HideSprite () {spritePlane.renderer.enabled = false;}

		public void SetLegal (bool legal) {legalPlane.renderer.enabled = legal;}

		public void MoveTo (Cell c) {
			//if (c.Prefab != default(GameObject)) {
				Vector3 pos = c.Location;
				pos.y += token.SpriteScale.z*4;
				if (token is ArenaNonSensus) {
					pos.x += (float)(Board.CellSize/2);
					pos.z += (float)(Board.CellSize/2);
					pos.y -= 20;
				}
				if (token.Plane.Is(EPlane.AIR) || token.Plane.Is(EPlane.ETH)) {
					if (token.Plane.Is(EPlane.GND)) {pos.y += 5;}
					else {pos.y +=20;}
				}
				gameObject.transform.position = pos;
			//}
		}

		float effectedTime = 0;
		float EffectElapsedTime () {return Time.time - effectedTime;}
		static float fadeDuration = 1.5f;
		EEffect currentEffect = EEffect.NONE;
		Texture2D effectTex;
		
		public void Effect (EEffect e) {
			effectedTime = Time.time;
			currentEffect = e;
			effectTex = SpriteEffects.Effect(e);
			ShowEffect(effectTex);
		}

		public void HideEffects () {
			effectTex = default(Texture2D);
			effectPlane.renderer.material.SetTexture("_MainTex", default(Texture2D));
			effectPlane.renderer.enabled = false;
		}
		
		void ShowEffect (Texture2D tex) {
			effectTex = tex;
			effectPlane.renderer.enabled = true;
			effectPlane.renderer.material.SetTexture("_MainTex", effectTex);
		}

		void FadeEffect () {
			if (EffectElapsedTime() < fadeDuration 
			    && currentEffect != EEffect.NONE) {
				float alpha;
				
				if (EffectElapsedTime() < (fadeDuration/2)) {alpha = (EffectElapsedTime()/fadeDuration)*2;}
				else {alpha = (1 - (EffectElapsedTime()/fadeDuration))*2;}
				SetAlpha (alpha);
			}
			
			else {
				effectedTime = 0;
				currentEffect = EEffect.NONE;
				HideEffects();
			}
		}

		void SetAlpha (float f) {
			Color c = effectPlane.renderer.material.color;
			Color d = new Color (c.r, c.g, c.b, f);
			effectPlane.renderer.material.color = d;
		}

		void FaceCamera () {
			Vector3 rot = gameObject.transform.eulerAngles;
			rot.y =	Camera.main.gameObject.transform.eulerAngles.y + 180;
			rot.x = Camera.main.gameObject.transform.parent.transform.eulerAngles.x+10 ;
			gameObject.transform.eulerAngles = rot;
		}
	}
}