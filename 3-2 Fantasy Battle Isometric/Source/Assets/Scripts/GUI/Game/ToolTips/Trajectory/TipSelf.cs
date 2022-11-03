using UnityEngine;

namespace HOA {
	
	public class TipSelf : Tip{
		
		public TipSelf () {
			Name = "Self";
			Icon = Icons.Aims.self;
			ETip = ETip.SELF;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Actions that are Self targeting do not " +
			          "\nrequire the selection of any target. ",
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipArc();
			tip.Link(p.LinePanel);
			
			tip = new TipLine();
			tip.Link(p.LinePanel);
			
			tip = new TipPath();
			tip.Link(p.LinePanel);
			
			tip = new TipNeighbor();
			tip.Link(p.LinePanel);
			
			tip = new TipFree();
			tip.Link(p.LinePanel);
		}
	}
	
}
