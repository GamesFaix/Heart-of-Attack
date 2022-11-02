using UnityEngine;
using HOA.Tokens;
using HOA.Actions;

public enum TIP {NONE, CELL, DEST, REM, KING, TRAM, HEART, ONDEATH, AP, FP, IN, DEF, HP, SELF, NEIGHBOR, PATH, LINE, ARC, FREE, GLOBAL, UNIT}


public static class GUIToolTips {

	public static void Tip (Vector2 mousePos, TIP t) {
		if (t == TIP.CELL) {TipCell.Draw(mousePos);}
		else if (t == TIP.DEST) {TipDest.Draw(mousePos);}
		else if (t == TIP.REM) {TipRem.Draw(mousePos);}
		else if (t == TIP.KING) {TipKing.Draw(mousePos);}
		else if (t == TIP.TRAM) {TipTram.Draw(mousePos);}
		else if (t == TIP.HEART) {TipHeart.Draw(mousePos);}

		else if (t == TIP.ONDEATH) {TipOnDeath.Draw(mousePos);}
		else if (t == TIP.AP) {TipAP.Draw(mousePos);}
		else if (t == TIP.FP) {TipFP.Draw(mousePos);}
		else if (t == TIP.IN) {TipIN.Draw(mousePos);}
		else if (t == TIP.DEF) {TipDEF.Draw(mousePos);}
		else if (t == TIP.HP) {TipHP.Draw(mousePos);}

		else if (t == TIP.SELF) {TipSelf.Draw(mousePos);}
		else if (t == TIP.NEIGHBOR) {TipNeighbor.Draw(mousePos);}
		else if (t == TIP.PATH) {TipPath.Draw(mousePos);}
		else if (t == TIP.LINE) {TipLine.Draw(mousePos);}
		else if (t == TIP.ARC) {TipArc.Draw(mousePos);}
		else if (t == TIP.FREE) {TipFree.Draw(mousePos);}
		else if (t == TIP.GLOBAL) {TipGlobal.Draw(mousePos);}

		else if (t == TIP.UNIT) {TipUnit.Draw(mousePos);}

	}

	public static TIP AimType (AIMTYPE a) {
		if (a == AIMTYPE.SELF) {return TIP.SELF;}
		else if (a == AIMTYPE.NEIGHBOR) {return TIP.NEIGHBOR;}
		else if (a == AIMTYPE.PATH) {return TIP.PATH;}
		else if (a == AIMTYPE.LINE) {return TIP.LINE;}
		else if (a == AIMTYPE.ARC) {return TIP.ARC;}
		else if (a == AIMTYPE.FREE) {return TIP.FREE;}
		else if (a == AIMTYPE.GLOBAL) {return TIP.GLOBAL;}
		return TIP.NONE;

	}
}


