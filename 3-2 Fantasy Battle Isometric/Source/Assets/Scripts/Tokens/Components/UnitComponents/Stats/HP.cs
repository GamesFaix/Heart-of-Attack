using UnityEngine; 

namespace HOA { 

	public class HP : Stat, IDeepCopyUnit<HP> {
		public HP (Unit parent, int normal) : base(parent, normal) {
			this.label = "Health";
			eStat = EStat.HP;
			eTip = ETip.HP;
			Min = 0;
			Max = Normal;
		}

		public HP DeepCopy (Unit parent) {return new HP (parent, Normal);}

		public override string ToString () {return "("+Current.ToString()+"/"+Max.ToString()+")";}
		
		public override int Modified () {
			int comparison = Current.CompareTo(Max);
			if (debuff) {comparison *= (-1);}
			return comparison;
		}
		
		public int MaxModified {
			get {
				int comparison = Max.CompareTo(Normal);
				if (debuff) {comparison *= (-1);}
				return comparison;
			} 
		}
		
		public override void Display (Panel p, float iconSize) {
			if (GUI.Button(p.FullBox, "")) {TipInspector.Inspect(eTip);}

			GUI.Box(p.Box(iconSize), Icons.Stats.health, p.s);
			p.NudgeX();
			p.NudgeY();
			
			
			GUI.Label(p.Box(7), "(", p.s);
			
			Color normColor = p.s.normal.textColor;
			if (Modified() > 0) {p.s.normal.textColor = Color.green;}
			else if (Modified() < 0) {p.s.normal.textColor = Color.red;}
			GUI.Label(p.Box(iconSize), Current.ToString(), p.s);
			p.s.normal.textColor = normColor;
			
			GUI.Label(p.Box(7), "/", p.s);
			
			if (MaxModified > 0) {p.s.normal.textColor = Color.green;}
			else if (MaxModified < 0) {p.s.normal.textColor = Color.red;}
			GUI.Label(p.Box(iconSize), Max.ToString(), p.s);
			p.s.normal.textColor = normColor;
			
			GUI.Label(p.Box(7), ")", p.s);
		}
	}
}
