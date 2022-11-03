using UnityEngine;

namespace HOA {

	public class TipUnit : Tip{

		public TipUnit () {
			Name = "Unit";
			Icon = Icons.Types.unit;
			ETip = ETip.Unit;
		}

		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
				"Units are Tokens that: " +
				"\n-Are controlled directly by players" +
				"\n-Take turns" +
				"\n-Can acquire Energy and Focus" +
				"\n-Have Health", 
			    p.s);
		}	

		public override void SeeAlso (Panel p) {
			Tip tip = new TipToken();
			tip.Link(p.LinePanel);

			tip = new TipIN();
			tip.Link(p.LinePanel);
			
			tip = new TipAP();
			tip.Link(p.LinePanel);
			
			tip = new TipFP();
			tip.Link(p.LinePanel);

			tip = new TipHP();
			tip.Link(p.LinePanel);

			tip = new TipTimer();
			tip.Link(p.LinePanel);
		}
	}
	
}
