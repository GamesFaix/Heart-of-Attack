using System;
using UnityEngine;

namespace HOA {
	
	public class Health{
		protected Unit parent;
		
		public virtual Stat HP {get; protected set;}
		public string HPString {get {return "("+HP+"/"+HP.Max+")";} }

		public virtual Stat DEF {get; protected set;}

		public Health () {}
		
		public Health(Unit u, byte hp=0, byte def=0){
			parent = u;
			HP = Stat.HP(parent, hp);
			DEF = Stat.DEF(parent, def);
		}

		protected void DieIfZero (Source source) {if (HP<1) {EffectQueue.Add(new EKill(source, parent));} }
	
		public virtual int AddHP (Source source, int n, bool log=true){
			HP.Add(source, n, log);
			DieIfZero(source);
			return HP;
		}
		public virtual int AddMaxHP (Source source, byte n, bool log=true){
			HP.AddMax(source, n, log);
			DieIfZero(source);
			return HP.Max;	
		}

		public virtual bool Damage(Source source, int n, bool log=true){
			if (n < 1) {
				if (log) {GameLog.Out("Less than 1 damage dealt");}
				return false;
			}
			else if (n <= DEF) {
				if (log) {GameLog.Out(parent+" defended against all damage from "+source.ToString()+".");} 
				return false;
			}
			else {
				int dmg = n-DEF;
				HP.Add(source, 0-dmg, false);
				if (log) {
					if (DEF==0) {GameLog.Out(source.ToString()+" did "+dmg+" damage to "+parent+". "+HPString);}
					if (DEF>0) {GameLog.Out(source.ToString()+" did "+dmg+" damage to "+parent+". "+parent+" defended against "+DEF+" damage. "+HPString);}
				}
				DieIfZero(source);
				return true;
			}
		}

		public virtual void Display (Panel p, float iconSize) {
			HP.Display (new Panel(p.Box(iconSize +95), p.LineH, p.s), iconSize);
			Rect defBox = p.Box(iconSize*2+5);

			if (DEF > 0) {DEF.Display(new Panel(defBox, p.LineH, p.s), iconSize);}
		}
	}

	public class HealthDEFCap : Health {
		byte cap;

		public HealthDEFCap (Unit parent, byte hp=0, byte def=0, byte defCap = 255) {
			this.parent = parent;
			this.cap = defCap;
			HP = Stat.HP(parent, hp);
			DEF = Stat.DEFCapped(parent, def, defCap);

		}

		public override void Display (Panel p, float iconSize) {
			HP.Display (new Panel(p.Box(iconSize +95), p.LineH, p.s), iconSize);
			Rect box = p.Box(iconSize*2+5);
			
			if (DEF > 0) {DEF.Display(new Panel(box, p.LineH, p.s), iconSize);}

			p.NudgeX(); p.NudgeX();
			iconSize = 20;
			GUI.Label(p.Box(30), "(Max");
			GUI.Box(p.Box(iconSize), Icons.Stat(EStat.DEF), p.s);
			GUI.Label(p.Box(40), "= "+cap+")");

		}
	}

	public class HealthHalfDodge : Health {

		public HealthHalfDodge (Unit u, byte hp=0, byte def=0){
			parent = u;
			HP = Stat.HP(parent, hp);
			DEF = Stat.DEF(parent, def);
		}

		public override bool Damage (Source source, int n, bool log=true) {
			int flip = DiceCoin.Throw(source, EDice.COIN);
			if (flip == 1) {return base.Damage(source, n, log);}
			else {
				GameLog.Out(source.ToString()+" tried to damage "+parent.ToString()+" and missed.");
				return false;
			}
		}

		public override void Display (Panel p, float iconSize) {
			HP.Display (new Panel(p.Box(iconSize +95), p.LineH, p.s), iconSize);
			Rect defBox = p.Box(iconSize*2+5);
			
			if (DEF > 0) {DEF.Display(new Panel(defBox, p.LineH, p.s), iconSize);}

			p.NudgeX(); p.NudgeX();p.NudgeY();
			GUI.Label(p.Box(200), "50% chance of taking no damage.");
		}
	}
}
