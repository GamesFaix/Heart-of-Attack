using UnityEngine; 

namespace HOA { 

	public class HP: Stat {
		public HP (Unit parent, byte normal) {
			this.parent = parent;
			this.label = "Health";
			eStat = EStat.HP;
			eTip = ETip.HP;
			this.Normal = normal;
			Current = Normal;
			Min = 0;
			Max = Normal;
		}

		public override string ToString () {
			return "("+Current.ToString()+"/"+Max.ToString()+")";
		}

		public override int Modified {
			get {
				int comparison = Current.CompareTo(Max);
				if (debuff) {comparison *= (-1);}
				return comparison;
			} 
		}


		public int MaxModified {
			get {
				int comparison = Max.CompareTo(Normal);
				if (debuff) {comparison *= (-1);}
				return comparison;
			} 
		}

		public override void Display (Panel p, float iconSize) {
			GUI.Box(p.Box(iconSize), Icons.Stat(eStat), p.s);
			p.NudgeX();
			p.NudgeY();
			

			GUI.Label(p.Box(7), "(", p.s);

			Color normColor = p.s.normal.textColor;
			if (Modified > 0) {p.s.normal.textColor = Color.green;}
			else if (Modified < 0) {p.s.normal.textColor = Color.red;}
			GUI.Label(p.Box(iconSize), Current.ToString(), p.s);
			p.s.normal.textColor = normColor;

			GUI.Label(p.Box(7), "/", p.s);
			
			if (MaxModified > 0) {p.s.normal.textColor = Color.green;}
			else if (MaxModified < 0) {p.s.normal.textColor = Color.red;}
			GUI.Label(p.Box(iconSize), Max.ToString(), p.s);
			p.s.normal.textColor = normColor;

			GUI.Label(p.Box(7), ")", p.s);

			if (GUIInspector.ShiftMouseOver(p.FullBox)) {
				GUIInspector.Tip = eTip;
			}
		}

	}



	public class FPaddsIN: Stat {
		//+1 IN per Focus (no cap)
		public FPaddsIN (Unit parent) {
			this.parent = parent;
			label = "Focus";
			eStat = EStat.FP;
			eTip = ETip.FP;
			Normal = 0;
			Current = Normal;
			Min = -127;
			Max = 255;
			debuff = false;
		}
		public override int Add (Source source, int n, bool log=true) {
			Current += n;
			parent.AddStat(source, EStat.IN, n, log);
			Clamp();
			string sign = Sign(n);
			if (log) {GameLog.Out(source+": "+parent+" "+sign+n+label+". "+label+" = "+Current);}
			return Current;
		}
	}

	public class FPaddsDEF: Stat {
		//+1 DEF per Focus (with cap)

		byte cap;

		public FPaddsDEF (Unit parent, byte cap) {
			this.cap = cap;
			this.parent = parent;
			label = "Focus";
			eStat = EStat.FP;
			eTip = ETip.FP;
			Normal = 0;
			Current = Normal;
			Min = -127;
			Max = 255;
			debuff = false;
		}
		public override int Add (Source source, int n, bool log=true) {
			sbyte defChange = 0;
			if (n > 0) {
				for (sbyte i=1; i<=n; i++) {
					if (Current+i <= cap) {defChange++;}
				}
			}
			if (n < 0) {
				for (int i=1; i<=(-n); i++) {
					if (Current-i < cap) {defChange--;}
				}
			}
			parent.AddStat(source, EStat.DEF, defChange, log);

			Current += n;
			Clamp();
			string sign = Sign(n);
			if (log) {GameLog.Out(source+": "+parent+" "+sign+n+label+". "+label+" = "+Current);}
			return Current;
		}
	}

}
