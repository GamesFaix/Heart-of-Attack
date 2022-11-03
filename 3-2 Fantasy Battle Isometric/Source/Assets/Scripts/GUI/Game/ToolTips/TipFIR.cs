using UnityEngine;

namespace HOA {
	
	public class TipFIR : Tip{
		
		public TipFIR () {
			Name = "Fire";
			Icon = Icons.FIR();
			ETip = ETip.FIR;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Fire effects may target Units or " +
			          "\nDestructibles." +

			          "\n\nIf the target is a Unit it takes the amount of" +
			          "\nDamage specified in the effect." +
			          "\nIf the target is Destructible, it is destroyed." +

			          "\n\nAny Units Neighboring the target take half" +
			          "\nthe Damage of the effect (rounded down)." +
			          "\nAny Destructibles Neighboring the target " +
			          "\nare destroyed.",
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipDamage();
			tip.Link(p.LinePanel);

			tip = new TipUnit();
			tip.Link(p.LinePanel);

			tip = new TipDest();
			tip.Link(p.LinePanel);

			tip = new TipNeighbor();
			tip.Link(p.LinePanel);

		}
	}
}
