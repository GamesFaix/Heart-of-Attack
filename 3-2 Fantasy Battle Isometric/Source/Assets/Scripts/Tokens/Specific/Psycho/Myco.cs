using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class Mycolonist : Unit {
		public Mycolonist(Source s, bool template=false){
			id = new ID(this, EToken.MYCO, s, false, template);
			plane = Plane.Gnd;
			ScaleMedium();
			NewHealth(40);
			NewWatch(2);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMovePath(this, 2),
				new AMycoSpore(this),
				new AMycoDonate(this),
				new AMycoSeed(this)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}

	public class AMycoSpore : Task {
		int damage = 12;

		int Cor {get {return (int)Mathf.Floor(damage*0.5f);} }

		public override string Desc {get {return "Do "+damage+" damage to target unit. " +
				"\nTarget recieves "+Cor+" corrosion counters." +
					"\n(If a unit has corrosion counters, at the beginning of its turn " +
						"it takes damage equal to the number of counters, " +
						"then removes half the counters (rounded up).)";} }

		public AMycoSpore (Unit u) {
			Name = "Sporatic Emission";
			Weight = 3;
			
			Price = Price.Cheap;
			AddAim(HOA.Aim.Arc(2));
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ECorrode(new Source(Parent), (Unit)targets[0], damage));
		}
	}

	public class AMycoDonate : Task {
		
		int damage = 6;

		public override string Desc {get {return "Target unit gains "+damage+" health. " +
				"\n"+Parent+" takes damage equal to health successfully gained.";} }

		public AMycoDonate (Unit u) {
			Name = "Donate Life";
			Weight = 4;
			
			Price = Price.Cheap;
			AddAim(HOA.Aim.Melee());
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EDonate(new Source(Parent), (Unit)targets[0], damage));
		}
	}
	
	public class AMycoSeed : Task {
		public override string Desc {get {return "Replace target non-Remains destructible with Lichenthrope.";} }

		public AMycoSeed (Unit par) {
			Name = "Seed";
			Weight = 5;

			Price = new Price(1,1);
			Parent = par;
			AddAim(new Aim (ETraj.ARC, EType.DEST, 2));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EReplace(new Source(Parent), (Token)targets[0], EToken.LICH));
		}
	}
}