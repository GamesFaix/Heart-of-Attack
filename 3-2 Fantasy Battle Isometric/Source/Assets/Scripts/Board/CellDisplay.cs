using UnityEngine;

namespace HOA {
	public class CellDisplay : TargetDisplay {

		public Cell Cell {get {return (Cell)Parent;} }

		public void EnterSunken (Token t) {
			if (t.Display != default(TokenDisplay)) {
				spriteCard.Show();
				spriteCard.Tex = t.Display.Sprite;
			}
		}
		public void ExitSunken () {spriteCard.Hide();}
	}
}