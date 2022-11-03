using System;
using UnityEngine;

namespace HOA { 

	public enum EStat {HP, MHP, DEF, IN, AP, FP, STUN}

	public class Stat {
	
		protected Unit parent;
		protected string label;

		protected sbyte Min {get; set;}
		public byte Max {get; protected set;}
		public int Current {get; protected set;}
		public byte Normal {get; protected set;}
		protected bool debuff;
		protected EStat eStat;
		protected ETip eTip;

		protected Stat () {}

		Stat (Unit parent, string label, EStat eStat, ETip eTip, byte normal, sbyte min=-127, byte max=255, bool debuff=false) {
			this.parent = parent;
			this.label = label;
			this.eStat = eStat;
			this.eTip = eTip;
			Normal = normal;
			Current = Normal;
			Min = min;
			Max = max;
			this.debuff = debuff;
		}


		public static implicit operator int (Stat stat) {return stat.Current;}

		public override string ToString() {return Current.ToString();}

		public virtual int Modified {
			get {
				int comparison = Current.CompareTo(Normal);
				if (debuff) {comparison *= (-1);}
				return comparison;
			} 
		}
	
		public virtual int Add (Source source, int n, bool log=true) {
			Current += n;
			Clamp();
			string sign = Sign(n);
			if (log) {GameLog.Out(source+": "+parent+" "+sign+n+label+". "+label+" = "+Current);}
			return Current;
		}

		public int AddMax(Source source, byte n, bool log=true) {
			Max += n;
			Clamp();
			string sign = Sign(n);
			if (log) {GameLog.Out(source+": "+parent+" "+sign+n+" Max "+label+". "+label+" = "+Current+"/"+Max);}
			return Max;
		}

		public virtual int Set (int n) {
			Current = n;
			Clamp();
			return Current;
		}

		public int SetMax (byte n) {
			Max = n;
			Clamp();
			return Max;
		}

		protected void Clamp () {
			if (Current < Min) {Current = Min;}
			if (Current > Max) {Current = Max;}
		}

		protected string Sign (int n) {
			if (n>0) {return "+";}
			return "";
		}
	
		public virtual void Display (Panel p, float iconSize) {
			GUI.Box(p.Box(iconSize), Icons.Stat(eStat), p.s);
			p.NudgeX();
			p.NudgeY();

			Color normColor = p.s.normal.textColor;
			if (Modified > 0) {p.s.normal.textColor = Color.green;}
			else if (Modified < 0) {p.s.normal.textColor = Color.red;}
			GUI.Label(p.Box(iconSize), ToString(), p.s);
			p.s.normal.textColor = normColor;

			if (GUIInspector.ShiftMouseOver(p.FullBox)) {
				GUIInspector.Tip = eTip;
			}
		}

		public static Stat HP (Unit parent, byte normal) {return new HP (parent, normal);}
		public static Stat DEF (Unit parent, byte normal) {return new Stat (parent, "Defense", EStat.DEF, ETip.DEF, normal, 0);}
		public static Stat DEFCapped (Unit parent, byte normal, byte max) {return new Stat(parent, "Defense", EStat.DEF, ETip.DEF, normal, 0, max);}
		public static Stat DEFBonus (Unit parent, byte normal) {return new DEFBonus(parent, normal);}
		public static Stat AP (Unit parent, byte max) {return new Stat (parent, "Energy", EStat.AP, ETip.AP, 0,0, max);}
		public static Stat FP (Unit parent) {return new Stat (parent, "Focus", EStat.FP, ETip.FP, 0,0);}
		public static Stat FPaddsIN (Unit parent) {return new FPaddsIN (parent);}
		public static Stat FPaddsDEF (Unit parent, byte cap) {return new FPaddsDEF (parent, cap);}
		public static Stat IN (Unit parent, byte normal) {return new Stat (parent, "Initiative", EStat.IN, ETip.IN, normal);}
		public static Stat STUN (Unit parent) {return new Stat (parent, "Stun", EStat.STUN, ETip.NONE, 0,0,255,true);}

	}
}
