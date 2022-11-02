using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class Decimatrix : Unit {
		public Decimatrix(Source s, bool template=false){
			NewLabel(EToken.DECI, s, true);
			BuildTrample();
			AddKing();
			OnDeath = EToken.HSTE;
			
			NewHealth(85,3);
			NewWatch(2);
			
			arsenal.Add(new AMove(this, Aim.MovePath(2)));
			arsenal.Add(new AAttack("Shoot", Price.Cheap, this, Aim.Shoot(3), 18));
			Aim fireAim = new Aim (EAim.LINE, new List<EClass> {EClass.UNIT, EClass.DEST}, 2);
			arsenal.Add(new AAttackFir("Flamethrower", new Price(1,1), this, fireAim, 12));
			arsenal.Add(new ADeciFortify(this));
			arsenal.Add(new ACreate(Price.Cheap, this, EToken.DEMO));
			arsenal.Add(new ACreate(new Price(1,1), this, EToken.MEIN));
			arsenal.Add(new ACreate(new Price(2,2), this, EToken.PANO));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
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

				InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, EStat.MHP, 10));
				InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, EStat.HP, 10));
				InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, EStat.DEF, 1));
				foreach (Action a in actor.Arsenal()) {if (a is AMove) {actor.Arsenal().Remove(a);} }
				foreach (Action a in actor.Arsenal()) {if (a is AAttack) {actor.Arsenal().Remove(a);} }
				foreach (Action a in actor.Arsenal()) {	if (a is ADeciFortify) {actor.Arsenal().Remove(a);} }
				actor.Arsenal().Add(new AAttack("Shoot", Price.Cheap, actor, HOA.Aim.Shoot(4), 22));
				actor.Arsenal().Add(new ADeciMortar(new Price(1,2), actor, 3, 5, 14));
				actor.Arsenal().Add(new ADeciMobilize(actor));
				actor.Arsenal().Sort();
				actor.SpriteEffect(EEffect.STATUP);

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

			InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, EStat.MHP, -10));
			InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, EStat.HP, -10));
			InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, EStat.DEF, -1));
			foreach (Action a in actor.Arsenal()) {	if (a is AAttack) {actor.Arsenal().Remove(a);} }
			foreach (Action a in actor.Arsenal()) { if (a is ADeciMortar) {actor.Arsenal().Remove(a);} }
			foreach (Action a in actor.Arsenal()) {	if (a is ADeciMobilize) {actor.Arsenal().Remove(a);} }
			actor.Arsenal().Add(new AMove(actor, HOA.Aim.MovePath(2)));
			actor.Arsenal().Add(new AAttack("Shoot", Price.Cheap, actor, HOA.Aim.Shoot(3), 18));
			actor.Arsenal().Add(new ADeciFortify(actor));
			actor.Arsenal().Sort();
			
			actor.SpriteEffect(EEffect.STATUP);

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
			desc = "Do "+d+" damage to all units in target cell. \nAll units in neighboring cells take 50% damage (rounded down). \nDamage continues to spread outward with 50% reduction until 1. \nDestroy all destructible tokens that would take damage.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			InputBuffer.Submit(new RExplosion(new Source(actor), (Cell)targets[0], damage));

		}
	}
}

