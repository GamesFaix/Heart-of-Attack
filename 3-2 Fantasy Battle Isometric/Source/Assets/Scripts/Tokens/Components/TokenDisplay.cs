using UnityEngine;

namespace HOA {
	public class TokenDisplay : MonoBehaviour, ITargetDisplay {

		static GameObject tokenPF = Resources.Load("Prefabs/TokenPrefab") as GameObject;
		static Texture2D texLegal = Resources.Load("Images/Textures/legal") as Texture2D;

		void OnGUI () {FaceCamera();}

		Token token;
		
		public Token Token {
			get {return token;}
			set {token = value;}
		}

		public ITarget Target() {return (ITarget)token;}
		public GameObject GO() {return gameObject;}

		public static void Attach (Token t) {
			GameObject prefab = GameObject.Instantiate (tokenPF, PrefabPos(t), Quaternion.identity) as GameObject;
			prefab.name = t.ToString();
			TokenDisplay td = prefab.GetComponent("TokenDisplay") as TokenDisplay;
			td.Token = t;
			t.Display = td;
			
			td.AttachCards();
			td.gameObject.transform.eulerAngles = new Vector3 (45, 225, 0);
			td.spriteCard.Tex = Thumbs.CodeToThumb(t.ID.Code);
			td.MoveTo(t.Body.Cell);
			if (t.Plane.Is(EPlane.SUNK)) {
				t.Display.spriteCard.Hide();
			}
			
			prefab.transform.localScale = t.SpriteScale;
		}

		static Vector3 PrefabPos (Token t) {
			Vector3 pos = new Vector3(0,0,0);
			pos.x = (t.Body.Cell.X-1)*Board.CellSize;
			pos.z = (t.Body.Cell.Y-1)*Board.CellSize;
			return pos;
		}

		public Card spriteCard;
		public Card legalCard;
		public EffectCard effectCard;
		
		public void AttachCards() {
			spriteCard  = Card.Attach(gameObject, 0.01f);
			spriteCard.gameObject.name = "Sprite Card";
			
			legalCard   = Card.Attach(gameObject, 0.02f);
			legalCard.gameObject.name = "Legal Card";
			
			effectCard  = Card.AttachEffect(gameObject, 0.03f);
			effectCard.gameObject.name = "Effect Card";
			
			legalCard.Tex = texLegal;
			legalCard.Hide();
			
			effectCard.Hide();
		}

		public void Effect (EEffect e) {effectCard.Effect(e);}

		bool moving = false;
		float moveStartTime = 0;
		float MoveElapsedTime () {return Time.time - moveStartTime;}
		static float moveDuration = 0.3f;
		float MovePercent () {return MoveElapsedTime()/moveDuration;}
		Vector3 startPos;
		Vector3 targetPos;

		public void MoveTo (Cell c) {
			startPos = gameObject.transform.position;
			targetPos = c.Location;

			targetPos.y += token.SpriteScale.z*4;
			
			if (token is ArenaNonSensus) {
				targetPos.x += (float)(Board.CellSize/2);
				targetPos.z += (float)(Board.CellSize/2);
				targetPos.y -= 20;
			}
			if (token.Plane.Is(EPlane.AIR) || token.Plane.Is(EPlane.ETH)) {
				if (token.Plane.Is(EPlane.GND)) {targetPos.y += 5;}
				else {targetPos.y +=20;}
			}
			moveStartTime = Time.time;
			moving = true;
		}

		void Update () {
			if (moving) {
				gameObject.transform.position = Vector3.Lerp(startPos, targetPos, MovePercent());

				if (MovePercent() > 0.995f) {
					gameObject.transform.position = targetPos;
					moving = false;
					moveStartTime = 0;
				}
				if (CameraPanner.Target == token) {CameraPanner.Focus(token, false);}
			}
		}

		void FaceCamera () {
			Vector3 rot = gameObject.transform.eulerAngles;
			rot.y =	Camera.main.gameObject.transform.eulerAngles.y + 180;
			rot.x = Camera.main.gameObject.transform.parent.transform.eulerAngles.x+10 ;
			gameObject.transform.eulerAngles = rot;
		}
	}
}