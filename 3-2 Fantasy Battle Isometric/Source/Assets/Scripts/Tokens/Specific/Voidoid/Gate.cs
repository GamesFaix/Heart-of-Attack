namespace HOA{
	public class Gatecreeper : Unit {
		public Gatecreeper(Source s, bool template=false){
			id = new ID(this, EToken.GATE, s, false, template);
			plane = Plane.Gnd;
			type.Add(EType.TRAM);

			ScaleLarge();
			NewHealth(30);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AGateBurrow(this),
				new AMonoReanimate(this, new Price(0,1)),
				new AGateFeast(this)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}

	public class AGateBurrow : Task {

		public override string Desc {get {return "Move "+Parent+" to target cell.";} }

		public AGateBurrow (Unit u) {
			Name = "Burrow";
			Weight = 1;
			AddAim(new Aim(ETraj.ARC, EType.CELL, EPurp.MOVE, 3, 0));
			Parent = u;
			Price = Price.Cheap;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EBurrow(new Source(Parent), Parent, (Cell)targets[0]));
		}
	}

	public class AGateFeast : Task {
		
		int damage = 12;

		public override string Desc {get {return "Do "+damage+" damage to target unit. " +
				"\nGain health equal to damage successfully dealt.";} }

		public AGateFeast (Unit u) {
			Name = "Feast";
			Weight = 3;
			Price = Price.Cheap;
			AddAim(HOA.Aim.Arc(3,2));
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ELeech(new Source(Parent), (Unit)targets[0], damage));
		}
	}
}
