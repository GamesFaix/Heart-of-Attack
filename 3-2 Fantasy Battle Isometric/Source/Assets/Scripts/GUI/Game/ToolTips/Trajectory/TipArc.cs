using UnityEngine;

namespace HOA {
	
	public class TipArc : Tip{
		
		public TipArc () {
			Name = "Arc";
			Icon = Icons.Aims.arc;
			ETip = ETip.ARC;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Actions targeting in an Arc are not blocked " +
			          "\nby any tokens, but targets must be within " +
			          "\nthe aim's Range. " +

			          "\n\nArc aims may have a Minimum Range in " +
			          "\naddition to the normal (Maximum) Range." +
			          "\nTargets cannot be selected which are closer " +
			          "\nto the performing Unit than the Mimimum " +
			          "\nRange.",
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipLine();
			tip.Link(p.LinePanel);

			tip = new TipPath();
			tip.Link(p.LinePanel);

			tip = new TipNeighbor();
			tip.Link(p.LinePanel);
		
			tip = new TipFree();
			tip.Link(p.LinePanel);
		
			tip = new TipSelf();
			tip.Link(p.LinePanel);
		}

	}
	
}
