using UnityEngine;

namespace HOA {
	
	public class TipLine : Tip{
		
		public TipLine () {
			Name = "Line";
			Icon = Icons.Aims.line;
			ETip = ETip.LINE;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Actions targeting in a Line may select " +
			          "\ntargets in one of 8 directions. " +
			          "\n(Horizontal, vertical, and basic diagonals) " +

			          "\n\nLine targets may not be further from the " +
			          "\npeforming Unit than the aim's Range." +

			          "\n\nMovement actions targeting in a Line may " +
			          "\nnot target Cells beyond a token in the same " +
			          "\nPlane as the moving Unit." +

			          "\n\nAttacks targetting in a Line may not target " +
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
