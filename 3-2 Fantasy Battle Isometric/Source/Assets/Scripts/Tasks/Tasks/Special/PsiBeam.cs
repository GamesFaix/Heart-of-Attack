namespace HOA.Actions { 

	public class PsiBeam : Task {

		int damage = 12;
		
		public override string Desc {get {return "Do "+damage+" damage to target unit." +
				"\nTarget loses all Focus.";} } 
		
		public PsiBeam (Unit u) {
			Name = "Psi Beam";
			Weight = 4;
			
			Parent = u;
			Price = new Price(1,1);
			NewAim(HOA.Aim.Shoot(3));
			
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new Effects.Damage (new Source(Parent), u, damage));
			EffectQueue.Add(new Effects.AddStat (new Source(Parent), u, EStat.FP, 0-u.FP));
		}
	}
}
