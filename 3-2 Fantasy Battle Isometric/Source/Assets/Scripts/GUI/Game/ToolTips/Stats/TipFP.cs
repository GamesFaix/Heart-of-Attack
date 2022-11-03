using UnityEngine;

namespace HOA {
	
	public class TipFP : Tip{
		
		public TipFP () {
			Name = "Focus";
			Icon = Icons.Stats.focus;
			ETip = ETip.FP;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Focus is a long-term resource used for " +
			          "\nperforming actions and gaining other " +
			          "\npowers." +

			          "\n\nMost Units may acquire Focus by " +
			          "\nperforming the 'Focus' action on " +
			          "\ntheir turn, some Units have other " +
			          "\nmeans of acquiring Focus." +

			          "\n\nFocus is only lost by spending it " +
			          "\non actions and may be accumulated " +
			          "\nover time.",
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipUnit();
			tip.Link(p.LinePanel);

			tip = new TipAP();
			tip.Link(p.LinePanel);
		}
	}
	
}
