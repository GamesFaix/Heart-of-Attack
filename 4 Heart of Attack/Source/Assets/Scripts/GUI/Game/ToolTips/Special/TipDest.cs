using UnityEngine;

namespace HOA {
	
	public class TipDest : Tip{
		
		public TipDest () {
			Name = "Destructible";
			Icon = Icons.Destructible;
			ETip = ETip.DEST;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Destructible tokens can be destroyed by " +
			          "\nseveral means:" +
			          "\n\n-Fire attacks" +
			          "\n-Explosive attacks" +
			          "\n-Units with Trample entering their Cell" +
			          "\n-Any action specifically Targeting " +
			          "\nDestructible tokens",
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipToken();
			tip.Link(p.LinePanel);

			tip = new TipTram();
			tip.Link(p.LinePanel);
		}
	}
	
}
