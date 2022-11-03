using UnityEngine; 
using System;

namespace HOA { 

	public class TargetDisplay : MonoBehaviour{
		static GameObject cellPF = Resources.Load("Prefabs/CellPrefab") as GameObject;
		static GameObject exoPF = Resources.Load("Prefabs/ExoCellPrefab") as GameObject;
		static GameObject tokenPF = Resources.Load("Prefabs/TokenPrefab") as GameObject;
		static GameObject postPF = Resources.Load("Prefabs/PostPrefab") as GameObject;

		static Texture2D texLegal = Resources.Load("Images/Textures/legal") as Texture2D;

		public Target Parent {get; set;}

		public GameObject GO {get {return gameObject;} }

		protected Card terrainCard;
		protected Card spriteCard;
		protected Card legalCard;
		protected EffectCard effectCard;

		public Texture2D Sprite {
			get {return spriteCard.Tex;} 
			set {spriteCard.Tex = value;}
		}

		public void HideSprite() {spriteCard.Hide();}
		public void HideTerrain() {terrainCard.Hide();}

		public bool Legal {
			set {
				if (value) {legalCard.Show();}
				else {legalCard.Hide();}
			}
		}

		public bool Clickable {
			get {return GO.collider.enabled;}
			set {GO.collider.enabled=value;}

		}

		public void Effect (EEffect e) {effectCard.Effect(e);}

		protected static Vector3 PrefabPos (Target t) {
			Cell c;
			if (t is Cell) {c = (Cell)t;}
			else if (t is Token) {c = ((Token)t).Body.Cell;}
			else {throw new Exception ("TargetDisplay: Can only determine prefab position for Cell or Token.");}

			return CellPos(c.Index);
		}

		static Vector3 CellPos (index2 index) {
			float x = index.x * BoardPhysical.CellSize;
			float z = index.y * BoardPhysical.CellSize;
			return new Vector3 (x,0,z);
		}

		public static void Attach (Target t) {
			GameObject go = Instantiate (t);
			TargetDisplay display;
			try {
				if (t is Cell) {
					display = go.GetComponent("CellDisplay") as TargetDisplay;
					if (!(t is ExoCell)){go.name = "Cell "+((Cell)t).ToString();}
					else {go.name = "Border "+((Cell)t).ToString();}
					go.transform.localScale = new Vector3 ((float)BoardPhysical.CellSize/10, 1, (float)BoardPhysical.CellSize/10);
				}
				else {
					display = go.GetComponent("TokenDisplay") as TargetDisplay;
					go.name = ((Token)t).ToString();
					go.transform.localScale = ((Token)t).SpriteScale;
				}
			}
			catch {
				throw new Exception ("TargetDisplay: Can only attach to Cell or Token.");
			}

			display.Parent = t;
			t.Display = display;
	
			AttachCards(display);

			if (t is Token) {
				display.Sprite = Thumbs.CodeToThumb(((Token)t).ID.Code);
				if (((Token)t).Plane.sunken) {
					display.HideSprite();
					display.Clickable = false;
				}
			}

		}

		static void AttachCards (TargetDisplay display) {
			if (display is CellDisplay) {
				display.terrainCard = Card.Attach(display.GO, 0);
				display.terrainCard.gameObject.name = "Terrain Card";
				display.terrainCard.Tex = ((CellDisplay)display).TerrainTex;
			}

			display.spriteCard  = Card.Attach(display.GO, 0.01f);
			display.spriteCard.gameObject.name = "Sprite Card";
			if (display is ExoCellDisplay) {
				display.spriteCard.Show();
				((ExoCellDisplay)display).AddShadow();
			}
			else if (display is CellDisplay) {display.spriteCard.Hide();}

			display.legalCard = Card.Attach(display.GO, 0.02f);
			display.legalCard.gameObject.name = "Legal Card";
			display.legalCard.Tex = texLegal;
			display.legalCard.Hide();

			display.effectCard  = Card.AttachEffect(display.GO, 0.03f);
			display.effectCard.gameObject.name = "Effect Card";
			display.effectCard.Hide();
		}
	
		static void AttachPost (TargetDisplay display) {
			GameObject post = GameObject.Instantiate(postPF, display.gameObject.transform.position, Quaternion.identity) as GameObject;
			post.transform.Rotate(new Vector3(90,0,0));
			post.transform.parent = display.gameObject.transform;
			Vector3 newPos = post.transform.localPosition;
			newPos.z += 5;
			//newPos.x += 5;
			post.transform.localPosition = newPos;

		}


		static GameObject Instantiate (Target t) {
			GameObject prefab;
			Vector3 pos;
			if (t is ExoCell) {
				prefab = exoPF;
				pos = PrefabPos(t);
			}
			else if (t is Cell) {
				prefab = cellPF;
				pos = PrefabPos(t);
			}
			else if (t is Token) {
				prefab = tokenPF;
				pos = new Vector3(0,0,0);
			}
			else {throw new Exception ("TargetDisplay: Can only insantiate on Cell or Token.");}
			return GameObject.Instantiate (prefab, pos, Quaternion.identity) as GameObject;
		}

	}


}
