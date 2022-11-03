using System.Collections.Generic;

namespace HOA{
	public class Reprospector : Unit {
		public Reprospector(Source s, bool template=false){
			id = new ID(this, EToken.REPR, s, false, template);
			plane = Plane.Gnd;
			ScaleLarge();

			NewHealth(55);
			NewWatch(2);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[] {
				new AMovePath(this, 4),
				new AReprMine(this),
				new AReprSlam(this),
				new AReprBomb(this)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}

	public class AReprMine : Task {

		public override string Desc {get {return "Destroy neighboring destructible." +
				"\nIf initative is less than 6, initiative +1.";} }

		public AReprMine (Unit parent) {
			Name = "Time Mine";
			Weight = 4;
			Parent = parent;
			Price = Price.Cheap;
			AddAim(new Aim(ETraj.NEIGHBOR, Special.DestRem));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Token t = (Token)targets[0];
			Cell c = t.Body.Cell;

			EffectQueue.Add(new EDestruct(new Source(Parent), t));

			EffectGroup nextEffects = new EffectGroup();

			if (Parent.IN < 7) {
				nextEffects.Add(new EAddStat(new Source(Parent), Parent, EStat.IN, 1));
			}
			if (Parent.Body.CanEnter(c)) {
				nextEffects.Add(new EMove(new Source(Parent), Parent, c));
			}

			if (nextEffects.Count > 0) {EffectQueue.Add(nextEffects);}
		}
	}

	public class AReprSlam : Task {

		public override string Desc {get {return "Target Unit takes "+damage+" damage and loses 2 Initiative for 2 turns." +
				"\n"+Parent.ID.Name+" switches cells with target, if legal.";} }

		int damage = 15;
		
		public AReprSlam (Unit parent) {
			Parent = parent;
			Name = "Time Slam";
			Weight = 4;
			Price = new Price(1,0);
			AddAim(HOA.Aim.Melee());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];

			EffectGroup effects = new EffectGroup();

			effects.Add(new ESwap(new Source(Parent), Parent, u));
			effects.Add(new EDamage (new Source(Parent), u, damage));

			EffectQueue.Add(effects);
			EffectQueue.Add(new EAddStat (new Source(Parent), u, EStat.IN, -2));

			u.timers.Add(new TSlam(u, Parent));
		}
	}
	public class TSlam : Timer {
		Token source;
		
		public TSlam (Unit par, Token s) {
			parent = par;
			source = s;
			
			turns = 2;
			
			name = "Time Slammed";
			desc = parent.ToString()+" Initiative -2 for 2 turns.";
		}
		
		public override void Activate () {
			parent.AddStat(new Source(source), EStat.IN, 2);
		}
	}

	public class AReprBomb : Task {

		public override string Desc {get {return "All Units in target cell take "+damage+" damage and lose 2 Initiative for 2 turns. " +
				"\nAll units in neighboring cells take 50% damage (rounded down) and lose 1 Initiative for 2 turns. " +
					"\nDamage continues to spread outward with 50% reduction until 1. " +
						"\nDestroy all destructible tokens that would take damage.";} }

		int damage;
		
		public AReprBomb (Unit u) {
			Name = "Time Bomb";
			Weight = 4;
			Parent = u;
			Price = new Price(1,1);
			AddAim(new Aim(ETraj.ARC, EType.CELL, EPurp.ATTACK, 2));
			damage = 10;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Cell c = (Cell)targets[0];
			EffectQueue.Add(new EExplosion (new Source(Parent), c, damage));
			EffectGroup nextEffects = new EffectGroup();

			TokenGroup affected = c.Occupants.OnlyType(EType.UNIT);
			foreach (Unit u in affected) {
				nextEffects.Add(new EAddStat (new Source(Parent), u, EStat.IN, -2));
				u.timers.Add(new TBomb(u, Parent, 2));
			}
			affected = c.Neighbors().Occupants.OnlyType(EType.UNIT);
			foreach (Unit u in affected) {
				nextEffects.Add(new EAddStat (new Source(Parent), u, EStat.IN, -1));
				u.timers.Add(new TBomb(u, Parent, 1));
			}
			EffectQueue.Add(nextEffects);
		}
	}

	public class TBomb : Timer {
		Token source;
		int magnitude;

		public TBomb (Unit par, Token s, int n) {
			parent = par;
			source = s;

			magnitude = n;
			turns = 2;
			
			name = "Time Bombed";
			desc = parent.ToString()+" Initiative -"+magnitude+" for 2 turns.";
		}
		
		public override void Activate () {
			parent.AddStat(new Source(source), EStat.IN, magnitude);
		}
	}
}