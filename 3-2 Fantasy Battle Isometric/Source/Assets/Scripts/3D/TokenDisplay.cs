using UnityEngine;

namespace HOA {
	public class TokenDisplay : MonoBehaviour {
		
		Token token;
		
		public Token Token {
			get {return token;}
			set {token = value;}
		}
		
		Texture2D normalTex;
		public Texture2D NormalTex {
			get {return normalTex;}
			set {
				normalTex = value;
				spritePrefab.renderer.material.SetTexture("_MainTex", normalTex);
			}
		}

		public void HideSprite () {
			spritePrefab.renderer.enabled = false;

		}


		public void SetLegal (bool legal) {
			if (legal) {ShowLegal();}
			else {HideLegal();}
		}

		void ShowLegal () {legalPrefab.renderer.enabled = true;}
		void HideLegal () {legalPrefab.renderer.enabled = false;}

		public void MoveTo (Cell c) {
			if (c.Prefab != default(GameObject)) {
				Vector3 pos = c.Prefab.transform.position;
				pos.y += token.SpriteScale.z*4;
				//pos.x += 5;
				//pos.z += 5;
				if (token.IsPlane(EPlane.AIR)) {
					if (token.IsPlane(EPlane.GND)) {pos.y += 5;}
					else {pos.y +=20;}
				}
				gameObject.transform.position = pos;
			}
		}

		public void Orient () {
			Vector3 rotation = new Vector3 (45, 225, 0);

			gameObject.transform.eulerAngles = rotation;
		}
		/*
		void OnGUI () {
			if (gameObject.renderer.isVisible) {
				Vector3 camPos = Camera.main.gameObject.transform.position;
				float distance = Vector3.Distance(gameObject.transform.position, camPos);
				//Debug.Log(distance);
				float spriteSize = 5000/distance;
				if (Token.IsClass(EClass.KING)) {spriteSize *= 1.5f;}

				Vector2 screenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
				screenPos.y = Screen.height-screenPos.y;

				float x, y, w, h;
				x = screenPos.x - (spriteSize/2);
				y = screenPos.y - (spriteSize/2);
				w = spriteSize;
				h = spriteSize;

				if (Token.IsPlane(EPlane.AIR)) {
					y -= spriteSize;

					if (Token.IsPlane(EPlane.GND)) {
						h += spriteSize; 
						x -= spriteSize/2;
						w += spriteSize;
					}
				}

				Rect spriteBox = new Rect (x,y,w,h);

				if(!Token.IsPlane(EPlane.SUNK)) {Token.Draw(spriteBox);}



			}
			
			
			
			
		}
		*/

		GameObject effectPrefab;
		GameObject spritePrefab;
		GameObject legalPrefab;

		void Awake () {
			GameObject effectPF = Resources.Load("Prefabs/EffectPrefab") as GameObject;
			effectPrefab = GameObject.Instantiate(effectPF, gameObject.transform.position, Quaternion.identity) as GameObject;
			effectPrefab.transform.localScale = new Vector3 (2,1,2);
			effectPrefab.transform.parent = gameObject.transform;
			Vector3 pos = gameObject.transform.position;
			pos.y += 0.01f;
			effectPrefab.transform.position = pos;
			HideEffects();

			GameObject spritePF = Resources.Load("Prefabs/SpritePrefab") as GameObject;
			spritePrefab = GameObject.Instantiate(spritePF, gameObject.transform.position, Quaternion.identity) as GameObject;
			spritePrefab.transform.localScale = new Vector3 (2.5f, 1, 2.5f);
			//spritePrefab.transform.position = new Vector3 (5,0,5);
			spritePrefab.transform.parent = gameObject.transform;
		
			legalPrefab = GameObject.Instantiate(spritePF, gameObject.transform.position, Quaternion.identity) as GameObject;
			legalPrefab.transform.localScale = new Vector3 (2.5f,1,2.5f);
			legalPrefab.transform.parent = gameObject.transform;
			Texture2D legalHighlight = Resources.Load("Textures/legal") as Texture2D;
			legalPrefab.renderer.material.SetTexture("_MainTex", legalHighlight);
			pos = gameObject.transform.position;
			pos.y += 0.01f;
			legalPrefab.transform.position = pos;
			HideLegal();
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
		
		void Update () {
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

		void HideEffects () {
			effectTex = default(Texture2D);
			effectPrefab.renderer.material.SetTexture("_MainTex", default(Texture2D));
			effectPrefab.renderer.enabled = false;

		}

		void ShowEffect (Texture2D tex) {
			effectTex = tex;
			effectPrefab.renderer.enabled = true;
			effectPrefab.renderer.material.SetTexture("_MainTex", effectTex);

		}

		void SetAlpha (float f) {
			Color c = effectPrefab.renderer.material.color;
			Color d = new Color (c.r, c.g, c.b, f);
			effectPrefab.renderer.material.color = d;
		}

		void OnGUI () {
			Vector3 rot = gameObject.transform.eulerAngles;
			rot.y =	Camera.main.gameObject.transform.eulerAngles.y + 180;
			rot.x = Camera.main.gameObject.transform.parent.transform.eulerAngles.x ;
			gameObject.transform.eulerAngles = rot;
			//gameObject.transform.LookAt(Camera.main.transform.position);


		}
	}
}