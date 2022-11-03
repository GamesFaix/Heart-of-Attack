using System;
using UnityEngine;

namespace HOA {
	
	public class Health : IDeepCopyUnit<Health> {
		protected Unit parent;
		
		public virtual Stat HP {get; protected set;}
		public string HPString {get {return "("+HP+"/"+HP.Max+")";} }

		public virtual Stat DEF {get; protected set;}

		public Health () {}
		
		public Health(Unit u, int hp=0, int def=0){
			parent = u;
			HP = new HP(parent, hp);
			DEF = new DEF(parent, def);
		}

		public Health DeepCopy (Unit parent) {return new Health (parent, HP.Max, DEF);}

		protected void DieIfZero (Source source) {if (HP<1) {EffectQueue.Add(new Effects.Kill(source, parent));} }
	
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
}
