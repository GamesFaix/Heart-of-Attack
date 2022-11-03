using System;
using UnityEngine;

namespace HOA { 

	public enum EStat {HP, MHP, DEF, IN, AP, FP, STUN}

	public abstract class Stat {
	
		protected Unit parent;
		protected string label;

		protected int Min {get; set;}
		public int Max {get; protected set;}
		public int Current {get; protected set;}
		public int Normal {get; protected set;}
		protected bool debuff;
		protected EStat eStat;
		protected ETip eTip;

		protected Stat () {}

		protected Stat (Unit parent, int normal) {
			this.parent = parent;
			Normal = normal;
			Current = Normal;
			debuff = false;
		}

		public static implicit operator int (Stat stat) {return stat.Current;}

		public override string ToString () {return Current.ToString();}

		public virtual int Modified () {
			int comparison = Current.CompareTo(Normal);
			if (debuff) {comparison *= (-1);}
			return comparison;
		}
	
		public virtual int Add (Source source, int n, bool log=true) {
			Current += n;
			Clamp();
			string sign = Sign(n);
			if (log) {GameLog.Out(source+": "+parent+" "+sign+n+label+". "+label+" = "+Current);}
			return Current;
		}

		public virtual int AddMax (Source source, int n, bool log=true) {
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

		public virtual int SetMax (int n) {
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
			if (GUI.Button(p.FullBox, "")) {TipInspector.Inspect(eTip);}
			GUI.Box(p.Box(iconSize), Icons.Stat(eStat), p.s);
			p.NudgeX();
			p.NudgeY();

			Color normColor = p.s.normal.textColor;
			if (Modified() > 0) {p.s.normal.textColor = Color.green;}
			else if (Modified() < 0) {p.s.normal.textColor = Color.red;}
			GUI.Label(p.Box(iconSize), ToString(), p.s);
			p.s.normal.textColor = normColor;
		}
	}
}
