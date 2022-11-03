namespace HOA.Actions { 

	public class Maul : Task {
		
		int damage = 12;
		
		public override string Desc {get {return "Do "+damage+" damage to target unit.";} }
		
		public Maul (Unit u) {
			Name = "Strike";
			Weight = 3;
			Parent = u;
			Price = new Price(0,1);
			NewAim(Aim.AttackNeighbor(Special.Unit));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Damage (new Source(Parent), (Unit)targets[0], damage));
		}
	}
}
