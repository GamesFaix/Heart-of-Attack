using UnityEngine;

namespace HOA {
	
	public class TipDEF : Tip{
		
		public TipDEF () {
			Name = "Defense";
			Icon = Icons.Stat(Stats.Defense);
			ETip = ETip.DEF;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Defense reduces the damage a Unit " +
			          "\nreceives." +

			          "\n\nWhen an action 'deals damage' to a Unit, " +
			          "\nthe damaged Unit's Current Health is " +
			          "\nlowered by the amount of damage minus " +
			          "\nthe damaged Unit's Defense.  " +
			          "\n(Health loss) = (Damage) - (Defense)" +

			          "\n\nIf an action causes a Unit to 'lose Health', " +
			          "\nDefense is not taken into account." +

			          "\n\nMost Units have 0 Defense; Defense is " +
			          "\nonly displayed if greater than 0.",
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipUnit();
			tip.Link(p.LinePanel);

			tip = new TipHP();
			tip.Link(p.LinePanel);

			tip = new TipDamage();
			tip.Link(p.LinePanel);

		}
	}
	
}
