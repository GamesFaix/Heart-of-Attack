using System.Collections.Generic;

namespace HOA{
	public class Lichenthrope : Unit {
		public Lichenthrope(Source s, bool template=false){
			id = new ID(this, EToken.LICH, s, false, template);
			plane = Plane.Gnd;
			onDeath = EToken.NONE;
			ScaleSmall();
			NewHealth(15);
			NewWatch(5);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMovePath(this, 0),
				new ALichFeed(this),
				new AEvolve(this, Price.Cheap, EToken.BEES),
				new AEvolve(this, new Price(1,2), EToken.MYCO),
				new AEvolve(this, new Price(1,3), EToken.MART)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}

	public class ALichFeed : Task {
		
		int damage = 5;

		public override string Desc {get {return "Do "+damage+" damage to target unit. " +
				"\nGain health equal to damage successfully dealt.";} }

		public ALichFeed (Unit u) {
			Name = "Feed";
			Weight = 3;
			
			Price = Price.Cheap;
			AddAim(HOA.Aim.Melee());
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ELeech(new Source(Parent), (Unit)targets[0], damage));
		}
	}

	public class AEvolve : Task {
		
		EToken child;

		string ChildName {get {return Template.ID.Name;} }

		public override string Desc {get {return "Transform "+Parent+" into a "+ChildName+".  " +
				"\n(New "+ChildName+" is added to the end of the Queue and does not retain any of "+Parent+"'s attributes.)";} }

		public AEvolve (Unit u, Price p, EToken chi) {
			Parent = u;
			child = chi;
			Template = TemplateFactory.Template(child);
			Name = "Evolve to "+ChildName;
			Weight = 4;
			Price = p;
			AddAim(HOA.Aim.Self());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EReplace(new Source(Parent), Parent, child));
		}
	}
}