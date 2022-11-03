using UnityEngine;

namespace HOA {
	
	public class TipToken : Tip{
		
		public TipToken () {
			Name = "Token";
			Icon = null;
			ETip = ETip.Token;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Tokens are any objects that exist on the board." +

			          "\n\nAll Tokens are one of two main types:", 
			          p.s);
			p.NextLine();
			p.NextLine();
			p.NextLine();
			p.NudgeX();
			Tip tip = new TipUnit();
			tip.Link(p.LinePanel);
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Units are player-controlled creatures and" +
			          "\nmachines.",
			          p.s);
			p.NextLine();
			p.NextLine();
			tip = new TipOb();
			tip.Link(p.LinePanel);
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Obstacles are less active: terrain, traps, etc.",
			          p.s);
			p.NextLine();
			p.NextLine();

			GUI.Label(p.Box(0.9f),
			          "Special Token types:",
			          p.s);
			p.NextLine();
			tip = new TipDest();
			tip.Link(p.LinePanel);
			
			tip = new TipKing();
			tip.Link(p.LinePanel);

			tip = new TipHeart();
			tip.Link(p.LinePanel);

			p.NextLine();
			
			GUI.Label(p.Box(0.9f),
			          "Token properties:",
			          p.s);
			p.NextLine();
			
			tip = new TipPlane();
			tip.Link(p.LinePanel);
			
			tip = new TipOnDeath();
			tip.Link(p.LinePanel);

			tip = new TipTram();
			tip.Link(p.LinePanel);

			tip = new TipSensor();
			tip.Link(p.LinePanel);
		}	
		
		public override void SeeAlso (Panel p) {

		}
	}
	
}
