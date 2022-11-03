using UnityEngine; 


namespace HOA { 


	public class APanoCannon : Task {
		
		int damage = 12;
		
		public override string Desc {get {return "Do "+damage+" damage to target unit.  " +
				"\nMax range +1 per focus (up to +3).";} }
		
		public APanoCannon (Unit u, Price p, int damage) {
			Name = "Cannon";
			Weight = 3;
			
			Price = p;
			Parent = u;
			
			NewAim(new Aim(ETraj.ARC, EType.UNIT, 3, 2));
			this.damage = damage;
		}
		
		public override void Adjust () {
			int bonus = Mathf.Min(Parent.FP, 3);
			Aim[0] = new Aim (Aim[0].Trajectory, Aim[0].Special, Aim[0].Range+bonus, Aim[0].MinRange);
		}
		
		public override void UnAdjust () {
			Aim[0] = new Aim(ETraj.ARC, EType.UNIT, 3, 2);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EDamage(new Source(Parent), (Unit)targets[0], damage));
		}
	}
	public class APanoPierce : Task {
		
		int damage = 12;
		
		public override string Desc {get {return "Do "+damage+" damage to target unit (ignore defense).  " +
				"\nMax range +1 per focus (up to +3)."; } }
		
		public APanoPierce (Unit u, Price p, int damage) {
			Name = "Armor Pierce";
			Weight = 4;
			
			Price = p;
			Parent = u;
			
			NewAim(new Aim(ETraj.ARC, EType.UNIT, 3, 2));
			this.damage = damage;
		}
		
		public override void Adjust () {
			int bonus = Mathf.Min(Parent.FP, 3);
			Aim[0] = new Aim (Aim[0].Trajectory, Aim[0].Special, Aim[0].Range+bonus, Aim[0].MinRange);
		}
		
		public override void UnAdjust () {
			Aim[0] = new Aim(ETraj.ARC, EType.UNIT, 4, 3);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EPierce (new Source(Parent), (Unit)targets[0], damage));
		}
	}
	public class ADeciMortar : Task {
		
		public override string Desc {get {return "Do "+damage+" damage to all units in target cell. " +
				"\nAll units in neighboring cells take 50% damage (rounded down). " +
					"\nDamage continues to spread outward with 50% reduction until 1. " +
						"\nDestroy all destructible tokens that would take damage." +
						"\nRange +1 per Focus (up to 3)";} }
		
		int minRange, range; 
		int damage =18;
		
		public ADeciMortar (Unit parent) {
			Name = "Mortar";
			Weight = 4;
			Price = new Price(2,1);
			Parent = parent;
			NewAim(new Aim (ETraj.ARC, EType.CELL, EPurp.ATTACK, 3, 2));
		}
		
		public override void Adjust () {
			int bonus = Mathf.Min(Parent.FP, 3);
			Aim[0] = new Aim (Aim[0].Trajectory, Aim[0].Special, Aim[0].Range+bonus, Aim[0].MinRange);
		}
		
		public override void UnAdjust () {
			Aim[0] = new Aim(ETraj.ARC, EType.UNIT, 3, 2);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EExplosion(new Source(Parent), (Cell)targets[0], damage));
		}
	}

	public class ARookVolley : Task {
		
		int damage = 12;
		
		public override string Desc {get {return "Do "+damage+" damage to target unit." +
				"\nMay only be used if neighboring or sharing cell with non-Rook teammate." +
					"\nRange +1 per focus (up to 3).";} }
		
		public ARookVolley (Unit u) {
			Name = "Volley";
			Weight = 3;
			Parent = u;
			Price = Price.Cheap;
			NewAim(new Aim(ETraj.ARC, EType.UNIT, EPurp.ATTACK, 2, 2));
		}
		
		public override void Adjust () {
			int bonus = Mathf.Min(Parent.FP, 3);
			Aim[0] = new Aim (Aim[0].Trajectory, Aim[0].Special, Aim[0].Range+bonus);
		}
		
		public override void UnAdjust () {
			Aim[0] = new Aim(ETraj.ARC, EType.UNIT, EPurp.ATTACK, 2, 2);
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
			EffectQueue.Add(new EDamage(new Source(Parent), (Unit)targets[0], damage));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			DrawAim(0, p.LinePanel);
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), Desc);	
		}
	}
}
