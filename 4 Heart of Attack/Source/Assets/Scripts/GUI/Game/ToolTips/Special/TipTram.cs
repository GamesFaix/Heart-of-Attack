using UnityEngine;

namespace HOA {
	
	public class TipTram : Tip{
		
		public TipTram () {
			Name = "Trample";
			Icon = Icons.TargetClass(TargetClasses.Tram);
			ETip = ETip.TRAM;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Trampling Units may move into Cells " +
			          "\nwhich already contain a token in the " +
			          "\nTrampling Unit's Plane(s), if the token " +
			          "\nin their Plane is Destructible. " +

			          "\n\nWhen a Trampling Unit enters a Cell, it " +
			          "\ndestroys all Destructible tokens in all " +
			          "\nPlanes." +

			          "\n\nIf a Trampling Unit enters a Cell with " +
			          "\na Destructible token in it's Plane(s), it " +
			          "\nmust stop moving.",
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipDest();
			tip.Link(p.LinePanel);

			tip = new TipUnit();
			tip.Link(p.LinePanel);
		}
	}
	
}
