using UnityEngine;

namespace HOA {
	
	public class TipAP : Tip{
		
		public TipAP () {
			Name = "Energy";
			Icon = Icons.Stat(Stats.Energy);
			ETip = ETip.AP;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Energy is a short-term resource used for " +
			          "\nperforming actions." +

			          "\n\nUnits receive Energy at the start of their " +
			          "\nturn, and lose all unspent Energy at the end " +
			          "\nof their turn." +

			          "\n\nMost units recieve 2 Energy per turn, " +
			          "\nAttack Kings receive 3.",
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipUnit();
			tip.Link(p.LinePanel);

			tip = new TipFP();
			tip.Link(p.LinePanel);
		}
	}
	
}
