using UnityEngine;

namespace HOA {
	public class CellDisplay : TargetDisplay {

		public Cell Cell {get {return (Cell)Parent;} }

		public virtual void EnterSunken (Token t) {
			if (t.Display != default(TokenDisplay)) {
				spriteCard.Show();
				spriteCard.Tex = t.Display.Sprite;
			}
		}
		public virtual void ExitSunken () {spriteCard.Hide();}

		public virtual Texture2D TerrainTex {
			get { 
				if ((Cell.X+Cell.Y)%2 == 0) {return Cell.Board.TileSet.Even;}
				else {return Cell.Board.TileSet.Odd;}
			} 
		}
	}
}