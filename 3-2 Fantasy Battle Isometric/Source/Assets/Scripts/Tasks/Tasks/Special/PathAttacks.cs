using UnityEngine; 
using System.Collections.Generic;

namespace HOA.Actions { 

	public class Grow : Task {
		
		public override string desc {get {return 
			"Switch cells with target Destructible. " +
			"\nRange +1 per focus.  " +
			"\n"+parent+" +1 Focus.";} }
		
		public Grow (Unit parent) : base(parent) {
			name = "Grow";
			weight = 2;
			aims += Aim.AttackPath(Filters.Destructible, 1);
		}
		
		public override void Adjust () {aims[0].range += parent.FP;}
		public override void UnAdjust () {aims[0].range = 1;}
		
		protected override void ExecuteMain (TargetGroup targets) {
			parent.AddStat(source, EStat.FP, 1, false);
			Token t = (Token)targets[0];
			parent.Body.Swap(t);
			UnAdjust();
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			Aim actual = Aim.MovePath(aims[0].range+parent.FP);
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), desc);	
		}
	}

	public class VineWhip : Task {
			
		int damage = 18;
		
		public override string desc {get {return 
			"Do "+damage+" damage target Unit." +
			"\nRange +1 per focus." +
			"\nIf target is killed and leaves Remains, switch cells with it's Remains.";} } 
		
		public VineWhip (Unit parent) : base(parent) {
			name = "Vine Whip";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.AttackLine(Filters.Units, 2);
		}
		
		public override void Adjust () {aims[0].range += parent.FP;}
		public override void UnAdjust () {aims[0].range = 2;}

		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new Effects.Damage(source, u, damage));
			TokenGroup dests = (u.Body.Cell.Occupants.destructible)/Plane.Ground;
			if (dests.Count > 0) {parent.Body.Swap(dests[0]);}
			UnAdjust();
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			Aim actual = Aim.MovePath(aims[0].range+parent.FP);
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), desc);	
		}
	}

	public class Flail : Task {
		int damage = 8;
		
		public override string desc {get {return 
			"Do "+damage+" damage to target unit.  " +
			"\nRange +1 per focus (Up to +3).  " +
			"\n"+parent+" loses all focus.";} }
		
		public Flail (Unit parent) : base(parent) {
			name = "Flail";
			weight = 3;
			aims += Aim.AttackPath(Filters.Units, 1);
		}
		
		public override void Adjust () {aims[0].range += Mathf.Min(parent.FP, 3);}
		public override void UnAdjust () {aims[0].range = 1;}

		protected override void ExecuteMain (TargetGroup targets) {
			parent.SetStat(source, EStat.FP, 0, false);
			EffectQueue.Add(new Effects.Damage(source, (Unit)targets[0], damage));
			UnAdjust();
		}
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			Aim actual = Aim.AttackPath(Filters.Units, aims[0].range+Mathf.Min(3, parent.FP));
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), desc);	
		}
	}
	
	public class Slam : Task {
		int damage = 8;
		
		public override string desc {get {return 
			"Do "+damage+" damage to target unit and each of its neighbors and cellmates.  " +
			"\nRange +1 per focus (up to +3).  " +
			"\n"+parent+" loses all focus.";} }
		
		public Slam (Unit parent) : base(parent) {
			name = "Slam";
			weight = 4;
			price = new Price(2,0);
			aims += Aim.AttackPath(Filters.Units, 1);
		}
		
		public override void Adjust () {aims[0].range += Mathf.Min(parent.FP, 3);}
		public override void UnAdjust () {aims[0].range = 1;}

		protected override void ExecuteMain (TargetGroup targets) {
			parent.SetStat(source, EStat.FP, 0, false);
			
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new Effects.Damage(source, u, damage));
			
			TokenGroup neighbors = u.Body.Neighbors(true).units;
			EffectGroup nextEffects = new EffectGroup();
			foreach (Unit u2 in neighbors) {
				nextEffects.Add(new Effects.Damage(source, u2, damage));
			}
			EffectQueue.Add(nextEffects);
			UnAdjust();
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			Aim actual = Aim.AttackPath(Filters.Units, aims[0].range+Mathf.Min(3, parent.FP));
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), desc);	
		}
	}
}
