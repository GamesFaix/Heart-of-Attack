using System;

namespace HOA {
	
	public class Watch{

		protected Unit parent;
		protected int init;
		protected int stun;
		protected bool skipped;
		protected int cor;
		
		public Watch () {}
		
		public Watch(Unit u, int i=0){
			parent = u;
			init = i;
			stun = 0;
			skipped = false;
			cor = 0;
		}

		public virtual int IN {
			get {return init;}
			set {init = value;}
		}

		public int AddIN (Source s, int n, bool log=true){
			init += n;
			string sign = Sign(n);
			if (log) {GameLog.Out(s+": "+parent+" "+sign+n+"IN. IN="+init);}
			return init;
		}

		public int STUN {
			get {return stun;}
			set {stun = Clamp(value);}
		}

		public bool IsStunned(){
			if (stun > 0) {return true;}
			return false;
		}

		public int AddSTUN (Source s, int n, bool log=true){
			stun = Clamp(stun+n);
			string sign = Sign(n);
			if (log) {GameLog.Out(s+": "+parent+" "+sign+n+"STUN. STUN="+stun);}
			return stun;
		}

		public bool IsSkipped(){return skipped;}

		public void Skip(bool log=true) {
			skipped = true;
			if (log) {GameLog.Out(parent+" has been skipped in the Queue.");}
		}
		public void ClearSkip(bool log=true) {
			skipped = false;
			if (log) {GameLog.Out(parent+" is now skippable.");}
		}

		public int COR {
			get {return cor;}
			set {cor = value;}
		}
		
		public bool IsCOR() {
			if (cor>0) {return true;}
			return false;
		}

		public int AddCOR (Source s, int n, bool log=true){
			cor = Clamp(cor+n);
			string sign = Sign(n);
			if (log) {GameLog.Out(s+": "+parent+" "+sign+n+"COR. COR="+cor);}
			return cor;
		}
		public int DecayCOR(bool log=true){
			int oldCor=cor;
			parent.AddStat(new Source(), EStat.HP, 0-cor, false);
			cor = (int)Math.Floor(cor*0.5f);
			if (log) {GameLog.Out(parent+" takes "+oldCor+" corrision damage. HP:"+parent.HPString+" COR:"+cor);}
			return cor;
		}

		protected int Clamp(int n) {
			if (n<0) {n=0;}
			return n;
		}

		protected string Sign (int n) {
			if (n>0) {return "+";}
			return "";
		}
	}
}
