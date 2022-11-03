using UnityEngine;

namespace HOA {
	
	public class TipCOR : Tip{
		
		public TipCOR () {
			Name = "Corrosion";
			Icon = Icons.Effects.corrosive;
			ETip = ETip.COR;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Corrosion effects target Units." +

			          "\n\nThe target Unit takes the damage" +
			          "\nspecified by the effect and receives a " +
			          "\nCorrosion timer with a value equal to half " +
			          "\nthe Damage taken (rounded down)." +

			          "\n\nIf a Unit has a Corrosion Timer, at the " +
			          "\nend of its turn, it loses Health equal to " +
			          "\nthe value of the Timer, then Timer's " +
			          "\nvalue is halved (rounded down)." +

			          "\n\nIf the value of a Corrosion Timer is 0, it is " +
			          "\nremoved from the Unit.",
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipDamage();
			tip.Link(p.LinePanel);

			tip = new TipUnit();
			tip.Link(p.LinePanel);

			tip = new TipTimer();
			tip.Link(p.LinePanel);

		}
	}
}
