using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class OldThreeHands : Unit {
		public OldThreeHands(Source s, bool template=false){
			id = new ID(this, EToken.OLDT, s, true, template);
			plane = Plane.Gnd;
			type.Add(EType.KING);
			onDeath = EToken.HBRA;
			ScaleJumbo();

			NewHealth(85,2);
			watch = new WatchOldt(this, 2);
			NewWallet(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();

			arsenal.Add(new Task[] {
				new AMovePath(this, 2), 
				new AVolley(this, 3, 15),
				new AOldtHour(this),
				new AOldMinute(this),
				new AOldtSecond(this),
				new ACreate(this, Price.Cheap, EToken.REVO),
				new ACreate(this, new Price(2,0), EToken.PIEC),
				new ACreate(this, new Price(2,1), EToken.REPR)
			} );
			arsenal.Sort();
		}

		public override string Notes () {return "Initiative +1 per Focus (up to +8).";}
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

	public class AOldtHour : Task {

		public override string Desc {get {return "Target Unit shifts to the bottom of the Queue";} }

		public AOldtHour (Unit parent) {
			Parent = parent;
			Name = "Hour Saviour";
			Weight = 4;
			Price = new Price(0,2);
			AddAim(new Aim(ETraj.FREE, EType.UNIT));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];

			int last = TurnQueue.Count-1;
			int current = TurnQueue.IndexOf(u);
			int magnitude = 0-(last-current);

			EffectQueue.Add(new EShift(new Source(Parent), u, magnitude));
		}
	}

	public class AOldMinute : Task {
		public override string Desc {get {return "Shuffle the Queue." +
					"\n(End "+Parent.ID.Name+"'s turn.)";} }
		
		public AOldMinute (Unit parent) {
			Parent = parent;
			Name = "Minute Waltz";
			Weight = 4;

			Price = new Price(1,1);
			AddAim(HOA.Aim.Self());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EShuffle(new Source(Parent)));
			EffectQueue.Add(new EAdvance(new Source(Parent), false));
		}
	}

	public class AOldtSecond : Task {
		public override string Desc {get {return "Target unit takes the next turn." +
				"\n(Cannot target self.)";} }
		
		public AOldtSecond (Unit parent) {
			Parent = parent;
			Name = "Second in Command";
			Weight = 4;
			Price = new Price(0,2);
			AddAim(new Aim(ETraj.FREE, EType.UNIT));
			aim[0].IncludeSelf = false;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			int magnitude = TurnQueue.IndexOf(u) - 1;
			EffectQueue.Add(new EShift (new Source(Parent), u, magnitude));
		}
	}
}