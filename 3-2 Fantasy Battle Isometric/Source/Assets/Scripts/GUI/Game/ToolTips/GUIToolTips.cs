using UnityEngine;

namespace HOA {
	public enum ETip {NONE, CELL, DEST, REM, KING, TRAM, HEART, ONDEATH, AP, FP, IN, DEF, HP, SELF, NEIGHBOR, PATH, LINE, ARC, FREE, GLOBAL, UNIT}

	public static class GUIToolTips {

		public static void Tip (Vector2 mousePos, ETip t) {
			if (t == ETip.CELL) {TipCell.Draw(mousePos);}
			else if (t == ETip.DEST) {TipDest.Draw(mousePos);}
			else if (t == ETip.REM) {TipRem.Draw(mousePos);}
			else if (t == ETip.KING) {TipKing.Draw(mousePos);}
			else if (t == ETip.TRAM) {TipTram.Draw(mousePos);}
			else if (t == ETip.HEART) {TipHeart.Draw(mousePos);}

			else if (t == ETip.ONDEATH) {TipOnDeath.Draw(mousePos);}
			else if (t == ETip.AP) {TipAP.Draw(mousePos);}
			else if (t == ETip.FP) {TipFP.Draw(mousePos);}
			else if (t == ETip.IN) {TipIN.Draw(mousePos);}
			else if (t == ETip.DEF) {TipDEF.Draw(mousePos);}
			else if (t == ETip.HP) {TipHP.Draw(mousePos);}

			else if (t == ETip.SELF) {TipSelf.Draw(mousePos);}
			else if (t == ETip.NEIGHBOR) {TipNeighbor.Draw(mousePos);}
			else if (t == ETip.PATH) {TipPath.Draw(mousePos);}
			else if (t == ETip.LINE) {TipLine.Draw(mousePos);}
			else if (t == ETip.ARC) {TipArc.Draw(mousePos);}
			else if (t == ETip.FREE) {TipFree.Draw(mousePos);}
			else if (t == ETip.GLOBAL) {TipGlobal.Draw(mousePos);}

			else if (t == ETip.UNIT) {TipUnit.Draw(mousePos);}

		}

		public static ETip AimType (EAim a) {
			if (a == EAim.SELF) {return ETip.SELF;}
			else if (a == EAim.NEIGHBOR) {return ETip.NEIGHBOR;}
			else if (a == EAim.PATH) {return ETip.PATH;}
			else if (a == EAim.LINE) {return ETip.LINE;}
			else if (a == EAim.ARC) {return ETip.ARC;}
			else if (a == EAim.FREE) {return ETip.FREE;}
			else if (a == EAim.GLOBAL) {return ETip.GLOBAL;}
			return ETip.NONE;

		}
	}

}
