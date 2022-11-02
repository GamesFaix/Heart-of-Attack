using System;
using HOA.Players;

namespace HOA.Tokens.Components {
	
	public class Health{
		protected int hp;
		protected int max;
		protected int def;
		protected Unit parent;
		
		public Health () {}
		
		public Health(Unit u, int n=0, int d=0){
			parent = u;
			max = n;
			Fill();
			def = d;
		}

		public virtual int HP {
			get {return hp;} 
			set {
				hp = Clamp(value);
				if (Empty()) {InputBuffer.Submit(new RKill (new Source(parent), parent));}
			}
		}

		public virtual int MaxHP {
			get {return max;} 
			set {
				max = Clamp(value);
				if (Empty()) {InputBuffer.Submit(new RKill (new Source(parent), parent));}
			}
		}
		public virtual int DEF {
			get {return def;} 
			set {def = Clamp(value);}
		}

		public virtual string HPString {get {return "("+hp+"/"+max+")";} }
		public virtual string DEFString {get {return "("+def+")";} }

		public virtual void Fill () {hp=max;}

		public virtual int AddHP (Source s, int n, bool log=true){
			hp = Clamp(hp+n);
			string sign = Sign(n);
			int reduction;
			bool full = Full(out reduction);

			if (log) {
				if (full) {GameLog.Out(s.ToString()+":  "+parent+" "+sign+n+"HP. HP full. "+HPString);}
				else {GameLog.Out(s.ToString()+":  "+parent+" "+sign+n+"HP. "+HPString);}
			}	
			if (Empty()) {
				InputBuffer.Submit(new RKill (new Source(parent), parent));
			}
			return hp;
		}
		public virtual int AddMaxHP (Source s, int n, bool log=true){
			max = Clamp(max+n);
			string sign = Sign(n);
			int reduction;
			bool full = Full(out reduction);
			if (log) {
				if (full) {GameLog.Out(s.ToString()+": "+parent+" "+sign+n+" max HP. HP full. "+HPString);}
				else {GameLog.Out(s.ToString()+": "+parent+" "+sign+n+" max HP. "+HPString);}
			}
			if (Empty()) {
				InputBuffer.Submit(new RKill (new Source(parent), parent));
			}
			return max;	
		}

		public virtual int MultHP (Source s, float f, bool log=true){
			int oldHP = hp;
			hp = Clamp((int)Math.Ceiling(hp*f));
			int change = hp-oldHP;
			string sign = Sign(change);
			int reduction;
			bool full = Full(out reduction);
			
			if (log) {
				if (full) {GameLog.Out(s.ToString()+": "+parent+" "+sign+change+"HP. HP full. "+HPString);}
				else {GameLog.Out(s.ToString()+": "+parent+" "+sign+change+"HP. "+HPString);}
			}	
			if (Empty()) {
				InputBuffer.Submit(new RKill (new Source(parent), parent));
			}
			return hp;
		}
		public virtual int MultMaxHP (Source s, float f, bool log=true){
			int oldMax = max;
			max = Clamp((int)Math.Ceiling(max*f));
			int change = max-oldMax;
			string sign = Sign(change);
			int reduction;
			bool full = Full(out reduction);
			if (log) {
				if (full) {GameLog.Out(s.ToString()+": "+parent+" "+sign+change+" max HP. HP full. "+HPString);}
				else {GameLog.Out(s.ToString()+": "+parent+" "+sign+change+" max HP. "+HPString);}
			}
			if (Empty()) {
				InputBuffer.Submit(new RKill (new Source(parent), parent));
			}
			return max;

		}

		public virtual int AddDEF (Source s, int n, bool log=true){
			def = Clamp(def+n);
			string sign = Sign(n);
			if (log) {GameLog.Out(s.ToString()+": "+parent+" "+sign+n+"DEF. DEF="+DEFString);}
			return def;
		}

		public virtual int Damage(Source s, int n, bool log=true){
			if (n >= 0){
				if (n <= def) {
					if (log) {GameLog.Out(parent+" defended against all damage from "+s.ToString()+".");}
				}
				if (n > def){
					int dmg = n-def;
					hp -= dmg;
					if (log) {
						if (def==0) {GameLog.Out(s.ToString()+" did "+dmg+" damage to "+parent+". "+HPString);}
						if (def>0) {GameLog.Out(s.ToString()+" did "+dmg+" damage to "+parent+". "+parent+" defended against "+def+" damage. "+HPString);}
					}
				}
				else {GameLog.Debug("Units cannot take negative damage.");}
			}			
			if (Empty()) {
				InputBuffer.Submit(new RKill (new Source(parent), parent));
			}
			return hp;
		}

		protected int Clamp(int n){
			if(n<0){n=0;}
			return n;
		}

		protected bool Empty(){
			if (hp<1) {return true;}
			return false;
		}

		protected bool Full(out int reduction){
			reduction = 0;
			if (hp>max) {
				reduction = hp-max;
				Fill();
				return true;
			}
			return false;
		}
		protected string Sign (int n) {
			if (n>0) {return "+";}
			return "";
		}
	}
}
