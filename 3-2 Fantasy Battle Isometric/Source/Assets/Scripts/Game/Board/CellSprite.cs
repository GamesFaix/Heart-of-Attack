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

		bool iso = true;

		public Rect Box {
			get {
				if (iso) {return Isometric();}

				Rect boardBox = GUIBoard.Box;
				float size = boardBox.width/Board.Size;

				Vector2 v = new Vector2(0,0);

				v.x = boardBox.x + (parent.X-1)*size;
				v.y = boardBox.y + boardBox.height - (parent.Y)*size;
			
				return new Rect(v.x,v.y,size,size);
			}
		}

		Rect Isometric () {
			Rect boardBox = GUIBoard.Box;
			float cellW = boardBox.width/(Board.Size+(Board.Size-1));
			float cellH = cellW * 0.66f;

			float x = boardBox.x+(boardBox.width*0.5f)+(parent.X - parent.Y-1)*cellW;
			float y = boardBox.y+boardBox.height - (parent.X + parent.Y)*cellH;

			return new Rect(x,y,cellW*2, cellH*2);
		}

		public void Draw () {
			GUI.DrawTexture(Box, tex, ScaleMode.StretchToFill);
			
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