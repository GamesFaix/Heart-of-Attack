using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class MartianManTrap : Unit {
		public MartianManTrap(Source s, bool template=false){
			id = new ID(this, EToken.MART, s, false, template);
			plane = Plane.Gnd;
			type.Add(EType.TRAM);
			ScaleLarge();
			NewHealth(70);
			NewWatch(4);
			BuildArsenal();
		}	

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Remove("Focus");
			arsenal.Add(new Task[]{
				new AMartMove(this),
				new AMartGrow(this),
				new AStrike(this, 12),
				new AMartWhip(this)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}

	public class AMartMove : Task, IMultiMove {
		Cell target;
		int range = 1;
		public int Optional () {return 1;}

		public override string Desc {get {return "Range +1 per focus.";} }

		public AMartMove (Unit u) {
			Name = "Move";
			Weight = 1;

			Parent = u;
			Price = Price.Cheap;

			ResetAim();
		}
		
		public override void Adjust () {
			int bonus = Parent.FP;
			for (int i=0; i<bonus; i++) {
				aim.Add(new Aim (ETraj.NEIGHBOR, EType.CELL, EPurp.MOVE));
			}
		}
		
		public override void UnAdjust () {
			ResetAim();
		}
		
		void ResetAim () {
			aim = new List<Aim>();
			AddAim(new Aim (ETraj.NEIGHBOR, EType.CELL, EPurp.MOVE));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new EMove(new Source(Parent), Parent, (Cell)target));
			}
		}
		
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			
			DrawPrice(new Panel(p.LineBox, p.LineH, p.s));
			
			Aim actual = new Aim(ETraj.PATH, EType.CELL, EPurp.MOVE, range+Parent.FP);
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			
			float descH = (p.H-(p.LineH*2))/p.H;
			//Rect descBox = new Rect(p.x2, p.y2, p.W, descH);
			
			GUI.Label(p.TallBox(descH), Desc);	
		}
	}

	public class AMartGrow : Task {

		public override string Desc {get {return "Switch cells with target Destructible. " +
				"\nRange +1 per focus.  " +
					"\n"+Parent+" +1 Focus.";} }

		public AMartGrow (Unit u) {
			Name = "Grow";
			Weight = 2;

			Price = Price.Cheap;
			Parent = u;
			
			AddAim(new Aim(ETraj.PATH, EType.DEST, 1));
		}
		
		public override void Adjust () {
			Debug.Log("adjusting");
			int bonus = Parent.FP;
			aim[0] = new Aim (aim[0].Trajectory, aim[0].Special, aim[0].Range+bonus);
		}
		
		public override void UnAdjust () {
			aim[0] = new Aim(ETraj.PATH, EType.DEST, 1);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Parent.AddStat(new Source(Parent), EStat.FP, 1, false);

			Token t = (Token)targets[0];
			Parent.Body.Swap(t);

			UnAdjust();
		}
	}

	public class AMartWhip : Task {

		int damage = 18;

		public override string Desc {get {return "Do "+damage+" damage target Unit." +
				"\nRange +1 per focus." +
					"\nIf target is killed and leaves Remains, switch cells with it's Remains.";} } 

		public AMartWhip (Unit u) {
			Name = "Vine Whip";
			Weight = 4;

			Price = new Price(1,1);
			Parent = u;
			
			AddAim(new Aim(ETraj.LINE, EType.UNIT, EPurp.ATTACK, 2));
		}
		
		public override void Adjust () {
			Debug.Log("adjusting");
			int bonus = Parent.FP;
			aim[0] = new Aim (aim[0].Trajectory, aim[0].Special, aim[0].Range+bonus);
		}
		
		public override void UnAdjust () {
			aim[0] = new Aim(ETraj.PATH, EType.UNIT, 2);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new EDamage(new Source(Parent), u, damage));
			Token dest;
			if (u.Body.Cell.Contains(EType.DEST, out dest)) {
				Parent.Body.Swap(dest);
			}

			UnAdjust();
		}
	}
}