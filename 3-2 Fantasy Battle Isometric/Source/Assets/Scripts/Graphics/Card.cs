using UnityEngine;

namespace HOA {

	public class Card : MonoBehaviour {
		protected static GameObject cardPF = Resources.Load("Prefabs/CardPrefab") as GameObject;

		public static Card Attach (GameObject p, float height, Vector3 scale) {
			Vector3 pos = p.transform.position;
			GameObject cardGO = GameObject.Instantiate(cardPF, pos, Quaternion.identity) as GameObject;
			Card card = cardGO.GetComponent("Card") as Card;

			cardGO.transform.parent = p.transform;


			cardGO.transform.localPosition = new Vector3 (0, height, 0);

			cardGO.transform.localScale = new Vector3 (1,1,1); 
			return card;
		}
		public static Card Attach (GameObject p, float height) {return Attach (p, height, new Vector3(1,1,1));}

		public static EffectCard AttachEffect (GameObject p, float height, Vector3 scale) {
			Vector3 pos = p.transform.position;
			GameObject cardGO = GameObject.Instantiate(cardPF, pos, Quaternion.identity) as GameObject;
			
			cardGO.transform.parent = p.transform;
			cardGO.transform.localPosition = new Vector3 (0, height, 0);
			cardGO.transform.localScale = new Vector3 (1,1,1); 

			cardGO.renderer.material = EffectCard.effectMat;

			Destroy(cardGO.GetComponent("Card"));
			EffectCard card = cardGO.AddComponent("EffectCard") as EffectCard;
			
			return card;
		}
		public static EffectCard AttachEffect (GameObject p, float height) {return AttachEffect (p, height, new Vector3(1,1,1));}

		public void Show () {
			gameObject.renderer.enabled = true;
		}
		public void Hide () {gameObject.renderer.enabled = false;}

		protected Texture2D tex;
		public Texture2D Tex {
			get {return tex;}
			set {
				tex = value;
				gameObject.renderer.material.SetTexture("_MainTex", tex);
			}
		}

	}
}