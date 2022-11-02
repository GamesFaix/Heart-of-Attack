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

		Rect Box {
			get {
				if (!moving) {
					Cell c = parent.Cell;
					Rect cBox = c.Sprite.Box;
					if (c.TokenCount > 1) {
						float size = cBox.width/2;
						Rect box = new Rect (cBox.x, cBox.y, size, size);
						if (parent.IsPlane(EPlane.AIR)) {box.x += size;}
						else if (parent.IsPlane(EPlane.GND)) {box.y += size;}
						else if (parent.IsPlane(EPlane.SUNK)) {box.x += size; box.y += size;}
						return box;
					}
					else {return cBox;}
				}
				else {
					return MovingBox();
				}
			} 
		}
	


		public void Draw () {

			GUI.Box(Box, thumb);
			LabelInstance(Box);
			Effects(Box);
			HighlightLegal(Box);

			if (GUI.Button(Box, "", GUIMaster.S)){
				if (Input.GetMouseButtonUp(0) && parent.IsLegal()){
					Targeter.Select(parent);
					GUIMaster.PlaySound(EGUISound.TARGET);
				}
				else if (Input.GetMouseButtonUp(1)) {
					
					GUIInspector.Inspected = parent;
					GUIMaster.PlaySound(EGUISound.INSPECT);
					
				}
				else if (Input.GetMouseButtonUp(0) && parent.Cell.IsLegal()) {
					Targeter.Select(parent.Cell);
					GUIMaster.PlaySound(EGUISound.TARGET);
				} 
			}
		}

		float moveStartTime = 0;
		float MoveElapsedTime {get {return Time.time - moveStartTime;} }
		float moveTotalTime = 2;
		float Percent{ get {return MoveElapsedTime/moveTotalTime;} }
		
		bool moving = false;
		Vector2 distance;
		
		public void Move (Cell newCell) {
			//Debug.Log("move started");
			moving = true;
			moveStartTime = Time.time;
			startBox = parent.Cell.Sprite.Box;
			endBox = newCell.Sprite.Box;

			distance = new Vector2(endBox.x-startBox.x, endBox.y-startBox.y);
			//Debug.Log("distance: "+distance);
		}

		Rect startBox;
		Rect endBox;

		Rect MovingBox () {
			//Debug.Log("currently moving ("+Percent+"%), elapsed time: "+MoveElapsedTime+"/"+moveTotalTime);
			float x = startBox.x + Percent*distance.x;
			float y = startBox.y + Percent*distance.y;
			
			Rect box = new Rect (x,y,startBox.width,startBox.height);

			if (Mathf.Abs(MoveElapsedTime - moveTotalTime) < 0.05) {
			  //  (endBox.x - box.x < 0.05) && (endBox.y - box.y < 0.05) ) {
				box = endBox;
				moving = false;
				//Debug.Log("moving done");
			}
			return box;
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
