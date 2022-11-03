namespace HOA.Actions { 

	public class Maul : Task {
		
		int damage = 12;
		
		public override string desc {get {return "Do "+damage+" damage to target unit.";} }
		
		public Maul (Unit parent) : base(parent) {
			name = "Strike";
			weight = 3;
			price = new Price(0,1);
			aims += Aim.AttackNeighbor(Filters.Units);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Damage (source, (Unit)targets[0], damage));
		}
	}
}
