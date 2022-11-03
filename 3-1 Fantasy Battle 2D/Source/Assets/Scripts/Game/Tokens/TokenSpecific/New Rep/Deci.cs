using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class Decimatrix : Unit {
		public Decimatrix(Source s, bool template=false){
			NewLabel(EToken.DECI, s, true);
			BuildTrample();
			AddKing();
			OnDeath = EToken.HSTE;
			
			health = new HealthPano(this, 85);
			NewWatch(2);
			
			arsenal.Add(new ADeciMove(this));


			arsenal.Add(new AAttack("Shoot", Price.Cheap, this, Aim.Shoot(3), 15));
			arsenal.Add(new APanoPierce(new Price(1,1), this, 15));
			arsenal.Add(new ADeciMortar(new Price(1,2), this, 2, 3, 18));
			//Aim fireAim = new Aim (EAim.LINE, new List<EClass> {EClass.UNIT, EClass.DEST}, 2);
			//arsenal.Add(new AAttackFir("Flamethrower", new Price(1,1), this, fireAim, 12));
			//arsenal.Add(new ADeciFortify(this));

			arsenal.Add(new ACreate(new Price(1,1), this, EToken.DEMO));
			arsenal.Add(new ACreate(new Price(2,1), this, EToken.MEIN));
			//arsenal.Add(new ACreate(new Price(2,2), this, EToken.PANO));
			arsenal.Sort();
		}		
		public override string Notes () {return "Defense +1 per Focus (up to 4).";}
	}

	public class ADeciMove : Action, IMultiMove {
		Cell target;
		int range = 3;
		public int Optional () {return 1;}
		
		public ADeciMove (Unit u) {
			weight = 1;
			actor = u;
			name = "Move";
			desc = "Move "+actor+" to target cell.";
			
			ResetAim();
			
			
		}

		public override void Adjust () {
			int bonus = actor.FP;
			for (int i=0; i<bonus; i++) {
				aim.Remove(aim[aim.Count-1]);
			}
		}
		
		public override void UnAdjust () {
			ResetAim();
		}

		void ResetAim () {
			aim = new List<Aim>();
			for (int i=0; i<range; i++) {
				Aim a = new Aim(EAim.NEIGHBOR, EClass.CELL, EPurpose.MOVE) ;
				AddAim(a);
				//Debug.Log(a);
			}
		}

		public override void Execute (List<ITargetable> targets) {
			Charge();
			foreach (ITargetable target in targets) {
				EffectQueue.Add(new EMove(new Source(actor), actor, (Cell)target));
			}
			Targeter.Reset();
		}
		
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			
			DrawPrice(new Panel(p.LineBox, p.LineH, p.s));
			
			Aim actual = new Aim(EAim.PATH, EClass.CELL, EPurpose.MOVE, Mathf.Max(0, range-actor.FP));
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			
			float descH = (p.H-(p.LineH*2))/p.H;
			//Rect descBox = new Rect(p.x2, p.y2, p.W, descH);
			
			GUI.Label(p.TallBox(descH), Desc());	
			
			
		}


	}


	public class ADeciFortify : Action {
		public ADeciFortify (Unit u) {
			weight = 4;
			
			actor = u;
			price = new Price(1,1);
			AddAim(HOA.Aim.Self);
			
			name = "Fortify";
			desc = "Health +10/10\nDefense + 1\nAttack range +1\nAttack damage +4\nForget 'Move'\nLearn 'Mortar'";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();

			EffectQueue.Add(new EAddStat(new Source(actor), actor, EStat.MHP, 10));
			EffectQueue.Add(new EAddStat(new Source(actor), actor, EStat.HP, 10));
			EffectQueue.Add(new EAddStat(new Source(actor), actor, EStat.DEF, 1));
			foreach (Action a in actor.Arsenal()) {if (a is AMove) {actor.Arsenal().Remove(a);} }
			foreach (Action a in actor.Arsenal()) {if (a is AAttack) {actor.Arsenal().Remove(a);} }
			foreach (Action a in actor.Arsenal()) {	if (a is ADeciFortify) {actor.Arsenal().Remove(a);} }
			actor.Arsenal().Add(new AAttack("Shoot", Price.Cheap, actor, HOA.Aim.Shoot(4), 22));
			actor.Arsenal().Add(new ADeciMortar(new Price(1,2), actor, 3, 5, 14));
			actor.Arsenal().Add(new ADeciMobilize(actor));
			actor.Arsenal().Sort();
			actor.SpriteEffect(EEffect.STATUP);
			Targeter.Reset();
		}
	}
	public class ADeciMobilize : Action {
		public ADeciMobilize (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(1,1);
			AddAim(HOA.Aim.Self);
			
			name = "Mobilize";
			desc = "Health -10/10\nDefense -1\nAttack range -1\nAttack damage -4\nLearn 'Move'\nForget 'Mortar'";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();

			EffectQueue.Add(new EAddStat(new Source(actor), actor, EStat.MHP, -10));
			EffectQueue.Add(new EAddStat(new Source(actor), actor, EStat.HP, -10));
			EffectQueue.Add(new EAddStat(new Source(actor), actor, EStat.DEF, -1));
			foreach (Action a in actor.Arsenal()) {	if (a is AAttack) {actor.Arsenal().Remove(a);} }
			foreach (Action a in actor.Arsenal()) { if (a is ADeciMortar) {actor.Arsenal().Remove(a);} }
			foreach (Action a in actor.Arsenal()) {	if (a is ADeciMobilize) {actor.Arsenal().Remove(a);} }
			actor.Arsenal().Add(new AMove(actor, HOA.Aim.MovePath(2)));
			actor.Arsenal().Add(new AAttack("Shoot", Price.Cheap, actor, HOA.Aim.Shoot(3), 18));
			actor.Arsenal().Add(new ADeciFortify(actor));
			actor.Arsenal().Sort();
			
			actor.SpriteEffect(EEffect.STATUP);
			Targeter.Reset();
		}
	}
	public class ADeciMortar : Action {
		int minRange, range, damage;
		
		public ADeciMortar (Price p, Unit u, int mr, int r, int d) {
			weight = 4;
			
			price = p;
			actor = u;
			AddAim(new Aim (EAim.ARC, EClass.CELL, EPurpose.ATTACK, r, mr));
			damage = d;
			
			name = "Mortar";
			desc = "Do "+d+" damage to all units in target cell. \nAll units in neighboring cells take 50% damage (rounded down). \nDamage continues to spread outward with 50% reduction until 1. \nDestroy all destructible tokens that would take damage.\nRange +1 per Focus (up to 3)";
		}

		public override void Adjust () {
			int bonus = Mathf.Min(actor.FP, 3);
			aim[0] = new Aim (aim[0].AimType, aim[0].TargetClass, aim[0].Range+bonus, aim[0].MinRange);
		}
		
		public override void UnAdjust () {
			aim[0] = new Aim(EAim.ARC, EClass.UNIT, 3, 2);
		}

		public override void Execute (List<ITargetable> targets) {
			Charge();
			EffectQueue.Add(new EExplosion(new Source(actor), (Cell)targets[0], damage));
			//AEffects.Explosion(new Source(actor), (Cell)targets[0], damage);
			Targeter.Reset();
		}
	}
}

