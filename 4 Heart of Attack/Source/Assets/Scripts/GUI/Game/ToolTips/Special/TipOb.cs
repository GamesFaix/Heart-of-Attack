using UnityEngine;

namespace HOA {
	
	public class TipOb : Tip{
		
		public TipOb () {
			Name = "Obstacle";
			Icon = Icons.Ob;
			ETip = ETip.OB;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Obstacles are Tokens that: " +
			          "\n-Cannot be directly controlled by players" +
			          "\n-Do not take turns" +
			          "\n-Cannot acquire Energy and Focus" +
			          "\n-Do not have Health", 
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipToken();
			tip.Link(p.LinePanel);
		}
	}
	
}
