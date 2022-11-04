using UnityEngine;

using HOA.Textures;

namespace HOA {
	
	public class TipEXP : Tip{
		
		public TipEXP () {
			Name = "Explosives";
			Icon = Icons.EXP();
			ETip = ETip.EXP;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Explosive effects Target Cells." +
			          
			          "\n\nAll Units in the Target Cell take the " +
			          "\namount of Damage specified in the effect." +

			          "\n\nUnits in Cells Neighboring the Target take " +
			          "\nhalf the Damage of the effect (rounded " +
			          "\ndown)." +

			          "\n\nUnits in Cells Neighboring the previous" +
			          "\nCells take half of the previous Damage" +
			          "\n(rounded down)." +

			          "\n\nAll Destructibles in Cells where Damage " +
			          "\ncould have been dealt are destroyed.",
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipDamage();
			tip.Link(p.LinePanel);
			
			tip = new TipUnit();
			tip.Link(p.LinePanel);
			
			tip = new TipDest();
			tip.Link(p.LinePanel);
			
			tip = new TipNeighbor();
			tip.Link(p.LinePanel);
			
		}
	}
}
