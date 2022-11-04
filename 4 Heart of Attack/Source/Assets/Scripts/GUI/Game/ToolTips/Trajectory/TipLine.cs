using UnityEngine;

using HOA.Textures;

namespace HOA {
	
	public class TipLine : Tip{
		
		public TipLine () {
			Name = "Line";
			Icon = Icons.Trajectories[ETraj.LINE];
			ETip = ETip.LINE;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Actions Targeting in a Line may select " +
			          "\nTargets in one of 8 directions. " +
			          "\n(Horizontal, vertical, and basic diagonals) " +

			          "\n\nLine Targets may not be further from the " +
			          "\npeforming Unit than the aim's Range." +

			          "\n\nMovement actions Targeting in a Line may " +
			          "\nnot Target Cells beyond a token in the same " +
			          "\nPlane as the moving Unit." +

			          "\n\nAttacks Targetting in a Line may not Target " +
			          "\ntokens or Cells beyond any token, except " +
			          "\nSunken tokens.",
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipArc();
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
