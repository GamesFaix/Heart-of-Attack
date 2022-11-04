using System;
using UnityEngine;

namespace HOA {
	
	public class Health : TokenComponent, IDeepCopyUnit<Health>, IInspectable{
		
		public virtual Stat HP {get; protected set;}
		public string HPString {get {return "("+HP+"/"+HP.Max+")";} }

		public virtual Stat DEF {get; protected set;}

		public Health(Unit parent, int hp=0, int def=0) : base (parent) {
			HP = Stat.Health(parent, hp);
			DEF = Stat.Defense(parent, def);
		}

		public Health DeepCopy (Unit parent) {return new Health (parent, HP.Max, DEF);}

		protected void DieIfZero (Source source) {if (HP<1) {EffectQueue.Add(Effect.DestroyUnit(source, Parent));} }
	
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
				if (log) {GameLog.Out(Parent+" defended against all damage from "+source.ToString()+".");} 
				return false;
			}
			else {
				int dmg = n-DEF;
				HP.Add(source, 0-dmg, false);
				if (log) {
					if (DEF==0) {GameLog.Out(source.ToString()+" did "+dmg+" damage to "+Parent+". "+HPString);}
					if (DEF>0) {GameLog.Out(source.ToString()+" did "+dmg+" damage to "+Parent+". "+Parent+" defended against "+DEF+" damage. "+HPString);}
				}
				DieIfZero(source);
				return true;
			}
		}

        public override void Draw (Panel p) {InspectorInfo.Health(this, p);}

	}
}
