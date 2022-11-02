using UnityEngine;

namespace HOA{
	public class Beesassin : Unit {
		public Beesassin(Source s, bool template=false){
			NewLabel(TTYPE.BEES, s, false, template);
			BuildAir();
			
			NewHealth(25);
			NewWatch(5);
			AddStat(new Source(this), STAT.COR, 12, false);
			
			arsenal.Add(new AMove(this, Aim.MoveLine(5)));
			arsenal.Add(new ACorrode("Sting", Price.Cheap, this, Aim.Melee(), 8));
			arsenal.Add(new ABeesDeathSting(new Price(1,1), this, Aim.Melee(), 15));
			arsenal.Sort();
			
		}		
		public override string Notes () {return "";}
	}
	
	public class ABeesDeathSting : Action {
		int range;
		int damage;
		
		public ABeesDeathSting (Price p, Unit u, Aim a, int d) {
			weight = 4;
			price = p;
			actor = u;
			aim = a;
			damage = d;
			int cor = (int)Mathf.Floor(d*0.5f);
			name = "Fatal Blow";
			desc = "Destroy "+actor+".\nDo "+d+" damage to target unit. \nTarget takes "+cor+" corrosion counters. \n(If a unit has corrosion counters, at the beginning of its turn it takes damage equal to the number of counters, then removes half the counters (rounded up).)";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RDeathSting(new Source(actor), default(Token), damage));
			}
		}
	}

	public class RDeathSting: RInstanceSelect {
		public int magnitude;	
		public RDeathSting (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
		
		public override void Grant () {
			int cor = (int)Mathf.Floor(magnitude*0.5f);
			if (instance is Unit) {
				Unit u = (Unit)instance;
				u.Damage(source, magnitude);
				u.AddStat(source, STAT.COR, cor);
				u.SpriteEffect(EFFECT.COR);
				Unit actor = (Unit)source.Token;
				actor.Die(source);
			}
			Reset();
		}
	}
}