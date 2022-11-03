namespace HOA.Actions { 

	public class TouchOfDeath : Task {
		int damage = 16;
		
		public override string desc {get {return "Do "+damage+" damage to target unit." +
				"\nIf target has less than 10 health after damage is dealt, destroy target." +
				"\nBROKEN: If target is destroyed and is not an Attack King, it leaves no remains and you may place a Corpse in any cell.";
			} } 
		
		public TouchOfDeath (Unit parent) : base(parent) {
			name = "Touch of Death";
			weight = 3;
			aims += Aim.AttackNeighbor(Filters.Units);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			int oldHP = u.HP;
			int def = u.DEF;
			
			int dmg = damage - def;
			if (oldHP - dmg < 10) {dmg = oldHP;}
			if (dmg >= oldHP) {
				EffectQueue.Add(new Effects.Kill(source, u));
				Targeter.Start(new Actions.Exhume(parent));
				
			}
			else {
				EffectQueue.Add(new Effects.Damage(source, u, damage));
				Targeter.Reset();
			}
		}
		protected override void ExecuteFinish() {}
	}}
