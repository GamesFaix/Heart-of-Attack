﻿using System.Collections.Generic;

namespace HOA{
	public class Reprospector : Unit {
		public Reprospector(Source s, bool template=false){
			NewLabel(EToken.REPR, s, false, template);
			BuildGround();
			
			NewHealth(55);
			NewWatch(2);
			
			arsenal.Add(new AMove(this, Aim.MovePath(4)));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 15));
			arsenal.Add(new AGrenade("Grenade", new Price(1,1), this, 2, 10));
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
			AddAim(new Aim(EAim.NEIGHBOR, new List<EClass> {EClass.DEST, EClass.REM}));
			
			name = "Time Mine";
			desc = "Destroy neighboring destructible.\nIf initative is less than 6, initiative +1.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			Token t = (Token)targets[0];
			t.Die(new Source(actor));

			if (actor.IN < 7) {
				actor.AddStat(new Source(actor), EStat.IN, 1);
				actor.SpriteEffect(EEffect.STATUP);
			}
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
			desc = "Target Unit takes "+damage+" damage and loses 2 Initiative for 2 turns.\n"+actor.Name+" switches cells with target, if legal.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			Unit u = (Unit)targets[0];
			InputBuffer.Submit(new RDamage (new Source(actor), u, damage));
			
			u.AddStat (new Source(actor), EStat.IN, -2);
			u.timers.Add(new TSlam(u, actor));

			actor.Swap(u);
			
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
			AddAim(new Aim(EAim.ARC, EClass.CELL, EPurpose.ATTACK, 2));
			damage = 10;
			
			name = "Time Bomb";
			desc = "All Units in target cell take "+damage+" damage and lose 2 Initiative for 2 turns. \nAll units in neighboring cells take 50% damage (rounded down) and lose 1 Initiative for 2 turns. \nDamage continues to spread outward with 50% reduction until 1. \nDestroy all destructible tokens that would take damage.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			Cell c = (Cell)targets[0];
			InputBuffer.Submit(new RExplosion (new Source(actor), c, damage));

			TokenGroup affected = c.Occupants.OnlyClass(EClass.UNIT);
			foreach (Unit u in affected) {
				u.AddStat (new Source(actor), EStat.IN, -2);
				u.timers.Add(new TBomb(u, actor, 2));
			}
			affected = c.Neighbors().Occupants.OnlyClass(EClass.UNIT);
			foreach (Unit u in affected) {
				u.AddStat (new Source(actor), EStat.IN, -1);
				u.timers.Add(new TBomb(u, actor, 1));
			}
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