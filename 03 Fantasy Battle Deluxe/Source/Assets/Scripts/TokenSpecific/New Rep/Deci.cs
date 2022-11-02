using UnityEngine;

namespace HOA{
	public class Decimatrix : Unit {
		public Decimatrix(Source s, bool template=false){
			NewLabel(TTYPE.DECI, s, true);
			BuildTrample();
			AddKing();
			OnDeath = TTYPE.HSTE;
			
			NewHealth(85,3);
			NewWatch(2);
			
			arsenal.Add(new AMove(this, Aim.MovePath(2)));
			arsenal.Add(new AAttack("Shoot", Price.Cheap, this, Aim.Shoot(3), 18));
			Aim fireAim = new Aim (AIMTYPE.LINE, TARGET.TOKEN, TTAR.UNITDEST, 2);
			arsenal.Add(new AAttackFir("Flamethrower", new Price(1,1), this, fireAim, 12));
			arsenal.Add(new ADeciFortify(this));
			arsenal.Add(new ACreate(Price.Cheap, this, TTYPE.DEMO));
			arsenal.Add(new ACreate(new Price(1,1), this, TTYPE.MEIN));
			arsenal.Add(new ACreate(new Price(2,2), this, TTYPE.PANO));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class ADeciFortify : Action {
		public ADeciFortify (Unit u) {
			weight = 4;
			
			actor = u;
			price = new Price(1,1);
			aim = new Aim (AIMTYPE.SELF, TARGET.SELF, CTAR.NA);
			
			name = "Fortify";
			desc = "Health +10/10\nDefense + 1\nAttack range +1\nAttack damage +4\nForget 'Move'\nLearn 'Mortar'";
		}
		
		public override void Perform () {
			if (Charge()) {
				InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, STAT.MHP, 10));
				InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, STAT.HP, 10));
				InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, STAT.DEF, 1));
				foreach (Action a in actor.Arsenal()) {if (a is AMove) {actor.Arsenal().Remove(a);} }
				foreach (Action a in actor.Arsenal()) {if (a is AAttack) {actor.Arsenal().Remove(a);} }
				foreach (Action a in actor.Arsenal()) {	if (a is ADeciFortify) {actor.Arsenal().Remove(a);} }
				actor.Arsenal().Add(new AAttack("Shoot", Price.Cheap, actor, Aim.Shoot(4), 22));
				actor.Arsenal().Add(new ADeciMortar(new Price(1,2), actor, 3, 5, 14));
				actor.Arsenal().Add(new ADeciMobilize(actor));
				actor.Arsenal().Sort();
				actor.SpriteEffect(EFFECT.STATUP);
			}
		}
	}
	public class ADeciMobilize : Action {
		public ADeciMobilize (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(1,1);
			aim = new Aim (AIMTYPE.SELF, TARGET.SELF, CTAR.NA);
			
			name = "Mobilize";
			desc = "Health -10/10\nDefense -1\nAttack range -1\nAttack damage -4\nLearn 'Move'\nForget 'Mortar'";
		}
		
		public override void Perform () {
			if (Charge()) {
				InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, STAT.MHP, -10));
				InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, STAT.HP, -10));
				InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, STAT.DEF, -1));
				foreach (Action a in actor.Arsenal()) {	if (a is AAttack) {actor.Arsenal().Remove(a);} }
				foreach (Action a in actor.Arsenal()) { if (a is ADeciMortar) {actor.Arsenal().Remove(a);} }
				foreach (Action a in actor.Arsenal()) {	if (a is ADeciMobilize) {actor.Arsenal().Remove(a);} }
				actor.Arsenal().Add(new AMove(actor, Aim.MovePath(2)));
				actor.Arsenal().Add(new AAttack("Shoot", Price.Cheap, actor, Aim.Shoot(3), 18));
				actor.Arsenal().Add(new ADeciFortify(actor));
				actor.Arsenal().Sort();
				
				actor.SpriteEffect(EFFECT.STATUP);
			}
		}
	}
	public class ADeciMortar : Action {
		int minRange, range, damage;
		
		public ADeciMortar (Price p, Unit u, int mr, int r, int d) {
			weight = 4;
			
			price = p;
			actor = u;
			aim = new Aim (AIMTYPE.ARC, TARGET.CELL, CTAR.ATTACK, r, mr);
			damage = d;
			
			name = "Mortar";
			desc = "Do "+d+" damage to all units in target cell. \nAll units in neighboring cells take 50% damage (rounded down). \nDamage continues to spread outward with 50% reduction until 1. \nDestroy all destructible tokens that would take damage.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithCell(new RExplosion(new Source(actor), default(Cell), damage));
			}
		}
	}
}

