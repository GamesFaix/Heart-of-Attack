using UnityEngine; 

namespace HOA { 

	public class TargetDisplay : MonoBehaviour{
	
		protected static Texture2D texLegal = Resources.Load("Images/Textures/legal") as Texture2D;

		public ITarget Parent {get; set;}

		public GameObject GO {get {return gameObject;} }

		Card spriteCard;
		Card legalCard;
		EffectCard effectCard;

		public void Effect (EEffect e) {effectCard.Effect(e);}




	
	}


}
