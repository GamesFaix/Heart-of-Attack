using System.Collections.Generic;

namespace HOA{
	public class Reprospector : Unit {
		public Reprospector(Source s, bool template=false){
			id = new ID(this, EToken.REPR, s, false, template);
			plane = Plane.Gnd;
			ScaleLarge();

			NewHealth(55);
			NewWatch(2);

			arsenal.Add(new AMovePath(this, 4));
			arsenal.Add(new AReprMine(Price.Cheap, this));
			arsenal.Add(new AReprSlam(this));
			arsenal.Add(new AReprBomb(this));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class AReprMine : Action {
		
		public AReprMine (Price p, Unit u) {
			weight = 4;
			actor = u;
			price = p;
			AddAim(new Aim(ETraj.NEIGHBOR, Type.DestRem));
			
			name = "Time Mine";
			desc = "Destroy neighboring destructible.\nIf initative is less than 6, initiative +1.";
		}
		
		public override void Execute (List<ITarget> targets) {
			Charge();
			Token t = (Token)targets[0];
			Cell c = t.Body.Cell;

			EffectQueue.Add(new EKill(new Source(actor), t));

			EffectGroup nextEffects = new EffectGroup();

			if (actor.IN < 7) {
				nextEffects.Add(new EAddStat(new Source(actor), actor, EStat.IN, 1));
			}
			if (actor.Body.CanEnter(c)) {
				nextEffects.Add(new EMove(new Source(actor), actor, c));
			}

			if (nextEffects.Count > 0) {EffectQueue.Add(nextEffects);}

			Targeter.Reset();
		}
	}

	public class AReprSlam : Action {
		
		int damage;
		
		public AReprSlam (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(1,0);
			AddAim(HOA.Aim.Melee());
			damage = 15;
			
			name = "Time Slam";
			desc = "Target Unit takes "+damage+" damage and loses 2 Initiative for 2 turns.\n"+actor.ID.Name+" switches cells with target, if legal.";
		}
		
		public override void Execute (List<ITarget> targets) {
			Charge();
			Unit u = (Unit)targets[0];

			EffectGroup effects = new EffectGroup();

			effects.Add(new ESwap(new Source(actor), actor, u));
			effects.Add(new EDamage (new Source(actor), u, damage));

			EffectQueue.Add(effects);


			EffectQueue.Add(new EAddStat (new Source(actor), u, EStat.IN, -2));

			u.timers.Add(new TSlam(u, actor));


			Targeter.Reset();
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

	public class AReprBomb : Action {
		
		int damage;
		
		public AReprBomb (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(1,1);
			AddAim(new Aim(ETraj.ARC, EType.CELL, EPurp.ATTACK, 2));
			damage = 10;
			
			name = "Time Bomb";
			desc = "All Units in target cell take "+damage+" damage and lose 2 Initiative for 2 turns. \nAll units in neighboring cells take 50% damage (rounded down) and lose 1 Initiative for 2 turns. \nDamage continues to spread outward with 50% reduction until 1. \nDestroy all destructible tokens that would take damage.";
		}
		
		public override void Execute (List<ITarget> targets) {
			Charge();
			Cell c = (Cell)targets[0];
			EffectQueue.Add(new EExplosion (new Source(actor), c, damage));
			//AEffects.Explosion (new Source(actor), c, damage);

			EffectGroup nextEffects = new EffectGroup();

			TokenGroup affected = c.Occupants.OnlyType(EType.UNIT);
			foreach (Unit u in affected) {
				nextEffects.Add(new EAddStat (new Source(actor), u, EStat.IN, -2));
				u.timers.Add(new TBomb(u, actor, 2));
			}
			affected = c.Neighbors().Occupants.OnlyType(EType.UNIT);
			foreach (Unit u in affected) {
				nextEffects.Add(new EAddStat (new Source(actor), u, EStat.IN, -1));
				u.timers.Add(new TBomb(u, actor, 1));
			}
			EffectQueue.Add(nextEffects);
			Targeter.Reset();
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