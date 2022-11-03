using UnityEngine;

namespace HOA {
	
	public class TipOnDeath : Tip{
		
		public TipOnDeath () {
			Name = "Token-on-Death";
			Icon = Icons.Other.onDeath;
			ETip = ETip.ONDEATH;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Many tokens will leave another token in " +
			          "\ntheir Cell after they are killed." +

			          "\n\nUnits will typically leave a 'Corpse'. " +

			          "\n\nAttack Kings will leave a Heart of Attack." +

			          "\n\nObstacles will typically leave nothing." +

			          "\n\nWhen a token is destroyed, if there is " +
			          "\nalready a token in its Cell occupying " +
			          "\nthe same Plane its Token-on-Death would, " +
			          "\nno Token-on-Death is created.",
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipToken();
			tip.Link(p.LinePanel);

			tip = new TipHeart();
			tip.Link(p.LinePanel);
		}
	}
	
}
