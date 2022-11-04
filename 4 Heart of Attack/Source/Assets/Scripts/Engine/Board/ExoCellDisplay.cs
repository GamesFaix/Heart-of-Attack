using UnityEngine; 

namespace HOA { 

	public class ExoCellDisplay : CellDisplay {
	
		static Texture2D[] shadow = new Texture2D[8] {
			Resources.Load("Images/Textures/Cell/fade_top") as Texture2D,
			Resources.Load("Images/Textures/Cell/fade_bottom") as Texture2D,
			Resources.Load("Images/Textures/Cell/fade_left") as Texture2D,
			Resources.Load("Images/Textures/Cell/fade_right") as Texture2D,
			Resources.Load("Images/Textures/Cell/fade_top-left") as Texture2D,
			Resources.Load("Images/Textures/Cell/fade_top-right") as Texture2D,
			Resources.Load("Images/Textures/Cell/fade_bottom-left") as Texture2D,
			Resources.Load("Images/Textures/Cell/fade_bottom-right") as Texture2D
		};


		public override Texture2D TerrainTex {
			get { 
				if ((Cell.X+Cell.Y)%2 == 0) {return Cell.Board.TileSet.Odd;}
				else {return Cell.Board.TileSet.Even;}
			} 
		}
		public void AddShadow () {
			spriteCard.Show();
			spriteCard.Tex = Shadow(Cell);
		}

		static Texture2D Shadow (Cell cell) {
			size2 cellCount = cell.Board.CellCount;
			if (cell.X==0 && cell.Y==0) {return shadow[5];}
			if (cell.X==0 && cell.Y==cellCount.y-1) {return shadow[7];}
			if (cell.X==0) {return shadow[3];}
			if (cell.X==cellCount.x-1 && cell.Y==0) {return shadow[4];}
			if (cell.X==cellCount.x-1 && cell.Y==cellCount.y-1) {return shadow[6];}
			if (cell.X==cellCount.x-1) {return shadow[2];}
			if (cell.Y==0) {return shadow[0];}
			if (cell.Y==cellCount.y-1) {return shadow[1];}
			return null;
		}
	}
}
