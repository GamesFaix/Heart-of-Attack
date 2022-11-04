using UnityEngine;

namespace HOA {
	
	public class TipRem : Tip{
		
		public TipRem () {
			Name = "Remains";
			Icon = Icons.TargetClass(TargetClasses.Corpse);
			ETip = ETip.REM;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Remains are tokens created by the death " +
			          "\nof most Units.",
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipToken();
			tip.Link(p.LinePanel);
			
			tip = new TipOnDeath();
			tip.Link(p.LinePanel);
		}
	}
	
}
