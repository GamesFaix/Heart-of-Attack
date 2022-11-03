using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class OldThreeHands : Unit {
		public OldThreeHands(Source s, bool template=false){
			NewLabel(EToken.OLDT, s, true, template);
			BuildGround();
			AddKing();
			OnDeath = EToken.HBRA;
			ScaleJumbo();

			NewHealth(85,2);
			watch = new WatchOldt(this, 2);
			
			//arsenal.Add(new AMove(this, Aim.MovePath(2)));
			arsenal.Add(new AMovePath(this, 2));

			Aim attackAim = new Aim (EAim.ARC, EClass.UNIT, 3);
			arsenal.Add(new AAttack("Snipe", Price.Cheap, this, attackAim, 15));
			arsenal.Add(new ACreate(Price.Cheap, this, EToken.REVO));
			arsenal.Add(new ACreate(new Price(2,0), this, EToken.PIEC));
			arsenal.Add(new ACreate(new Price(3,0), this, EToken.REPR));
			arsenal.Add(new AOldtHour(this));
			arsenal.Add(new AOldMinute(this));
			arsenal.Add(new AOldtSecond(this));
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

	/*public class AOldtHour : Action {
		
		public AOldtHour (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(1,1);
			AddAim(new Aim(EAim.GLOBAL, EClass.UNIT));
			
			name = "Hour Saviour";
			desc = "All teammates shift up one slot in the Queue.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			TokenGroup team = actor.Owner.OwnedUnits;
			team.Remove(actor);
			foreach (Token t in team) {
				if (t is Unit) {
					Unit u = (Unit)t;
					AEffects.Shift(new Source(actor), u, 1);
				}
			}
		}
	}*/

	public class AOldtHour : Action {
		
		public AOldtHour (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(0,2);
			AddAim(new Aim(EAim.FREE, EClass.UNIT));

			name = "Hour Saviour";
			desc = "Target Unit shifts to the bottom of the Queue";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			Unit u = (Unit)targets[0];

			int last = TurnQueue.Count-1;
			int current = TurnQueue.IndexOf(u);
			int magnitude = 0-(last-current);

			EffectQueue.Add(new EShift(new Source(actor), u, magnitude));
			Targeter.Reset();
		}
	}

	public class AOldMinute : Action {
		
		public AOldMinute (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(0,2);
			AddAim(new Aim(EAim.GLOBAL, EClass.UNIT));
			
			name = "Minute Waltz";
			desc = "Shuffle the Queue.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			EffectQueue.Add(new EShuffle(new Source(actor)));
			Targeter.Reset();
		}
	}

	public class AOldtSecond : Action {
		
		public AOldtSecond (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(0,2);
			AddAim(new Aim(EAim.FREE, EClass.UNIT));
			aim[0].IncludeSelf = false;
			
			name = "Second in Command";
			desc = "Target unit takes the next turn.\n(Cannot target self.)";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			Unit u = (Unit)targets[0];
			int magnitude = TurnQueue.IndexOf(u) - 1;

			EffectQueue.Add(new EShift (new Source(actor), u, magnitude));
			Targeter.Reset();
		}
	}
}