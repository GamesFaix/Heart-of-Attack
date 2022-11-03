using UnityEngine; 

namespace HOA.Actions { 

	public class Cannon : Task {
		
		int damage = 12;
		
		public override string Desc {get {return "Do "+damage+" damage to target unit.  " +
				"\nMax range +1 per focus (up to +3).";} }
		
		public Cannon (Unit u, Price p, int damage) {
			Name = "Cannon";
			Weight = 3;
			
			Price = p;
			Parent = u;
			
			NewAim(new Aim(ETraj.ARC, ESpecial.UNIT, 3, 2));
			this.damage = damage;
		}
		
		public override void Adjust () {
			int bonus = Mathf.Min(Parent.FP, 3);
			Aim[0] = new Aim (Aim[0].Trajectory, Aim[0].Special, Aim[0].Range+bonus, Aim[0].MinRange);
		}
		
		public override void UnAdjust () {
			Aim[0] = new Aim(ETraj.ARC, ESpecial.UNIT, 3, 2);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Damage(new Source(Parent), (Unit)targets[0], damage));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);
			
			Rect box = p.IconBox;
			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.DAMAGE);}
			GUI.Box(box,Icons.DMG(),p.s);
			p.NudgeX();
			GUI.Box(p.Box(30),damage.ToString(), p.s);
			p.NextLine();
			GUI.Label(p.Box(0.9f), "Max Range +1 per Focus (up to +3).");
		}
	}
	public class Pierce : Task {
		
		int damage = 12;
		
		public override string Desc {get {return "Do "+damage+" damage to target unit (ignore defense).  " +
				"\nMax range +1 per focus (up to +3)."; } }
		
		public Pierce (Unit u, Price p, int damage) {
			Name = "Armor Pierce";
			Weight = 4;
			
			Price = p;
			Parent = u;
			
			NewAim(new Aim(ETraj.ARC, ESpecial.UNIT, 3, 2));
			this.damage = damage;
		}
		
		public override void Adjust () {
			int bonus = Mathf.Min(Parent.FP, 3);
			Aim[0] = new Aim (Aim[0].Trajectory, Aim[0].Special, Aim[0].Range+bonus, Aim[0].MinRange);
		}
		
		public override void UnAdjust () {
			Aim[0] = new Aim(ETraj.ARC, ESpecial.UNIT, 4, 3);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Pierce (new Source(Parent), (Unit)targets[0], damage));
		}
	}
	public class Mortar : Task {
		
		public override string Desc {get {return "Do "+damage+" damage to all units in target cell. " +
				"\nAll units in neighboring cells take 50% damage (rounded down). " +
					"\nDamage continues to spread outward with 50% reduction until 1. " +
						"\nDestroy all destructible tokens that would take damage." +
						"\nRange +1 per Focus (up to 3)";} }
		
		int minRange, range; 
		int damage =18;
		
		public Mortar (Unit parent) {
			Name = "Mortar";
			Weight = 4;
			Price = new Price(2,1);
			Parent = parent;
			NewAim(new Aim (ETraj.ARC, ESpecial.CELL, EPurp.ATTACK, 3, 2));
		}
		
		public override void Adjust () {
			int bonus = Mathf.Min(Parent.FP, 3);
			Aim[0] = new Aim (Aim[0].Trajectory, Aim[0].Special, Aim[0].Range+bonus, Aim[0].MinRange);
		}
		
		public override void UnAdjust () {
			Aim[0] = new Aim(Aim[0].Trajectory, Aim[0].Special, 3, 2);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Explosion(new Source(Parent), (Cell)targets[0], damage));
		}
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);
			
			Rect box = p.IconBox;
			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.EXP);}
			GUI.Box(box,Icons.EXP(),p.s);
			p.NudgeX();
			GUI.Box(p.Box(30),damage.ToString(), p.s);
			
			p.NextLine();
			GUI.Label(p.LineBox, "Max Range +1 per Focus (up to +3).");
		}
	}

	public class Volley : Task {
		
		int damage = 12;
		
		public override string Desc {get {return "Do "+damage+" damage to target unit." +
				"\nMay only be used if neighboring or sharing cell with non-Rook teammate." +
					"\nRange +1 per focus (up to 3).";} }
		
		public Volley (Unit u) {
			Name = "Volley";
			Weight = 3;
			Parent = u;
			Price = Price.Cheap;
			NewAim(new Aim(ETraj.ARC, ESpecial.UNIT, EPurp.ATTACK, 2, 2));
		}
		
		public override void Adjust () {
			int bonus = Mathf.Min(Parent.FP, 3);
			Aim[0] = new Aim (Aim[0].Trajectory, Aim[0].Special, Aim[0].Range+bonus);
		}
		
		public override void UnAdjust () {
			Aim[0] = new Aim(ETraj.ARC, ESpecial.UNIT, EPurp.ATTACK, 2, 2);
		}
		
		public override bool Restrict () {
			TokenGroup neighbors = Parent.Body.Neighbors(true);
			for (int i=neighbors.Count-1; i>=0; i--) {
				Token t = neighbors[i];
				if (t.Owner == Parent.Owner 
				    && t.ID.Code != EToken.ROOK) {
					return false;
				}
			}
			return true;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Damage(new Source(Parent), (Unit)targets[0], damage));
		}

		public override void Draw (Panel p) {

			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);
		
			Rect box = p.IconBox;
			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.DAMAGE);}
			GUI.Box(box,Icons.DMG(),p.s);
			p.NudgeX();
			GUI.Box(p.Box(30),damage.ToString(), p.s);
			p.NextLine();
			GUI.Label(p.Box(0.9f), "Range +1 per Focus (up to +3).");
		}
	}
}
