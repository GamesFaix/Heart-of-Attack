using UnityEngine;

namespace HOA {
	
	public class TipHP : Tip{
		
		public TipHP () {
			Name = "Health";
			Icon = Icons.Stats.health;
			ETip = ETip.HP;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Health determines how much of a beating " +
			          "\na Unit can take. " +

			          "\n\nAll Units have Health; all obstacles do not." +

			          "\n\nA Unit's Health is represented by two " +
			          "\nnumbers: " +
			          "\n(Current Health)/(Maximum Health)." +

			          "\n\nIf a Unit's Current Health is less than 1, " +
			          "\nit dies." +

			          "\n\nActions that increase a Unit's Health cannot " +
			          "\nincrease it beyond the Unit's Maximum " +
			          "\nHealth.",
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipUnit();
			tip.Link(p.LinePanel);

			tip = new TipDamage();
			tip.Link(p.LinePanel);

			tip = new TipDEF();
			tip.Link(p.LinePanel);
		}
	}
	
}
