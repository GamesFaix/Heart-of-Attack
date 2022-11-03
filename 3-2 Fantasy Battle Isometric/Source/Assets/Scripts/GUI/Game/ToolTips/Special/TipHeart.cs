using UnityEngine;

namespace HOA {
	
	public class TipHeart : Tip{
		
		public TipHeart () {
			Name = "Heart of Attack";
			Icon = Icons.Special(EType.HEART);
			ETip = ETip.HEART;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.TallWideBox(5), 
			          "Heart of Attacks are a class of unique " +
			          "\nObstacles, created on the death of Attack " +
			          "\nKings." +

			          "\n\nAttack Kings (and only Attack Kings) may " +
			          "\ncapture Heart of Attacks by entering the " +
			          "\nHeart's Cell. (This destroys the Heart.)" +

			          "\n\nCapturing a Heart of Attack gives the " +
			          "\ncapturing King control of any remaining " +
			          "\nUnits that were on the Heart's King's team.",
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipToken();
			tip.Link(p.LinePanel);

			tip = new TipOb();
			tip.Link(p.LinePanel);

			tip = new TipKing();
			tip.Link(p.LinePanel);

			tip = new TipOnDeath();
			tip.Link(p.LinePanel);
		}
	}
	
}
