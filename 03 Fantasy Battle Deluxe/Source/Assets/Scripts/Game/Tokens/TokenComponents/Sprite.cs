using UnityEngine;

namespace HOA {
	
	public class Sprite {
		static GUIStyle spriteLabel = new GUIStyle(){fontSize = 30};

		Token parent;

		Texture2D thumb;
		public Texture2D Thumb {get {return thumb;} }
		
		public Sprite (Token t){
			parent = t;	
			thumb = Thumbs.CodeToThumb(parent.Code);
		}

		Rect currentBox;
		Rect endBox;

		public void Draw (Rect box) {
			if (!moving) {currentBox = box;}
			else {
				currentBox = MovingBox();

			}


			if (currentBox == endBox) {moving = false;}

			GUI.Box(currentBox, thumb);
			LabelInstance(currentBox);
			Effects(currentBox);
			HighlightLegal(currentBox);
		}

		float moveStartTime = 0;
		float MoveElapsedTime {get {return Time.time - moveStartTime;} }
		static float moveTotalTime = 22000;
		
		bool moving = false;
		Vector2 distance;
		
		public void Move (Cell newCell) {
			moving = true;
			moveStartTime = Time.time;
			Cell startCell = parent.Cell;

			distance = new Vector2(newCell.X-startCell.X, newCell.Y-startCell.Y);
			
			
		}
		
		Rect MovingBox (Rect box) {
			float x = box.x + (MoveElapsedTime/moveTotalTime)*distance.x;
			float y = box.y + (MoveElapsedTime/moveTotalTime)*distance.y;
			
			return new Rect (x,y,box.width,box.height);
			
		}












		void LabelInstance (Rect box) {
			if (!parent.Unique && parent is Unit) {
				FancyText.Highlight(box, ""+parent.Instance, spriteLabel, parent.Owner.Colors);
			}
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
		}

		void Effects (Rect box) {
			if (EffectElapsedTime() < fadeDuration 
			&& currentEffect != EEffect.NONE) {
				Color oldColor = GUI.color;
				float alpha;

				if (EffectElapsedTime() < (fadeDuration/2)) {alpha = (EffectElapsedTime()/fadeDuration)*2;}
				else {alpha = (1 - (EffectElapsedTime()/fadeDuration))*2;}

				GUI.color = new Color (1,1,1,alpha);
				GUI.Box (box, effectTex, spriteLabel);

				GUI.color = oldColor;
			}

			else {
				effectedTime = 0;
				currentEffect = EEffect.NONE;
			}
		}

		void HighlightLegal (Rect box) {
			if (parent.IsLegal()) {
				Color c = GUI.color;
				GUI.color = new Color (1,1,1,0.5f);
				GUI.Box(box, ImageLoader.yellowBtn);
				GUI.color = c;
			}
		}
	
	}
}
