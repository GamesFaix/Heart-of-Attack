using UnityEngine;

namespace HOA {
	public class TokenDisplay : TargetDisplay {

		void OnGUI () {FaceCamera();}

		public Token Token {get {return (Token)Parent;} }

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

			targetPos.y += Token.SpriteScale.z*4;

			if (Token.Plane.air || Token.Plane.ethereal) {
				if (Token.Plane.ground) {targetPos.y += 5;}
				else {targetPos.y +=20;}
			}
			moveStartTime = Time.time;
			moving = true;
		}

		public void Enter (Cell c) {
			targetPos = c.Location;
			
			targetPos.y += Token.SpriteScale.z*4;
			
			if (Token.Plane.air || Token.Plane.ethereal) {
				if (Token.Plane.ground) {targetPos.y += 5;}
				else {targetPos.y +=20;}
			}
			gameObject.transform.position = targetPos;
		}

		void Update () {
			if (moving) {
				gameObject.transform.position = Vector3.Lerp(startPos, targetPos, MovePercent());

				if (MovePercent() > 0.995f) {
					gameObject.transform.position = targetPos;
					moving = false;
					moveStartTime = 0;
				}
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