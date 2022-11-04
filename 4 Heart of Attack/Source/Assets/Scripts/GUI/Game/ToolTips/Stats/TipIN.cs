using UnityEngine;

namespace HOA {
	
	public class TipIN : Tip{
		
		public TipIN () {
			Name = "Initiative";
			Icon = Icons.Stats[Stats.Initiative];
			ETip = ETip.IN;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Initiative affects how frequently a Unit takes " +
			          "\nturns." +

			          "\n\nWhen a Unit ends its turn, it moves to the" +
			          "\nbottom of the Queue, it may then skip over" +
			          "\nUnits already at the bottom by winning" +
			          "\nInitiative Battles against them." +

			          "\n\nDuring Initiative Battles, the Unit whose " +
			          "\nturn just ended is called the Initiator." +

			          "\n\nIn the first Initiative Battle at the end of a " +
			          "\nturn, the Initiator and the Unit above it each " +
			          "\npick a random number between 1 and its " +
			          "\nown Initiative. If the Initiator's number is " +
			          "\ngreater or tied, it skips over the Unit above " +
			          "\nit." +

			          "\n\nAfter each Initiative Battle, if the Initiator" +
			          "\nwon, its Initiative is temporarily decreased " +
			          "\nby 1 and it then Battles the next Unit above " +
			          "\nit." +
			          "\nIf the Initator loses a Battle, or its Initiative" +
			          "\nis 1, no more Battles occur and its Initiative" +
			          "\nis restored from any previous Battles." +

			          "\n\nUnits may not skip to the top of the Queue " +
			          "\nwith Initiative Battles. (No double turns.)",

			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			p.NextLine();
			p.NextLine();
			p.NextLine();
			p.NextLine();
			p.NextLine();
			Tip tip = new TipUnit();
			tip.Link(p.LinePanel);

		}

	}
	
}
