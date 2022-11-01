using UnityEngine;
using System.Collections;

namespace Tokens {
	public class Health{
		int hp;
		int max;
		int def;
		Unit parent;

		public Health(Unit u, int n=0, int d=0){
			parent = u;
			max = n;
			Fill();
			def = d;
		}

		public int HP(){return hp;}
		public int MaxHP(){return max;}
		public int DEF(){return def;}

		public string HPString(){return "("+hp+"/"+max+")";}
		public string DEFString(){return "("+def+")";}

		public void Fill(){hp=max;}

		public int SetHP (int n, bool log=true){
			hp = Clamp(n);
			int reduction;
			bool full = Full(out reduction);
			if (log) { 
				if (full) {GameLog.Out(parent+" HP set to max. "+HPString());}
				else {GameLog.Out(parent+" HP set to "+HPString()+".");}
			}		
			if (Empty()) {Console.Submit("KILL "+parent);}
			return hp;
		}
		public int SetMaxHP (int n, bool log=true){
			max = Clamp(n);
			int reduce = 0;
			bool full = Full(out reduce);
			if (log) { 
				if (full) {GameLog.Out(parent+" max HP set below HP. -"+reduce+"HP. "+HPString());}
				else {GameLog.Out(parent+" max HP set to "+HPString()+".");}
			}
			if (Empty()) {Console.Submit("KILL "+parent);}
			return max;
		}
		public int AddHP (int n, bool log=true){
			hp = Clamp(hp+n);
			string sign = Sign(n);
			int reduction;
			bool full = Full(out reduction);

			if (log) {
				if (full) {GameLog.Out(parent+" "+sign+n+"HP. HP full. "+HPString());}
				else {GameLog.Out(parent+" "+sign+n+"HP. "+HPString());}
			}	
			if (Empty()) {Console.Submit("KILL "+parent);}
			return hp;
		}
		public int AddMaxHP (int n, bool log=true){
			max = Clamp(max+n);
			string sign = Sign(n);
			int reduction;
			bool full = Full(out reduction);
			if (log) {
				if (full) {GameLog.Out(parent+" "+sign+n+" max HP. HP full. "+HPString());}
				else {GameLog.Out(parent+" "+sign+n+" max HP. "+HPString());}
			}
			if (Empty()) {Console.Submit("KILL "+parent);}
			return max;	
		}

		public int MultHP (float f, bool log=true){
			int oldHP = hp;
			hp = Clamp((int)Mathf.Ceil(hp*f));
			int change = hp-oldHP;
			string sign = Sign(change);
			int reduction;
			bool full = Full(out reduction);
			
			if (log) {
				if (full) {GameLog.Out(parent+" "+sign+change+"HP. HP full. "+HPString());}
				else {GameLog.Out(parent+" "+sign+change+"HP. "+HPString());}
			}	
			if (Empty()) {Console.Submit("KILL "+parent);}
			return hp;
		}
		public int MultMaxHP (float f, bool log=true){
			int oldMax = max;
			max = Clamp((int)Mathf.Ceil(max*f));
			int change = max-oldMax;
			string sign = Sign(change);
			int reduction;
			bool full = Full(out reduction);
			if (log) {
				if (full) {GameLog.Out(parent+" "+sign+change+" max HP. HP full. "+HPString());}
				else {GameLog.Out(parent+" "+sign+change+" max HP. "+HPString());}
			}
			if (Empty()) {Console.Submit("KILL "+parent);}
			return max;

		}


		public int SetDEF (int n, bool log=true){
			def = Clamp(n);
			if (log) {GameLog.Out(parent+"'s DEF set to "+DEFString()+".");}
			return def;
		}
		public int AddDEF (int n, bool log=true){
			def = Clamp(def+n);
			string sign = Sign(n);
			if (log) {GameLog.Out(parent+" "+sign+n+"DEF. DEF="+DEFString());}
			return def;
		}

		public int Damage(int n, bool log=true){
			if (n >= 0){
				if (n <= def) {
					if (log) {GameLog.Out(parent+" defended against all damage. "+HPString());}
				}
				if (n > def){
					int dmg = n-def;
					hp -= dmg;
					if (log) {
						if (def==0) {GameLog.Out(parent+" took "+dmg+" damage. "+HPString());}
						if (def>0) {GameLog.Out(parent+" defended against "+def+", took "+dmg+" damage. "+HPString());}
					}
				}
				else {GameLog.Debug("Units cannot take negative damage.");}
			}			
			if (Empty()) {Console.Submit("KILL "+parent);}
			return hp;
		}

		int Clamp(int n){
			if(n<0){n=0;}
			return n;
		}

		bool Empty(){
			if (hp<1) {return true;}
			return false;
		}

		bool Full(out int reduction){
			reduction = 0;
			if (hp>max) {
				reduction = hp-max;
				Fill();
				return true;
			}
			return false;
		}
		string Sign (int n) {
			if (n>0) {return "+";}
			return "";
		}
	}
}
