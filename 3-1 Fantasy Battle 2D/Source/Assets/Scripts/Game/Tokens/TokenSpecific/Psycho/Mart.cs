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

	public class AMartMove : Action, IMultiMove {
		Cell target;
		int range = 1;
		public int Optional () {return 1;}

		public AMartMove (Unit u) {
			weight = 1;
			actor = u;
			price = new Price(1,0);
			name = "Move";
			desc = "Range +1 per focus.";
			
			ResetAim();
			
		}
		
		public override void Adjust () {
			int bonus = actor.FP;
			for (int i=0; i<bonus; i++) {
				aim.Add(new Aim (EAim.NEIGHBOR, EClass.CELL, EPurpose.MOVE));
			}
		}
		
		public override void UnAdjust () {
			ResetAim();

		}
		
		void ResetAim () {
			aim = new List<Aim>();
			AddAim(new Aim (EAim.NEIGHBOR, EClass.CELL, EPurpose.MOVE));
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
			
			Aim actual = new Aim(EAim.PATH, EClass.CELL, EPurpose.MOVE, range+actor.FP);
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			
			float descH = (p.H-(p.LineH*2))/p.H;
			//Rect descBox = new Rect(p.x2, p.y2, p.W, descH);
			
			GUI.Label(p.TallBox(descH), Desc());	
			
			
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
			EffectQueue.Add(new EDamage(new Source(actor), u, damage));
			Token dest;
			if (c.Contains(EClass.DEST, out dest)) {
				actor.Body.Swap(dest);
			}

			UnAdjust();
			Targeter.Reset();
		}
	}
}