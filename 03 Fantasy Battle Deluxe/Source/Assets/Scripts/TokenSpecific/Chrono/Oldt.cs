using UnityEngine;

namespace HOA{
	public class OldThreeHands : Unit {
		public OldThreeHands(Source s, bool template=false){
			NewLabel(TTYPE.OLDT, s, true, template);
			BuildGround();
			AddKing();
			OnDeath = TTYPE.HBRA;
			
			NewHealth(85,2);
			watch = new WatchOldt(this, 2);
			
			arsenal.Add(new AMove(this, Aim.MovePath(2)));
			Aim attackAim = new Aim (AIMTYPE.ARC, TARGET.TOKEN, TTAR.UNIT, 3);
			arsenal.Add(new AAttack("Snipe", Price.Cheap, this, attackAim, 15));
			arsenal.Add(new ACreate(Price.Cheap, this, TTYPE.REVO));
			arsenal.Add(new ACreate(new Price(1,1), this, TTYPE.PIEC));
			arsenal.Add(new ACreate(new Price(1,2), this, TTYPE.REPR));
			arsenal.Add(new AOldtSecond(new Price(1,1), this));
			arsenal.Add(new AOldtHour(new Price(1,1), this));
			arsenal.Sort();
		}		
		public override string Notes () {return "Initiative +1 per Focus (up to 8).";}
	}

	public class WatchOldt : Watch {
		public WatchOldt (Unit u, int i=0){
			parent = u;
			init = i;
			stun = 0;
			skipped = false;
			cor = 0;
		}
		
		public override int IN {
			get {return init + (Mathf.Min(8, parent.FP));}
			set {init = value;}
		}
	}

	public class AOldtHour : Action {
		
		public AOldtHour (Price p, Unit u) {
			weight = 4;
			actor = u;
			price = p;
			aim = new Aim(AIMTYPE.GLOBAL, TARGET.TOKEN, TTAR.NA);
			
			name = "Hour Saviour";
			desc = "All teammates shift up one slot in the Queue.";
		}
		
		public override void Perform () {
			if (Charge()) {
				TokenGroup team = actor.Owner.OwnedUnits;
				team.Remove(actor);
				foreach (Token t in team) {
					InputBuffer.Submit(new RQueueShift(new Source(actor), t, 1));
				}
			}
		}
	}

	public class AOldtSecond : Action {
		
		public AOldtSecond (Price p, Unit u) {
			weight = 4;
			actor = u;
			price = p;
			aim = new Aim(AIMTYPE.FREE, TARGET.TOKEN, TTAR.UNIT);
			aim.IncludeSelf = false;
			
			name = "Second in Command";
			desc = "Target unit takes the next turn.\n(Cannot target self.)";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new ROldtSecond(new Source(actor), default(Token)));
				
			}
		}
	}

	public class ROldtSecond : RInstanceSelect {
		public int magnitude;
		public ROldtSecond (Source s, Token t) {source = s; instance = t;}
		
		public override void Grant () {
			if (instance is Unit) {
				int magnitude = TurnQueue.IndexOf((Unit)instance) - 1;
				TurnQueue.MoveUp((Unit)instance, magnitude);
			}	
			Reset();
		}
	}
}