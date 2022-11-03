using System.Collections.Generic;

namespace HOA{

	public class Gargoliath : Unit {
		public Gargoliath(Source s, bool template=false){
			id = new ID(this, EToken.GARG, s, true, template);
			plane = Plane.Air;
			type.Add(EType.KING);
			onDeath = EToken.HSTO;
			ScaleJumbo();
			NewHealth(75);
			NewWatch(3);
			NewWallet(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal() {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMovePath(this, 4),
				new AStrike(this, 18),
				new AGargLand(this),
				new AGargPetrify(this),
				new AGargRook(this),
				new ACreate(this, Price.Cheap, EToken.SMAS),
				new ACreate(this, new Price(1,1), EToken.CONF),
				new ACreate(this, new Price(2,2), EToken.BATT)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}	

	public class AGargLand : Task {

		public override string Desc {get {return "Becomes trampling ground unit. " +
				"\nMove range -2 " +
				"\nDefense +2" +
				"\nForget 'Create Rook' " +
				"\nLearn 'Tail Whip'";} }

		public AGargLand (Unit u) {
			Name = "Land";
			Weight = 4;
			Parent = u;
			Price = new Price(1,1);
			AddAim(HOA.Aim.Self());
		}

		public override bool Restrict () {
			if (!Parent.Body.Cell.Contains(EPlane.GND)) {return false;}
			Token t;
			if (Parent.Body.Cell.Contains(EPlane.GND, out t)) {
				if (t.Special.Is(EType.DEST)) {return false;}
			}
			return true;
		}

		protected override void ExecuteMain (TargetGroup targets) {
			Token t;
			if (Parent.Body.Cell.Contains(EPlane.GND, out t)) {
				if (t.Special.Is(EType.DEST)) {
					EffectQueue.Add(new EDestruct(new Source(Parent), t));
				}
			}
			
			EffectQueue.Add(new EAddStat(new Source(Parent), Parent, EStat.DEF, 2));
			Parent.Plane.Set(EPlane.GND);

			Cell cell = Parent.Body.Cell;
			Parent.Body.Exit();
			Parent.Body.Enter(cell);

			Parent.Special.Set(new List<EType> {EType.UNIT, EType.KING, EType.TRAM});

			Parent.Arsenal().Replace("Move", new AMovePath(Parent, 3));
			Parent.Arsenal().Replace("Land", new AGargFly(Parent));
			Parent.Arsenal().Replace("Create Rook", new AGargTailWhip(Parent));
			Parent.Arsenal().Sort();
			
			Parent.Display.Effect(EEffect.STATUP);
		}
	}
	public class AGargFly : Task {

		public override string Desc {get {return "Becomes air unit. " +
				"\nMove range +2" +
				"\nDefense -2" +
				"\nForget 'Tail Whip'" +
				"\nLearn 'Create Rook'";} }

		public AGargFly (Unit u) {
			Name = "Take Flight";
			Weight = 4;
					Parent = u;
			Price = new Price(1,1);
			AddAim(HOA.Aim.Self());
			
		}
		public override bool Restrict () {
			if (Parent.Body.Cell.Contains(EPlane.AIR)) {return true;}
			return false;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EAddStat(new Source(Parent), Parent, EStat.DEF, -2));
			Parent.Plane.Set(EPlane.AIR);
			Cell cell = Parent.Body.Cell;
			Parent.Body.Exit();
			Parent.Body.Enter(cell);

			Parent.Special.Set(new List<EType> {EType.UNIT, EType.KING, EType.TRAM});

			Parent.Arsenal().Replace("Move", new AMovePath(Parent, 5));
			Parent.Arsenal().Replace("Take Flight", new AGargLand(Parent));
			Parent.Arsenal().Replace("Tail Whip", new AGargRook(Parent));
			Parent.Arsenal().Sort();

			Parent.Display.Effect(EEffect.STATUP);
		}
	}
	
	public class AGargTailWhip : Task {
		int damage = 10;

		public override string Desc {get {return "Do "+damage+" damage to all neighboring units.";} }

		public AGargTailWhip (Unit u) {
			Name = "Tail Whip";
			Weight = 4;
			Price = new Price(1,1);
			Parent = u;
			AddAim(HOA.Aim.Self());
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

	public class AGargRook : Task {

		public override string Desc {get {return "Create Rook in "+Parent+"'s cell.";} } 

		public AGargRook (Unit par) {
			Name = "Build Rook";
			Weight = 5;
			Parent = par;
			Price = new Price(1,1);
			AddAim(HOA.Aim.Self());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			if (!Parent.Body.Cell.Occupied(EPlane.GND)) {
				Charge();
				TokenFactory.Add(EToken.ROOK, new Source(Parent), Parent.Body.Cell);
			}
		}
	}

	public class AGargPetrify : Task {
		
		int damage = 15;

		public override string Desc {get {return "Target Unit takes "+damage+" damage and cannot move on its next turn.";} }

		public AGargPetrify (Unit u) {
			Name = "Petrify";
			Weight = 4;
			Parent = u;
			Price = new Price(1,1);
			AddAim(HOA.Aim.Shoot(2));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new EDamage (new Source(Parent), u, damage));
			if (u.Arsenal().Move != default(Task)) {
				Task move = u.Arsenal().Move;
				u.timers.Add(new TPetrify(u, Parent, move));
				u.Arsenal().Remove(move);
			}
		}
	}
	public class TPetrify : Timer {
		
		Task move;

		public TPetrify (Unit par, Token s, Task m) {
			parent = par;
			turns = 1;
			move = m;
			name = "Petrified";
			desc = parent.ToString()+" cannot move on its next turn.";
		}
		
		public override void Activate () {
			parent.Arsenal().Add(move);
			parent.Arsenal().Sort();
		}
	}
}