using UnityEngine;

namespace HOA{
	public class Smashbuckler : Unit {
		public Smashbuckler(Source s, bool template=false){
			NewLabel(TTYPE.SMAS, s, false, template);
			BuildGround();
			
			NewHealth(30);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new ASmasFlail(Price.Cheap, this, Aim.Melee(), 8));
			arsenal.Add(new ASmasSlam(new Price(1,1), this, Aim.Melee(), 8));
			arsenal.Sort();
		}		
		
		public override string Notes () {return "";}
	}	

	public class ASmasFlail : Action {
		int damage;
		
		public ASmasFlail (Price p, Unit u, Aim a, int d) {
			weight = 3;
			price = p;
			actor = u;
			
			aim = a;
			damage = d;
			
			name = "Flail";
			desc = "Do "+d+" damage to target unit.  \nRange +1 per focus (Up to +3).  \n"+actor+" loses all focus.";
			
		}
		
		public override void Perform () {
			if (Charge()) {
				int bonus = Mathf.Min(actor.FP, 3);
				actor.SetStat(new Source(actor), STAT.FP, 0, false);
				aim = new Aim (aim.AimType, aim.Target, aim.TTar, aim.Range+bonus);
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RDamage(new Source(actor), default(Token), damage));
			}
		}
	}

	public class ASmasSlam : Action {
		int range;
		int damage;
		
		public ASmasSlam (Price p, Unit u, Aim a, int d) {
			weight = 4;
			
			price = p;
			actor = u;
			aim = a;
			damage = d;
			
			name = "Slam";
			desc = "Do "+d+" damage to target unit and each of its neighbors and cellmates.  \nRange +1 per focus (up to +3).  \n"+actor+" loses all focus.";
			
		}
		
		public override void Perform () {
			if (Charge()) {
				int bonus = Mathf.Min(actor.FP, 3);
				actor.SetStat(new Source(actor), STAT.FP, 0, false);
				aim = new Aim (aim.AimType, aim.Target, aim.TTar, aim.Range+bonus);
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RSmasSlam(new Source(actor), default(Token), damage));
			}
		}
	}

	public class RSmasSlam : RInstanceSelect {
		public int magnitude;	
		public RSmasSlam (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
		
		public override void Grant () {
			if (instance is Unit) {
				Unit u = (Unit)instance;
				u.Damage(source, magnitude);
				u.SpriteEffect(EFFECT.DMG);
				TokenGroup neighbors = u.Neighbors(true).FilterUnit;
				foreach (Unit u2 in neighbors) {
					u2.Damage(source, magnitude);
					u2.SpriteEffect(EFFECT.DMG);
				}
			}
			Reset();
		}

	}
}