namespace HOA.Actions { 

	public class PsiBeam : Task {

		int damage = 12;
		
		public override string desc {get {return "Do "+damage+" damage to target unit." +
				"\nTarget loses all Focus.";} } 
		
		public PsiBeam (Unit parent) : base(parent) {
			name = "Psi Beam";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.AttackLine(Filters.Units,3);
			
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new Effects.Damage (source, u, damage));
			EffectQueue.Add(new Effects.AddStat (source, u, EStat.FP, 0-u.FP));
		}
	}
}
