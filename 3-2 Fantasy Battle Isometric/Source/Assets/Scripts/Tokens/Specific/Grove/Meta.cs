using System.Collections.Generic;

namespace HOA{
	public class Metaterrainean : Unit {
		public Metaterrainean(Source s, bool template=false){
			id = new ID(this, EToken.META, s, false, template);
			plane = Plane.Gnd;
			type.Add(EType.TRAM);
			onDeath = EToken.ROCK;

			ScaleLarge();
			NewHealth(50);
			NewWatch(1);
			BuildArsenal();
		}		

		protected override void BuildArsenal() {
			base.BuildArsenal();
			arsenal.Add(new Task[] {
				new AMovePath(this, 2),
				new AStrike(this, 20),
				new AMetaConsume(this)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}

	public class AMetaConsume : Task {

		public override string Desc {get {return "Destroy neighboring non-Remains destructible." +
				"\n"+Parent+" gains 12 health.";} }

		public AMetaConsume (Unit parent) {
			Name = "Consume Terrain";
			Weight = 4;
			Parent = parent;
			Price = new Price(1,1);
			AddAim(new Aim(ETraj.NEIGHBOR, EType.DEST));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Token t = (Token)targets[0];
			t.Die(new Source(Parent));
			Parent.AddStat(new Source(Parent), EStat.HP, 12);
			Parent.Display.Effect(EEffect.STATUP);
		}
	}
}