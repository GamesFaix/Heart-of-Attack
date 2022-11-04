using UnityEngine; 

namespace HOA { 

	public class EffectCard : Card {

		public static Material effectMat = Resources.Load("Materials/EffectMat") as Material;

		float effectedTime = 0;
		float EffectElapsedTime () {return Time.time - effectedTime;}
		static float fadeDuration = 1.5f;
		
		public void Effect (Texture2D tex) {
			effectedTime = Time.time;
			Tex = tex;
			Show();
		}
		
		void Update () {
			if (Tex != null) {
				if (EffectElapsedTime() < fadeDuration) {
					float alpha;
					
					if (EffectElapsedTime() < (fadeDuration/2)) {alpha = (EffectElapsedTime()/fadeDuration)*2;}
					else {alpha = (1 - (EffectElapsedTime()/fadeDuration))*2;}
					
					Color c = gameObject.renderer.material.color;
					Color d = new Color (c.r, c.g, c.b, alpha);
					gameObject.renderer.material.color = d;
				}
				
				else {
					effectedTime = 0;
					Tex = null;
					Hide();
				}
			}
		}
	
	
	
	}
}
