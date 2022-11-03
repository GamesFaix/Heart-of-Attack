using UnityEngine;

namespace HOA {
	
	public class TipIN : Tip{
		
		public TipIN () {
			Name = "Initiative";
			Icon = Icons.Stat(EStat.IN);
			ETip = ETip.IN;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Initiative affects how frequently a Unit takes turns.",

			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipUnit();
			tip.Link(p.LinePanel);

		}

	}
	
}
