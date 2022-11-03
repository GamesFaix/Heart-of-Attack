using System.Collections.Generic;

namespace HOA{

	public class Gargoliath : Unit {
		public Gargoliath(Source s, bool template=false){
			ID = new ID(this, EToken.GARG, s, true, template);
			Plane = Plane.Air;
			Special.Add(EType.KING);
			OnDeath = EToken.HSTO;
			ScaleJumbo();
			NewHealth(75);
			NewWatch(3);
			NewWallet(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal() {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new AMovePath(this, 4),
				new AStrike(this, 18),
				new AGargLand(this),
				new AGargPetrify(this),
				new AGargRook(this),
				new ACreate(this, Price.Cheap, EToken.SMAS),
				new ACreate(this, new Price(1,1), EToken.CONF),
				new ACreate(this, new Price(2,2), EToken.BATT)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}	


	
	public class AGargTailWhip : Task {
		int damage = 10;

		public override string Desc {get {return "Do "+damage+" damage to all neighboring units.";} }

		public AGargTailWhip (Unit u) {
			Name = "Tail Whip";
			Weight = 4;
			Price = new Price(1,1);
			Parent = u;
			NewAim(HOA.Aim.Self());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			TokenGroup neighbors = Parent.Body.Neighbors(false);
			neighbors = neighbors.OnlyType(EType.UNIT);
			foreach (Token t in neighbors) {
				Unit u = (Unit)t;
				EffectQueue.Add(new EDamage(new Source(Parent), u, damage));
			}
		}
	}
}