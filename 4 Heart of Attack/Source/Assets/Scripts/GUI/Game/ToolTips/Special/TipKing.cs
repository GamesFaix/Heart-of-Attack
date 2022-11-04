using UnityEngine;

namespace HOA {
	
	public class TipKing : Tip{
		
		public TipKing () {
			Name = "Attack King";
			Icon = Icons.TargetClasses[TargetClasses.King];
			ETip = ETip.KING;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Attack Kings are a class of unique Units." +

			          "\n\nEach player starts the game with only " +
			          "\nan Attack King." +

			          "\n\nIf an Attack King is killed, its controller " +
			          "\nis eliminated from the game." +

			          "\n\nAttack Kings leave a Heart of Attack in " +
			          "\ntheir Cell upon death, which may be " +
			          "\ncaptured by other Attack Kings." +

			          "\n\nAny remaining Units on the team of a dead " +
			          "\nAttack King remain on the board, but do " +
			          "\nnot take turns until that King's Heart has " +
			          "\nbeen captured." +

			          "\n\nAn Attack King may capture a Heart of " +
			          "\nAttack by moving into the Heart's Cell.  " +
			          "\nThis destroys the Heart and adds any " +
			          "\nremaining Units from the Heart's King's " +
			          "\nteam to the capturing King's team.",
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipToken();
			tip.Link(p.LinePanel);

			tip = new TipUnit();
			tip.Link(p.LinePanel);

			tip = new TipHeart();
			tip.Link(p.LinePanel);

			tip = new TipOnDeath();
			tip.Link(p.LinePanel);
		}
	}
	
}
