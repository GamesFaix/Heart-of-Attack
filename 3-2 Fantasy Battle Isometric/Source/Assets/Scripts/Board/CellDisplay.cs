using UnityEngine;

namespace HOA {
	public class CellDisplay : MonoBehaviour, ITargetDisplay {

		static GameObject cellPF = Resources.Load("Prefabs/CellPrefab") as GameObject;
		static Texture2D texLegal = Resources.Load("Images/Textures/legal") as Texture2D;
		static Texture2D texEven = Resources.Load("Images/Textures/Cell/mc grass") as Texture2D;
		static Texture2D texOdd = Resources.Load("Images/Textures/Cell/mc dirt") as Texture2D;

		public ITarget Target() {return (ITarget)cell;}
		public GameObject GO() {return gameObject;}

		Cell cell;
		public Cell Parent {
			get {return cell;}
			set {cell = value;}
		}

		public static void Attach (Cell c) {
			GameObject prefab = GameObject.Instantiate (cellPF, PrefabPos(c), Quaternion.identity) as GameObject;
			prefab.transform.localScale = new Vector3 ((float)Board.CellSize/10,1,(float)Board.CellSize/10);
			prefab.name = "Cell ("+c.X+","+c.Y+")";
			CellDisplay cd = prefab.GetComponent("CellDisplay") as CellDisplay;
			cd.Parent = c;
			c.Display = cd;
			
			cd.AttachCards();

		}

		static Vector3 PrefabPos (Cell c) {
			Vector3 pos = new Vector3(0,0,0);
			pos.x = (c.X-1)*Board.CellSize;
			pos.z = (c.Y-1)*Board.CellSize;
			return pos;
		}

		public Card terrainCard;
		public Card spriteCard;
		public Card legalCard;
		public EffectCard effectCard;

		public void AttachCards() {
			terrainCard = Card.Attach(gameObject, 0);
			terrainCard.gameObject.name = "Terrain Card";

			spriteCard  = Card.Attach(gameObject, 0.01f);
			spriteCard.gameObject.name = "Sprite Card";

			legalCard   = Card.Attach(gameObject, 0.02f);
			legalCard.gameObject.name = "Legal Card";

			effectCard  = Card.AttachEffect(gameObject, 0.03f);
			effectCard.gameObject.name = "Effect Card";

			if ((cell.X+cell.Y)%2 == 0) {terrainCard.Tex = texEven;}
			else {terrainCard.Tex = texOdd;}
		
			spriteCard.Hide();

			legalCard.Tex = texLegal;
			legalCard.Hide();

			effectCard.Hide();
		}

		float effectedTime = 0;
		float EffectElapsedTime () {return Time.time - effectedTime;}

		public void Effect (EEffect e) {effectCard.Effect(e);}

		public void EnterSunken (Token t) {
			if (t.Display != default(TokenDisplay)) {
				spriteCard.Show();
				spriteCard.Tex = t.Display.spriteCard.Tex;
			}
		}
		public void ExitSunken () {spriteCard.Hide();}
	}
}