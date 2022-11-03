using UnityEngine; 

namespace HOA { 
	public enum ETip {
		NONE, 
		Cell, Token,
		Unit, Obstacle, King, Heart, Destructible, Trample, 
		ONDEATH, 
		AP, FP, IN, DEF, HP, 
		SELF, NEIGHBOR, PATH, LINE, ARC, FREE, 
		Plane, DAMAGE, TIMER, SENSOR,
		STUN, SKIP, FIR, EXP, COR
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


		public static ETip Trajectory (Trajectory traj) {
			switch (traj) {
			case HOA.Trajectory.Self: return ETip.SELF;
			case HOA.Trajectory.Neighbor: return ETip.NEIGHBOR;
			case HOA.Trajectory.Path: return ETip.PATH;
			case HOA.Trajectory.Line: return ETip.LINE;
			case HOA.Trajectory.Arc: return ETip.ARC;
			case HOA.Trajectory.Free: return ETip.FREE;
			default: return ETip.NONE;
			}
		}
		
		public static ETip TargetType (TargetTypes type) {
			switch (type) {
			case TargetTypes.Cell: {return ETip.Cell;}
			case TargetTypes.Unit: {return ETip.Unit;}
			case TargetTypes.Obstacle: {return ETip.Obstacle;}
			case TargetTypes.King: {return ETip.King;}
			case TargetTypes.Heart: {return ETip.Heart;}
			case TargetTypes.Destructible: {return ETip.Destructible;}
			case TargetTypes.Trample: {return ETip.Trample;}
			default: return ETip.NONE;
			}
		}

		public static Tip FromETip (ETip eTip) {
			switch (eTip) {
			case ETip.Cell: return new TipCell();
			case ETip.Token: return new TipToken();
			case ETip.Unit: return new TipUnit();
			case ETip.Obstacle: return new TipOb();
			case ETip.King: return new TipKing();
			case ETip.Heart: return new TipHeart();
			case ETip.Destructible: return new TipDest();
			case ETip.Trample: return new TipTram();
			case ETip.Plane: return new TipPlane();
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
			case ETip.DAMAGE: return new TipDamage();
			case ETip.TIMER: return new TipTimer();
			case ETip.SENSOR: return new TipSensor();
			case ETip.FIR: return new TipFIR();
			case ETip.EXP: return new TipEXP();
			case ETip.COR: return new TipCOR();
			default: return null;
			}
		}
	}
}
