using UnityEngine; 
using System;

namespace HOA { 

	public class TargetDisplay : MonoBehaviour{
		static GameObject cellPF = Resources.Load("Prefabs/CellPrefab") as GameObject;
		static GameObject tokenPF = Resources.Load("Prefabs/TokenPrefab") as GameObject;

		static Texture2D texLegal = Resources.Load("Images/Textures/legal") as Texture2D;
		static Texture2D cellTexEven = Resources.Load("Images/Textures/Cell/mc grass") as Texture2D;
		static Texture2D cellTexOdd = Resources.Load("Images/Textures/Cell/mc dirt") as Texture2D;

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

		public void HideSprite() {
			spriteCard.Hide();
		}

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

		static Vector3 CellPos (Index2 index) {
			float x = index.x * BoardPhysical.CellSize;
			float z = index.y * BoardPhysical.CellSize;
			return new Vector3 (x,0,z);
		}

		public static void Attach (Target t) {
			GameObject go = Instantiate (t);
			TargetDisplay display;
			try {
				display = (t is Cell ? 
				 go.GetComponent("CellDisplay") as TargetDisplay: 
				 go.GetComponent("TokenDisplay") as TargetDisplay);
				go.name = (t is Cell ?
				   "Cell "+((Cell)t).ToString():
				   ((Token)t).ToString());
				go.transform.localScale = (t is Cell ?
				   new Vector3 ((float)BoardPhysical.CellSize/10, 1, (float)BoardPhysical.CellSize/10):
				   ((Token)t).SpriteScale);
			}
			catch {
				throw new Exception ("TargetDisplay: Can only attach to Cell or Token.");
			}

			display.Parent = t;
			t.Display = display;
	
			AttachCards(display);

			if (t is Token) {
				display.Sprite = Thumbs.CodeToThumb(((Token)t).ID.Code);
				if (((Token)t).Plane.Is(EPlane.SUNK)) {
					display.HideSprite();
					display.Clickable = false;
				}
				((TokenDisplay)display).MoveTo(((Token)t).Body.Cell);

			}

		}

		static void AttachCards (TargetDisplay display) {
			if (display is CellDisplay) {
				display.terrainCard = Card.Attach(display.GO, 0);
				display.terrainCard.gameObject.name = "Terrain Card";

				if (Even(display)) {display.terrainCard.Tex = cellTexEven;}
				else {display.terrainCard.Tex = cellTexOdd;}
			}

			display.spriteCard  = Card.Attach(display.GO, 0.01f);
			display.spriteCard.gameObject.name = "Sprite Card";
			if (display is CellDisplay) {display.spriteCard.Hide();}

			display.legalCard = Card.Attach(display.GO, 0.02f);
			display.legalCard.gameObject.name = "Legal Card";
			display.legalCard.Tex = texLegal;
			display.legalCard.Hide();

			display.effectCard  = Card.AttachEffect(display.GO, 0.03f);
			display.effectCard.gameObject.name = "Effect Card";
			display.effectCard.Hide();
		}
		static bool Even (TargetDisplay display) {
			if (display is CellDisplay) {
				CellDisplay cd = (CellDisplay)display;
				Cell cell = cd.Cell;
				if ((cell.X+cell.Y)%2 == 0) {return true;}
			}
			return false;
		}

		static GameObject Instantiate (Target t) {
			GameObject prefab;
			if (t is Cell) {prefab = cellPF;}
			else if (t is Token) {prefab = tokenPF;}
			else {throw new Exception ("TargetDisplay: Can only insantiate on Cell or Token.");}
			return GameObject.Instantiate (prefab, PrefabPos(t), Quaternion.identity) as GameObject;
		}

	}


}
