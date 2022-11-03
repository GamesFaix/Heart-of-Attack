using UnityEngine; 

namespace HOA { 
	public enum ETip {
		NONE, 
		CELL, TOKEN,
		DEST, REM, KING, TRAM, HEART, 
		ONDEATH, 
		AP, FP, IN, DEF, HP, 
		SELF, NEIGHBOR, PATH, LINE, ARC, FREE, 
		UNIT, OB,
		PLANE, DAMAGE, TIMER, SENSOR,
		STUN, SKIP, FIRE, EXP, COR
	}

	public abstract class Tip {
		public string Name {get; protected set;}
		public Texture2D Icon {get; protected set;}
		public ETip ETip {get; protected set;}

		public void Label (Panel p) {
			p.NudgeX();
			Rect box = p.IconBox;
			if (Icon != null) {
				GUI.Box(box, Icon, p.s);
			}
			p.NudgeX();
			GUI.Label(p.Box(70), Name, p.s);
		}

		public void Link (Panel p) {
			p.NudgeX();
			if (GUI.Button(p.Box(0.5f), "")) {TipInspector.Inspect(ETip);}
			p.ResetX();
			p.NudgeX();
			Rect box = p.IconBox;
			if (Icon != null) {
				GUI.Box(box, Icon, p.s);

			}
			p.NudgeX();p.NudgeY();
			GUI.Label(p.Box(0.5f), Name, p.s);
		}

		public abstract void Content (Panel p);

		public abstract void SeeAlso (Panel p);


		public static ETip Trajectory (ETraj a) {
			if (a == ETraj.SELF) {return ETip.SELF;}
			else if (a == ETraj.NEIGHBOR) {return ETip.NEIGHBOR;}
			else if (a == ETraj.PATH) {return ETip.PATH;}
			else if (a == ETraj.LINE) {return ETip.LINE;}
			else if (a == ETraj.ARC) {return ETip.ARC;}
			else if (a == ETraj.FREE) {return ETip.FREE;}
			return ETip.NONE;
		}
		
		public static ETip Special (EType special) {
			if (special == EType.KING) {return ETip.KING;}
			else if (special == EType.HEART) {return ETip.HEART;}
			else if (special == EType.DEST) {return ETip.DEST;}
			else if (special == EType.REM) {return ETip.REM;}
			else if (special == EType.TRAM) {return ETip.TRAM;}
			else if (special == EType.UNIT) {return ETip.UNIT;}
			else if (special == EType.OB) {return ETip.OB;}
			else if (special == EType.CELL) {return ETip.CELL;}
			return ETip.NONE;
		}

		public static Tip FromETip (ETip eTip) {
			switch (eTip) {
			case ETip.UNIT: return new TipUnit();
			case ETip.OB: return new TipOb();
			case ETip.KING: return new TipKing();
			case ETip.HEART: return new TipHeart();
			case ETip.DEST: return new TipDest();
			case ETip.REM: return new TipRem();
			case ETip.CELL: return new TipCell();
			case ETip.PLANE: return new TipPlane();
			case ETip.TRAM: return new TipTram();
			case ETip.AP: return new TipAP();
			case ETip.FP: return new TipFP();
			case ETip.HP: return new TipHP();
			case ETip.DEF: return new TipDEF();
			case ETip.IN: return new TipIN();
			case ETip.ONDEATH: return new TipOnDeath();
			case ETip.NEIGHBOR: return new TipNeighbor();
			case ETip.LINE: return new TipLine();
			case ETip.PATH: return new TipPath();
			case ETip.ARC: return new TipArc();
			case ETip.FREE: return new TipFree();
			case ETip.SELF: return new TipSelf();
			case ETip.TOKEN: return new TipToken();
			case ETip.DAMAGE: return new TipDamage();
			case ETip.TIMER: return new TipTimer();
			case ETip.SENSOR: return new TipSensor();
			default: return null;
			}
		}
	}
}
