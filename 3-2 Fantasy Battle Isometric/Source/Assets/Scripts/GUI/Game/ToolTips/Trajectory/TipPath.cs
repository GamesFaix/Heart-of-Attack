using UnityEngine;

namespace HOA {
	
	public class TipPath : Tip{
		
		public TipPath () {
			Name = "Path";
			Icon = Icons.Aims.path;
			ETip = ETip.PATH;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Actions targeting in a Path may target " +
			          "\n'around corners'." +

			          "\n\nMovement actions targeting in a Path " +
			          "\nrequire the selection of each Cell to move " +
			          "\nthrough, each successive selection can be " +
			          "\nany Cell touching the last selection, as " +
			          "\nlong as there are no tokens occupying the " +
			          "\nmoving token's Plane." +
			          "\nThe number of Cells selected cannot " +
			          "\nexceed the aim's Range." +

			          "\n\nAttacks targeting in a Path only require " +
			          "\nthe selection of one target, which can be " +
			          "\nselected 'around corners' in any " +
			          "\ncombination of directions, so long as the " +
			          "\nnumber of Cells in the path is less than " +
			          "\nthe aim's Range, and only empty Cells or " +
			          "\nCells with Sunken tokens are passed " +
			          "\nthrough.",

			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipArc();
			tip.Link(p.LinePanel);
			
			tip = new TipLine();
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
