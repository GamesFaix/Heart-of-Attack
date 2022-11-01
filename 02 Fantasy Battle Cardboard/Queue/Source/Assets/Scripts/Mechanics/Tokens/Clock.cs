using UnityEngine;
using System.Collections;

namespace Tokens {
	public class Clock{

		Unit parent;
		int init;
		int stun;
		bool skipped;
		int cor;

		public Clock(Unit u, int i=0){
			parent = u;
			init = i;
			stun = 0;
			skipped = false;
			cor = 0;
		}

		public int IN(){return init;}
		public int Stunned(){return stun;}
		public bool IsSkipped(){return skipped;}
		public bool IsStunned(){
			if (stun > 0) {return true;}
			return false;
		}
		public int COR(){return cor;}
		public bool IsCOR() {
			if (cor>0) {return true;}
			return false;
		}

		public int SetIN (int n, bool log=true){
			init = n;
			if (log) {GameLog.Out(parent+"'s IN set to "+init+".");}
			return init;
		}
		public int AddIN (int n, bool log=true){
			init += n;
			string sign = Sign(n);
			if (log) {GameLog.Out(parent+" "+sign+n+"IN. IN="+init);}
			return init;
		}

		public int SetStun (int n, bool log=true){
			stun = Clamp(n);
			if (log) {GameLog.Out(parent+"'s STUN set to "+stun+".");}
			return stun;
		}
		public int AddStun (int n, bool log=true){
			stun = Clamp(stun+n);
			string sign = Sign(n);
			if (log) {GameLog.Out(parent+" "+sign+n+"STUN. STUN="+stun);}
			return stun;
		}

		public void Skip(bool log=true) {
			skipped = true;
			if (log) {GameLog.Out(parent+" has been skipped in the Queue.");}
		}
		public void ClearSkip(bool log=true) {
			skipped = false;
			if (log) {GameLog.Out(parent+" is now skippable.");}
		}

		public int SetCOR (int n, bool log=true){
			cor = Clamp(n);
			if (log) {GameLog.Out(parent+"'s corrosion counters set to "+cor+".");}
			return cor;
		}
		public int AddCOR (int n, bool log=true){
			cor = Clamp(cor+n);
			string sign = Sign(n);
			if (log) {GameLog.Out(parent+" "+sign+n+"COR. COR="+cor);}
			return cor;
		}
		public int DecayCOR(bool log=true){
			int oldCor=cor;
			parent.AddHP(0-cor,false);
			cor = (int)Mathf.Floor(cor*0.5f);
			if (log) {GameLog.Out(parent+" takes "+oldCor+" corrision damage. HP:"+parent.HPString()+" COR:"+cor);}
			return cor;
		}

		int Clamp(int n) {
			if (n<0) {n=0;}
			return n;
		}

		string Sign (int n) {
			if (n>0) {return "+";}
			return "";
		}






	}
}
