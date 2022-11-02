using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class MartianManTrap : Unit {
		public MartianManTrap(Source s, bool template=false){
			NewLabel(EToken.MART, s, false, template);
			BuildTrample();
			
			NewHealth(70);
			NewWatch(4);

			foreach(Action a in arsenal) {
				if (a is AFocus) {arsenal.Remove(a);}
			}
			arsenal.Add(new AMartMove(this));
			arsenal.Add(new AMartGrow(this));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 12));
			arsenal.Add(new AMartWhip(this));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class AMartMove : Action {
		
		public AMartMove (Unit u) {
			weight = 1;
			price = Price.Cheap;
			actor = u;
			
			AddAim(new Aim(EAim.PATH, EClass.CELL, 1));
			
			name = "Move";
			desc = "Range +1 per focus.";
			
		}
		
		public override void Adjust () {
			Debug.Log("adjusting");
			int bonus = actor.FP;
			aim[0] = new Aim (aim[0].AimType, aim[0].TargetClass, aim[0].Range+bonus);
		}
		
		public override void UnAdjust () {
			aim[0] = new Aim(EAim.PATH, EClass.CELL, 1);
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();

			AEffects.Move(new Source(actor), actor, (Cell)targets[0]);
			
			UnAdjust();
			Targeter.Reset();
		}
	}
	public class AMartGrow : Action {

		public AMartGrow (Unit u) {
			weight = 2;
			price = Price.Cheap;
			actor = u;
			
			AddAim(new Aim(EAim.PATH, EClass.DEST, 1));

			name = "Grow";
			desc = "Switch cells with target Destructible.  \nRange +1 per focus.  \n"+actor+" +1 Focus.";
			
		}
		
		public override void Adjust () {
			Debug.Log("adjusting");
			int bonus = actor.FP;
			aim[0] = new Aim (aim[0].AimType, aim[0].TargetClass, aim[0].Range+bonus);
		}
		
		public override void UnAdjust () {
			aim[0] = new Aim(EAim.PATH, EClass.DEST, 1);
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			actor.AddStat(new Source(actor), EStat.FP, 1, false);

			Token t = (Token)targets[0];
			actor.Swap(t);

			UnAdjust();
			Targeter.Reset();
		}
	}

	public class AMartWhip : Action {

		int damage;

		public AMartWhip (Unit u) {
			weight = 4;
			price = new Price(1,1);
			actor = u;
			
			AddAim(new Aim(EAim.LINE, EClass.UNIT, EPurpose.ATTACK, 2));
			damage = 18;

			name = "Vine Whip";
			desc = "Do "+damage+"damage target Unit.\nRange +1 per focus.\nIf target is killed and leaves Remains, switch cells with it's Remains.";
			
		}
		
		public override void Adjust () {
			Debug.Log("adjusting");
			int bonus = actor.FP;
			aim[0] = new Aim (aim[0].AimType, aim[0].TargetClass, aim[0].Range+bonus);
		}
		
		public override void UnAdjust () {
			aim[0] = new Aim(EAim.PATH, EClass.UNIT, 2);
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();

			Unit u = (Unit)targets[0];
			Cell c = u.Cell;
			AEffects.Damage(new Source(actor), u, damage);
			Token dest;
			if (c.Contains(EClass.DEST, out dest)) {
				actor.Body.Swap(dest);
			}

			UnAdjust();
			Targeter.Reset();
		}
	}
}