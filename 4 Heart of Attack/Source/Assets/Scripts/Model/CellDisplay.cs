using UnityEngine;

namespace HOA {
	public class CellDisplay : TargetDisplay {

		public Cell Cell {get {return (Cell)Parent;} }

		public virtual Texture2D TerrainTex {
			get { 
				if ((Cell.X+Cell.Y)%2 == 0) {return Cell.Board.TileSet.Even;}
				else {return Cell.Board.TileSet.Odd;}
			} 
		}

        public void OccupationSubscribe(object sender, OccupationEventArgs args)
        {
            if ((args.Token.Plane & Plane.Sunken) == Plane.Sunken)
            {
                if (args.Enter)
                {
                    if (args.Token.Display != default(TokenDisplay))
                    {
                        spriteCard.Show();
                        spriteCard.Tex = args.Token.Display.Sprite;
                    }
                }
                else
                    spriteCard.Hide();
            }
        }
       
	}
}