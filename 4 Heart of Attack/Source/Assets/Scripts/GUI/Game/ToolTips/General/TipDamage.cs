using UnityEngine;
using HOA.Textures;

namespace HOA {
	
	public class TipDamage : Tip{
		
		public TipDamage () {
			Name = "Damage";
			Icon = Icons.DMG();
			ETip = ETip.DAMAGE;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "When a Unit receives Damage, its Current " +
			          "\nHealth is reduced by the amount of Damage, " +
			          "\nunless the Unit has more then 0 Defense." +

			          "\n\nIf a Unit has Defense, the amount of " +
			          "\nDefense is subtracted from every Damage " +
			          "\nsource." +
			          "" +
			          "\n\nActions that 'reduce Health', but do not use " +
			          "\nthe word 'Damage' do not factor in Defense.",
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipUnit();
			tip.Link(p.LinePanel);
			
			tip = new TipHP();
			tip.Link(p.LinePanel);
			
			tip = new TipDEF();
			tip.Link(p.LinePanel);
		}
	}
	
}
