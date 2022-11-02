using UnityEngine;

namespace HOA{
	public class Recyclops : Unit {
		public Recyclops(Source s, bool template=false){
			NewLabel(TTYPE.RECY, s, false, template);
			BuildGround();
			AddRem();
			
			NewHealth(15);
			NewWatch(4);
			
			arsenal.Add(new AMove(this, Aim.MovePath(2)));
			arsenal.Add(new ARage(new Price(1,0), this, Aim.Melee(), 12));
			arsenal.Add(new ARecyExplode(new Price(1,1), this, 12));
			arsenal.Add(new ARecyCannibal(new Price(1,1), this));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class ARecyCannibal : Action {
		
		Cell cell;
		
		public ARecyCannibal (Price p, Unit par) {
			weight = 4;
			price = p;
			actor = par;
			aim = new Aim (AIMTYPE.NEIGHBOR, TARGET.TOKEN, TTAR.REM);
			
			name = "Cannibalize";
			desc = "Destroy target remains.\nHealth +10/10";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RRecyCannibal(new Source(actor), default(Token)));
			}
		}
	}

	public class ARecyExplode : Action {
		int damage;
		
		public ARecyExplode (Price p, Unit u, int d) {
			weight = 4;
			price = p;
			aim = new Aim (AIMTYPE.SELF, TARGET.SELF, TTAR.NA);
			actor = u;
			
			damage = d;
			int cor = (int)Mathf.Floor(d*0.5f);
			name = "Burst";
			desc = "Destroy "+actor+".\nDo "+d+" damage to cellmates and neighbors. \nDamaged units take "+cor+" corrosion counters. \n(If a unit has corrosion counters, at the beginning of its turn it takes damage equal to the number of counters, then removes half the counters (rounded up).)";
		}
		
		public override void Perform () {
			if (Charge()) {
				TokenGroup victims = actor.Neighbors(true);
				foreach (Token t in victims) {
					InputBuffer.Submit(new RCorrode(new Source(actor), t, damage));	
				}
				actor.Die(new Source(actor));
			}
		}
	}

	public class RRecyCannibal : RInstanceSelect{
		Aim aim2; int damage;
		
		public RRecyCannibal (Source s, Token t) {source = s; instance = t;}
		
		public override void Grant () {
			instance.Die(source);
			Unit actor = (Unit)(source.Token);
			actor.AddStat(source, STAT.MHP, 10);
			actor.AddStat(source, STAT.HP, 10);
			Reset();
		}
	}
}