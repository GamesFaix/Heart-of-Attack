using UnityEngine;

using HOA.Textures;

namespace HOA {
	
	public class TipFree : Tip{
		
		public TipFree () {
			Name = "Free";
			Icon = Icons.Trajectories[ETraj.FREE];
			ETip = ETip.FREE;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Actions with Free Targetting may select " +
			          "\nTargets anywhere on the board.",
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
			
			tip = new TipSelf();
			tip.Link(p.LinePanel);
		}
	}
	
}
