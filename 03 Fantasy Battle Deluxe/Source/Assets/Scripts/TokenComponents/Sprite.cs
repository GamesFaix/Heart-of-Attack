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
		
		public void Draw (Rect box) {
			GUI.Box(box, thumb);
			LabelInstance(box);
			Effects(box);
			HighlightLegal(box);
		}

		void LabelInstance (Rect box) {
			if (!parent.Unique && parent is Unit) {
				FancyText.Highlight(box, ""+parent.Instance, spriteLabel, parent.Owner.Colors);
			}
		}
	
		float effectedTime = 0;
		EFFECT currentEffect = EFFECT.NONE;
		Texture2D effectTex;

		public void Effect (EFFECT e) {
			effectedTime = Time.time;
			currentEffect = e;
			effectTex = SpriteEffects.Effect(e);
		}

		float ElapsedTime () {return Time.time - effectedTime;}
		static float fadeDuration = 1.5f;

		void Effects (Rect box) {
			if (ElapsedTime() < fadeDuration 
			&& currentEffect != EFFECT.NONE) {
				Color oldColor = GUI.color;
				float alpha;

				if (ElapsedTime() < (fadeDuration/2)) {alpha = (ElapsedTime()/fadeDuration)*2;}
				else {alpha = (1 - (ElapsedTime()/fadeDuration))*2;}

				GUI.color = new Color (1,1,1,alpha);
				GUI.Box (box, effectTex, spriteLabel);

				GUI.color = oldColor;
			}

			else {
				effectedTime = 0;
				currentEffect = EFFECT.NONE;
			}
		}




		void HighlightLegal (Rect box) {
			if (parent.Legal) {
				Color c = GUI.color;
				GUI.color = new Color (1,1,1,0.5f);
				GUI.Box(box, ImageLoader.yellowBtn);
				GUI.color = c;
			}
		}
	
	}
}
