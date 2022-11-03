using UnityEngine; 

namespace HOA { 

	public class EffectCard : Card {

		public static Material effectMat = Resources.Load("Materials/EffectMat") as Material;

		float effectedTime = 0;
		float EffectElapsedTime () {return Time.time - effectedTime;}
		static float fadeDuration = 1.5f;
		EEffect currentEffect = EEffect.NONE;
		
		public void Effect (EEffect e) {
			currentEffect = e;
			effectedTime = Time.time;
			Tex = SpriteEffects.Effect(e);
			Show();
		}
		
		void Update () {
			if (currentEffect != EEffect.NONE) {
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
					currentEffect = EEffect.NONE;
					Hide();
				}
			}
		}
	
	
	
	}
}
