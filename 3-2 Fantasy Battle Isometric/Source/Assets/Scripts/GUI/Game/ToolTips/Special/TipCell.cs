using UnityEngine;

namespace HOA {
	
	public class TipCell : Tip{
		
		public TipCell () {
			Name = "Cell";
			Icon = Icons.Types.cell;
			ETip = ETip.Cell;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.TallWideBox(5), 
			          "",
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipPlane();
			tip.Link(p.LinePanel);
		}
	}
	
}
