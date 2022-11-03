using UnityEngine;
using System.Collections;

namespace HOA {

	public class CellSprite {

		Cell parent;
		Texture2D tex;

		public CellSprite (Cell c) {
			parent = c;
			if (Even) {tex = ImageLoader.cells[0];}
			else {tex = ImageLoader.cells[1];}
		}

		bool Even {
			get {
				if ( (parent.X + parent.Y) %2 == 0) {return true;}
				return false;
			}
		}



		public Rect Box {
			get {
				Rect boardBox = GUIBoard.Box;
				float size = boardBox.width/Board.Size;
				
				float x = boardBox.x + (parent.X-1)*size;
				float y = boardBox.y + (parent.Y-1)*size;
			
				return new Rect(x,y,size,size);
			}
		}

		public void Draw () {
			GUI.Box(Box, tex, GUIMaster.S);
			
			if (Input.GetKey("left shift") || Input.GetKey("right shift")) {
				if (GUI.Button(Box, "", GUIMaster.S)) {
					GUIInspector.Inspected = parent;
					GUIMaster.PlaySound(EGUISound.INSPECT);
				}
			}

			if (parent.IsLegal()) {
				Color c = GUI.color;
				GUI.color = new Color (1,1,1,0.5f);
				if (GUI.Button(Box, ImageLoader.yellowBtn, GUIMaster.S)
				    && Input.GetMouseButtonUp(0)) {
					Targeter.Select(parent);
					GUIMaster.PlaySound(EGUISound.TARGET);
				}
				GUI.color = c;
			}
			Effects(Box);
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
				GUI.Box (box, effectTex /*,spriteLabel*/);
				
				GUI.color = oldColor;
			}
			
			else {
				effectedTime = 0;
				currentEffect = EEffect.NONE;
			}
		}

	}
}