﻿using UnityEngine; 
using System.Collections.Generic;

namespace HOA.Actions { 

	public class Grow : Task {
		
		public override string Desc {get {return 
			"Switch cells with target Destructible. " +
			"\nRange +1 per focus.  " +
			"\n"+Parent+" +1 Focus.";} }
		
		public Grow (Unit u) {
			Name = "Grow";
			Weight = 2;
			Price = Price.Cheap;
			Parent = u;
			NewAim(Aim.AttackPath(Special.Dest, 1));
		}
		
		public override void Adjust () {Aims[0].Range += Parent.FP;}
		public override void UnAdjust () {Aims[0].Range = 1;}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Parent.AddStat(new Source(Parent), EStat.FP, 1, false);
			Token t = (Token)targets[0];
			Parent.Body.Swap(t);
			UnAdjust();
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			Aim actual = Aim.MovePath(Aims[0].Range+Parent.FP);
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), Desc);	
		}
	}

	public class VineWhip : Task {
			
		int damage = 18;
		
		public override string Desc {get {return 
			"Do "+damage+" damage target Unit." +
			"\nRange +1 per focus." +
			"\nIf target is killed and leaves Remains, switch cells with it's Remains.";} } 
		
		public VineWhip (Unit u) {
			Name = "Vine Whip";
			Weight = 4;
			Price = new Price(1,1);
			Parent = u;
			NewAim(Aim.AttackLine(Special.Unit, 2));
		}
		
		public override void Adjust () {Aims[0].Range += Parent.FP;}
		public override void UnAdjust () {Aims[0].Range = 2;}

		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new Effects.Damage(new Source(Parent), u, damage));
			Token dest;
			if (u.Body.Cell.Contains(ESpecial.DEST, out dest)) {Parent.Body.Swap(dest);}
			UnAdjust();
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			Aim actual = Aim.MovePath(Aims[0].Range+Parent.FP);
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), Desc);	
		}
	}

	public class Flail : Task {
		int damage = 8;
		
		public override string Desc {get {return 
			"Do "+damage+" damage to target unit.  " +
			"\nRange +1 per focus (Up to +3).  " +
			"\n"+Parent+" loses all focus.";} }
		
		public Flail (Unit u) {
			Name = "Flail";
			Weight = 3;
			Price = Price.Cheap;
			Parent = u;
			NewAim(Aim.AttackPath(Special.Unit, 1));
		}
		
		public override void Adjust () {Aims[0].Range += Mathf.Min(Parent.FP, 3);}
		public override void UnAdjust () {Aims[0].Range = 1;}

		protected override void ExecuteMain (TargetGroup targets) {
			Parent.SetStat(new Source(Parent), EStat.FP, 0, false);
			EffectQueue.Add(new Effects.Damage(new Source(Parent), (Unit)targets[0], damage));
			UnAdjust();
		}
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			Aim actual = Aim.AttackPath(Special.Unit, Aims[0].Range+Mathf.Min(3, Parent.FP));
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), Desc);	
		}
	}
	
	public class Slam : Task {
		int damage = 8;
		
		public override string Desc {get {return 
			"Do "+damage+" damage to target unit and each of its neighbors and cellmates.  " +
			"\nRange +1 per focus (up to +3).  " +
			"\n"+Parent+" loses all focus.";} }
		
		public Slam (Unit u) {
			Name = "Slam";
			Weight = 4;
			Price = new Price(2,0);
			Parent = u;
			NewAim(Aim.AttackPath(Special.Unit, 1));
		}
		
		public override void Adjust () {Aims[0].Range += Mathf.Min(Parent.FP, 3);}
		public override void UnAdjust () {Aims[0].Range = 1;}

		protected override void ExecuteMain (TargetGroup targets) {
			Parent.SetStat(new Source(Parent), EStat.FP, 0, false);
			
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new Effects.Damage(new Source(Parent), u, damage));
			
			TokenGroup neighbors = u.Body.Neighbors(true).OnlyType(ESpecial.UNIT);
			EffectGroup nextEffects = new EffectGroup();
			foreach (Unit u2 in neighbors) {
				nextEffects.Add(new Effects.Damage(new Source(Parent), u2, damage));
			}
			EffectQueue.Add(nextEffects);
			UnAdjust();
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			Aim actual = Aim.AttackPath(Special.Unit, Aims[0].Range+Mathf.Min(3, Parent.FP));
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), Desc);	
		}
	}
}
